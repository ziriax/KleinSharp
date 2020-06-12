// ReSharper disable IdentifierTypo

using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

using __m128 = System.Runtime.Intrinsics.Vector128<float>;
using __m128i = System.Runtime.Intrinsics.Vector128<int>;

namespace KleinSharp
{
	public static class Simd
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static byte _MM_SHUFFLE(int a, int b, int c, int d)
		{
			return (byte)(a << 6 | b << 4 | c << 2 | d);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static __m128 _mm_mul_ps(__m128 a, __m128 b) => Sse.Multiply(a, b);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static __m128 _mm_add_ps(__m128 a, __m128 b) => Sse.Add(a, b);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static __m128 _mm_movehdup_ps(__m128 a) => Sse3.MoveHighAndDuplicate(a);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static __m128 _mm_unpacklo_ps(__m128 a, __m128 b) => Sse.UnpackLow(a, b);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static __m128 _mm_movehl_ps(__m128 a, __m128 b) => Sse.MoveHighToLow(a, b);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static __m128 _mm_rcp_ps(__m128 a) => Sse.Reciprocal(a);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static __m128 _mm_sub_ps(__m128 a, __m128 b) => Sse.Subtract(a, b);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static __m128 _mm_sub_ss(__m128 a, __m128 b) => Sse.SubtractScalar(a, b);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static __m128 _mm_mul_ss(__m128 a, __m128 b) => Sse.MultiplyScalar(a, b);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static __m128 _mm_set1_ps(float v) => Vector128.Create(v);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static __m128 _mm_set_ss(float v) => Vector128.CreateScalar(v);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static __m128 _mm_setzero_ps() => __m128.Zero;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static __m128 _mm_set_ps(float f3, float f2, float f1, float f0) => Vector128.Create(f0, f1, f2, f3);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static __m128 _mm_rsqrt_ps(__m128 a) => Sse.ReciprocalSqrt(a);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static __m128 _mm_dp_ps(__m128 a, __m128 b, byte control) => Sse41.DotProduct(a, b, control);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static __m128 _mm_and_ps(__m128 a, __m128 b) => Sse.And(a, b);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static __m128 _mm_castsi128_ps(__m128i a) => a.AsSingle();

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static __m128i _mm_set_epi32(int e3, int e2, int e1, int e0) => Vector128.Create(e0, e1, e2, e3);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static __m128 _mm_shuffle_ps(__m128 left, __m128 right, byte control) => Sse.Shuffle(left, right, control);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static __m128 _mm_add_ss(__m128 a, __m128 b) => Sse.AddScalar(a, b);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static __m128 KLN_SWIZZLE(__m128 reg, int x, int y, int z, int w) => _mm_shuffle_ps(reg, reg, _MM_SHUFFLE(x, y, z, w));

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static unsafe __m128 KLN_SWIZZLE(__m128* reg, int x, int y, int z, int w) => _mm_shuffle_ps(*reg, *reg, _MM_SHUFFLE(x, y, z, w));

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static unsafe void _mm_store_ss(float* address, __m128 source) => Sse.StoreScalar(address, source);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static unsafe void _mm_store_ss(out float address, __m128 source)
		{
			fixed (float* ptr = &address)
			{
				Sse.StoreScalar(ptr, source);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static __m128 _mm_xor_ps(__m128 a, __m128 b) => Sse.Xor(a, b);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static __m128 _mm_blend_ps(__m128 left, __m128 right, byte control) => Sse41.Blend(left, right, control);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static __m128 _mm_moveldup_ps(__m128 a) => Sse3.MoveLowAndDuplicate(a);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static __m128 _mm_movelh_ps(__m128 a, __m128 b) => Sse.MoveLowToHigh(a, b);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static __m128 _mm_unpackhi_ps(__m128 a, __m128 b) => Sse.UnpackHigh(a, b);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static unsafe void _mm_storeu_ps(float* address, __m128 source) => Sse.Store(address, source);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static unsafe void _mm_store_ps(float* address, __m128 source) => Sse.StoreAligned(address, source);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static unsafe __m128 _mm_loadu_ps(float* address) => Sse.LoadVector128(address);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static __m128 _mm_cmplt_ps(__m128 a, __m128 b) => Sse.CompareLessThan(a, b);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static __m128 _mm_andnot_ps(__m128 a, __m128 b) => Sse.AndNot(a, b);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int _mm_movemask_ps(__m128 a) => Sse.MoveMask(a);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static __m128 _mm_cmpeq_ps(__m128 a, __m128 b) => Sse.CompareEqual(a, b);
	}
}