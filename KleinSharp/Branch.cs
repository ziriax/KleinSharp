using System;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics;
using __m128 = System.Runtime.Intrinsics.Vector128<float>;
using static KleinSharp.Simd;

namespace KleinSharp
{
	/// <summary>
	/// The `Branch` is both a line through the origin and
	/// also the principal Branch of the logarithm of a rotor.
	///
	/// The rotor Branch will be most commonly constructed by taking the
	/// logarithm of a normalized rotor. The Branch may then be linearly scaled
	/// to adjust the "strength" of the rotor, and subsequently re-exponentiated
	/// to create the adjusted rotor.
	///
	/// !!! example
	///
	///     Suppose we have a rotor <b>r</b> and we wish to produce a rotor
	///     $\sqrt[4]{r}$ which performs a quarter of the rotation produced by
	///     $r$. We can construct it like so:
	///
	///     ```cs
	///         Branch b = r.Log();
	///         Rotor r_4 = (0.25f * b).Exp();
	///     ```
	///
	/// !!! note
	///
	///     The Branch of a rotor is technically a `line`, but because there are
	///     no translational components, the Branch is given its own type for
	///     efficiency.
	/// </summary>
	/// <remarks>
	/// Klein provides three line classes: <see cref="Line"/>, <see cref="Branch"/>, and <see cref="IdealLine"/>.
	///
	/// The line class represents a full six-coordinate bivector.
	///
	/// The Branch contains three non-degenerate components (aka, a line through the origin).
	///
	/// The ideal line represents the line at infinity.
	///
	/// When the line is created as a meet of two planes or join of two points
	/// (or carefully selected Plücker coordinates), it will be a Euclidean line
	/// (factorisable as the meet of two vectors).
	/// </remarks>
	[StructLayout(LayoutKind.Sequential)]
	public readonly struct Branch : IEquatable<Branch>
	{
		public readonly __m128 P1;

		/// <summary>
		/// Construct the Branch as the following multivector:
		/// <br/>
		/// <c>a e₂₃ + b e₃₁ + c e₁₂</c>
		/// <br/>
		/// To convince yourself this is a line through the origin, remember that
		/// such a line can be generated using the geometric product of two planes
		/// through the origin.
		/// </summary>
		public Branch(float a, float b, float c)
		{
			P1 = _mm_set_ps(c, b, a, 0f);
		}

		public Branch(__m128 xmm)
		{
			P1 = xmm;
		}

		public void Deconstruct(out float e23, out float e31, out float e12)
		{
			e23 = E23;
			e31 = E31;
			e12 = E12;
		}

		public float E12 => P1.GetElement(3);
		public float E21 => -E12;
		public float Z => E12;

		public float E31 => P1.GetElement(2);
		public float E13 => -E31;
		public float Y => E12;

		public float E23 => P1.GetElement(1);
		public float E32 => -E23;
		public float X => E23;

		/// <summary>
		/// If a line is constructed as the regressive product (join) of
		/// two points, the squared norm provided here is the squared
		/// distance between the two points (provided the points are
		/// normalized). Returns $d^2 + e^2 + f^2$.
		/// </summary>
		public float SquaredNorm()
		{
			var dp = Detail.hi_dp(P1, P1);
			return _mm_store_ss(dp);
		}

		/// <summary>
		/// Returns the square root of the quantity produced by `squared_norm`.
		/// </summary>
		public float Norm()
		{
			return MathF.Sqrt(SquaredNorm());
		}

		public static __m128 Normalized(__m128 p)
		{
			__m128 inv = Detail.rsqrt_nr1(Detail.hi_dp_bc(p, p));
			return _mm_mul_ps(p, inv);
		}

		public Branch Normalized() => new Branch(Normalized(P1));

		public static __m128 Inverse(__m128 p)
		{
			__m128 inv = Detail.rsqrt_nr1(Detail.hi_dp_bc(p, p));
			p = _mm_mul_ps(p, inv);
			p = _mm_mul_ps(p, inv);
			p = _mm_xor_ps(_mm_set_ps(-0f, -0f, -0f, 0f), p);
			return p;
		}

		public Branch Inverse() => new Branch(Inverse(P1));

		public Rotor Sqrt()
		{
			return new Rotor(Rotor.Normalized(_mm_add_ss(P1, _mm_set_ss(1f))));
		}

		/// <summary>
		/// Exponentiate a Branch to produce a Rotor.
		/// </summary>
		public Rotor Exp()
		{
			// Compute the Rotor angle
			var ang = _mm_store_ss(Detail.sqrt_nr1(Detail.hi_dp(P1, P1)));
			float cos_ang = MathF.Cos(ang);
			float sin_ang = MathF.Sin(ang) / ang;

			var p1 = _mm_mul_ps(_mm_set1_ps(sin_ang), P1);
			p1 = _mm_add_ps(p1, _mm_set_ps(0f, 0f, 0f, cos_ang));
			return new Rotor(p1);
		}

		public static Branch operator +(Branch a, Branch b)
		{
			return new Branch(_mm_add_ps(a.P1, b.P1));
		}

		public static Branch operator -(Branch a, Branch b)
		{
			return new Branch(_mm_sub_ps(a.P1, b.P1));
		}

		public static Branch operator *(Branch b, float s)
		{
			return new Branch(_mm_mul_ps(b.P1, _mm_set1_ps(s)));
		}

		public static Branch operator *(float s, Branch b)
		{
			return new Branch(_mm_mul_ps(b.P1, _mm_set1_ps(s)));
		}

		public static Branch operator /(Branch b, float s)
		{
			return new Branch(_mm_mul_ps(b.P1, Detail.rcp_nr1(_mm_set1_ps(s))));
		}

		public static Branch operator -(Branch b)
		{
			return new Branch(_mm_xor_ps(b.P1, _mm_set1_ps(-0f)));
		}

		/// <summary>
		/// Reversion operator
		/// </summary>
		public static Branch operator ~(Branch b)
		{
			__m128 flip = _mm_set_ps(-0f, -0f, -0f, 0f);
			return new Branch(_mm_xor_ps(b.P1, flip));
		}

		/// <summary>
		/// TODO: Document!
		/// </summary>
		public static IdealLine operator !(Branch b)
		{
			return new IdealLine(b.P1);
		}

		public static Dual operator ^(Branch a, IdealLine b)
		{
			return new Dual(0, _mm_store_ss(Detail.hi_dp_ss(a.P1, b.P2)));
		}

		public static Point operator ^(Branch b, Plane a)
		{
			return a ^ b;
		}

		public static Dual operator ^(Branch b, Line a)
		{
			return a ^ b;
		}

		public bool Equals(Branch other)
		{
			return P1.Equals(other.P1);
		}

		public override bool Equals(object obj)
		{
			return obj is Branch other && Equals(other);
		}

		public override int GetHashCode()
		{
			return P1.GetHashCode();
		}

		public static bool operator ==(Branch left, Branch right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(Branch left, Branch right)
		{
			return !left.Equals(right);
		}

		public override string ToString()
		{
			return $"Branch({E12} e12 + {E31} e31 + {E23} e23)";
		}
	}
}