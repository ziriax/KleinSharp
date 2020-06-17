using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

// ReSharper disable InconsistentNaming

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

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Dual(float p, float q)
		{
			P = p;
			Q = q;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Deconstruct(out float p, out float q)
		{
			p = P;
			q = Q;
		}

		public float Scalar => P;

		public float e0123 => Q;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Dual operator +(Dual a, Dual b)
		{
			return new Dual(a.P + b.P, a.Q + b.Q);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Dual operator -(Dual a, Dual b)
		{
			return new Dual(a.P - b.P, a.Q - b.Q);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Dual operator *(Dual a, float s)
		{
			return new Dual(a.P * s, a.Q * s);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Dual operator *(float s, Dual a)
		{
			return new Dual(a.P * s, a.Q * s);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Dual operator /(Dual a, float s)
		{
			return new Dual(a.P / s, a.Q / s);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Dual operator !(Dual d)
		{
			return new Dual(d.Q, d.P);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(Dual other)
		{
			return P.Equals(other.P) && Q.Equals(other.Q);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override bool Equals(object? obj)
		{
			return obj is Dual other && Equals(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return HashCode.Combine(P, Q);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator ==(Dual left, Dual right)
		{
			return left.Equals(right);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator !=(Dual left, Dual right)
		{
			return !left.Equals(right);
		}

		public override string ToString()
		{
			return new StringBuilder(64)
				.AppendScalar(P)
				.AppendElement(Q, "e₀₁₂₃)")
				.ZeroWhenEmpty();
		}
	}
}