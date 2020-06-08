using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics;
using __m128 = System.Runtime.Intrinsics.Vector128<float>;
using static KleinSharp.Simd;

namespace KleinSharp
{

	/// <summary>
	/// Directions in 3D PGA are represented using points at infinity (homogeneous coordinate 0).
	/// Having a homogeneous coordinate of zero ensures that directions are translation-invariant.
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	public readonly struct Direction : IEquatable<Direction>
	{
		public readonly __m128 Packed;

		[MethodImpl(MethodImplOptions.AggressiveOptimization)]
		public static __m128 Normalized(__m128 dir)
		{
			__m128 tmp = Detail.rsqrt_nr1(Detail.hi_dp_bc(dir, dir));
			return _mm_mul_ps(dir, tmp);
		}

		/// <summary>
		/// Creates a normalized direction from coordinates
		/// </summary>
		public Direction(float x, float y, float z)
		{
			Packed = Normalized(_mm_set_ps(z, y, x, 0f));
		}

		/// <summary>
		/// Creates a direction from a raw SIMD vector.
		/// It is assumed the SIMD vector has 0 homogeneous coordinate.
		/// </summary>
		public Direction(__m128 packed)
		{
			Packed = packed;
		}

		/// Data should point to four floats with memory layout `(0f, x, y, z)`
		/// where the zero occupies the lowest address in memory.
		public unsafe Direction(float* data)
		{
			// ReSharper disable once CompareOfFloatsByEqualityOperator
			Debug.Assert(data[0] == 0f, "Homogeneous coordinate of point data used to initialize a direction must be exactly zero");
			Packed = _mm_loadu_ps(data);
		}

		public void Deconstruct(out float x, out float y, out float z)
		{
			x = X;
			y = Y;
			z = Z;
		}

		public float X => Packed.GetElement(1);

		public float Y => Packed.GetElement(2);

		public float Z => Packed.GetElement(3);

		public ReadOnlySpan<float> ToSpan() => Helpers.ToFloatSpan(this);

		/// Return a normalized direction by dividing all components by the
		/// magnitude (for speed, `rsqrtps` is used with a single Newton-Raphson refinement iteration)
		/// Return a normalized copy of this direction
		public Direction Normalized()
		{
			return new Direction(Normalized(Packed));
		}

		public static Direction operator +(Direction a, Direction b)
		{
			return new Direction(_mm_add_ps(a.Packed, b.Packed));
		}

		public static Direction operator -(Direction a, Direction b)
		{
			return new Direction(_mm_sub_ps(a.Packed, b.Packed));
		}

		public static Direction operator *(Direction a, float s)
		{
			return new Direction(_mm_mul_ps(a.Packed, _mm_set1_ps(s)));
		}

		public static Direction operator *(float s, Direction a)
		{
			return new Direction(_mm_mul_ps(a.Packed, _mm_set1_ps(s)));
		}

		public static Direction operator /(Direction a, float s)
		{
			return new Direction(_mm_mul_ps(a.Packed, Detail.rcp_nr1(_mm_set1_ps(s))));
		}

		public bool Equals(Direction other)
		{
			return Packed.Equals(other.Packed);
		}

		public override bool Equals(object obj)
		{
			return obj is Direction other && Equals(other);
		}

		public override int GetHashCode()
		{
			return Packed.GetHashCode();
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
			// TODO: Show units, e.g. e₀₁₂ e₀₂₃ e₀₁₃
			// [PV] Learning PGA myself, need to get the signs right below
			// e₁ <=> plane x=0 <=> yz plane
			// e₂ <=> plane y=0 <=> zx plane
			// e₃ <=> plane z=0 <=> xy plane
			// e₀ <=> plane w=0 <=> ideal "sphere at infinity"
			// e₁₂ <=> z axis
			// e₂₃ <=> x axis
			// e₃₁ <=> y axis
			// e₀₁₂ <=> z direction
			// e₀₂₃ <=> x direction
			// e₀₃₁ <=> y direction
			return $"Direction({X}, {Y}, {Z})";
		}
	}
}