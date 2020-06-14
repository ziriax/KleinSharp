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
	/// A general Line in PGA is given as a 6-coordinate bivector with a direct  correspondence to Plücker coordinates.
	/// <br/>
	/// All Lines can be exponentiated using the <see cref="Line.Exp"></see> method to generate a motor.
	/// </summary>
	/// <remarks>
	/// Klein provides three Line classes: <see cref="Line"/>, <see cref="Branch"/>, and <see cref="IdealLine"/>.
	/// <br/>
	/// The Line class represents a full six-coordinate bivector.
	/// <br/>
	/// The Branch contains three non-degenerate components (aka, a Line through the origin).
	/// <br/>
	/// The ideal Line represents the Line at infinity.
	/// <br/>
	/// When the Line is created as a meet of two planes or join of two points
	/// (or carefully selected Plücker coordinates), it will be a Euclidean Line
	/// (factorisable as the meet of two vectors).
	/// </remarks>
	[StructLayout(LayoutKind.Sequential)]
	public readonly struct Line : IEquatable<Line>
	{
		// p1: (1, e12, e31, e23)
		// p2: (e0123, e01, e02, e03)
		public readonly __m128 P1;
		public readonly __m128 P2;

		/// <summary>
		/// A Line is specified by 6 coordinates which correspond to the Line's
		/// [Plücker coordinates](https://en.wikipedia.org/wiki/Pl%C3%BCcker_coordinates).
		/// <br/>
		/// The coordinates specified in this way correspond to the following  multivector:
		/// <br/>
		/// <c>a e₀₁ + b e₀₂ + c e₀₃ + d e₂₃ + e e₃₁ + f e₁₂</c>
		/// </summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Line(float a, float b, float c, float d, float e, float f)
		{
			P1 = _mm_set_ps(f, e, d, 0f);
			P2 = _mm_set_ps(c, b, a, 0f);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Line(__m128 p2)
		{
			P1 = default;
			P2 = p2;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Line(__m128 p1, __m128 p2)
		{
			P1 = p1;
			P2 = p2;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Line(IdealLine other)
		{
			P1 = _mm_setzero_ps();
			P2 = other.P2;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Line(Branch other)
		{
			P1 = other.P1;
			P2 = _mm_setzero_ps();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe Line(__m128* p1p2)
		{
			P1 = p1p2[0];
			P2 = p1p2[1];
		}

		/// <summary>
		/// Store the 8 float components into memory
		/// </summary>
		public unsafe void Store(float* data)
		{
			_mm_storeu_ps(data, P1);
			_mm_storeu_ps(data + 4, P2);
		}

		/// <summary>
		/// Store the 8 float components in a span
		/// </summary>
		public unsafe void Store(Span<float> data)
		{
			if (data.Length < 8)
				throw new ArgumentOutOfRangeException(nameof(data));

			fixed (float* p = data)
			{
				_mm_storeu_ps(p, P1);
				_mm_storeu_ps(p + 4, P2);
			}
		}

		/// <summary>
		/// Deconstructs the components of the line <c>a e₀₁ + b e₀₂ + c e₀₃ + d e₂₃ + e e₃₁ + f e₁₂</c>
		/// </summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Deconstruct(out float a, out float b, out float c, out float d, out float e, out float f)
		{
			a = e01;
			b = e02;
			c = e03;
			d = e23;
			e = e31;
			f = e12;
		}

		/// <summary>
		/// Deconstructs the line in
		/// p1: (1, e12, e31, e23)
		/// p2: (e0123, e01, e02, e03)
		/// </summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Deconstruct(out __m128 p1, out __m128 p2)
		{
			p1 = P1;
			p2 = P2;
		}

		/// <summary>
		/// Returns the square root of the quantity produced by `squared_norm`.
		/// </summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float Norm()
		{
			return MathF.Sqrt(SquaredNorm());
		}

		/// <summary>
		/// If a Line is constructed as the regressive product (join) of
		/// two points, the squared norm provided here is the squared
		/// distance between the two points (provided the points are
		/// normalized). Returns $d^2 + e^2 + f^2$.
		/// </summary>
		public float SquaredNorm()
		{
			__m128 dp = Detail.hi_dp(P1, P1);
			return _mm_store_ss(dp);
		}

		/// Normalize a Line such that $\ell^2 = -1$.
		public static (__m128, __m128) Normalized(__m128 p1, __m128 p2)
		{
			// l = b + c where b is p1 and c is p2
			// l * ~l = |b|^2 - 2(b1 c1 + b2 c2 + b3 c3)e0123
			//
			// sqrt(l*~l) = |b| - (b1 c1 + b2 c2 + b3 c3)/|b| e0123
			//
			// 1/sqrt(l*~l) = 1/|b| + (b1 c1 + b2 c2 + b3 c3)/|b|^3 e0123
			//              = s + t e0123
			__m128 b2 = Detail.hi_dp_bc(p1, p1);
			__m128 s = Detail.rsqrt_nr1(b2);
			__m128 bc = Detail.hi_dp_bc(p1, p2);
			__m128 t = _mm_mul_ps(_mm_mul_ps(bc, Detail.rcp_nr1(b2)), s);

			// p1 * (s + t e0123) = s * p1 - t P1perp
			__m128 tmp = _mm_mul_ps(p2, s);
			p2 = _mm_sub_ps(tmp, _mm_mul_ps(p1, t));
			p1 = _mm_mul_ps(p1, s);

			return (p1, p2);
		}

		/// Return a normalized copy of this Line
		public Line Normalized()
		{
			var (p1, p2) = Normalized(P1, P2);
			return new Line(p1, p2);
		}

		public static (__m128 p1, __m128 p2) Inverse(__m128 p1, __m128 p2)
		{
			// s, t computed as in the normalization
			__m128 b2 = Detail.hi_dp_bc(p1, p1);
			__m128 s = Detail.rsqrt_nr1(b2);
			__m128 bc = Detail.hi_dp_bc(p1, p2);
			__m128 b2_inv = Detail.rcp_nr1(b2);
			__m128 t = _mm_mul_ps(_mm_mul_ps(bc, b2_inv), s);
			__m128 neg = _mm_set_ps(-0f, -0f, -0f, 0f);

			// p1 * (s + t e0123)^2 = (s * p1 - t P1perp) * (s + t e0123)
			// = s^2 p1 - s t P1perp - s t P1perp
			// = s^2 p1 - 2 s t P1perp
			// p2 * (s + t e0123)^2 = s^2 p2
			// NOTE: s^2 = b2_inv
			__m128 st = _mm_mul_ps(s, t);
			st = _mm_mul_ps(p1, st);
			p2 = _mm_sub_ps(_mm_mul_ps(p2, b2_inv), _mm_add_ps(st, st));
			p2 = _mm_xor_ps(p2, neg);

			p1 = _mm_xor_ps(_mm_mul_ps(p1, b2_inv), neg);

			return (p1, p2);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Line Inverse()
		{
			var (p1, p2) = Inverse(P1, P2);
			return new Line(p1, p2);
		}

		public float e23 => P1.GetElement(1);
		public float e32 => -e23;

		public float e31 => P1.GetElement(2);
		public float e13 => -e31;

		public float e12 => P1.GetElement(3);
		public float e21 => e12;

		public float e01 => P2.GetElement(1);
		public float e10 => -e01;

		public float e02 => P2.GetElement(2);
		public float e20 => -e02;

		public float e03 => P2.GetElement(3);
		public float e30 => -e03;

		/// <summary>
		/// Approximate equality test
		/// </summary>
		public bool Equals(in Line other, float epsilon)
		{
			__m128 eps = _mm_set1_ps(epsilon);
			__m128 neg = _mm_set1_ps(-0f);
			__m128 cmp1 = _mm_cmplt_ps(_mm_andnot_ps(neg, _mm_sub_ps(P1, other.P1)), eps);
			__m128 cmp2
				= _mm_cmplt_ps(_mm_andnot_ps(neg, _mm_sub_ps(P2, other.P2)), eps);
			__m128 cmp = _mm_and_ps(cmp1, cmp2);
			return _mm_movemask_ps(cmp) == 0xf;
		}

		public bool Equals(Line other)
		{
			return P1.Equals(other.P1) && P2.Equals(other.P2);
		}

		public override bool Equals(object obj)
		{
			return obj is Line other && Equals(other);
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(P1, P2);
		}

		public static bool operator ==(in Line left, in Line right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(in Line left, in Line right)
		{
			return !left.Equals(right);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Line operator +(in Line a, in Line b)
		{
			var p1 = _mm_add_ps(a.P1, b.P1);
			var p2 = _mm_add_ps(a.P2, b.P2);
			return new Line(p1, p2);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Line operator -(in Line a, in Line b)
		{
			var p1 = _mm_sub_ps(a.P1, b.P1);
			var p2 = _mm_sub_ps(a.P2, b.P2);
			return new Line(p1, p2);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Line operator *(in Line l, float s)
		{
			__m128 vs = _mm_set1_ps(s);
			var p1 = _mm_mul_ps(l.P1, vs);
			var p2 = _mm_mul_ps(l.P2, vs);
			return new Line(p1, p2);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Line operator *(float s, in Line l)
		{
			return l * s;
		}

		public static Line operator /(in Line r, float s)
		{
			__m128 vs = Detail.rcp_nr1(_mm_set1_ps(s));
			var p1 = _mm_mul_ps(r.P1, vs);
			var p2 = _mm_mul_ps(r.P2, vs);
			return new Line(p1, p2);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Line operator -(in Line l)
		{
			__m128 flip = _mm_set1_ps(-0f);
			var p1 = _mm_xor_ps(l.P1, flip);
			var p2 = _mm_xor_ps(l.P2, flip);
			return new Line(p1, p2);
		}

		/// <summary>
		/// Reversion operator
		/// </summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Line operator ~(in Line l)
		{
			__m128 flip = _mm_set_ps(-0f, -0f, -0f, 0f);
			var p1 = _mm_xor_ps(l.P1, flip);
			var p2 = _mm_xor_ps(l.P2, flip);
			return new Line(p1, p2);
		}

		/// <summary>
		/// TODO: Document!
		/// </summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Line operator !(in Line l)
		{
			return new Line(l.P2, l.P1);
		}

		/// <summary>
		/// TODO: Document!
		/// </summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Dual operator ^(Line a, IdealLine b)
		{
			return new Branch(a.P1) ^ b;
		}

		public static Dual operator ^(Line a, Line b)
		{
			__m128 aP1bP2 = Detail.hi_dp_ss(a.P1, b.P2);
			__m128 bP1aP2 = Detail.hi_dp_ss(b.P1, a.P2);
			return new Dual(0, _mm_store_ss(aP1bP2) + _mm_store_ss(bP1aP2));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Point operator ^(Line b, Plane a)
		{
			return a ^ b;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Dual operator ^(Line a, Branch b)
		{
			return new IdealLine(a.P2) ^ b;
		}

		public static Plane operator |(Line b, Plane a)
		{
			return new Plane(Detail.dotPL(true, a.P0, b.P1, b.P2));
		}

		public static float operator |(Line a, Line b)
		{
			return _mm_store_ss(Detail.dot11(a.P1, b.P1));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Plane operator |(Line b, Point a)
		{
			return a | b;
		}

		/// <summary>
		/// Generates a Motor $m$ that produces a screw motion about the common normal
		/// to lines $a$ and $b$. The Motor given by $\sqrt{m}$ takes $b$ to $a$
		/// provided that $a$ and $b$ are both normalized.
		/// </summary>
		public static Motor operator *(Line a, Line b)
		{
			Detail.gpLL(a.P1, a.P2, b.P1, b.P2, out var p1, out var p2);
			return new Motor(p1, p2);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Motor operator /(Line a, Line b)
		{
			return a * b.Inverse();
		}


		/// <summary>
		/// Convert the line through a branch (i.e. line parallel through the origin)
		/// TODO: Check if this is correct! Klein C++ doesn't have this.
		/// </summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator Branch(in Line l)
		{
			return new Branch(l.P1);
		}

		/// <summary>
		/// Formats the line to the string <c>a e₀₁ + b e₀₂ + c e₀₃ + d e₂₃ + e e₃₁ + f e₁₂</c>,
		/// dropping elements that have zero components.
		/// </summary>
		public override string ToString()
		{
			var (a, b, c, d, e, f) = this;

			return new StringBuilder(64)
				.AppendElement(a, "e₀₁")
				.AppendElement(b, "e₀₂")
				.AppendElement(c, "e₀₃ ")
				.AppendElement(d, "e₂₃")
				.AppendElement(e, "e₃₁")
				.AppendElement(f, "e₁₂")
				.ZeroWhenEmpty();
		}
	}
}