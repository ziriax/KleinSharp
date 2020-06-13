using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics;
using System.Text;
using __m128 = System.Runtime.Intrinsics.Vector128<float>;
using static KleinSharp.Simd;
// ReSharper disable InconsistentNaming

namespace KleinSharp
{

	/// <summary>
	/// Directions in 3D PGA are represented using points at infinity (homogeneous coordinate 0).
	/// Having a homogeneous coordinate of zero ensures that directions are translation-invariant.
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	public readonly struct Direction : IEquatable<Direction>
	{
		public readonly __m128 P3;

		/// <summary>
		/// Creates a normalized direction from coordinates
		/// </summary>
		public Direction(float x, float y, float z)
		{
			__m128 dir = _mm_set_ps(z, y, x, 0f);
			__m128 tmp = Detail.rsqrt_nr1(Detail.hi_dp_bc(dir, dir));
			P3 = _mm_mul_ps(dir, tmp);
		}

		/// <summary>
		/// Creates a direction from a raw SIMD vector.
		/// It is assumed the SIMD vector has 0 homogeneous coordinate.
		/// </summary>
		public Direction(__m128 p3)
		{
			P3 = p3;
		}

		/// Data should point to four floats with memory layout `(0f, x, y, z)`
		/// where the zero occupies the lowest address in memory.
		public unsafe Direction(float* data)
		{
			// ReSharper disable once CompareOfFloatsByEqualityOperator
			Debug.Assert(data[0] == 0f, "Homogeneous coordinate of point data used to initialize a direction must be exactly zero");
			P3 = _mm_loadu_ps(data);
		}

		public void Deconstruct(out float x, out float y, out float z)
		{
			x = X;
			y = Y;
			z = Z;
		}

		public float E1 => P3.GetElement(1);
		public float X => E1;
		public float e032 => E1;
		public float e023 => -E1;

		public float E2 => P3.GetElement(2);
		public float Y => E2;
		public float e013 => E2;
		public float e031 => -E2;

		public float E3 => P3.GetElement(3);
		public float Z => E3;
		public float e0=> E2;
		public float e021 => E3;
		public float e012 => -E3;

		/// Return a normalized direction by dividing all components by the
		/// magnitude (for speed, `rsqrtps` is used with a single Newton-Raphson refinement iteration)
		/// Return a normalized copy of this direction
		public Direction Normalized()
		{
			__m128 tmp = Detail.rsqrt_nr1(Detail.hi_dp_bc(P3, P3));
			return new Direction(_mm_mul_ps(P3, tmp));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Direction operator +(Direction a, Direction b)
		{
			return new Direction(_mm_add_ps(a.P3, b.P3));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Point operator +(Point a, Direction b)
		{
			return new Point(_mm_add_ps(a.P3, b.P3));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Point operator +(Direction a, Point b)
		{
			return new Point(_mm_add_ps(a.P3, b.P3));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Direction operator -(Direction a, Direction b)
		{
			return new Direction(_mm_sub_ps(a.P3, b.P3));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Point operator -(Point a, Direction b)
		{
			return new Point(_mm_sub_ps(a.P3, b.P3));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Point operator -(Direction a, Point b)
		{
			return new Point(_mm_sub_ps(a.P3, b.P3));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Direction operator *(Direction a, float s)
		{
			return new Direction(_mm_mul_ps(a.P3, _mm_set1_ps(s)));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Direction operator *(float s, Direction a)
		{
			return new Direction(_mm_mul_ps(a.P3, _mm_set1_ps(s)));
		}

		public static Direction operator /(Direction a, float s)
		{
			return new Direction(_mm_mul_ps(a.P3, Detail.rcp_nr1(_mm_set1_ps(s))));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator Point(Direction d)
		{
			return new Point(d.P3);
		}

		public bool Equals(Direction other)
		{
			return P3.Equals(other.P3);
		}

		public override bool Equals(object obj)
		{
			return obj is Direction other && Equals(other);
		}

		public override int GetHashCode()
		{
			return P3.GetHashCode();
		}

		public static bool operator ==(Direction left, Direction right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(Direction left, Direction right)
		{
			return !left.Equals(right);
		}

		public override string ToString()
		{
			return new StringBuilder(64)
				.AppendElement(X, "E₁")
				.AppendElement(Y, "E₂")
				.AppendElement(Z, "E₃")
				.ZeroWhenEmpty();
		}
	}
}