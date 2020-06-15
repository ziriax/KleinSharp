using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static KleinSharp.Math;

namespace KleinSharp.Tests
{
	[TestClass]
	public class BasicTests
	{
		public const float Epsilon = 1e-6f;

		[TestMethod]
		public void Add_Planes()
		{
			var p = e0 - 2 * e1 + 3 * e2 - 4 * e3;

			var (x, y, z, w) = p;

			Assert.AreEqual(-2f, x);
			Assert.AreEqual(3f, y);
			Assert.AreEqual(-4f, z);
			Assert.AreEqual(1f, w);
		}

		[TestMethod]
		public void Constructs_Plane()
		{
			var p = new Plane(-2, 3, -4, 1);

			var (x, y, z, w) = p;

			Assert.AreEqual(-2f, x);
			Assert.AreEqual(3f, y);
			Assert.AreEqual(-4f, z);
			Assert.AreEqual(1f, w);
		}

		[TestMethod]
		public void Add_Points()
		{
			var p = e0 - 2 * e1 + 3 * e2 - 4 * e3;

			var (x, y, z, w) = p;

			Assert.AreEqual(-2f, x);
			Assert.AreEqual(3f, y);
			Assert.AreEqual(-4f, z);
			Assert.AreEqual(1f, w);
		}

		[TestMethod]
		public void Construct_Point3()
		{
			var p = new Point(-2, 3, -4);

			var (x, y, z, w) = p;

			Assert.AreEqual(-2f, x);
			Assert.AreEqual(3f, y);
			Assert.AreEqual(-4f, z);
			Assert.AreEqual(1f, w);
		}

		[TestMethod]
		public void Construct_Point4()
		{
			var p = new Point(-2, 3, -4, 1);

			var (x, y, z, w) = p;

			Assert.AreEqual(-2f, x);
			Assert.AreEqual(3f, y);
			Assert.AreEqual(-4f, z);
			Assert.AreEqual(1f, w);
		}


		[TestMethod]
		public void Add_Lines()
		{
			var l = -e01 + 2 * e02 - 3 * e03 + 4 * e23 - 5 * e31 + 6 * e12;

			var (a, b, c, d, e, f) = l;

			Assert.AreEqual(-1f, a);
			Assert.AreEqual(2f, b);
			Assert.AreEqual(-3f, c);
			Assert.AreEqual(4f, d);
			Assert.AreEqual(-5f, e);
			Assert.AreEqual(6f, f);
		}

		[TestMethod]
		public void Constructs_Line()
		{
			var l = new Line(-1, 2, -3, 4, -5, 6);

			var (a, b, c, d, e, f) = l;

			Assert.AreEqual(-1f, a);
			Assert.AreEqual(2f, b);
			Assert.AreEqual(-3f, c);
			Assert.AreEqual(4f, d);
			Assert.AreEqual(-5f, e);
			Assert.AreEqual(6f, f);
		}

		[TestMethod]
		public void Add_Directions()
		{
			var p = -2 * E1 + 3 * E2 - 4 * E3;

			var (x, y, z) = p;

			Assert.AreEqual(-2f, x, Epsilon);
			Assert.AreEqual(3f, y, Epsilon);
			Assert.AreEqual(-4f, z, Epsilon);
		}

		[TestMethod]
		public void Construct_Direction()
		{
			var p = new Direction(-2, 3, -4);

			var (x, y, z) = p;

			var norm = 1f / MathF.Sqrt(2 * 2 + 3 * 3 + 4 * 4);
			Assert.AreEqual(-2f * norm, x, Epsilon);
			Assert.AreEqual(3f * norm, y, Epsilon);
			Assert.AreEqual(-4f * norm, z, Epsilon);
		}

		[TestMethod]
		public void Add_Branches()
		{
			var p = -2 * e23 + 3 * e31 - 4 * e12;

			var (x, y, z) = p;

			Assert.AreEqual(-2f, x);
			Assert.AreEqual(3f, y);
			Assert.AreEqual(-4f, z);
		}

		[TestMethod]
		public void Construct_Branch()
		{
			var p = new Branch(-2, 3, -4);

			var (x, y, z) = p;

			Assert.AreEqual(-2f, x);
			Assert.AreEqual(3f, y);
			Assert.AreEqual(-4f, z);
		}

		[TestMethod]
		public void Add_IdealLines()
		{
			var p = -2 * e01 + 3 * e02 - 4 * e03;

			var (x, y, z) = p;

			Assert.AreEqual(-2f, x);
			Assert.AreEqual(3f, y);
			Assert.AreEqual(-4f, z);
		}

		[TestMethod]
		public void Construct_IdealLine()
		{
			var p = new IdealLine(-2, 3, -4);

			var (x, y, z) = p;

			Assert.AreEqual(-2f, x);
			Assert.AreEqual(3f, y);
			Assert.AreEqual(-4f, z);
		}
	}
}