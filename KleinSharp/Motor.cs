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
	/// A `Motor` represents a kinematic motion in our algebra. From
	/// [Chasles'  theorem](https://en.wikipedia.org/wiki/Chasles%27_theorem_(kinematics)), we
	/// know that any rigid body displacement can be produced by a translation along
	/// a Line, followed or preceded by a rotation about an axis parallel to that
	/// Line. The Motor algebra is isomorphic to the dual quaternions but exists
	/// here in the same algebra as all the other geometric entities and actions at
	/// our disposal. Operations such as composing a Motor with a rotor or
	/// translator are possible for example. The primary benefit to using a Motor
	/// over its corresponding matrix operation is twofold. First, you get the
	/// benefit of numerical stability when composing multiple actions via the
	/// geometric product (`*`). Second, because the Motors constitute a continuous
	/// group, they are amenable to smooth interpolation and differentiation.
	/// !!! example
	///
	///     ```c++
	///         // Create a rotor representing a pi/2 rotation about the z-axis
	///         // Normalization is done automatically
	///         rotor r{Pi * 0.5f, 0f, 0f, 1f};
	///
	///         // Create a translator that represents a translation of 1 unit
	///         // in the yz-direction. Normalization is done automatically.
	///         translator t{1f, 0f, 1f, 1f};
	///
	///         // Create a Motor that combines the action of the rotation and
	///         // translation above.
	///         Motor m = r * t;
	///
	///         // Initialize a point at (1, 3, 2)
	///         Point p1{1f, 3f, 2f};
	///
	///         // Translate p1 and rotate it to create a new point p2
	///         Point p2 = m(p1);
	///     ```
	///
	/// Motors can be multiplied to one another with the `*` operator to create
	/// a new Motor equivalent to the application of each factor.
	///
	/// !!! example
	///
	///     ```c++
	///         // Suppose we have 3 Motors m1, m2, and m3
	///
	///         // The Motor m created here represents the combined action of m1,
	///         // m2, and m3.
	///         Motor m = m3 * m2 * m1;
	///     ```
	///
	/// The same `*` operator can be used to compose the Motor's action with other
	/// translators and rotors.
	///
	/// A demonstration of using the exponential and logarithmic map to blend
	/// between two Motors is provided in a test case
	/// [here](https://github.com/jeremyong/Klein/blob/master/test/test_exp_log.cpp#L48).
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	public readonly struct Motor
	{
		public readonly __m128 P1;
		public readonly __m128 P2;

		/// <summary>
		/// Direct initialization from components.
		/// <br/>
		/// A more common way of creating a Motor is to take a product between a rotor and a translator.
		/// <br/>
		/// The arguments corresponds to the multivector
		/// <br/>
		/// <c>a + be₂₃ + ce₃₁ + de₁₂ + ee₀₁ + fe₀₂ + ge₀₃ + he₀₁₂₃</c>
		/// </summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Motor(float a, float b, float c, float d, float e, float f, float g, float h)
		{
			P1 = _mm_set_ps(d, c, b, a);
			P2 = _mm_set_ps(g, f, e, h);
		}

		/// <summary>
		/// Produce a screw motion rotating and translating by given amounts along a
		/// provided Euclidean axis.
		/// </summary>
		public Motor(float angRad, float d, Line l)
		{
			Detail.gpDL(-angRad * 0.5f, d * 0.5f, l.P1, l.P2, out var p1, out var p2);
			Detail.exp(p1, p2, out P1, out P2);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Motor(__m128 p1, __m128 p2)
		{
			P1 = p1;
			P2 = p2;
		}

		/// Load Motor data using two unaligned loads. This routine does *not*
		/// assume the data passed in this way is normalized.
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe Motor(float* input)
		{
			// Aligned and unaligned loads incur the same amount of latency and have
			// identical throughput on most modern processors
			P1 = _mm_loadu_ps(input);
			P2 = _mm_loadu_ps(input + 4);
		}

		/// <summary>
		/// Store the 8 float components into memory
		/// </summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
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

		public float Scalar => P1.GetElement(0);
		public float e12 => P1.GetElement(3);
		public float e21 => -e12;
		public float e31 => P1.GetElement(2);
		public float e13 => -e31;
		public float e23 => P1.GetElement(1);
		public float e32 => -e23;
		public float e01 => P2.GetElement(1);
		public float e10 => -e01;
		public float e02 => P2.GetElement(2);
		public float e20 => -e02;
		public float e03 => P2.GetElement(3);
		public float e30 => -e03;
		public float e0123 => P2.GetElement(0);
		public float I => e0123;

		/// <summary>
		/// Deconstructs the components of the motor <c>a + be₂₃ + ce₃₁ + de₁₂ + ee₀₁ + fe₀₂ + ge₀₃ + he₀₁₂₃</c>
		/// </summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Deconstruct(out float a, out float b, out float c, out float d, out float e, out float f, out float g, out float h)
		{
			a = Scalar;
			b = e23;
			c = e31;
			d = e12;
			e = e01;
			f = e02;
			g = e03;
			h = e0123;
		}

		/// Normalizes this Motor $m$ such that $m\widetilde{m} = 1$.
		internal static (__m128, __m128) Normalized(__m128 p1, __m128 p2)
		{
			// m = b + c where b is p1 and c is p2
			//
			// m * ~m = |b|^2 + 2(b0 c0 - b1 c1 - b2 c2 - b3 c3)e0123
			//
			// The square root is given as:
			// |b| + (b0 c0 - b1 c1 - b2 c2 - b3 c3)/|b| e0123
			//
			// The inverse of this is given by:
			// 1/|b| + (-b0 c0 + b1 c1 + b2 c2 + b3 c3)/|b|^3 e0123 = s + t e0123
			//
			// Multiplying our original Motor by this inverse will give us a
			// normalized Motor.
			__m128 b2 = Detail.dp_bc(p1, p1);
			__m128 s = Detail.rsqrt_nr1(b2);
			__m128 bc = Detail.dp_bc(_mm_xor_ps(p1, _mm_set_ss(-0f)), p2);
			__m128 t = _mm_mul_ps(_mm_mul_ps(bc, Detail.rcp_nr1(b2)), s);

			// (s + t e0123) * Motor =
			//
			// s b0 +
			// s b1 e23 +
			// s b2 e31 +
			// s b3 e12 +
			// (s c0 + t b0) e0123 +
			// (s c1 - t b1) e01 +
			// (s c2 - t b2) e02 +
			// (s c3 - t b3) e03

			__m128 tmp = _mm_mul_ps(p2, s);
			p2 = _mm_sub_ps(tmp, _mm_xor_ps(_mm_mul_ps(p1, t), _mm_set_ss(-0f)));
			p1 = _mm_mul_ps(p1, s);

			return (p1, p2);
		}

		/// Return a normalized copy of this Motor.
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Motor Normalized()
		{
			var (p1, p2) = Normalized(P1, P2);
			return new Motor(p1, p2);
		}

		internal static (__m128, __m128) Inverse(__m128 p1, __m128 p2)
		{
			// s, t computed as in the normalization
			__m128 b2 = Detail.dp_bc(p1, p1);
			__m128 s = Detail.rsqrt_nr1(b2);
			__m128 bc = Detail.dp_bc(_mm_xor_ps(p1, _mm_set_ss(-0f)), p2);
			__m128 b2Inv = Detail.rcp_nr1(b2);
			__m128 t = _mm_mul_ps(_mm_mul_ps(bc, b2Inv), s);
			__m128 neg = _mm_set_ps(-0f, -0f, -0f, 0f);

			// p1 * (s + t e0123)^2 = (s * p1 - t P1perp) * (s + t e0123)
			// = s^2 p1 - s t P1perp - s t P1perp
			// = s^2 p1 - 2 s t P1perp
			// (the scalar component above needs to be negated)
			// p2 * (s + t e0123)^2 = s^2 p2 NOTE: s^2 = b2_inv
			__m128 st = _mm_mul_ps(s, t);
			st = _mm_mul_ps(p1, st);
			p2 = _mm_sub_ps(_mm_mul_ps(p2, b2Inv),
								  _mm_xor_ps(_mm_add_ps(st, st), _mm_set_ss(-0f)));
			p2 = _mm_xor_ps(p2, neg);

			p1 = _mm_xor_ps(_mm_mul_ps(p1, b2Inv), neg);

			return (p1, p2);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Motor Inverse()
		{
			var (p1, p2) = Inverse(P1, P2);
			return new Motor(p1, p2);
		}

		/// Constrains the Motor to traverse the shortest arc
		internal static (__m128, __m128) Constrained(__m128 p1, __m128 p2)
		{
			__m128 mask = KLN_SWIZZLE(_mm_and_ps(p1, _mm_set_ss(-0f)), 0, 0, 0, 0);
			p1 = _mm_xor_ps(mask, p1);
			p2 = _mm_xor_ps(mask, p2);
			return (p1, p2);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Motor Constrained()
		{
			var (p1, p2) = Constrained(P1, P2);
			return new Motor(p1, p2);
		}

		/// Bitwise comparison
		public bool Equals(Motor other)
		{
			__m128 p1Eq = _mm_cmpeq_ps(P1, other.P1);
			__m128 p2Eq = _mm_cmpeq_ps(P2, other.P2);
			__m128 eq = _mm_and_ps(p1Eq, p2Eq);
			return _mm_movemask_ps(eq) == 0xf;
		}

		public bool Equals(Motor other, float epsilon)
		{
			__m128 eps = _mm_set1_ps(epsilon);
			__m128 neg = _mm_set1_ps(-0f);
			__m128 cmp1
				 = _mm_cmplt_ps(_mm_andnot_ps(neg, _mm_sub_ps(P1, other.P1)), eps);
			__m128 cmp2
				 = _mm_cmplt_ps(_mm_andnot_ps(neg, _mm_sub_ps(P2, other.P2)), eps);
			__m128 cmp = _mm_and_ps(cmp1, cmp2);
			return _mm_movemask_ps(cmp) == 0xf;
		}

		public static implicit operator Motor(Rotor r)
		{
			return new Motor(r.P1, _mm_setzero_ps());
		}

		public static implicit operator Motor(Translator t)
		{
			return new Motor(_mm_set_ss(1f), t.P2);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Motor operator +(Motor a, Motor b)
		{
			var p1 = _mm_add_ps(a.P1, b.P1);
			var p2 = _mm_add_ps(a.P2, b.P2);
			return new Motor(p1, p2);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Motor operator -(Motor a, Motor b)
		{
			var p1 = _mm_sub_ps(a.P1, b.P1);
			var p2 = _mm_sub_ps(a.P2, b.P2);
			return new Motor(p1, p2);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Motor operator *(Motor l, float s)
		{
			__m128 vs = _mm_set1_ps(s);
			var p1 = _mm_mul_ps(l.P1, vs);
			var p2 = _mm_mul_ps(l.P2, vs);
			return new Motor(p1, p2);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Motor operator *(float s, Motor l)
		{
			return l * s;
		}

		public static Motor operator /(Motor r, float s)
		{
			__m128 vs = Detail.rcp_nr1(_mm_set1_ps(s));
			var p1 = _mm_mul_ps(r.P1, vs);
			var p2 = _mm_mul_ps(r.P2, vs);
			return new Motor(p1, p2);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Motor operator -(Motor m)
		{
			__m128 flip = _mm_set1_ps(-0f);
			return new Motor(_mm_xor_ps(m.P1, flip), _mm_xor_ps(m.P2, flip));
		}

		/// <summary>
		/// Reversion operator
		/// </summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Motor operator ~(Motor m)
		{
			__m128 flip = _mm_set_ps(-0f, -0f, -0f, 0f);
			return new Motor(_mm_xor_ps(m.P1, flip), _mm_xor_ps(m.P2, flip));
		}

		/// <summary>
		/// Formats the motor as <c>a + be₂₃ + ce₃₁ + de₁₂ + ee₀₁ + fe₀₂ + ge₀₃ + he₀₁₂₃</c>
		/// Elements with zero components are dropped.
		/// </summary>
		public override string ToString()
		{
			var (a, b, c, d, e, f, g, h) = this;

			return new StringBuilder(64)
				.AppendScalar(a)
				.AppendElement(b, "e₂₃")
				.AppendElement(c, "e₃₁")
				.AppendElement(d, "e₁₂")
				.AppendElement(e, "e₀₁")
				.AppendElement(f, "e₀₂")
				.AppendElement(g, "e₀₃")
				.AppendElement(h, "e₀₁₂₃")
				.ZeroWhenEmpty();
		}
	}
}
#if false

namespace kln
{
    Motor& operator=(rotor r) 
    {
        P1 = r.P1;
        P2 = _mm_setzero_ps();
        return *this;
    }

    Motor& operator=(translator t) 
    {
        P1 = _mm_setzero_ps();
        P2 = t.P2;
        return *this;
    }

    /// Convert this Motor to a 3x4 column-major matrix representing this
    /// Motor's action as a Linear transformation. The Motor must be normalized
    /// for this conversion to produce well-defined results, but is more
    /// efficient than a 4x4 matrix conversion.
    public mat3x4 as_mat3x4() 
    {
        mat3x4 out;
        mat4x4_12<true, true>(P1, &P2, out.cols);
        return out;
    }

    /// Convert this Motor to a 4x4 column-major matrix representing this
    /// Motor's action as a Linear transformation.
    public mat4x4 as_mat4x4() 
    {
        mat4x4 out;
        mat4x4_12<true, false>(P1, &P2, out.cols);
        return out;
    }

    /// Conjugates a plane $p$ with this Motor and returns the result
    /// $mp\widetilde{m}$.
    public plane operator()(plane const& p) 
    {
        plane out;
        Detail.sw012<false, true>(&p.p0_, P1, &P2, &out.p0_);
        return out;
    }

    /// Conjugates an array of planes with this Motor in the input array and
    /// stores the result in the output array. Aliasing is only permitted when
    /// `in == out` (in place Motor application).
    ///
    /// !!! tip
    ///
    ///     When applying a Motor to a list of tightly packed planes, this
    ///     routine will be *significantly faster* than applying the Motor to
    ///     each plane individually.
    void operator()(plane* in, plane* out, size_t count) 
    {
        Detail.sw012<true, true>(&in->p0_, P1, &P2, &out->p0_, count);
    }

    /// Conjugates a Line $\ell$ with this Motor and returns the result
    /// $m\ell \widetilde{m}$.
    public Line operator()(Line const& l) 
    {
        Line out;
        Detail.swMM<false, true, true>(&l.P1, P1, &P2, &out.P1);
        return out;
    }

    /// Conjugates an array of Lines with this Motor in the input array and
    /// stores the result in the output array. Aliasing is only permitted when
    /// `in == out` (in place Motor application).
    ///
    /// !!! tip
    ///
    ///     When applying a Motor to a list of tightly packed Lines, this
    ///     routine will be *significantly faster* than applying the Motor to
    ///     each Line individually.
    void operator()(Line* in, Line* out, size_t count) 
    {
        Detail.swMM<true, true, true>(&in->P1, P1, &P2, &out->P1, count);
    }

    /// Conjugates a point $p$ with this Motor and returns the result
    /// $mp\widetilde{m}$.
    public point operator()(point const& p) 
    {
        point out;
        Detail.sw312<false, true>(&p.p3_, P1, &P2, &out.p3_);
        return out;
    }

    /// Conjugates an array of points with this Motor in the input array and
    /// stores the result in the output array. Aliasing is only permitted when
    /// `in == out` (in place Motor application).
    ///
    /// !!! tip
    ///
    ///     When applying a Motor to a list of tightly packed points, this
    ///     routine will be *significantly faster* than applying the Motor to
    ///     each point individually.
    void operator()(point* in, point* out, size_t count) 
    {
        Detail.sw312<true, true>(&in->p3_, P1, &P2, &out->p3_, count);
    }

    /// Conjugates the origin $O$ with this Motor and returns the result
    /// $mO\widetilde{m}$.
    public point operator()(origin) 
    {
        point out;
        out.p3_ = Detail.swo12(P1, P2);
        return out;
    }

    /// Conjugates a direction $d$ with this Motor and returns the result
    /// $md\widetilde{m}$.
    ///
    /// The cost of this operation is the same as the application of a rotor due
    /// to the translational invariance of directions (points at infinity).
    public direction operator()(direction const& d) 
    {
        direction out;
        Detail.sw312<false, false>(&d.p3_, P1, nullptr, &out.p3_);
        return out;
    }

    /// Conjugates an array of directions with this Motor in the input array and
    /// stores the result in the output array. Aliasing is only permitted when
    /// `in == out` (in place Motor application).
    ///
    /// The cost of this operation is the same as the application of a rotor due
    /// to the translational invariance of directions (points at infinity).
    ///
    /// !!! tip
    ///
    ///     When applying a Motor to a list of tightly packed directions, this
    ///     routine will be *significantly faster* than applying the Motor to
    ///     each direction individually.
    void operator()(direction* in,
                                 direction* out,
                                 size_t count) 
    {
        Detail.sw312<true, false>(&in->p3_, P1, nullptr, &out->p3_, count);
    }

};

} // namespace kln
  /// @}
#endif
