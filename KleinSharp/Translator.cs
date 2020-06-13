using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;
using System.Text;
using __m128 = System.Runtime.Intrinsics.Vector128<float>;
using static KleinSharp.Simd;
// ReSharper disable ParameterHidesMember
// ReSharper disable InconsistentNaming

namespace KleinSharp
{
	/// <summary>
	/// A Translator represents a rigid-body displacement along a normalized axis.
	/// To apply the Translator to a supported entity, the call operator is
	/// available.
	///
	/// !!! example
	///
	///     ```c++
	///         // Initialize a point at (1, 3, 2)
	///         kln::point p{1f, 3f, 2f};
	///
	///         // Create a normalized Translator representing a 4-unit
	///         // displacement along the xz-axis.
	///         kln::Translator r{4f, 1f, 0f, 1f};
	///
	///         // Displace our point using the created Translator
	///         kln::point translated = r(p);
	///     ```
	///     We can translate lines and planes as well using the Translator's call
	///     operator.
	///
	/// Translators can be multiplied to one another with the `*` operator to create
	/// a new Translator equivalent to the application of each factor.
	///
	/// !!! example
	///
	///     ```c++
	///         // Suppose we have 3 Translators t1, t2, and t3
	///
	///         // The Translator t created here represents the combined action of
	///         // t1, t2, and t3.
	///         kln::Translator t = t3 * t2 * t1;
	///     ```
	///
	/// The same `*` operator can be used to compose the Translator's action with
	/// other rotors and motors.
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	public readonly struct Translator
	{
		public readonly __m128 P2;

		public Translator(__m128 p2)
		{
			P2 = p2;
		}

		public Translator(float delta, float x, float y, float z)
		{
			float norm = MathF.Sqrt(x * x + y * y + z * z);
			float invNorm = 1f / norm;
			float halfD = -0.5f * delta;
			P2 = _mm_mul_ps(_mm_set1_ps(halfD), _mm_set_ps(z, y, x, 0f));
			P2 = _mm_mul_ps(P2, _mm_set_ps(invNorm, invNorm, invNorm, 0f));
		}

		/// <summary>
		/// Fast load operation for packed data that is already normalized. The
		/// argument `data` should point to a set of 4 float values with layout
		/// `(0f, a, b, c)` corresponding to the multivector $ae₀₁ +
		/// be₀₂ + ce₀₃$.
		///
		/// !!! danger
		///
		///     The Translator data loaded this way *must* be normalized. That is,
		///     the quantity $-\sqrt{a^2 + b^2 + c^2}$ must be half the desired
		///     displacement.
		/// </summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe Translator(float* normalizedData)
		{
			P2 = _mm_loadu_ps(normalizedData);
		}


		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Deconstruct(out float e01, out float e02, out float e03)
		{
			e01 = this.e01;
			e02 = this.e02;
			e03 = this.e03;
		}

		public float Scalar => 1f;

		public float e01 => P2.GetElement(1);
		public float e10 => -e01;

		public float e02 => P2.GetElement(2);
		public float e20 => -e02;

		public float e03 => P2.GetElement(3);
		public float e30 => -e03;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Translator Inverse()
		{
			return new Translator(_mm_xor_ps(_mm_set_ps(-0f, -0f, -0f, 0f), P2));
		}

		/// <summary>
		/// Conjugates a plane $p$ with this Translator and returns the result $tp\widetilde{t}$.
		/// </summary>
		public Plane Conjugate(Plane p)
		{
			__m128 tmp = Sse41.IsSupported
				? _mm_blend_ps(P2, _mm_set_ss(1f), 1)
				: _mm_add_ps(P2, _mm_set_ss(1f));

			return new Plane(Detail.sw02(p.P0, tmp));
		}

		/// <summary>
		/// Conjugates a line $\ell$ with this Translator and returns the result $t\ell\widetilde{t}$.
		/// </summary>
		public unsafe Line Conjugate(in Line l)
		{
			var res = stackalloc __m128[2];
			Detail.swL2(l.P1, l.P2, P2, res);
			return new Line(res);
		}

		/// <summary>
		/// Conjugates a point $p$ with this Translator and returns the result $tp\widetilde{t}$.
		/// </summary>
		public Point Conjugate(Point p)
		{
			return new Point(Detail.sw32(p.P3, P2));
		}

		/// <summary>
		/// Indexer is alias for conjugate
		/// </summary>
		public Plane this[Plane p] => Conjugate(p);
		public Line this[Line p] => Conjugate(p);
		public Point this[Point p] => Conjugate(p);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Translator operator +(Translator a, Translator b)
		{
			return new Translator(_mm_add_ps(a.P2, b.P2));
		}

		/// Translator subtraction
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Translator operator -(Translator a, Translator b)
		{
			return new Translator(_mm_sub_ps(a.P2, b.P2));
		}

		/// Translator uniform scale
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Translator operator *(Translator t, float s)
		{
			return new Translator(_mm_mul_ps(t.P2, _mm_set1_ps(s)));
		}

		/// Translator uniform scale
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Translator operator *(float s, Translator t)
		{
			return t * s;
		}

		/// Translator uniform inverse scale
		public static Translator operator /(Translator t, float s)
		{
			return new Translator(_mm_mul_ps(t.P2, Detail.rcp_nr1(_mm_set1_ps(s))));
		}

		public override string ToString()
		{
			return new StringBuilder(64)
				.AppendElement(e01, "e₀₁")
				.AppendElement(e02, "e₀₂")
				.AppendElement(e03, "e₀₃")
				.ZeroWhenEmpty();
		}
	}
}
