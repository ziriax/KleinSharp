using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.X86;
using __m128 = System.Runtime.Intrinsics.Vector128<float>;
using static KleinSharp.Simd;

namespace KleinSharp
{
	internal static partial class Detail
	{
		// DP high components and caller ignores returned high components
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static __m128 hi_dp_ss(__m128 a, __m128 b)
		{
			// 0 1 2 3 -> 1 + 2 + 3, 0, 0, 0

			__m128 res = _mm_mul_ps(a, b);

			// 0 1 2 3 -> 1 1 3 3
			__m128 hi = _mm_movehdup_ps(res);

			// 0 1 2 3 + 1 1 3 3 -> (0 + 1, 1 + 1, 2 + 3, 3 + 3)
			__m128 sum = _mm_add_ps(hi, res);

			// unpacklo: 0 0 1 1
			res = _mm_add_ps(sum, _mm_unpacklo_ps(res, res));

			// (1 + 2 + 3, _, _, _)
			return _mm_movehl_ps(res, res);
		}

		// Reciprocal with an additional single Newton-Raphson refinement
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static __m128 rcp_nr1(__m128 a)
		{
			// f(x) = 1/x - a
			// f'(x) = -1/x^2
			// x_{n+1} = x_n - f(x)/f'(x)
			//         = 2x_n - a x_n^2 = x_n (2 - a x_n)

			// ~2.7x baseline with ~22 bits of accuracy
			__m128 xn = _mm_rcp_ps(a);
			__m128 axn = _mm_mul_ps(a, xn);
			return _mm_mul_ps(xn, _mm_sub_ps(_mm_set1_ps(2f), axn));
		}

		// Reciprocal sqrt with an additional single Newton-Raphson refinement.
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static __m128 rsqrt_nr1(__m128 a)
		{
			// f(x) = 1/x^2 - a
			// f'(x) = -1/(2x^(3/2))
			// Let x_n be the estimate, and x_{n+1} be the refinement
			// x_{n+1} = x_n - f(x)/f'(x)
			//         = 0.5 * x_n * (3 - a x_n^2)

			// From Intel optimization manual: expected performance is ~5.2x
			// baseline (sqrtps + divps) with ~22 bits of accuracy

			__m128 xn = _mm_rsqrt_ps(a);
			__m128 axn2 = _mm_mul_ps(xn, xn);
			axn2 = _mm_mul_ps(a, axn2);
			__m128 xn3 = _mm_sub_ps(_mm_set1_ps(3f), axn2);
			return _mm_mul_ps(_mm_mul_ps(_mm_set1_ps(0.5f), xn), xn3);
		}

		// Sqrt Newton-Raphson is evaluated in terms of rsqrt_nr1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static __m128 sqrt_nr1(__m128 a)
		{
			return _mm_mul_ps(a, rsqrt_nr1(a));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static __m128 hi_dp(__m128 a, __m128 b)
		{
			if (Sse41.IsSupported)
				return _mm_dp_ps(a, b, 0b11100001);

			// 0 1 2 3 -> 1 + 2 + 3, 0, 0, 0
			__m128 res = _mm_mul_ps(a, b);

			// 0 1 2 3 -> 1 1 3 3
			__m128 hi = _mm_movehdup_ps(res);

			// 0 1 2 3 + 1 1 3 3 -> (0 + 1, 1 + 1, 2 + 3, 3 + 3)
			__m128 sum = _mm_add_ps(hi, res);

			// unpacklo: 0 0 1 1
			res = _mm_add_ps(sum, _mm_unpacklo_ps(res, res));

			// (1 + 2 + 3, _, _, _)
			res = _mm_movehl_ps(res, res);

			return _mm_and_ps(res, _mm_castsi128_ps(_mm_set_epi32(0, 0, 0, -1)));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static __m128 hi_dp_bc(__m128 a, __m128 b)
		{
			if (Sse41.IsSupported)
				return _mm_dp_ps(a, b, 0b11101111);

			// Multiply across and mask low component
			__m128 res = _mm_mul_ps(a, b);

			// 0 1 2 3 -> 1 1 3 3
			__m128 hi = _mm_movehdup_ps(res);

			// 0 1 2 3 + 1 1 3 3 -> (0 + 1, 1 + 1, 2 + 3, 3 + 3)
			__m128 sum = _mm_add_ps(hi, res);

			// unpacklo: 0 0 1 1
			res = _mm_add_ps(sum, _mm_unpacklo_ps(res, res));

			return KLN_SWIZZLE(res, 2, 2, 2, 2);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static __m128 dp(__m128 a, __m128 b)
		{
			if (Sse41.IsSupported)
				return _mm_dp_ps(a, b, 0b11110001);

			// Multiply across and shift right (shifting in zeros)
			__m128 res = _mm_mul_ps(a, b);
			__m128 hi = _mm_movehdup_ps(res);
			// (a1 b1, a2 b2, a3 b3, 0) + (a2 b2, a2 b2, 0, 0)
			// = (a1 b1 + a2 b2, _, a3 b3, 0)
			res = _mm_add_ps(hi, res);
			res = _mm_add_ss(res, _mm_movehl_ps(hi, res));
			return _mm_and_ps(res, _mm_castsi128_ps(_mm_set_epi32(0, 0, 0, -1)));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static __m128 dp_bc(__m128 a, __m128 b)
		{
			if (Sse41.IsSupported)
				return _mm_dp_ps(a, b, 0xff);

			// Multiply across and shift right (shifting in zeros)
			__m128 res = _mm_mul_ps(a, b);
			__m128 hi = _mm_movehdup_ps(res);
			// (a1 b1, a2 b2, a3 b3, 0) + (a2 b2, a2 b2, 0, 0)
			// = (a1 b1 + a2 b2, _, a3 b3, 0)
			res = _mm_add_ps(hi, res);
			res = _mm_add_ss(res, _mm_movehl_ps(hi, res));
			return KLN_SWIZZLE(res, 0, 0, 0, 0);
		}
	}
}