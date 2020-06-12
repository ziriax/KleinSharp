using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.X86;
using __m128 = System.Runtime.Intrinsics.Vector128<float>;
using static KleinSharp.Simd;

namespace KleinSharp
{
	internal static partial class Detail
	{
		// Partition memory layouts
		//     LSB --> MSB
		// p0: (e0, e1, e2, e3)
		// p1: (1, e23, e31, e12)
		// p2: (e0123, e01, e02, e03)
		// p3: (e123, e032, e013, e021)
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void dot00(__m128 a, __m128 b, out __m128 p1_out)
		{
			// a1 b1 + a2 b2 + a3 b3
			p1_out = hi_dp(a, b);
		}

		// The symmetric inner product on these two partitions commutes
		[MethodImpl(MethodImplOptions.AggressiveOptimization)]
		public static void dot03out(__m128 a, __m128 b, out __m128 p1_out, out __m128 p2_out)
		{
			// (a2 b1 - a1 b2) e03 +
			// (a3 b2 - a2 b3) e01 +
			// (a1 b3 - a3 b1) e02 +
			// a1 b0 e23 +
			// a2 b0 e31 +
			// a3 b0 e12
			p1_out = _mm_mul_ps(a, KLN_SWIZZLE(b, 0, 0, 0, 0));

			p1_out = Sse41.IsSupported
				? _mm_blend_ps(p1_out, _mm_setzero_ps(), 1)
				: _mm_and_ps(p1_out, _mm_castsi128_ps(_mm_set_epi32(-1, -1, -1, 0)));

			p2_out
				= KLN_SWIZZLE(_mm_sub_ps(_mm_mul_ps(KLN_SWIZZLE(a, 1, 3, 2, 0), b),
						_mm_mul_ps(a, KLN_SWIZZLE(b, 1, 3, 2, 0))),
					1,
					3,
					2,
					0);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void dot11(__m128 a, __m128 b, out __m128 p1_out)
		{
			p1_out = _mm_xor_ps(_mm_set_ss(-0f), hi_dp_ss(a, b));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void dot33(__m128 a, __m128 b, out __m128 p1_out)
		{
			// -a0 b0
			p1_out = _mm_mul_ps(_mm_set_ss(-1f), _mm_mul_ss(a, b));
		}

		// Point | Line
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void dotPTL(__m128 a, __m128 b, out __m128 p0)
		{
			// (a1 b1 + a2 b2 + a3 b3) e0 +
			// -a0 b1 e1 +
			// -a0 b2 e2 +
			// -a0 b3 e3

			__m128 dp = hi_dp_ss(a, b);
			p0 = _mm_mul_ps(KLN_SWIZZLE(a, 0, 0, 0, 0), b);
			p0 = _mm_xor_ps(p0, _mm_set_ps(-0f, -0f, -0f, 0f));
			p0 = Sse41.IsSupported ? _mm_blend_ps(p0, dp, 1) : _mm_add_ss(p0, dp);
		}

		// Plane | Ideal Line
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void dotPIL(bool flip, __m128 a, __m128 c, out __m128 p0)
		{
			p0 = hi_dp(a, c);

			if (!flip)
			{
				p0 = _mm_xor_ps(p0, _mm_set_ss(-0f));
			}
		}

		// Plane | Line
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void dotPL(bool flip, __m128 a, __m128 b, __m128 c, out __m128 p0)
		{
			if (flip)
			{
				// (a1 c1 + a2 c2 + a3 c3) e0 +
				// (a1 b2 - a2 b1) e3
				// (a2 b3 - a3 b2) e1 +
				// (a3 b1 - a1 b3) e2 +

				p0 = _mm_mul_ps(a, KLN_SWIZZLE(b, 1, 3, 2, 0));
				p0 = _mm_sub_ps(p0, _mm_mul_ps(KLN_SWIZZLE(a, 1, 3, 2, 0), b));
				p0 = _mm_add_ss(KLN_SWIZZLE(p0, 1, 3, 2, 0), hi_dp_ss(a, c));
			}
			else
			{
				// -(a1 c1 + a2 c2 + a3 c3) e0 +
				// (a2 b1 - a1 b2) e3
				// (a3 b2 - a2 b3) e1 +
				// (a1 b3 - a3 b1) e2 +

				p0 = _mm_mul_ps(KLN_SWIZZLE(a, 1, 3, 2, 0), b);
				p0 = _mm_sub_ps(p0, _mm_mul_ps(a, KLN_SWIZZLE(b, 1, 3, 2, 0)));
				p0 = _mm_sub_ss(KLN_SWIZZLE(p0, 1, 3, 2, 0), hi_dp_ss(a, c));
			}
		}
	}
}