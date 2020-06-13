using System;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics;
using __m128 = System.Runtime.Intrinsics.Vector128<float>;
using static KleinSharp.Simd;

namespace KleinSharp
{
	/// <summary>
	/// An ideal line represents a line at infinity and
	/// corresponds to the multivector <b>a e₀₁ + b e₀₂ + c e₀₃</b>
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
	public readonly struct IdealLine
	{
		public readonly __m128 P2;

		public IdealLine(float a, float b, float c)
		{
			P2 = _mm_set_ps(c, b, a, 0f);
		}

		internal IdealLine(__m128 abc)
		{
			P2 = abc;
		}

		public void Deconstruct(out float e01, out float e02, out float e03)
		{
			e01 = E01;
			e02 = E02;
			e03 = E03;
		}

		public float SquaredIdealNorm()
		{
			var dp = Detail.hi_dp(P2, P2);
			return _mm_store_ss(dp);
		}

		public float IdealNorm()
		{
			return MathF.Sqrt(SquaredIdealNorm());
		}

		public static IdealLine operator +(IdealLine a, IdealLine b)
		{
			return new IdealLine(_mm_add_ps(a.P2, b.P2));
		}

		public static IdealLine operator -(IdealLine a, IdealLine b)
		{
			return new IdealLine(_mm_sub_ps(a.P2, b.P2));
		}

		public static IdealLine operator *(IdealLine a, float s)
		{
			return new IdealLine(_mm_mul_ps(a.P2, _mm_set1_ps(s)));
		}

		public static IdealLine operator *(float s, IdealLine a)
		{
			return new IdealLine(_mm_mul_ps(a.P2, _mm_set1_ps(s)));
		}

		public static IdealLine operator /(IdealLine a, float s)
		{
			return new IdealLine(_mm_mul_ps(a.P2, Detail.rcp_nr1(_mm_set1_ps(s))));
		}

		public static IdealLine operator -(IdealLine l)
		{
			return new IdealLine(_mm_xor_ps(l.P2, _mm_set1_ps(-0f)));
		}

		/// <summary>
		/// Reversion operator
		/// </summary>
		public static IdealLine operator ~(IdealLine l)
		{
			__m128 flip = _mm_set_ps(-0f, -0f, -0f, 0f);
			return new IdealLine(_mm_xor_ps(l.P2, flip));
		}

		/// <summary>
		/// TODO: Document!
		/// </summary>
		public static Branch operator !(IdealLine l)
		{
			return new Branch(l.P2);
		}

		/// <summary>
		/// TODO: Document!
		/// </summary>
		public static Plane operator |(IdealLine b, Plane a)
		{
			return new Plane(Detail.dotPIL(true , a.P0, b.P2));
		}

		/// <summary>
		/// TODO: Document!
		/// </summary>
		public static Point operator ^(IdealLine b, Plane a)
		{
			return a ^ b;
		}

		/// <summary>
		/// TODO: Document!
		/// </summary>
		public static Dual operator ^(IdealLine b, Branch a)
		{
			return a ^ b;
		}

		/// <summary>
		/// TODO: Document!
		/// </summary>
		public static Dual operator ^(IdealLine b, Line a)
		{
			return a ^ b;
		}

		/// <summary>
		/// TODO: Document!
		/// </summary>
		public static Plane operator &(IdealLine b, Point a)
		{
			return a & b;
		}


		public float E01 => P2.GetElement(1);
		public float E10 => -E01;

		public float E02 => P2.GetElement(2);
		public float E20 => -E02;

		public float E03 => P2.GetElement(3);
		public float E30 => -E03;

		public override string ToString()
		{
			return $"IdealLine({E01} e₀₁ + {E02} e₀₂ + {E03} e₀₃)";
		}
	}
}