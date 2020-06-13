using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KleinSharp.Tests
{
	[TestClass]
	public class ExteriorProductTests
	{
		[TestMethod]
		public void PlanePlane()
		{
			// d*e_0 + a*e_1 + b*e_2 + c*e_3
			var p1 = new Plane(1f, 2f, 3f, 4f);
			var p2 = new Plane(2f, 3f, -1f, -2f);
			Line p12 = p1 ^ p2;
			Assert.AreEqual(10f, p12.e01);
			Assert.AreEqual(16f, p12.e02);
			Assert.AreEqual(2f, p12.e03);
			Assert.AreEqual(-1f, p12.e12);
			Assert.AreEqual(7f, p12.e31);
			Assert.AreEqual(-11f, p12.e23);
		}

		[TestMethod]
		public void PlaneLine()
		{
			// d*e_0 + a*e_1 + b*e_2 + c*e_3
			var p1 = new Plane(1f, 2f, 3f, 4f);

			// a*e01 + b*e02 + c*e03 + d*e23 + e*e31 + f*e12
			var l1 = new Line(0f, 0f, 1f, 4f, 1f, -2f);

			Point p1l1 = p1 ^ l1;
			Assert.AreEqual(8f, p1l1.e021);
			Assert.AreEqual(-5f, p1l1.e013);
			Assert.AreEqual(-14f, p1l1.e032);
			Assert.AreEqual(0f, p1l1.e123);
		}

		[TestMethod]
		public void PlaneIdealLine()
		{
			// d*e_0 + a*e_1 + b*e_2 + c*e_3
			var p1 = new Plane(1f, 2f, 3f, 4f);

			// a*e01 + b*e02 + c*e03
			var l1 = new IdealLine(-2f, 1f, 4f);

			Point p1l1 = p1 ^ l1;
			Assert.AreEqual(5f, p1l1.e021);
			Assert.AreEqual(-10f, p1l1.e013);
			Assert.AreEqual(5f, p1l1.e032);
			Assert.AreEqual(0f, p1l1.e123);
		}

		[TestMethod]
		public void PlanePoint()
		{
			// d*e_0 + a*e_1 + b*e_2 + c*e_3
			var p1 = new Plane(1f, 2f, 3f, 4f);
			// x*e_032 + y*e_013 + z*e_021 + e_123
			var p2 = new Point(-2f, 1f, 4f);

			Dual p1p2 = p1 ^ p2;
			Assert.AreEqual(0f, p1p2.Scalar);
			Assert.AreEqual(16f, p1p2.e0123);
		}

		[TestMethod]
		public void LinePlane()
		{
			// d*e_0 + a*e_1 + b*e_2 + c*e_3
			var p1 = new Plane(1f, 2f, 3f, 4f);

			// a*e01 + b*e01 + c*e02 + d*e23 + e*e31 + f*e12
			var l1 = new Line(0f, 0f, 1f, 4f, 1f, -2f);

			Point p1l1 = l1 ^ p1;
			Assert.AreEqual(8f, p1l1.e021);
			Assert.AreEqual(-5f, p1l1.e013);
			Assert.AreEqual(-14f, p1l1.e032);
			Assert.AreEqual(0f, p1l1.e123);
		}

		[TestMethod]
		public void LineLine()
		{
			// a*e01 + b*e01 + c*e02 + d*e23 + e*e31 + f*e12
			var l1 = new Line(1f, 0f, 0f, 3f, 2f, 1f);
			var l2 = new Line(0f, 1f, 0f, 4f, 1f, -2f);

			Dual l1l2 = l1 ^ l2;
			Assert.AreEqual(6f, l1l2.e0123);
		}

		[TestMethod]
		public void LineIdealLine()
		{
			// a*e01 + b*e01 + c*e02 + d*e23 + e*e31 + f*e12
			var l1 = new Line(0f, 0f, 1f, 3f, 2f, 1f);
			// a*e01 + b*e02 + c*e03
			var l2 = new IdealLine(-2f, 1f, 4f);

			Dual l1l2 = l1 ^ l2;
			Assert.AreEqual(0f, l1l2.e0123);
			Assert.AreEqual(0f, l1l2.Scalar);

			Dual l2l1 = l2 ^ l1;
			Assert.AreEqual(0f, l2l1.e0123);
			Assert.AreEqual(0f, l2l1.Scalar);
		}

		[TestMethod]
		public void IdealLineLine()
		{
			// a*e01 + b*e01 + c*e02 + d*e23 + e*e31 + f*e12
			var l1 = new Line(1f, 0f, 0f, 3f, 2f, 1f);
			// a*e01 + b*e02 + c*e03
			var l2 = new IdealLine(-4f, -5f, -6f);

			Dual l1l2 = l1 ^ l2;
			Assert.AreEqual(-28f, l1l2.e0123);
			Assert.AreEqual(0f, l1l2.Scalar);

			Dual l2l1 = l2 ^ l1;
			Assert.AreEqual(-28f, l1l2.e0123);
			Assert.AreEqual(0f, l2l1.Scalar);
		}

		[TestMethod]
		public void IdealLinePlane()
		{
			// d*e_0 + a*e_1 + b*e_2 + c*e_3
			var p1 = new Plane(1f, 2f, 3f, 4f);

			// a*e01 + b*e02 + c*e03
			var l1 = new IdealLine(-2f, 1f, 4f);

			Point p1l1 = l1 ^ p1;
			Assert.AreEqual(5f, p1l1.e021);
			Assert.AreEqual(-10f, p1l1.e013);
			Assert.AreEqual(5f, p1l1.e032);
			Assert.AreEqual(0f, p1l1.e123);
		}

		[TestMethod]
		public void PointPlane()
		{
			// x*e_032 + y*e_013 + z*e_021 + e_123
			var p1 = new Point(-2f, 1f, 4f);
			// d*e_0 + a*e_1 + b*e_2 + c*e_3
			var p2 = new Plane(1f, 2f, 3f, 4f);

			Dual p1p2 = p1 ^ p2;
			Assert.AreEqual(-16f, p1p2.e0123);
		}

		[TestMethod]
		public void LineBranch()
		{
			// line:   a e₀₁ + b e₀₂ + c e₀₃ + d e₂₃ + e e₃₁ + f e₁₂
			// branch:                         a e₂₃ + b e₃₁ + c e₁₂

			var l1 = new Line(1f, 0f, 0f, 3f, 2f, 1f);
			var l2 = new Branch(4f, 1f, -2f);

			// Klein-shell: (e01 + 3e23 + 2e31 + e12) ^ (4 e23 + e31 - 2 e12)
			var l1l2 = l1 ^ l2;
			Assert.AreEqual(4f, l1l2.e0123);

			// Klein-shell: (4 e23 + e31 - 2 e12) ^ (e01 + 3e23 + 2e31 + e12)
			var l2l1 = l2 ^ l1;
			Assert.AreEqual(4f, l2l1.e0123);
		}

		[TestMethod]
		public void PlaneBranch()
		{
			// plane: d e₀ + a e₁ + b e₂ + c e₃
			// branch: a e₂₃ + b e₃₁ + c e₁₂
			var p1 = new Plane(1f, 2f, 3f, 4f);
			var l2 = new Branch(4f, 1f, -2f);

			// Klein-shell: (1e1 + 2e2 + 3e3 + 4e0) ^ (4 e23 + e31 - 2 e12)
			// => -8 e012 + -4 e013 + 16 e023
			var p1l2 = p1 ^ l2;
			Assert.AreEqual(-8, p1l2.e012);
			Assert.AreEqual(-4, p1l2.e013);
			Assert.AreEqual(16, p1l2.e023);
			Assert.AreEqual(0, p1l2.e123);

			var l2p1 = l2 ^ p1;
			Assert.AreEqual(-8, l2p1.e012);
			Assert.AreEqual(-4, l2p1.e013);
			Assert.AreEqual(16, l2p1.e023);
			Assert.AreEqual(0, l2p1.e123);
		}
	}
}
