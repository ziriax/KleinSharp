using System;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.X86;
using static KleinSharp.Simd;

// ReSharper disable InconsistentNaming
[assembly: InternalsVisibleTo("KleinSharp.Tests")]

namespace KleinSharp
{
	/// <summary>
	/// KleinSharp static helper methods and fields.
	/// 
	/// `using static KleinSharp.Math` lets you import static KleinSharp math functions (log,exp,sqrt) and basis element fields.
	///
	/// The fields allow you to form elements by scaling and adding the various basis elements together.
	/// <br/>
	/// For example, to create a point (1,2,3), <c>use E1 + 2*E2 + 3*E3</c>
	/// <br/>
	/// <br/>
	/// <p>Exponential and Logarithm</p>
	/// The group of rotations, translations, and screws (combined rotatation and
	/// translation) is _nonlinear_. This means, given say, a rotor $\mathbf{r}$,
	/// the rotor
	/// $\frac{\mathbf{r}}{2}$ _does not_ correspond to half the rotation.
	/// Similarly, for a motor $\mathbf{m}$, the motor $n \mathbf{m}$ is not $n$
	/// applications of the motor $\mathbf{m}$. One way we could achieve this is
	/// through exponentiation; for example, the motor $\mathbf{m}^3$ will perform
	/// the screw action of $\mathbf{m}$ three times. However, repeated
	/// multiplication in this fashion lacks both efficiency and numerical
	/// stability.
	/// <br/>
	/// The solution is to take the logarithm of the action which maps the action to
	/// a linear space. Using `log(A)` where `A` is one of `rotor`,
	/// `translator`, or `motor`, we can apply linear scaling to `log(A)`,
	/// and then re-exponentiate the result. Using this technique, `exp(n * log(A))`
	/// is equivalent to $\mathbf{A}^n$.
	/// <br/>
	/// </summary>
	/// <remarks>
	/// If performance is your concern, then create elements directly using the constructor, don't add separately scaled basis elements together.
	/// </remarks>
	public static class Math
	{
		public static readonly Origin origin = default;

		/// <summary>
		/// e1 is the plane x=0, i.e. the YZ plane
		/// </summary>
		public static readonly Plane e1 = new Plane(1, 0, 0, 0);

		/// <summary>
		/// e2 is the plane y=0, i.e. the ZX plane
		/// </summary>
		public static readonly Plane e2 = new Plane(0, 1, 0, 0);

		/// <summary>
		/// e3 is the plane z=0, i.e. the XY plane
		/// </summary>
		public static readonly Plane e3 = new Plane(0, 0, 1, 0);

		/// <summary>
		/// e0 is the plane w=0, i.e. the ideal plane (aka plane at infinity)
		/// </summary>
		public static readonly Plane e0 = new Plane(0, 0, 0, 1);

		/// <summary>
		/// E1 is the X-direction (an alias of of e032)
		/// </summary>
		public static readonly Point E1 = new Point(1, 0, 0);

		/// <summary>
		/// e032 is the X-direction (an alias of E1)
		/// </summary>
		public static readonly Point e032 = E1;

		/// <summary>
		/// E2 is the Y-direction (an alias of e013)
		/// </summary>
		public static readonly Point E2 = new Direction(0, 1, 0);

		/// <summary>
		/// e013 is the X-direction (an alias of E1)
		/// </summary>
		public static readonly Point e013 = E2;

		/// <summary>
		/// E3 is the Z-direction (an alias of e021)
		/// </summary>
		public static readonly Point E3 = new Point(0, 0, 1);

		/// <summary>
		/// e021 is the Z-direction (an alias of E3)
		/// </summary>
		public static readonly Point e021 = E3;

		/// <summary>
		/// E0 is the origin (an alias of e123)
		/// </summary>
		public static readonly Point E0 = new Point(0, 0, 0, 1);

		/// <summary>
		/// e123 is the origin (an alias of E0)
		/// </summary>
		public static readonly Point e123 = E0;

		/// <summary>
		/// e23 is the X-axis line
		/// </summary>
		public static readonly Branch e23 = new Branch(1, 0, 0);

		/// <summary>
		/// e31 is the Y-axis line
		/// </summary>
		public static readonly Branch e31 = new Branch(0, 1, 0);

		/// <summary>
		/// e12 is the Z-axis line
		/// </summary>
		public static readonly Branch e12 = new Branch(0, 0, 1);

		/// <summary>
		/// e01 is the ideal line in the plane x=0, i.e. the ideal line in the YZ plane
		/// </summary>
		public static readonly IdealLine e01 = new IdealLine(1, 0, 0);

		/// <summary>
		/// e02 is the ideal line in the plane y=0, i.e. the ideal line in the ZX plane
		/// </summary>
		public static readonly IdealLine e02 = new IdealLine(0, 1, 0);

		/// <summary>
		/// e03 is the ideal line in the plane z=0, i.e. the ideal line in the XY plane
		/// </summary>
		public static readonly IdealLine e03 = new IdealLine(0, 0, 1);

		/// <summary>
		/// Takes the principal branch of the logarithm of the motor, returning a
		/// bivector. Exponentiation of that bivector without any changes produces
		/// this motor again. Scaling that bivector by $\frac{1}{n}$,
		/// re-exponentiating, and taking the result to the $n$th power will also
		/// produce this motor again. The logarithm presumes that the motor is
		/// normalized.
		/// </summary>
		[MethodImpl(MethodImplOptions.AggressiveOptimization)]
		public static Line Log(Motor m)
		{
			Detail.log(m.P1, m.P2, out var p1, out var p2);
			return new Line(p1, p2);
		}

		/// <summary>
		/// Exponentiate a line to produce a motor that posesses this line
		/// as its axis. This routine will be used most often when this line is
		/// produced as the logarithm of an existing rotor, then scaled to subdivide
		/// or accelerate the motor's action. The line need not be a _simple bivector_
		/// for the operation to be well-defined.
		/// </summary>
		[MethodImpl(MethodImplOptions.AggressiveOptimization)]
		public static Motor Exp(Line l)
		{
			Detail.exp(l.P1, l.P2, out var p1, out var p2);
			return new Motor(p1, p2);

		}

		/// <summary>
		/// Compute the logarithm of the translator, producing an ideal line axis.
		/// In practice, the logarithm of a translator is simply the ideal partition
		/// (without the scalar $1$).
		/// </summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IdealLine Log(Translator t)
		{
			return new IdealLine(t.P2);
		}

		/// <summary>
		/// Exponentiate an ideal line to produce a translation.
		///
		/// The exponential of an ideal line
		/// $a \mathbf{e}_{01} + b\mathbf{e}_{02} + c\mathbf{e}_{03}$ is given as:
		///
		/// $$\exp{\left[a\ee_{01} + b\ee_{02} + c\ee_{03}\right]} = 1 +\
		/// a\ee_{01} + b\ee_{02} + c\ee_{03}$$
		/// </summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Translator Exp(IdealLine il)
		{
			return new Translator(il.P2);
		}

		/// <summary>
		/// Returns the principal branch of this rotor's logarithm. Invoking
		/// `exp` on the returned `kln::branch` maps back to this rotor.
		///
		/// Given a rotor $\cos\alpha + \sin\alpha\left[a\ee_{23} + b\ee_{31} +\
		/// c\ee_{23}\right]$, the log is computed as simply
		/// $\alpha\left[a\ee_{23} + b\ee_{31} + c\ee_{23}\right]$.
		/// This map is only well-defined if the
		/// rotor is normalized such that $a^2 + b^2 + c^2 = 1$.
		/// </summary>
		[MethodImpl(MethodImplOptions.AggressiveOptimization)]
		public static Branch Log(Rotor r)
		{
			float cos_ang = _mm_store_ss(r.P1);
			float ang = MathF.Acos(cos_ang);
			float sin_ang = MathF.Sin(ang);

			var p1 = _mm_mul_ps(r.P1, Detail.rcp_nr1(_mm_set1_ps(sin_ang)));
			p1 = _mm_mul_ps(p1, _mm_set1_ps(ang));
			p1 = Sse41.IsSupported
				? _mm_blend_ps(p1, _mm_setzero_ps(), 1)
				: _mm_and_ps(p1, _mm_castsi128_ps(_mm_set_epi32(-1, -1, -1, 0)));

			return new Branch(p1);
		}

		/// <summary>
		/// Exponentiate a branch to produce a rotor.
		/// </summary>
		[MethodImpl(MethodImplOptions.AggressiveOptimization)]
		public static Rotor Exp(Branch b)
		{
			// Compute the rotor angle
			float ang = _mm_store_ss(Detail.sqrt_nr1(Detail.hi_dp(b.P1, b.P1)));
			float cos_ang = MathF.Cos(ang);
			float sin_ang = MathF.Sin(ang) / ang;

			var p1 = _mm_mul_ps(_mm_set1_ps(sin_ang), b.P1);
			p1 = _mm_add_ps(p1, _mm_set_ps(0f, 0f, 0f, cos_ang));
			return new Rotor(p1);
		}

		/// <summary>
		/// Compute the square root of the provided rotor $r$.
		/// </summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Rotor Sqrt(Rotor r)
		{
			var p1 = _mm_add_ss(r.P1, _mm_set_ss(1f));
			return new Rotor(Rotor.Normalized(p1));
		}

		/// <summary>
		/// Compute the square root of the provided branch $b$.
		/// </summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Rotor Sqrt(Branch b)
		{
			var p1 = _mm_add_ss(b.P1, _mm_set_ss(1f));
			return new Rotor(Rotor.Normalized(p1));
		}

		/// <summary>
		/// Compute the square root of the provided translator $t$.
		/// </summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Translator Sqrt(Translator t)
		{
			return t * 0.5f;
		}

		/// <summary>
		/// Compute the square root of the provided motor $m$.
		/// </summary>
		[MethodImpl(MethodImplOptions.AggressiveOptimization)]
		public static Motor Sqrt(Motor m)
		{
			var (p1, p2) = Motor.Normalized(_mm_add_ss(m.P1, _mm_set_ss(1f)), m.P2);
			return new Motor(p1, p2);
		}
	}
}