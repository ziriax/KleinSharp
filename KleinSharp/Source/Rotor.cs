using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics;
using System.Text;
using __m128 = System.Runtime.Intrinsics.Vector128<float>;
using static KleinSharp.Simd;
// ReSharper disable ParameterHidesMember
// ReSharper disable InconsistentNaming

namespace KleinSharp
{
	/// <summary>
	/// The Rotor is an entity that represents a rigid rotation about an axis
	/// To apply the Rotor to a supported entity, the call operator is available.
	///
	/// !!! example
	///
	///     ```c++
	///         // Initialize a point at (1, 3, 2)
	///         kln::point p{1f, 3f, 2f};
	///
	///         // Create a normalized Rotor representing a pi/2 radian
	///         // rotation about the xz-axis.
	///         kln::Rotor r{kln::pi * 0.5f, 1f, 0f, 1f};
	///
	///         // Rotate our point using the created Rotor
	///         kln::point rotated = r(p);
	///     ```
	///     We can rotate lines and planes as well using the Rotor's call operator.
	///
	/// Rotors can be multiplied to one another with the `*` operator to create
	/// a new Rotor equivalent to the application of each factor.
	///
	/// !!! example
	///
	///     ```c++
	///         // Create a normalized Rotor representing a $\frac{\pi}{2}$ radian
	///         // rotation about the xz-axis.
	///         kln::Rotor r1{kln::pi * 0.5f, 1f, 0f, 1f};
	///
	///         // Create a second Rotor representing a $\frac{\pi}{3}$ radian
	///         // rotation about the yz-axis.
	///         kln::Rotor r2{kln::pi / 3f, 0f, 1f, 1f};
	///
	///         // Use the geometric product to create a Rotor equivalent to first
	///         // applying r1, then applying r2. Note that the order of the
	///         // operands here is significant.
	///         kln::Rotor r3 = r2 * r1;
	///     ```
	///
	/// The same `*` operator can be used to compose the Rotor's action with other
	/// translators and motors.
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	public readonly struct Rotor :
		IConjugator<Line>,
		IConjugator<Point>,
		IConjugator<Plane>,
		IConjugator<Branch>,
		IConjugator<Direction>
	{
		public readonly __m128 P1;

		internal Rotor(float rollX, float pitchY, float yawZ)
		{
			// https://en.wikipedia.org/wiki/Conversion_between_quaternions_and_Euler_angles#cite_note-3
			float half_yaw = yawZ * 0.5f;
			float half_pitch = pitchY * 0.5f;
			float half_roll = rollX * 0.5f;
			float cos_y = MathF.Cos(half_yaw);
			float sin_y = MathF.Sin(half_yaw);
			float cos_p = MathF.Cos(half_pitch);
			float sin_p = MathF.Sin(half_pitch);
			float cos_r = MathF.Cos(half_roll);
			float sin_r = MathF.Sin(half_roll);

			P1 = _mm_set_ps(cos_r * cos_p * sin_y - sin_r * sin_p * cos_y,
				cos_r * sin_p * cos_y + sin_r * cos_p * sin_y,
				sin_r * cos_p * cos_y - cos_r * sin_p * sin_y,
				cos_r * cos_p * cos_y + sin_r * sin_p * sin_y);
		}

		/// <summary>
		/// Create a Rotor from Euler angles
		/// <br/>
		/// <a href="https://en.wikipedia.org/wiki/Aircraft_principal_axes"/>
		/// </summary>
		/// <param name="rollX">Rotation in radians about X axis</param>
		/// <param name="pitchY">Rotation in radians about Y axis</param>
		/// <param name="yawZ">Rotation in radians about Z axis</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Rotor FromEulerAngles(float rollX, float pitchY, float yawZ)
		{
			return new Rotor(rollX, pitchY, yawZ);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Rotor(__m128 p1)
		{
			P1 = p1;
		}

		internal Rotor(float angleInRadians, float x, float y, float z)
		{
			float norm = MathF.Sqrt(x * x + y * y + z * z);
			float inv_norm = 1f / norm;

			float half = 0.5f * angleInRadians;
			// Rely on compiler to coalesce these two assignments into a single
			// sincos call at instruction selection time
			float sin_ang = MathF.Sin(half);
			float scale = sin_ang * inv_norm;
			P1 = _mm_set_ps(z, y, x, MathF.Cos(half));
			P1 = _mm_mul_ps(P1, _mm_set_ps(scale, scale, scale, 1f));
		}

		/// <summary>
		/// Convenience constructor. Computes transcendentals and normalizes rotation axis.
		/// </summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Rotor FromAngleAxis(float angleInRadians, float x, float y, float z)
		{
			return new Rotor(angleInRadians, x, y, z);
		}

		/// <summary>
		/// Fast load operation for packed data that is already normalized.
		/// <br/>
		/// The argument `data` should point to a set of 4 float values with layout `(a, b, c, d)`,
		/// corresponding to the multivector <c>a + be₂₃ + ce₃₁ + de₁₂</c>
		/// </summary>
		/// <remarks>
		/// The Rotor data loaded this way <b>must be normalized</b>!
		/// <br/>
		/// That is, the rotor <c>r</c> must satisfy <c>r ~r = 1</c>.
		/// </remarks>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static unsafe Rotor LoadNormalized(float* data)
		{
			return new Rotor(_mm_loadu_ps(data));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Rotor LoadNormalized(ReadOnlySpan<float> data)
		{
			return new Rotor(_mm_loadu_ps(data));
		}

		/// <summary>
		/// Store the 4 float components in memory
		/// </summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe void Store(float* data)
		{
			_mm_storeu_ps(data, P1);
		}

		/// <summary>
		/// Store the 4 float components in a span
		/// </summary>
		public void Store(Span<float> data)
		{
			_mm_storeu_ps(data, P1);
		}

		/// <summary>
		/// Deconstructs the components of the rotor <c>a + be₂₃ + ce₃₁ + de₁₂</c>
		/// </summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Deconstruct(out float a, out float b, out float c, out float d)
		{
			a = Scalar;
			b = e23;
			c = e31;
			d = e12;
		}

		/// <summary>
		/// Return a normalized copy of this rotor
		/// </summary>
		/// <remarks>
		/// Normalizes a rotor <c>r</c> such that <c>r ~r = 1</c>.
		/// </remarks>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Rotor Normalized()
		{
			// A Rotor is normalized if r * ~r is unity.
			return new Rotor(Normalized(P1));
		}

		internal static __m128 Normalized(__m128 p1)
		{
			__m128 invNorm = Detail.rsqrt_nr1(Detail.dp_bc(p1, p1));
			return _mm_mul_ps(p1, invNorm);
		}

		public static __m128 Inverse(__m128 p1)
		{
			__m128 inv_norm = Detail.rsqrt_nr1(Detail.hi_dp_bc(p1, p1));
			p1 = _mm_mul_ps(p1, inv_norm);
			p1 = _mm_mul_ps(p1, inv_norm);
			return _mm_xor_ps(_mm_set_ps(-0f, -0f, -0f, 0f), p1);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Rotor Inverse()
		{
			return new Rotor(Inverse(P1));
		}

		/// <summary>
		/// Returns a Rotor constrained to traverse the shortest arc
		/// </summary>
		public Rotor Constrained()
		{
			__m128 mask = _mm_swizzle_ps(_mm_and_ps(P1, _mm_set_ss(-0f)), 0);
			return new Rotor(_mm_xor_ps(mask, P1));
		}

		public bool Equals(Rotor other)
		{
			return P1.Equals(other.P1);
		}

		public bool Equals(Rotor other, float epsilon)
		{
			__m128 eps = _mm_set1_ps(epsilon);
			__m128 cmp = _mm_cmplt_ps(
				_mm_andnot_ps(_mm_set1_ps(-0f), _mm_sub_ps(P1, other.P1)), eps);
			return _mm_movemask_ps(cmp) != 0b1111;
		}

		public (float rollX, float pitchY, float yawZ) AsEulerAngles()
		{
			var (a, b, c, d) = this;

			float bb = b * b;
			float cc = c * c;

			var rollX = MathF.Atan2(2 * (a * b + c * d), 1 - 2 * (bb + cc));

			var pitchY = MathF.Asin(2 * (a * c - b * d));

			var yawZ = MathF.Atan2(2 * (a * d + b * c), 1 - 2 * (cc + d * d));

			return (rollX, pitchY, yawZ);
		}

		/// <summary>
		/// Conjugates an array of planes with this Rotor in the input array and
		/// stores the result in the output array. Aliasing is only permitted when
		/// `in == out` (in place motor application).
		///
		/// !!! tip
		///
		///     When applying a Rotor to a list of tightly packed planes, this
		///     routine will be *significantly faster* than applying the Rotor to
		///     each plane individually.
		/// </summary>
		public unsafe void Conjugate(Plane* input, Plane* output, int count)
		{
			Detail.sw012(false, &input->P0, P1, default, &output->P0, count);
		}

		public unsafe Plane Conjugate(Plane input)
		{
			__m128 res;
			var p0 = input.P0;
			Detail.sw012(false, &p0, P1, default, &res, 1);
			return new Plane(res);
		}

		public Plane this[Plane p] => Conjugate(p);
		public Plane[] this[ReadOnlySpan<Plane> input] => Conjugator.Apply(this, input);
		public Span<Plane> this[ReadOnlySpan<Plane> input, Span<Plane> output] => Conjugator.Apply(this, input, output);

		public unsafe void Conjugate(Branch* input, Branch* output, int count)
		{
			Detail.swMM(false, false, &input->P1, P1, default, &output->P1, count);
		}

		public unsafe Branch Conjugate(Branch input)
		{
			__m128 res;
			var p1 = input.P1;
			Detail.swMM(false, false, &p1, P1, default, &res, 1);
			return new Branch(res);
		}

		public Branch this[Branch p] => Conjugate(p);
		public Branch[] this[ReadOnlySpan<Branch> input] => Conjugator.Apply(this, input);
		public Span<Branch> this[ReadOnlySpan<Branch> input, Span<Branch> output] => Conjugator.Apply(this, input, output);

		/// Conjugates an array of lines with this Rotor in the input array and
		/// stores the result in the output array. Aliasing is only permitted when
		/// `in == out` (in place Rotor application).
		///
		/// !!! tip
		///
		///     When applying a Rotor to a list of tightly packed lines, this
		///     routine will be *significantly faster* than applying the Rotor to
		///     each line individually.
		public unsafe void Conjugate(Line* input, Line* output, int count)
		{
			Detail.swMM(false, true, &input->P1, P1, default, &output->P1, count);
		}

		public unsafe Line Conjugate(Line input)
		{
			var res = stackalloc __m128[2];
			Detail.swMM(false, true, &input.P1, P1, default, res, 1);
			return new Line(res[0], res[1]);
		}

		public Line this[Line p] => Conjugate(p);
		public Line[] this[ReadOnlySpan<Line> input] => Conjugator.Apply(this, input);
		public Span<Line> this[ReadOnlySpan<Line> input, Span<Line> output] => Conjugator.Apply(this, input, output);

		/// Conjugates an array of points with this Rotor in the input array and
		/// stores the result in the output array. Aliasing is only permitted when
		/// `in == out` (in place Rotor application).
		///
		/// !!! tip
		///
		///     When applying a Rotor to a list of tightly packed points, this
		///     routine will be *significantly faster* than applying the Rotor to
		///     each point individually.
		public unsafe void Conjugate(Point* input, Point* output, int count)
		{
			// NOTE: Conjugation of a plane and point with a Rotor is identical
			Detail.sw012(false, &input->P3, P1, default, &output->P3, count);
		}

		public unsafe Point Conjugate(Point input)
		{
			__m128 p3;
			Detail.sw012(false, &input.P3, P1, default, &p3, 1);
			return new Point(p3);
		}

		public Point this[Point p] => Conjugate(p);
		public Point[] this[ReadOnlySpan<Point> input] => Conjugator.Apply(this, input);
		public Span<Point> this[ReadOnlySpan<Point> input, Span<Point> output] => Conjugator.Apply(this, input, output);

		/// Conjugates an array of directions with this Rotor in the input array and
		/// stores the result in the output array. Aliasing is only permitted when
		/// `in == out` (in place Rotor application).
		///
		/// !!! tip
		///
		///     When applying a Rotor to a list of tightly packed directions, this
		///     routine will be *significantly faster* than applying the Rotor to
		///     each Direction individually.
		public unsafe void Conjugate(Direction* input, Direction* output, int count)
		{
			Detail.sw012(false, &input->P3, P1, default, &output->P3, count);
		}

		public unsafe Direction Conjugate(Direction input)
		{
			__m128 p3;
			Detail.sw012(false, &input.P3, P1, default, &p3, 1);
			return new Direction(p3);
		}

		public Direction this[Direction p] => Conjugate(p);
		public Direction[] this[ReadOnlySpan<Direction> input] => Conjugator.Apply(this, input);
		public Span<Direction> this[ReadOnlySpan<Direction> input, Span<Direction> output] => Conjugator.Apply(this, input, output);

		public float Scalar => P1.GetElement(0);

		public float e23 => P1.GetElement(1);
		public float e32 => -e23;

		public float e31 => P1.GetElement(2);
		public float e13 => -e31;

		public float e12 => P1.GetElement(3);
		public float e21 => -e12;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Rotor operator +(Rotor a, Rotor b)
		{
			return new Rotor(_mm_add_ps(a.P1, b.P1));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Rotor operator -(Rotor a, Rotor b)
		{
			return new Rotor(_mm_sub_ps(a.P1, b.P1));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Rotor operator *(Rotor r, float s)
		{
			return new Rotor(_mm_mul_ps(r.P1, _mm_set1_ps(s)));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Rotor operator *(float s, Rotor r)
		{
			return r * s;
		}

		/// Composes two rotational actions such that the produced Rotor has the same
		/// effect as applying Rotor $b$, then Rotor $a$.
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Rotor operator *(Rotor a, Rotor b)
		{
			var p1 = Detail.gp11(a.P1, b.P1);
			return new Rotor(p1);
		}


		/// Compose the action of a Translator and Rotor (`b` will be applied, then `a`)
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Motor operator *(Rotor a, Translator b)
		{
			var p2 = Detail.gpRT(false, a.P1, b.P2);
			return new Motor(a.P1, p2);
		}

		/// Compose the action of a Rotor and Motor (`b` will be applied, then `a`)
		public static Motor operator *(Rotor a, Motor b)
		{
			var p1 = Detail.gp11(a.P1, b.P1);
			var p2 = Detail.gp12(false, a.P1, b.P2);
			return new Motor(p1, p2);
		}


		public static Rotor operator /(Rotor r, float s)
		{
			return new Rotor(_mm_mul_ps(r.P1, Detail.rcp_nr1(_mm_set1_ps(s))));
		}

		/// <summary>
		/// Reversion operator
		/// </summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Rotor operator ~(Rotor r)
		{
			__m128 flip = _mm_set_ps(-0f, -0f, -0f, 0f);
			return new Rotor(_mm_xor_ps(r.P1, flip));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Rotor operator -(Rotor r)
		{
			return new Rotor(_mm_xor_ps(r.P1, _mm_set1_ps(-0f)));
		}

		/// <summary>
		/// Formats the rotor as <c>a + be₂₃ + ce₃₁ + de₁₂</c>
		/// Elements with zero components are dropped.
		/// </summary>
		public override string ToString()
		{
			var (a, b, c, d) = this;
			return new StringBuilder(64)
				.AppendScalar(a)
				.AppendElement(b, "e₂₃")
				.AppendElement(c, "e₃₁")
				.AppendElement(d, "e₁₂")
				.ZeroWhenEmpty();
		}
	}
}

#if false
namespace kln
{

/// \defgroup Rotor Rotors
///
/// \addtogroup Rotor
/// @{
class Rotor final
{
public:
    Rotor()  = default;

    /// Converts the Rotor to a 3x4 column-major matrix. The results of this
    /// conversion are only defined if the Rotor is normalized, and this
    /// conversion is preferable if so.
    public mat3x4 as_mat3x4()
    {
        mat3x4 out;
        mat4x4_12<false, true>(P1, nullptr, out.cols);
        return out;
    }

    /// Converts the Rotor to a 4x4 column-major matrix.
    public mat4x4 as_mat4x4()
    {
        mat4x4 out;
        mat4x4_12<false, false>(P1, nullptr, out.cols);
        return out;
    }

};
#endif