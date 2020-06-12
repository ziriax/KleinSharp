using System;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;
using __m128 = System.Runtime.Intrinsics.Vector128<float>;
using static KleinSharp.Simd;

namespace KleinSharp
{
	/// <summary>
	/// <p>In projective geometry, <b>planes</b> are the fundamental element through which all
	/// other entities are constructed.</p>
	/// <br/>
	/// <p>Lines are the meet of two planes, and points are the meet of three planes (equivalently, a line and a plane).</p>
	/// <br/>
	/// <p>The Plane multivector in PGA looks like <c>d e₀ + a e₁ + b e₂ + c e₃</c>
	/// </p>
	/// <br/>
	/// where <c>e₁, e₂, e₃ </c>are the basis Euclidean YZ, ZX and XY planes, and e₀ is the ideal plane (aka the plane "at infinity")
	/// <br/>
	/// <p>
	/// <br/>
	/// <br/>
	/// Points that reside on the plane satisfy the familiar equation:
	/// <br/>
	/// <br/>
	/// d + a x + b y + c z = 0
	/// <br/>
	/// </p>
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	public struct Plane : IEquatable<Plane>
	{
		public readonly __m128 P0;

		public Plane(__m128 p0)
		{
			P0 = p0;
		}

		/// <summary>
		/// The constructor performs the rearrangement so the Plane
		/// can be specified in the familiar form: ax + by + cz + d
		/// </summary>
		public Plane(float a, float b, float c, float d)
		{
			P0 = _mm_set_ps(c, b, a, d);
		}

		/// <summary>
		/// Data should point to four floats with memory layout `(d, a, b, c)` where
		/// `d` occupies the lowest address in memory.
		/// </summary>
		public unsafe Plane(float* data)
		{
			P0 = _mm_loadu_ps(data);
		}

		public unsafe Plane(Span<float> data)
		{
			if (data.Length < 4)
				throw new ArgumentOutOfRangeException(nameof(data));

			fixed (float* ptr = data)
			{
				P0 = _mm_loadu_ps(ptr);
			}
		}

		/// <summary>
		/// Normalize this Plane $p$ such that $p \cdot p = 1$.
		///
		/// In order to compute the cosine of the angle between Planes via the
		/// inner product operator `|`, the Planes must be normalized. Producing a
		/// normalized rotor between two Planes with the geometric product `*` also
		/// requires that the Planes are normalized.
		/// 
		/// Return a normalized copy of this Plane.
		/// </summary>
		public Plane Normalized()
		{
			__m128 invNorm = Detail.rsqrt_nr1(Detail.hi_dp_bc(P0, P0));
			invNorm = Sse41.IsSupported
				? _mm_blend_ps(invNorm, _mm_set_ss(1f), 1)
				: _mm_add_ps(invNorm, _mm_set_ss(1f));
			return new Plane(_mm_mul_ps(invNorm, P0));
		}

		/// <summary>
		/// Compute the Plane norm, which is often used to compute distances
		/// between points and lines.
		///
		/// Given a normalized point $P$ and normalized line $\ell$, the Plane
		/// $P\vee\ell$ containing both $\ell$ and $P$ will have a norm equivalent
		/// to the distance between $P$ and $\ell$.
		/// </summary>
		public float Norm()
		{
			_mm_store_ss(out var norm, Detail.sqrt_nr1(Detail.hi_dp(P0, P0)));
			return norm;
		}

		public Plane Inverse()
		{
			__m128 p0 = P0;
			__m128 invNorm = Detail.rsqrt_nr1(Detail.hi_dp_bc(p0, p0));
			p0 = _mm_mul_ps(invNorm, p0);
			p0 = _mm_mul_ps(invNorm, p0);
			return new Plane(p0);
		}

		public bool Equals(Plane other)
		{
			return _mm_movemask_ps(_mm_cmpeq_ps(P0, other.P0)) == 0b1111;
		}

		public bool Equals(Plane other, float epsilon)
		{
			__m128 eps = _mm_set1_ps(epsilon);
			__m128 cmp = _mm_cmplt_ps(
				_mm_andnot_ps(_mm_set1_ps(-0f), _mm_sub_ps(P0, other.P0)), eps);
			return _mm_movemask_ps(cmp) != 0b1111;
		}

		/// <summary>
		/// Reflect another Plane $p_2$ through this Plane $p_1$. The operation
		/// performed via this method is an optimized routine equivalent to
		/// the expression $p_1 p_2 p_1$.
		/// </summary>
		public Plane Reflect(Plane p)
		{
			Detail.sw00(P0, p.P0, out var p0);
			return new Plane(p0);
		}

		/// <summary>
		/// <p>Reflect line <i>L</i> through this plane <b>p</b>.</p>
		/// <p>This an optimized routine equivalent to the expression <b>p</b> <i>L</i> <b>p</b></p>
		/// </summary>
		public Line Reflect(in Line l)
		{
			Detail.sw10(P0, l.P1, out var p1, out var p2);
			p2 = _mm_add_ps(p2, Detail.sw20(P0, l.P2));
			return new Line(p1, p2);
		}

		/// <summary>
		/// Reflect the point $Q$ through this plane $p$.
		///
		/// The operation performed via this index operator, e.g. p[Q],
		/// is an optimized routine equivalent to the expression $p Q p$.
		/// </summary>
		public Point Reflect(Point p) => new Point(Detail.sw30(P0, p.P3));

		/// <summary>
		/// Same as <see cref="Reflect(Plane)"/>
		/// </summary>
		public Plane this[in Plane p] => Reflect(p);

		/// <summary>
		/// Same as <see cref="Reflect(Line)"/>
		/// </summary>
		public Line this[in Line l] => Reflect(l);

		/// <summary>
		/// Same as <see cref="Reflect(Point)"/>
		/// </summary>
		public Point this[Point p] => Reflect(p);

		public float X => P0.GetElement(1);
		public float E1 => X;

		public float Y => P0.GetElement(2);
		public float E2 => Y;

		public float Z => P0.GetElement(3);
		public float E3 => Z;

		public float D => P0.GetElement(0);
		public float E0 => D;

		public static Plane operator +(Plane a, Plane b)
		{
			return new Plane(_mm_add_ps(a.P0, b.P0));
		}

		public static Plane operator -(Plane a, Plane b)
		{
			return new Plane(_mm_sub_ps(a.P0, b.P0));
		}

		public static Plane operator *(Plane p, float s)
		{
			return new Plane(_mm_mul_ps(p.P0, _mm_set1_ps(s)));
		}

		public static Plane operator *(float s, Plane p)
		{
			return p * s;
		}

		public static Plane operator /(Plane p, float s)
		{
			return new Plane(_mm_mul_ps(p.P0, Detail.rcp_nr1(_mm_set1_ps(s))));
		}

		/// Unary minus (leaves displacement from origin untouched, changing orientation only)
		public static Plane operator -(Plane p)
		{
			return new Plane(_mm_xor_ps(p.P0, _mm_set_ps(-0f, -0f, -0f, 0f)));
		}

		public override bool Equals(object obj)
		{
			return obj is Plane other && Equals(other);
		}

		public override int GetHashCode()
		{
			return P0.GetHashCode();
		}

		public static bool operator ==(Plane left, Plane right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(Plane left, Plane right)
		{
			return !left.Equals(right);
		}
	}
}
