// ReSharper disable InconsistentNaming

using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.X86;
using __m128 = System.Runtime.Intrinsics.Vector128<float>;
using static KleinSharp.Simd;

namespace KleinSharp
{
	// File: geometric_product.hpp
	// Purpose: Define functions of the form gpAB where A and B are partition
	// indices. Each function so-defined computes the geometric product using vector
	// intrinsics. The partition index determines which basis elements are present
	// in each XMM component of the operand.
	// A number of the computations in this file are performed symbolically in
	// scripts/validation.klein
	internal static partial class Detail
	{
		// Partition memory layouts
		//     LSB --> MSB
		// p0: (e0, e1, e2, e3)
		// p1: (1, e12, e31, e23)
		// p2: (e0123, e01, e02, e03)
		// p3: (e123, e032, e013, e021)

		// p0: (e0, e1, e2, e3)
		// p1: (1, e23, e31, e12)
		// p2: (e0123, e01, e02, e03)
		[MethodImpl(MethodImplOptions.AggressiveOptimization)]
		public static void gp00(__m128 a, __m128 b, out __m128 p1_out, out __m128 p2_out)
		{
			// (a1 b1 + a2 b2 + a3 b3) +
			//
			// (a2 b3 - a3 b2) e23 +
			// (a3 b1 - a1 b3) e31 +
			// (a1 b2 - a2 b1) e12 +
			//
			// (a0 b1 - a1 b0) e01 +
			// (a0 b2 - a2 b0) e02 +
			// (a0 b3 - a3 b0) e03

			p1_out = _mm_mul_ps(_mm_swizzle_ps(a, 121 /* 1, 3, 2, 1 */), _mm_swizzle_ps(b, 157 /* 2, 1, 3, 1 */));

			p1_out = _mm_sub_ps(p1_out,
									  _mm_xor_ps(_mm_set_ss(-0f),
													 _mm_mul_ps(_mm_swizzle_ps(a, 158 /* 2, 1, 3, 2 */),
																	_mm_swizzle_ps(b, 122 /* 1, 3, 2, 2 */))));
			// Add a3 b3 to the lowest component
			p1_out = _mm_add_ss(
				 p1_out,
				 _mm_mul_ps(_mm_swizzle_ps(a, 3 /* 0, 0, 0, 3 */), _mm_swizzle_ps(b, 3 /* 0, 0, 0, 3 */)));

			// (a0 b0, a0 b1, a0 b2, a0 b3)
			p2_out = _mm_mul_ps(_mm_swizzle_ps(a, 0 /* 0, 0, 0, 0 */), b);

			// Sub (a0 b0, a1 b0, a2 b0, a3 b0)
			// Note that the lowest component cancels
			p2_out = _mm_sub_ps(p2_out, _mm_mul_ps(a, _mm_swizzle_ps(b, 0 /* 0, 0, 0, 0 */)));
		}

		// p0: (e0, e1, e2, e3)
		// p3: (e123, e032, e013, e021)
		// p1: (1, e12, e31, e23)
		// p2: (e0123, e01, e02, e03)
		[MethodImpl(MethodImplOptions.AggressiveOptimization)]
		public static void gp03(bool flip, __m128 a, __m128 b, out __m128 p1, out __m128 p2)
		{
			if (flip)
			{
				// a1 b0 e23 +
				// a2 b0 e31 +
				// a3 b0 e12 +
				// (a0 b0 + a1 b1 + a2 b2 + a3 b3) e0123 +
				// (a3 b2 - a2 b3) e01 +
				// (a1 b3 - a3 b1) e02 +
				// (a2 b1 - a1 b2) e03
				p1 = _mm_mul_ps(a, _mm_swizzle_ps(b, 0 /* 0, 0, 0, 0 */));
				p1 = Sse41.IsSupported
					? _mm_blend_ps(p1, _mm_setzero_ps(), 1)
					: _mm_and_ps(p1, _mm_castsi128_ps(_mm_set_epi32(-1, -1, -1, 0)));

				// (_, a3 b2, a1 b3, a2 b1)
				p2 = _mm_mul_ps(_mm_swizzle_ps(a, 156 /* 2, 1, 3, 0 */), _mm_swizzle_ps(b, 120 /* 1, 3, 2, 0 */));
				p2 = _mm_sub_ps(
					p2,
					_mm_mul_ps(_mm_swizzle_ps(a, 120 /* 1, 3, 2, 0 */), _mm_swizzle_ps(b, 156 /* 2, 1, 3, 0 */)));

				// Compute a0 b0 + a1 b1 + a2 b2 + a3 b3 and store it in the low
				// component
				__m128 tmp = dp(a, b);

				tmp = _mm_xor_ps(tmp, _mm_set_ss(-0f));

				p2 = _mm_add_ps(p2, tmp);
			}
			else
			{
				// a1 b0 e23 +
				// a2 b0 e31 +
				// a3 b0 e12 +
				// -(a0 b0 + a1 b1 + a2 b2 + a3 b3) e0123 +
				// (a3 b2 - a2 b3) e01 +
				// (a1 b3 - a3 b1) e02 +
				// (a2 b1 - a1 b2) e03

				p1 = _mm_mul_ps(a, _mm_swizzle_ps(b, 0 /* 0, 0, 0, 0 */));
				p1 = Sse41.IsSupported
					? _mm_blend_ps(p1, _mm_setzero_ps(), 1)
					: _mm_and_ps(p1, _mm_castsi128_ps(_mm_set_epi32(-1, -1, -1, 0)));

				// (_, a3 b2, a1 b3, a2 b1)
				p2 = _mm_mul_ps(_mm_swizzle_ps(a, 156 /* 2, 1, 3, 0 */), _mm_swizzle_ps(b, 120 /* 1, 3, 2, 0 */));
				p2 = _mm_sub_ps(
					p2,
					_mm_mul_ps(_mm_swizzle_ps(a, 120 /* 1, 3, 2, 0 */), _mm_swizzle_ps(b, 156 /* 2, 1, 3, 0 */)));

				// Compute a0 b0 + a1 b1 + a2 b2 + a3 b3 and store it in the low
				// component
				__m128 tmp = dp(a, b);
				p2 = _mm_add_ps(p2, tmp);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveOptimization)]
		public static __m128 gp11(__m128 a, __m128 b)
		{
			// (a0 b0 - a1 b1 - a2 b2 - a3 b3) +
			// (a0 b1 - a2 b3 + a1 b0 + a3 b2)*e23
			// (a0 b2 - a3 b1 + a2 b0 + a1 b3)*e31
			// (a0 b3 - a1 b2 + a3 b0 + a2 b1)*e12

			// We use abcd to refer to the slots to avoid conflating bivector/scalar
			// coefficients with cartesian coordinates

			// In general, we can get rid of at most one swizzle
			var p1Out = _mm_mul_ps(_mm_swizzle_ps(a, 0 /*0, 0, 0, 0*/), b);

			p1Out = _mm_sub_ps(
				 p1Out,
				 _mm_mul_ps(_mm_swizzle_ps(a, 121 /* 1, 3, 2, 1 */), _mm_swizzle_ps(b, 157 /* 2, 1, 3, 1 */)));

			// In a separate register, accumulate the later components so we can
			// negate the lower single-precision element with a single instruction
			__m128 tmp1
				 = _mm_mul_ps(_mm_swizzle_ps(a, 230 /* 3, 2, 1, 2 */), _mm_swizzle_ps(b, 2 /*0, 0, 0, 2*/));

			__m128 tmp2
				 = _mm_mul_ps(_mm_swizzle_ps(a, 159 /*2, 1, 3, 3*/), _mm_swizzle_ps(b, 123 /*1, 3, 2, 3*/));

			__m128 tmp = _mm_xor_ps(_mm_add_ps(tmp1, tmp2), _mm_set_ss(-0f));

			p1Out = _mm_add_ps(p1Out, tmp);

			return p1Out;
		}
		// p3: (e123, e021, e013, e032) // p2: (e0123, e01, e02, e03) [MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static __m128 gp33(__m128 a, __m128 b)
		{
			// (-a0 b0) +
			// (-a0 b1 + a1 b0) e01 +
			// (-a0 b2 + a2 b0) e02 +
			// (-a0 b3 + a3 b0) e03
			//
			// Produce a translator by dividing all terms by a0 b0

			__m128 tmp = _mm_mul_ps(_mm_swizzle_ps(a, 0 /* 0, 0, 0, 0 */), b);
			tmp = _mm_mul_ps(tmp, _mm_set_ps(-1f, -1f, -1f, -2f));
			tmp = _mm_add_ps(tmp, _mm_mul_ps(a, _mm_swizzle_ps(b, 0 /* 0, 0, 0, 0 */)));

			// (0, 1, 2, 3) -> (0, 0, 2, 2)
			__m128 ss = _mm_moveldup_ps(tmp);
			ss = _mm_movelh_ps(ss, ss);
			tmp = _mm_mul_ps(tmp, rcp_nr1(ss));

			return Sse41.IsSupported
				? _mm_blend_ps(tmp, _mm_setzero_ps(), 1)
				: _mm_and_ps(tmp, _mm_castsi128_ps(_mm_set_epi32(-1, -1, -1, 0)));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void gpDL(float u, float v, __m128 b, __m128 c, out __m128 p1, out __m128 p2)
		{
			// b1 u e23 +
			// b2 u e31 +
			// b3 u e12 +
			// (-b1 v + c1 u) e01 +
			// (-b2 v + c2 u) e02 +
			// (-b3 v + c3 u) e03
			__m128 u_vec = _mm_set1_ps(u);
			__m128 v_vec = _mm_set1_ps(v);
			p1 = _mm_mul_ps(u_vec, b);
			p2 = _mm_mul_ps(c, u_vec);
			p2 = _mm_sub_ps(p2, _mm_mul_ps(b, v_vec));
		}

		[MethodImpl(MethodImplOptions.AggressiveOptimization)]
		public static __m128 gpRT(bool flip, __m128 a, __m128 b)
		{
			__m128 p2;

			if (flip)
			{
				// (a1 b1 + a2 b2 + a3 b3) e0123 +
				// (a0 b1 + a2 b3 - a3 b2) e01 +
				// (a0 b2 + a3 b1 - a1 b3) e02 +
				// (a0 b3 + a1 b2 - a2 b1) e03

				p2 = _mm_mul_ps(_mm_swizzle_ps(a, 1 /* 0, 0, 0, 1 */), _mm_swizzle_ps(b, 229 /* 3, 2, 1, 1 */));
				p2 = _mm_add_ps(
					p2,
					_mm_mul_ps(_mm_swizzle_ps(a, 122 /* 1, 3, 2, 2 */), _mm_swizzle_ps(b, 158 /* 2, 1, 3, 2 */)));
				p2 = _mm_sub_ps(p2,
					_mm_xor_ps(_mm_set_ss(-0f),
						_mm_mul_ps(_mm_swizzle_ps(a, 159 /*2, 1, 3, 3*/),
							_mm_swizzle_ps(b, 123 /*1, 3, 2, 3*/))));
			}
			else
			{
				// (a1 b1 + a2 b2 + a3 b3) e0123 +
				// (a0 b1 + a3 b2 - a2 b3) e01 +
				// (a0 b2 + a1 b3 - a3 b1) e02 +
				// (a0 b3 + a2 b1 - a1 b2) e03

				p2 = _mm_mul_ps(_mm_swizzle_ps(a, 1 /* 0, 0, 0, 1 */), _mm_swizzle_ps(b, 229 /* 3, 2, 1, 1 */));
				p2 = _mm_add_ps(
					p2,
					_mm_mul_ps(_mm_swizzle_ps(a, 158 /* 2, 1, 3, 2 */), _mm_swizzle_ps(b, 122 /* 1, 3, 2, 2 */)));
				p2 = _mm_sub_ps(p2,
					_mm_xor_ps(_mm_set_ss(-0f),
						_mm_mul_ps(_mm_swizzle_ps(a, 123 /*1, 3, 2, 3 */),
				_mm_swizzle_ps(b, 159 /*2, 1, 3, 3*/))));
			}

			return p2;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static __m128 gp12(bool flip, __m128 a, __m128 b)
		{
			var p2 = gpRT(flip, a, b);
			p2 = _mm_sub_ps(p2,
								 _mm_xor_ps(_mm_set_ss(-0f),
												_mm_mul_ps(a, _mm_swizzle_ps(b, 0 /* 0, 0, 0, 0 */))));
			return p2;
		}

		/// <summary>
		/// Optimized line * line operation
		/// (a,d) = first line P1, P2
		/// (b,c) = second line P1, P2
		/// </summary>
		[MethodImpl(MethodImplOptions.AggressiveOptimization)]
		public static void gpLL(__m128 a, __m128 d, __m128 b, __m128 c, out __m128 p1, out __m128 p2)
		{
			// (-a1 b1 - a3 b3 - a2 b2) +
			// (a2 b1 - a1 b2) e12 +
			// (a1 b3 - a3 b1) e31 +
			// (a3 b2 - a2 b3) e23 +
			// (a1 c1 + a3 c3 + a2 c2 + b1 d1 + b3 d3 + b2 d2) e0123
			// (a3 c2 - a2 c3         + b2 d3 - b3 d2) e01 +
			// (a1 c3 - a3 c1         + b3 d1 - b1 d3) e02 +
			// (a2 c1 - a1 c2         + b1 d2 - b2 d1) e03 +
			__m128 flip = _mm_set_ss(-0f);

			p1 = _mm_mul_ps(_mm_swizzle_ps(a, 217 /* 3, 1, 2, 1 */), _mm_swizzle_ps(b, 181 /* 2, 3, 1, 1 */));
			p1 = _mm_xor_ps(p1, flip);
			p1 = _mm_sub_ps(p1,
				 _mm_mul_ps(_mm_swizzle_ps(a, 183 /* 2, 3, 1, 3 */), _mm_swizzle_ps(b, 219 /* 3, 1, 2, 3 */)));
			__m128 a2 = _mm_unpackhi_ps(a, a);
			__m128 b2 = _mm_unpackhi_ps(b, b);
			p1 = _mm_sub_ss(p1, _mm_mul_ss(a2, b2));

			p2 = _mm_mul_ps(_mm_swizzle_ps(a, 157 /* 2, 1, 3, 1 */), _mm_swizzle_ps(c, 121 /* 1, 3, 2, 1 */));
			p2 = _mm_sub_ps(p2,
								 _mm_xor_ps(flip,
												_mm_mul_ps(_mm_swizzle_ps(a, 123 /*1, 3, 2, 3 */),
													_mm_swizzle_ps(c, 159 /*2, 1, 3, 3*/))));
			p2 = _mm_add_ps(
				 p2,
				 _mm_mul_ps(_mm_swizzle_ps(b, 121 /* 1, 3, 2, 1 */), _mm_swizzle_ps(d, 157 /* 2, 1, 3, 1 */)));
			p2 = _mm_sub_ps(p2,
								 _mm_xor_ps(flip,
												_mm_mul_ps(_mm_swizzle_ps(b, 159 /*2, 1, 3, 3*/),
													_mm_swizzle_ps(d, 123 /*1, 3, 2, 3*/))));
			__m128 c2 = _mm_unpackhi_ps(c, c);
			__m128 d2 = _mm_unpackhi_ps(d, d);
			p2 = _mm_add_ss(p2, _mm_mul_ss(a2, c2));
			p2 = _mm_add_ss(p2, _mm_mul_ss(b2, d2));
		}

		// Optimized motor * motor operation
		[MethodImpl(MethodImplOptions.AggressiveOptimization)]
		public static void gpMM(__m128 a, __m128 b, __m128 c, __m128 d, out __m128 e, out __m128 f)
		{
			// (a0 c0 - a1 c1 - a2 c2 - a3 c3) +
			// (a0 c1 + a3 c2 + a1 c0 - a2 c3) e23 +
			// (a0 c2 + a1 c3 + a2 c0 - a3 c1) e31 +
			// (a0 c3 + a2 c1 + a3 c0 - a1 c2) e12 +
			//
			// (a0 d0 + b0 c0 + a1 d1 + b1 c1 + a2 d2 + a3 d3 + b2 c2 + b3 c3)
			//  e0123 +
			// (a0 d1 + b1 c0 + a3 d2 + b3 c2 - a1 d0 - a2 d3 - b0 c1 - b2 c3)
			//  e01 +
			// (a0 d2 + b2 c0 + a1 d3 + b1 c3 - a2 d0 - a3 d1 - b0 c2 - b3 c1)
			//  e02 +
			// (a0 d3 + b3 c0 + a2 d1 + b2 c1 - a3 d0 - a1 d2 - b0 c3 - b1 c2)
			//  e03
			__m128 a_xxxx = _mm_swizzle_ps(a, 0 /* 0, 0, 0, 0 */);
			__m128 a_zyzw = _mm_swizzle_ps(a, 230 /* 3, 2, 1, 2 */);
			__m128 a_ywyz = _mm_swizzle_ps(a, 157 /* 2, 1, 3, 1 */);
			__m128 a_wzwy = _mm_swizzle_ps(a, 123 /*1, 3, 2, 3 */);
			__m128 c_wwyz = _mm_swizzle_ps(c, 159 /*2, 1, 3, 3*/);
			__m128 c_yzwy = _mm_swizzle_ps(c, 121 /* 1, 3, 2, 1 */);
			__m128 s_flip = _mm_set_ss(-0f);

			e = _mm_mul_ps(a_xxxx, c);
			__m128 t = _mm_mul_ps(a_ywyz, c_yzwy);
			t = _mm_add_ps(t, _mm_mul_ps(a_zyzw, _mm_swizzle_ps(c, 2 /*0, 0, 0, 2*/)));
			t = _mm_xor_ps(t, s_flip);
			e = _mm_add_ps(e, t);
			e = _mm_sub_ps(e, _mm_mul_ps(a_wzwy, c_wwyz));

			f = _mm_mul_ps(a_xxxx, d);
			f = _mm_add_ps(f, _mm_mul_ps(b, _mm_swizzle_ps(c, 0 /* 0, 0, 0, 0 */)));
			f = _mm_add_ps(f, _mm_mul_ps(a_ywyz, _mm_swizzle_ps(d, 121 /* 1, 3, 2, 1 */)));
			f = _mm_add_ps(f, _mm_mul_ps(_mm_swizzle_ps(b, 157 /* 2, 1, 3, 1 */), c_yzwy));
			t = _mm_mul_ps(a_zyzw, _mm_swizzle_ps(d, 2 /*0, 0, 0, 2*/));
			t = _mm_add_ps(t, _mm_mul_ps(a_wzwy, _mm_swizzle_ps(d, 159 /*2, 1, 3, 3*/)));
			t = _mm_add_ps(t, _mm_mul_ps(_mm_swizzle_ps(b, 2 /*0, 0, 0, 2*/), _mm_swizzle_ps(c, 230 /* 3, 2, 1, 2 */)));
			t = _mm_add_ps(t, _mm_mul_ps(_mm_swizzle_ps(b, 123 /*1, 3, 2, 3*/), c_wwyz));
			t = _mm_xor_ps(t, s_flip);
			f = _mm_sub_ps(f, t);
		}
	}
}