using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KleinSharp.Tests
{
	[TestClass]
	public class RegressiveProduct
	{
		[TestMethod]
		public void Join_TwoPoints_PosZLine()
		{
			var p1 = new Point(0f, 0f, 0f);
			var p2 = new Point(0f, 0f, 1f);
			Line p12 = p1 & p2;
			Assert.AreEqual(1f, p12.e12);
		}

		[TestMethod]
		public void Join_TwoPoints_PosYLine()
		{
			var p1 = new Point(0f, -1f, 0f);
			var p2 = new Point(0f, 0f, 0f);
			Line p12 = p1 & p2;
			Assert.AreEqual(1f, p12.e31);
		}

		[TestMethod]
		public void Join_TwoPoints_PosXLine()
		{
			var p1 = new Point(-2f, 0f, 0f);
			var p2 = new Point(-1f, 0f, 0f);
			Line p12 = p1 & p2;
			Assert.AreEqual(1f, p12.e23);
		}

		[TestMethod]
		public void Join_ThreePoints_Plane()
		{
			var p1 = new Point(1f, 3f, 2f);
			var p2 = new Point(-1f, 5f, 2f);
			var p3 = new Point(2f, -1f, -4f);

			var p123 = p1 & p2 & p3;

			// Check that all 3 points lie on the Plane
			Assert.AreEqual(0f, p123.e1 + p123.e2 * 3f + p123.e3 * 2f + p123.e0);
			Assert.AreEqual(0f, -p123.e1 + p123.e2 * 5f + p123.e3 * 2f + p123.e0);
			Assert.AreEqual(0f, p123.e1 * 2f - p123.e2 - p123.e3 * 4f + p123.e0);
		}

   }
}