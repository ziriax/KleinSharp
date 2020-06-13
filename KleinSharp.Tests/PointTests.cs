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
		public void Constructs_FromFloats3()
		{
			var p1 = new Point(2f, 3f, 4f);
			Assert.AreEqual(Vector128.Create(1f, 2f, 3f, 4f), p1.P3);
		}

		[TestMethod]
		public void Constructs_FromFloats4()
		{
			var p1 = new Point(2f, 3f, 4f, 5f);
			Assert.AreEqual(Vector128.Create(5f, 2f, 3f, 4f), p1.P3);
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
		public void Deconstructs_XYZW()
		{
			var p1 = new Point(2f, 3f, 4f);
			var (x, y, z, w) = p1;
			Assert.AreEqual((1f, 2f, 3f, 4f), (w, x, y, z));
		}

		[TestMethod]
		public void Aliases_Properties()
		{
			var p = new Point(1f, 2f, 3f, 4f);

			Assert.AreEqual(1f, p.e032);
			Assert.AreEqual(1f, p.E1);
			Assert.AreEqual(1f, p.X);

			Assert.AreEqual(2f, p.e013);
			Assert.AreEqual(2f, p.E2);
			Assert.AreEqual(2f, p.Y);

			Assert.AreEqual(3f, p.e021);
			Assert.AreEqual(3f, p.E3);
			Assert.AreEqual(3f, p.Z);

			Assert.AreEqual(4f, p.e123);
			Assert.AreEqual(4f, p.E0);
			Assert.AreEqual(4f, p.W);
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

		[TestMethod]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
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

			Assert.AreEqual(2f, p1.e032);
			Assert.AreEqual(3f, p1.e013);
			Assert.AreEqual(4f, p1.e021);
			Assert.AreEqual(1f, p1.e123);
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
			Assert.AreEqual(1f + 2f, p3.X);
			Assert.AreEqual(2f + 3f, p3.Y);
			Assert.AreEqual(3f - 1f, p3.Z);
		}

		[TestMethod]
		public void Subtracts_TwoPoints()
		{
			var p1 = new Point(1f, 2f, 3f);
			var p2 = new Point(2f, 3f, -1f);
			var p3 = p1 - p2;
			Assert.AreEqual(1f - 2f, p3.X);
			Assert.AreEqual(2f - 3f, p3.Y);
			Assert.AreEqual(3f + 1f, p3.Z);
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
			var (x, y, z, w) = p2;
			Assert.AreEqual(0.5f, w, Epsilon);
			Assert.AreEqual(1f, x, Epsilon);
			Assert.AreEqual(2f, y, Epsilon);
			Assert.AreEqual(3f, z, Epsilon);
		}

		[TestMethod]
		public void Normalized_Point()
		{
			var p1 = new Point(2f, 3f, 4f) * 10;
			var p2 = p1.Normalized();
			var (x, y, z, w) = p2;
			Assert.AreEqual(1f, w, Epsilon);
			Assert.AreEqual(2f, x, Epsilon);
			Assert.AreEqual(3f, y, Epsilon);
			Assert.AreEqual(4f, z, Epsilon);
		}

		[TestMethod]
		public void Inverse_Point()
		{
			var p1 = new Point(2f, 3f, 4f) * 10;
			var p2 = p1.Inverse();
			var (x, y, z, w) = p2;
			Assert.AreEqual(1f / 10, w, Epsilon);
			Assert.AreEqual(2f / 10, x, Epsilon);
			Assert.AreEqual(3f / 10, y, Epsilon);
			Assert.AreEqual(4f / 10, z, Epsilon);
		}

		[TestMethod]
		public void Negate_Point()
		{
			var p1 = -new Point(2f, 3f, 4f);
			var (x, y, z, w) = p1;
			Assert.AreEqual(1f, w, Epsilon);
			Assert.AreEqual(-2f, x, Epsilon);
			Assert.AreEqual(-3f, y, Epsilon);
			Assert.AreEqual(-4f, z, Epsilon);
		}

		[TestMethod]
		public void Reverse_Point()
		{
			var p1 = ~new Point(2f, 3f, 4f);
			var (x, y, z, w) = p1;
			Assert.AreEqual(-1f, w, Epsilon);
			Assert.AreEqual(-2f, x, Epsilon);
			Assert.AreEqual(-3f, y, Epsilon);
			Assert.AreEqual(-4f, z, Epsilon);
		}

		[TestMethod]
		public void Convert_FromOrigin()
		{
			Point p = Point.Origin;
			var (x, y, z, w) = p;
			Assert.AreEqual(1f, w, Epsilon);
			Assert.AreEqual(0f, x, Epsilon);
			Assert.AreEqual(0f, y, Epsilon);
			Assert.AreEqual(0f, z, Epsilon);
		}

		[TestMethod]
		public void Multiply_Points()
		{
			var p1 = new Point(2f, 3f, 4f) * 10;
			var p2 = new Point(2f, 3f, 4f) / 10;
			var t = p1 * p2;
			var (x, y, z) = t;
			Assert.AreEqual(0f, x, Epsilon);
			Assert.AreEqual(0f, y, Epsilon);
			Assert.AreEqual(0f, z, Epsilon);
		}

		[TestMethod]
		public void Divide_Points()
		{
			var p1 = new Point(2f, 3f, 4f);
			var p2 = new Point(9f, 8f, 7f);

			var t = p1 / p2;

			// Klein-shell: (e123 + 2 e032 + 3 e013 + 4 e021) * (e123 + 9 e032 + 8 e013 + 7 e021)
			// => 7 e01 + -5 e02 + -3 e03
			var (x, y, z) = t;
			Assert.AreEqual(7f, x, Epsilon);
			Assert.AreEqual(5f, y, Epsilon);
			Assert.AreEqual(3f, z, Epsilon);
		}
	}
}