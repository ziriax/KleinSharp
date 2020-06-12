using System;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics;
using System.Text;
using __m128 = System.Runtime.Intrinsics.Vector128<float>;
using static KleinSharp.Simd;

namespace KleinSharp
{
	/// <summary>
	/// A Point is represented as the multivector <code>e₁₂₃ + x e₀₃₂ + y e₀₁₃ + z e₀₂₁</code>
	/// <br/>
	/// The Point has a trivector representation because it is the
	/// fixed point of 3 planar reflections (each of which is a grade-1 multivector).
	/// <br/>
	/// In practice, the coordinate mapping can be thought of as an implementation detail.
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	public readonly struct Point : IEquatable<Point>
	{
		public static readonly Origin Origin;

		public readonly __m128 P3;

		public Point(__m128 p3)
		{
			P3 = p3;
		}

		/// Component-wise constructor (homogeneous coordinate is automatically initialized to 1)
		public Point(float x, float y, float z)
		{
			P3 = _mm_set_ps(z, y, x, 1f);
		}

		/// <summary>
		/// Fast load from a pointer to an array of four floats with layout
		/// `(w, x, y, z)` where `w` occupies the lowest address in memory.
		///
		/// !!! tip
		///
		///     This load operation is more efficient that modifying individual
		///     components back-to-back.
		///
		/// !!! danger
		///
		///     Unlike the component-wise constructor, the load here requires the
		///     homogeneous coordinate `w` to be supplied as well in the lowest
		///     address Pointed to by `data`.
		/// </summary>
		public unsafe Point(float* data)
		{
			P3 = _mm_loadu_ps(data);
		}

		public unsafe Point(Span<float> data)
		{
			if (data.Length < 4)
				throw new ArgumentOutOfRangeException(nameof(data));

			fixed (float* ptr = data)
			{
				P3 = _mm_loadu_ps(ptr);
			}
		}

		public void Deconstruct(out float x, out float y, out float z)
		{
			x = X;
			y = Y;
			z = Z;
		}

		public void Deconstruct(out float e123, out float e032, out float e013, out float e021)
		{
			e032 = E032;
			e013 = E013;
			e021 = E021;
			e123 = E123;
		}

		public ReadOnlySpan<float> ToSpan() => Helpers.ToFloatSpan(this);

		/// <summary>
		/// Store m128 contents into an array of 4 floats
		/// </summary>
		public unsafe void Store(float* data) => _mm_store_ps(data, P3);

		/// <summary>
		/// Return a normalized copy of this Point, so that the coefficient of e₁₂₃ becomes 1.
		/// </summary>
		/// <remarks>
		/// Division is done via rcpps with an additional Newton-Raphson refinement.
		/// </remarks>
		public Point Normalized()
		{
			__m128 tmp = Detail.rcp_nr1(KLN_SWIZZLE(P3, 0, 0, 0, 0));
			return new Point(_mm_mul_ps(P3, tmp));
		}

		/// <summary>
		/// Returns the inverse of this point. A point multiplied with its inverse is 1.
		/// </summary>
		/// <remarks>
		/// Division is done via rcpps with an additional Newton-Raphson refinement.
		/// </remarks>
		public Point Inverse()
		{
			__m128 p3 = P3;
			__m128 invNorm = Detail.rcp_nr1(KLN_SWIZZLE(p3, 0, 0, 0, 0));
			p3 = _mm_mul_ps(invNorm, p3);
			p3 = _mm_mul_ps(invNorm, p3);
			return new Point(p3);
		}

		public float X => P3.GetElement(1);
		public float E032 => X;

		public float Y => P3.GetElement(2);
		public float E013 => Y;

		public float Z => P3.GetElement(3);
		public float E021 => Z;

		/// The homogeneous coordinate `w` is exactly $1$ when normalized.
		public float W => P3.GetElement(0);
		public float E123 => W;

		public static Point operator +(Point a, Point b)
		{
			return new Point(_mm_add_ps(a.P3, b.P3));
		}

		public static Point operator -(Point a, Point b)
		{
			return new Point(_mm_sub_ps(a.P3, b.P3));
		}

		public static Point operator *(Point p, float s)
		{
			return new Point(_mm_mul_ps(p.P3, _mm_set1_ps(s)));
		}

		public static Point operator *(float s, Point p)
		{
			return p * s;
		}

		public static Point operator /(Point p, float s)
		{
			return new Point(_mm_mul_ps(p.P3, Detail.rcp_nr1(_mm_set1_ps(s))));
		}

		/// <remarks>
		/// Unary minus (leaves homogeneous coordinate untouched)
		/// </remarks>
		public static Point operator -(Point p)
		{
			return new Point(_mm_xor_ps(p.P3, _mm_set_ps(-0f, -0f, -0f, 0f)));
		}

		/// <summary>
		/// Reversion operator
		/// </summary>
		public static Point operator~ (Point p)
		{
			__m128 flip = _mm_set1_ps(-0f);
			return new Point(_mm_xor_ps(p.P3, flip));
		}

		/// <summary>
		/// Generates a translator $t$ that produces a displacement along the line
		/// between points $a$ and $b$. The translator given by $\sqrt{t}$ takes $b$ to $a$.
		/// </summary>
		public static Translator operator* (Point a, Point b)
		{
			return new Translator(Detail.gp33(a.P3, b.P3));
		}

		public static implicit operator Point(Origin _)
		{
			return new Point(_mm_set_ss(1f));
		}

		public bool Equals(Point other)
		{
			return P3.Equals(other.P3);
		}

		public override bool Equals(object obj)
		{
			return obj is Point other && Equals(other);
		}

		public override int GetHashCode()
		{
			return P3.GetHashCode();
		}

		public static bool operator ==(Point left, Point right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(Point left, Point right)
		{
			return !left.Equals(right);
		}

		public override string ToString()
		{
			return new StringBuilder("e₁₂₃", 64)
				.AppendElement(X, "e₀₃₂")
				.AppendElement(Y, "e₀₁₃")
				.AppendElement(Z, "e₀₂₁")
				.ToString();
		}
	}
}

/// <summary>
/// The origin is a convenience type that occupies no memory but is castable to
/// a Point entity. Several operations like conjugation of the origin by a motor
/// is optimized.
/// </summary>
/// <remarks>
/// On its own, the origin occupies no memory, but it can be casted as an
/// entity at any Point, at which Point it is represented as e₁₂₃
/// </remarks>
public readonly struct Origin
{
}