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

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public IdealLine(float a, float b, float c)
		{
			P2 = _mm_set_ps(c, b, a, 0f);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal IdealLine(__m128 abc)
		{
			P2 = abc;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Deconstruct(out float e01, out float e02, out float e03)
		{
			e01 = this.e01;
			e02 = this.e02;
			e03 = this.e03;
		}

		public float SquaredIdealNorm()
		{
			var dp = Detail.hi_dp(P2, P2);
			return _mm_store_ss(dp);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float IdealNorm()
		{
			return MathF.Sqrt(SquaredIdealNorm());
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IdealLine operator +(IdealLine a, IdealLine b)
		{
			return new IdealLine(_mm_add_ps(a.P2, b.P2));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Line operator +(IdealLine a, Line b)
		{
			return new Line(b.P1, _mm_add_ps(a.P2, b.P2));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Line operator +(Line a, IdealLine b)
		{
			return new Line(a.P1, _mm_add_ps(a.P2, b.P2));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IdealLine operator -(IdealLine a, IdealLine b)
		{
			return new IdealLine(_mm_sub_ps(a.P2, b.P2));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Line operator -(IdealLine a, Line b)
		{
			return new Line(b.P1, _mm_sub_ps(a.P2, b.P2));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Line operator -(Line a, IdealLine b)
		{
			return new Line(a.P1, _mm_sub_ps(a.P2, b.P2));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IdealLine operator *(IdealLine a, float s)
		{
			return new IdealLine(_mm_mul_ps(a.P2, _mm_set1_ps(s)));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IdealLine operator *(float s, IdealLine a)
		{
			return new IdealLine(_mm_mul_ps(a.P2, _mm_set1_ps(s)));
		}

		public static IdealLine operator /(IdealLine a, float s)
		{
			return new IdealLine(_mm_mul_ps(a.P2, Detail.rcp_nr1(_mm_set1_ps(s))));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IdealLine operator -(IdealLine l)
		{
			return new IdealLine(_mm_xor_ps(l.P2, _mm_set1_ps(-0f)));
		}

		/// <summary>
		/// Reversion operator
		/// </summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IdealLine operator ~(IdealLine l)
		{
			__m128 flip = _mm_set_ps(-0f, -0f, -0f, 0f);
			return new IdealLine(_mm_xor_ps(l.P2, flip));
		}

		/// <summary>
		/// TODO: Document!
		/// </summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Branch operator !(IdealLine l)
		{
			return new Branch(l.P2);
		}

		/// <summary>
		/// TODO: Document!
		/// </summary>
		public static Plane operator |(IdealLine b, Plane a)
		{
			return new Plane(Detail.dotPIL(true, a.P0, b.P2));
		}

		/// <summary>
		/// TODO: Document!
		/// </summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Point operator ^(IdealLine b, Plane a)
		{
			return a ^ b;
		}

		/// <summary>
		/// TODO: Document!
		/// </summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Dual operator ^(IdealLine b, Branch a)
		{
			return a ^ b;
		}

		/// <summary>
		/// TODO: Document!
		/// </summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Dual operator ^(IdealLine b, Line a)
		{
			return a ^ b;
		}

		/// <summary>
		/// TODO: Document!
		/// </summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Plane operator &(IdealLine b, Point a)
		{
			return a & b;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator Line(IdealLine l)
		{
			return new Line(l);
		}

		public float e01 => P2.GetElement(1);
		public float e10 => -e01;

		public float e02 => P2.GetElement(2);
		public float e20 => -e02;

		public float e03 => P2.GetElement(3);
		public float e30 => -e03;

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