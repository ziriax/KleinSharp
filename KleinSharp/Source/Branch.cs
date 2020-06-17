using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics;
using System.Text;
using __m128 = System.Runtime.Intrinsics.Vector128<float>;
using static KleinSharp.Simd;
// ReSharper disable InconsistentNaming
// ReSharper disable ParameterHidesMember

namespace KleinSharp
{
	/// <summary>
	/// The `Branch` is both a line through the origin and
	/// also the principal Branch of the logarithm of a rotor.
	/// <br/>
	/// It is represented as <c>a e₂₃ + b e₃₁ + c e₁₂</c>
	/// <br/>
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
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Branch(float a, float b, float c)
		{
			P1 = _mm_set_ps(c, b, a, 0f);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Branch(__m128 xmm)
		{
			P1 = xmm;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Deconstruct(out float e23, out float e31, out float e12)
		{
			e23 = this.e23;
			e31 = this.e31;
			e12 = this.e12;
		}

		public float e12 => P1.GetElement(3);
		public float e21 => -e12;
		public float Z => e12;

		public float e31 => P1.GetElement(2);
		public float e13 => -e31;
		public float Y => e12;

		public float e23 => P1.GetElement(1);
		public float e32 => -e23;
		public float X => e23;

		/// <summary>
		/// If a line is constructed as the regressive product (join) of
		/// two points, the squared norm provided here is the squared
		/// distance between the two points (provided the points are
		/// normalized). Returns $d^2 + e^2 + f^2$.
		/// </summary>
		[MethodImpl(MethodImplOptions.AggressiveOptimization)]
		public float SquaredNorm()
		{
			var dp = Detail.hi_dp(P1, P1);
			return _mm_store_ss(dp);
		}

		/// <summary>
		/// Returns the square root of the quantity produced by `squared_norm`.
		/// </summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float Norm()
		{
			return MathF.Sqrt(SquaredNorm());
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static __m128 Normalized(__m128 p)
		{
			__m128 inv = Detail.rsqrt_nr1(Detail.hi_dp_bc(p, p));
			return _mm_mul_ps(p, inv);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Branch Normalized() => new Branch(Normalized(P1));

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static __m128 Inverse(__m128 p)
		{
			__m128 inv = Detail.rsqrt_nr1(Detail.hi_dp_bc(p, p));
			p = _mm_mul_ps(p, inv);
			p = _mm_mul_ps(p, inv);
			p = _mm_xor_ps(_mm_set_ps(-0f, -0f, -0f, 0f), p);
			return p;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Branch Inverse() => new Branch(Inverse(P1));

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Branch operator +(Branch a, Branch b)
		{
			return new Branch(_mm_add_ps(a.P1, b.P1));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Line operator +(Branch a, Line b)
		{
			return new Line(_mm_add_ps(a.P1, b.P1), b.P2);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Line operator +(Line a, Branch b)
		{
			return new Line(_mm_add_ps(a.P1, b.P1), a.P2);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Line operator +(Branch a, IdealLine b)
		{
			return new Line(a.P1, b.P2);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Line operator +(IdealLine a, Branch b)
		{
			return new Line(b.P1, a.P2);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Branch operator -(Branch a, Branch b)
		{
			return new Branch(_mm_sub_ps(a.P1, b.P1));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Line operator -(Branch a, Line b)
		{
			return new Line(_mm_sub_ps(a.P1, b.P1), b.P2);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Line operator -(Line a, Branch b)
		{
			return new Line(_mm_sub_ps(a.P1, b.P1), a.P2);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Line operator -(Branch a, IdealLine b)
		{
			return new Line(a.P1, b.P2);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Line operator -(IdealLine a, Branch b)
		{
			return new Line(b.P1, a.P2);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Branch operator *(Branch b, float s)
		{
			return new Branch(_mm_mul_ps(b.P1, _mm_set1_ps(s)));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Branch operator *(float s, Branch b)
		{
			return new Branch(_mm_mul_ps(b.P1, _mm_set1_ps(s)));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Branch operator /(Branch b, float s)
		{
			return new Branch(_mm_mul_ps(b.P1, Detail.rcp_nr1(_mm_set1_ps(s))));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Branch operator -(Branch b)
		{
			return new Branch(_mm_xor_ps(b.P1, _mm_set1_ps(-0f)));
		}

		/// <summary>
		/// Reversion operator
		/// </summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Branch operator ~(Branch b)
		{
			__m128 flip = _mm_set_ps(-0f, -0f, -0f, 0f);
			return new Branch(_mm_xor_ps(b.P1, flip));
		}

		/// <summary>
		/// TODO: Document!
		/// </summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IdealLine operator !(Branch b)
		{
			return new IdealLine(b.P1);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Dual operator ^(Branch a, IdealLine b)
		{
			return new Dual(0, _mm_store_ss(Detail.hi_dp_ss(a.P1, b.P2)));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Point operator ^(Branch b, Plane a)
		{
			return a ^ b;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Dual operator ^(Branch b, Line a)
		{
			return a ^ b;
		}

		/// <summary>
		/// Generate a Rotor $r$ such that $\widetilde{\sqrt{r}}$ takes Branch $b$ to Branch $a$.
		/// </summary>
		[MethodImpl(MethodImplOptions.AggressiveOptimization)]
		public static Rotor operator *(Branch a, Branch b)
		{
			return new Rotor(Detail.gp11(a.P1, b.P1));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Rotor operator /(Branch a, Branch b)
		{
			return a * b.Inverse();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Plane operator &(Branch b, Point a)
		{
			return a & b;
		}


		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(Branch other)
		{
			return P1.Equals(other.P1);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override bool Equals(object? obj)
		{
			return obj is Branch other && Equals(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return P1.GetHashCode();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator ==(Branch left, Branch right)
		{
			return left.Equals(right);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator !=(Branch left, Branch right)
		{
			return !left.Equals(right);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator Line(Branch b)
		{
			return new Line(b);
		}

		public override string ToString()
		{
			return new StringBuilder(64)
				.AppendElement(e12, "e₁₂")
				.AppendElement(e31, "e₃₁")
				.AppendElement(e23, "e₂₃")
				.ZeroWhenEmpty();
		}
	}
}