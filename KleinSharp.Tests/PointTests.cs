using System;
using System.Runtime.Intrinsics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KleinSharp.Tests
{
	[TestClass]
	public class PointTests
	{
		public const float Epsilon = 1e-6f;

		[TestMethod]
		public void Constructs_FromFloats()
		{
			var p1 = new Point(2f, 3f, 4f);
			Assert.AreEqual(Vector128.Create(1f, 2f, 3f, 4f), p1.P3);
		}

		[TestMethod]
		public unsafe void Constructs_FromPointer()
		{
			var ps = stackalloc float[4];
			ps[0] = 1; ps[1] = 2; ps[2] = 3; ps[3] = 4;
			var p1 = new Point(ps);
			Assert.AreEqual(Vector128.Create(1f, 2f, 3f, 4f), p1.P3);
		}

		[TestMethod]
		public void Constructs_FromSpan()
		{
			var ps = new Span<float>(new[] { 1f, 2f, 3f, 4f });
			var p1 = new Point(ps);
			Assert.AreEqual(Vector128.Create(1f, 2f, 3f, 4f), p1.P3);
		}

		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		[TestMethod]
		public void Constructs_FromTinySpan_Throws()
		{
			var ps = new Span<float>(new[] { 1f, 2f, 3f });
			new Point(ps);
		}

		[TestMethod]
		public void Deconstructs_XYZ()
		{
			var p1 = new Point(2f, 3f, 4f);
			var (x, y, z) = p1;
			Assert.AreEqual((2f, 3f, 4f), (x, y, z));
		}

		[TestMethod]
		public void Deconstructs_WXYZ()
		{
			var p1 = new Point(2f, 3f, 4f);
			var (w, x, y, z) = p1;
			Assert.AreEqual((1f, 2f, 3f, 4f), (w, x, y, z));
		}

		[TestMethod]
		public void Copies_ToSpan()
		{
			var ps = new Span<float>(new float[4]);
			new Point(2, 3, 4).Store(ps);
			Assert.AreEqual(4, ps.Length);
			Assert.AreEqual(1f, ps[0]);
			Assert.AreEqual(2f, ps[1]);
			Assert.AreEqual(3f, ps[2]);
			Assert.AreEqual(4f, ps[3]);
		}

		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		[TestMethod]
		public void Copies_ToTinySpan_Throws()
		{
			var ps = new Span<float>(new[] { 1f, 2f, 3f });
			new Point(ps).Store(ps);
		}

		[TestMethod]
		public unsafe void Copies_ToPointer()
		{
			var ps = stackalloc float[4];
			var p1 = new Point(2, 3, 4);
			p1.Store(ps);

			Assert.AreEqual(1f, ps[0]);
			Assert.AreEqual(2f, ps[1]);
			Assert.AreEqual(3f, ps[2]);
			Assert.AreEqual(4f, ps[3]);
		}

		[TestMethod]
		public void Properties()
		{
			var p1 = new Point(2f, 3f, 4f);
			Assert.AreEqual(2f, p1.X);
			Assert.AreEqual(3f, p1.Y);
			Assert.AreEqual(4f, p1.Z);
			Assert.AreEqual(1f, p1.W);

			Assert.AreEqual(2f, p1.E032);
			Assert.AreEqual(3f, p1.E013);
			Assert.AreEqual(4f, p1.E021);
			Assert.AreEqual(1f, p1.E123);
		}

		[TestMethod]
		public void Formats_AllZero()
		{
			var p1 = new Point(0, 0, 0);
			Assert.AreEqual("e₁₂₃", p1.ToString());
		}

		[TestMethod]
		public void Formats_OnlyPos1_X()
		{
			var p1 = new Point(1, 0, 0);
			Assert.AreEqual("e₁₂₃ + e₀₃₂", p1.ToString());
		}

		[TestMethod]
		public void Formats_OnlyPos2_X()
		{
			var p1 = new Point(2, 0, 0);
			Assert.AreEqual("e₁₂₃ + 2e₀₃₂", p1.ToString());
		}

		[TestMethod]
		public void Formats_OnlyNeg1_X()
		{
			var p1 = new Point(-1, 0, 0);
			Assert.AreEqual("e₁₂₃ - e₀₃₂", p1.ToString());
		}

		[TestMethod]
		public void Formats_OnlyNeg2_X()
		{
			var p1 = new Point(-2, 0, 0);
			Assert.AreEqual("e₁₂₃ - 2e₀₃₂", p1.ToString());
		}

		[TestMethod]
		public void Formats_AllPos()
		{
			var p1 = new Point(2f, 3f, 4f);
			Assert.AreEqual("e₁₂₃ + 2e₀₃₂ + 3e₀₁₃ + 4e₀₂₁", p1.ToString());
		}

		[TestMethod]
		public void Formats_AllNeg()
		{
			var p1 = new Point(-2f, -3f, -4f);
			Assert.AreEqual("e₁₂₃ - 2e₀₃₂ - 3e₀₁₃ - 4e₀₂₁", p1.ToString());
		}

		[TestMethod]
		public void Equality_TwoPoints()
		{
			var p1 = new Point(2f, 3f, 4f);
			var p2 = new Point(2f, 3f, 4f);
			Assert.AreEqual(p1, p2);
			Assert.IsTrue(p1 == p2);
			Assert.IsFalse(p1 != p2);
		}

		[TestMethod]
		public void Inequality_TwoPoints()
		{
			var p1 = new Point(2f, 3f, 4f);
			var p2 = new Point(-2f, 3f, 4f);
			Assert.AreNotEqual(p1, p2);
			Assert.IsFalse(p1 == p2);
			Assert.IsTrue(p1 != p2);
		}

		[TestMethod]
		public void GetHashCode_TwoPoints()
		{
			var p1 = new Point(2f, 3f, 4f);
			var p2 = new Point(-2f, 3f, 4f);
			var h1 = p1.GetHashCode();
			var h2 = p2.GetHashCode();
			Assert.IsTrue(h1 != 0);
			Assert.IsTrue(h2 != 0);
			Assert.IsTrue(h1 != h2);
		}

		[TestMethod]
		public void Adds_TwoPoints()
		{
			var p1 = new Point(1f, 2f, 3f);
			var p2 = new Point(2f, 3f, -1f);
			var p3 = p1 + p2;
			Assert.AreEqual(p3.X, 1f + 2f);
			Assert.AreEqual(p3.Y, 2f + 3f);
			Assert.AreEqual(p3.Z, 3f - 1f);
		}

		[TestMethod]
		public void Subtracts_TwoPoints()
		{
			var p1 = new Point(1f, 2f, 3f);
			var p2 = new Point(2f, 3f, -1f);
			var p3 = p1 - p2;
			Assert.AreEqual(p3.X, 1f - 2f);
			Assert.AreEqual(p3.Y, 2f - 3f);
			Assert.AreEqual(p3.Z, 3f + 1f);
		}

		[TestMethod]
		public void Scales_OnePoint()
		{
			var p1 = new Point(2f, 3f, 4f);
			var p2 = p1 * 2;
			var p3 = 2 * p1;
			Assert.AreEqual(Vector128.Create(1f * 2, 2f * 2, 3f * 2, 4f * 2), p2.P3);
			Assert.AreEqual(Vector128.Create(1f * 2, 2f * 2, 3f * 2, 4f * 2), p3.P3);
		}

		[TestMethod]
		public void Divide_OnePoint_ByScalar()
		{
			var p1 = new Point(2f, 4f, 6f);
			var p2 = p1 / 2;
			var (w, x, y, z) = p2;
			Assert.AreEqual(w, 0.5f, Epsilon);
			Assert.AreEqual(x, 1f, Epsilon);
			Assert.AreEqual(y, 2f, Epsilon);
			Assert.AreEqual(z, 3f, Epsilon);
		}

		[TestMethod]
		public void Normalized_Point()
		{
			var p1 = new Point(2f, 3f, 4f) * 10;
			var p2 = p1.Normalized();
			var (w, x, y, z) = p2;
			Assert.AreEqual(w, 1f, Epsilon);
			Assert.AreEqual(x, 2f, Epsilon);
			Assert.AreEqual(y, 3f, Epsilon);
			Assert.AreEqual(z, 4f, Epsilon);
		}

		[TestMethod]
		public void Inverse_Point()
		{
			var p1 = new Point(2f, 3f, 4f) * 10;
			var p2 = p1.Inverse();
			var (w, x, y, z) = p2;
			Assert.AreEqual(w, 1f / 10, Epsilon);
			Assert.AreEqual(x, 2f / 10, Epsilon);
			Assert.AreEqual(y, 3f / 10, Epsilon);
			Assert.AreEqual(z, 4f / 10, Epsilon);
		}

		[TestMethod]
		public void Negate_Point()
		{
			var p1 = -new Point(2f, 3f, 4f);
			var (w, x, y, z) = p1;
			Assert.AreEqual(w, 1f, Epsilon);
			Assert.AreEqual(x, -2f, Epsilon);
			Assert.AreEqual(y, -3f, Epsilon);
			Assert.AreEqual(z, -4f, Epsilon);
		}

		[TestMethod]
		public void Reverse_Point()
		{
			var p1 = ~new Point(2f, 3f, 4f);
			var (w, x, y, z) = p1;
			Assert.AreEqual(w, -1f, Epsilon);
			Assert.AreEqual(x, -2f, Epsilon);
			Assert.AreEqual(y, -3f, Epsilon);
			Assert.AreEqual(z, -4f, Epsilon);
		}

		[TestMethod]
		public void Convert_FromOrigin()
		{
			Point p = Point.Origin;
			var (w, x, y, z) = p;
			Assert.AreEqual(w, 1f, Epsilon);
			Assert.AreEqual(x, 0f, Epsilon);
			Assert.AreEqual(y, 0f, Epsilon);
			Assert.AreEqual(z, 0f, Epsilon);
		}

		[TestMethod]
		public void Multiply_Points()
		{
			var p1 = new Point(2f, 3f, 4f) * 10;
			var p2 = new Point(2f, 3f, 4f) / 10;
			var t = p1 * p2;
			var (x, y, z) = t;
			Assert.AreEqual(x, 0f, Epsilon);
			Assert.AreEqual(y, 0f, Epsilon);
			Assert.AreEqual(z, 0f, Epsilon);
		}
	}
}