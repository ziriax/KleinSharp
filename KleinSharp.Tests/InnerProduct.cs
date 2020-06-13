using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KleinSharp.Tests
{
	[TestClass]
	public class InnerProduct
	{
		public const float Epsilon = 1e-6f;

		[TestMethod]
		public void PlanePlane()
		{
			// d*e_0 + a*e_1 + b*e_2 + c*e_3
			var p1 = new Plane(1f, 2f, 3f, 4f);
			var p2 = new Plane(2f, 3f, -1f, -2f);
			float p12 = p1 | p2;
			Assert.AreEqual(5f, p12);
		}

		[TestMethod]
		public void PlaneLine()
		{
			// d*e_0 + a*e_1 + b*e_2 + c*e_3
			var p1 = new Plane(1f, 2f, 3f, 4f);

			// a*e01 + b*e01 + c*e02 + d*e23 + e*e31 + f*e12
			var l1 = new Line(0f, 0f, 1f, 4f, 1f, -2f);

			Plane p1l1 = p1 | l1;
			Assert.AreEqual(-3f, p1l1.e0);
			Assert.AreEqual(7f, p1l1.e1);
			Assert.AreEqual(-14f, p1l1.e2);
			Assert.AreEqual(7f, p1l1.e3);
		}

		[TestMethod]
		public void PlaneIdealLine()
		{
			// a*e_1 + b*e_2 + c*e_3 + d*e_0 
			var p1 = new Plane(1f, 2f, 3f, 4f);

			// a*e01 + b*e02 + c*e03
			var l1 = new IdealLine(-2f, 1f, 4f);

			Plane p1l1 = p1 | l1;
			Assert.AreEqual(-12f, p1l1.e0);

			Plane l1p1 = l1 | p1;
			Assert.AreEqual(12f, l1p1.e0);
		}

		[TestMethod]
		public void PlanePoint()
		{
			// d*e_0 + a*e_1 + b*e_2 + c*e_3
			var p1 = new Plane(1f, 2f, 3f, 4f);
			// x*e_032 + y*e_013 + z*e_021 + e_123
			var p2 = new Point(-2f, 1f, 4f);

			Line p1p2 = p1 | p2;
			Assert.AreEqual(-5f, p1p2.e01);
			Assert.AreEqual(10f, p1p2.e02);
			Assert.AreEqual(-5f, p1p2.e03);
			Assert.AreEqual(3f, p1p2.e12);
			Assert.AreEqual(2f, p1p2.e31);
			Assert.AreEqual(1f, p1p2.e23);
		}

		[TestMethod]
		public void LinePlane()
		{
			// d*e_0 + a*e_1 + b*e_2 + c*e_3
			var p1 = new Plane(1f, 2f, 3f, 4f);

			// a*e01 + b*e01 + c*e02 + d*e23 + e*e31 + f*e12
			var l1 = new Line(0f, 0f, 1f, 4f, 1f, -2f);

			Plane p1l1 = l1 | p1;
			Assert.AreEqual(3f, p1l1.e0);
			Assert.AreEqual(-7f, p1l1.e1);
			Assert.AreEqual(14f, p1l1.e2);
			Assert.AreEqual(-7f, p1l1.e3);
		}

		[TestMethod]
		public void LineLine()
		{
			// a*e01 + b*e01 + c*e02 + d*e23 + e*e31 + f*e12
			var l1 = new Line(1f, 0f, 0f, 3f, 2f, 1f);
			var l2 = new Line(0f, 1f, 0f, 4f, 1f, -2f);

			float l1l2 = l1 | l2;
			Assert.AreEqual(-12, l1l2);
		}

		[TestMethod]
		public void LinePoint()
		{
			// a*e01 + b*e01 + c*e02 + d*e23 + e*e31 + f*e12
			var l1 = new Line(0f, 0f, 1f, 3f, 2f, 1f);
			// x*e_032 + y*e_013 + z*e_021 + e_123
			var p2 = new Point(-2f, 1f, 4f);

			Plane l1p2 = l1 | p2;
			Assert.AreEqual(0f, l1p2.e0);
			Assert.AreEqual(-3f, l1p2.e1);
			Assert.AreEqual(-2f, l1p2.e2);
			Assert.AreEqual(-1f, l1p2.e3);
		}

		[TestMethod]
		public void PointPlane()
		{
			// x*e_032 + y*e_013 + z*e_021 + e_123
			var p1 = new Point(-2f, 1f, 4f);
			// d*e_0 + a*e_1 + b*e_2 + c*e_3
			var p2 = new Plane(1f, 2f, 3f, 4f);

			Line p1p2 = p1 | p2;
			Assert.AreEqual(-5f, p1p2.e01);
			Assert.AreEqual(10f, p1p2.e02);
			Assert.AreEqual(-5f, p1p2.e03);
			Assert.AreEqual(3f, p1p2.e12);
			Assert.AreEqual(2f, p1p2.e31);
			Assert.AreEqual(1f, p1p2.e23);
		}

		[TestMethod]
		public void PointLine()
		{
			// a*e01 + b*e01 + c*e02 + d*e23 + e*e31 + f*e12
			var l1 = new Line(0f, 0f, 1f, 3f, 2f, 1f);
			// x*e_032 + y*e_013 + z*e_021 + e_123
			var p2 = new Point(-2f, 1f, 4f);
			Plane l1p2 = p2 | l1;
			Assert.AreEqual(0f, l1p2.e0);
			Assert.AreEqual(-3f, l1p2.e1);
			Assert.AreEqual(-2f, l1p2.e2);
			Assert.AreEqual(-1f, l1p2.e3);
		}

		[TestMethod]
		public void PointPoint()
		{
			// x*e_032 + y*e_013 + z*e_021 + e_123
			var p1 = new Point(1f, 2f, 3f);
			var p2 = new Point(-2f, 1f, 4f);

			float p1p2 = p1 | p2;
			Assert.AreEqual(-1f, p1p2);
		}

		[TestMethod]
		public void ProjectPointOnLine()
		{
			var p1 = new Point(2f, 2f, 0f);
			var p2 = new Point(0f, 0f, 0f);
			var p3 = new Point(1f, 0f, 0f);
			Line l = p2 & p3;
			var p4 = ((l | p1) ^ l).Normalized();

			Assert.AreEqual(1f, p4.e123, Epsilon);
			Assert.AreEqual(2f, p4.X, Epsilon);
			Assert.AreEqual(0f, p4.Y, Epsilon);
			Assert.AreEqual(0f, p4.Z, Epsilon);
		}
	}
}