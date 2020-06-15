using System.Runtime.Intrinsics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using __m128 = System.Runtime.Intrinsics.Vector128<float>;
using static KleinSharp.Simd;

namespace KleinSharp.Tests
{
	[TestClass]
	public class TestSimd
	{
		public const float Epsilon = 1e-6f;

		[TestMethod]
		public void TestRcpNr1()
		{
			__m128 a = _mm_set_ps(4f, 3f, 2f, 1f);

			__m128 b = Detail.rcp_nr1(a);

			Assert.AreEqual(1f, b.GetElement(0), Epsilon);
			Assert.AreEqual(0.5f, b.GetElement(1), Epsilon);
			Assert.AreEqual(1f / 3f, b.GetElement(2), Epsilon);
			Assert.AreEqual(0.25f, b.GetElement(3), Epsilon);
		}
	}
}
