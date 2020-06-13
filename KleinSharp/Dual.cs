using System;
using System.Runtime.InteropServices;

namespace KleinSharp
{
	/// <summary>
	/// A Dual number is a multi-vector of the form <b>p + q e₀₁₂₃</b>
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	public readonly struct Dual : IEquatable<Dual>
	{
		public readonly float P;
		public readonly float Q;

		public Dual(float p, float q)
		{
			P = p;
			Q = q;
		}

		public void Deconstruct(out float p, out float q)
		{
			p = P;
			q = Q;
		}

		public float Scalar => P;
		public float E0123 => Q;

		public static Dual operator +(Dual a, Dual b)
		{
			return new Dual(a.P + b.P, a.Q + b.Q);
		}

		public static Dual operator -(Dual a, Dual b)
		{
			return new Dual(a.P - b.P, a.Q - b.Q);
		}

		public static Dual operator *(Dual a, float s)
		{
			return new Dual(a.P * s, a.Q * s);
		}

		public static Dual operator *(float s, Dual a)
		{
			return new Dual(a.P * s, a.Q * s);
		}

		public static Dual operator /(Dual a, float s)
		{
			return new Dual(a.P / s, a.Q / s);
		}

		public static Dual operator !(Dual d)
		{
			return new Dual(d.Q, d.P);
		}

		public bool Equals(Dual other)
		{
			return P.Equals(other.P) && Q.Equals(other.Q);
		}

		public override bool Equals(object obj)
		{
			return obj is Dual other && Equals(other);
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(P, Q);
		}

		public static bool operator ==(Dual left, Dual right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(Dual left, Dual right)
		{
			return !left.Equals(right);
		}

		public override string ToString()
		{
			return $"Dual({P} + {Q} e₀₁₂₃)";
		}
	}
}