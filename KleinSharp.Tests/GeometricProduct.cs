using Microsoft.VisualStudio.TestTools.UnitTesting;
using __m128 = System.Runtime.Intrinsics.Vector128<float>;
using static KleinSharp.Simd;

namespace KleinSharp.Tests
{
	[TestClass]
	public class GeometricProduct
	{
		public const float Epsilon = 1e-6f;

		[TestMethod]
		public void Plane_Times_Plane()
		{
			// d*e_0 + a*e_1 + b*e_2 + c*e_3
			var p1 = new Plane(1f, 2f, 3f, 4f);
			var p2 = new Plane(2f, 3f, -1f, -2f);
			Motor p12 = p1 * p2;
			Assert.AreEqual(5f, p12.Scalar);
			Assert.AreEqual(-1f, p12.e12);
			Assert.AreEqual(7f, p12.e31);
			Assert.AreEqual(-11f, p12.e23);
			Assert.AreEqual(10f, p12.e01);
			Assert.AreEqual(16f, p12.e02);
			Assert.AreEqual(2f, p12.e03);
			Assert.AreEqual(0f, p12.e0123);

			Plane p3 = (p1 * p2).Sqrt()[p2];
			Assert.IsTrue(p3.Equals(p1, Epsilon));

			p1 = p1.Normalized();
			Motor m = p1 * p1;

			Assert.AreEqual(1f, m.Scalar, Epsilon);
		}

		[TestMethod]
		public void Plane_Div_Plane()
		{
			var p1 = new Plane(1f, 2f, 3f, 4f);
			Motor m = p1 / p1;
			Assert.AreEqual(1f, m.Scalar, Epsilon);
			Assert.AreEqual(0f, m.e12);
			Assert.AreEqual(0f, m.e31);
			Assert.AreEqual(0f, m.e23);
			Assert.AreEqual(0f, m.e01);
			Assert.AreEqual(0f, m.e02);
			Assert.AreEqual(0f, m.e03);
			Assert.AreEqual(0f, m.e0123);
		}

		[TestMethod]
		public void Plane_Times_Point()
		{
			// d*e_0 + a*e_1 + b*e_2 + c*e_3
			var p1 = new Plane(1f, 2f, 3f, 4f);
			// x*e_032 + y*e_013 + z*e_021 + e_123
			var p2 = new Point(-2f, 1f, 4f);

			Motor p1p2 = p1 * p2;
			Assert.AreEqual(0f, p1p2.Scalar);
			Assert.AreEqual(-5f, p1p2.e01);
			Assert.AreEqual(10f, p1p2.e02);
			Assert.AreEqual(-5f, p1p2.e03);
			Assert.AreEqual(3f, p1p2.e12);
			Assert.AreEqual(2f, p1p2.e31);
			Assert.AreEqual(1f, p1p2.e23);
			Assert.AreEqual(16f, p1p2.e0123);
		}

		[TestMethod]
		public void Line_normalization()
		{
			var l = new Line(1f, 2f, 3f, 3f, 2f, 1f).Normalized();
			Motor m = l * ~l;
			Assert.AreEqual(1f, m.Scalar, Epsilon);
			Assert.AreEqual(0f, m.e23, Epsilon);
			Assert.AreEqual(0f, m.e31, Epsilon);
			Assert.AreEqual(0f, m.e12, Epsilon);
			Assert.AreEqual(0f, m.e01, Epsilon);
			Assert.AreEqual(0f, m.e02, Epsilon);
			Assert.AreEqual(0f, m.e03, Epsilon);
			Assert.AreEqual(0f, m.e0123, Epsilon);
		}

		[TestMethod]
		public void Branch_Times_Branch()
		{
			var b1 = new Branch(2f, 1f, 3f);
			var b2 = new Branch(1f, -2f, -3f);
			Rotor r = b2 * b1;
			Assert.AreEqual(9f, r.Scalar);
			Assert.AreEqual(3f, r.e23);
			Assert.AreEqual(9f, r.e31);
			Assert.AreEqual(-5f, r.e12);

			b1 = b1.Normalized();
			b2 = b2.Normalized();
			Branch b3 = ~((b2 * b1).Sqrt())[b1];
			Assert.AreEqual(b2.X, b3.X, Epsilon);
			Assert.AreEqual(b2.Y, b3.Y, Epsilon);
			Assert.AreEqual(b2.Z, b3.Z, Epsilon);
		}

		[TestMethod]
		public void Branch_Div_Branch()
		{
			var b = new Branch(2f, 1f, 3f);
			Rotor r = b / b;
			Assert.AreEqual(1f, r.Scalar, Epsilon);
			Assert.AreEqual(0f, r.e23);
			Assert.AreEqual(0f, r.e31);
			Assert.AreEqual(0f, r.e12);
		}

		[TestMethod]
		public void Line_Times_Line()
		{
			// a*e01 + b*e02 + c*e03 + d*e23 + e*e31 + f*e12
			var l1 = new Line(1f, 0f, 0f, 3f, 2f, 1f);
			var l2 = new Line(0f, 1f, 0f, 4f, 1f, -2f);

			Motor l1l2 = l1 * l2;
			Assert.AreEqual(-12f, l1l2.Scalar);
			Assert.AreEqual(5f, l1l2.e12);
			Assert.AreEqual(-10f, l1l2.e31);
			Assert.AreEqual(5f, l1l2.e23);
			Assert.AreEqual(1f, l1l2.e01);
			Assert.AreEqual(-2f, l1l2.e02);
			Assert.AreEqual(-4f, l1l2.e03);
			Assert.AreEqual(6f, l1l2.e0123);

			l1 = l1.Normalized();
			l2 = l2.Normalized();
			Line l3 = (l1 * l2).Sqrt()[l2];
			Assert.IsTrue(l3.Equals(-l1, 0.001f));
		}

		[TestMethod]
		public void Line_Div_Line()
		{
			var l = new Line(1f, -2f, 2f, -3f, 3f, -4f);
			Motor m = l / l;
			Assert.AreEqual(1f, m.Scalar, Epsilon);
			Assert.AreEqual(0f, m.e12);
			Assert.AreEqual(0f, m.e31);
			Assert.AreEqual(0f, m.e23);
			Assert.AreEqual(0f, m.e01, Epsilon);
			Assert.AreEqual(0f, m.e02, Epsilon);
			Assert.AreEqual(0f, m.e03, Epsilon);
			Assert.AreEqual(0f, m.e0123, Epsilon);
		}

		[TestMethod]
		public void Point_Times_Plane()
		{
			// x*e_032 + y*e_013 + z*e_021 + e_123
			var p1 = new Point(-2f, 1f, 4f);
			// d*e_0 + a*e_1 + b*e_2 + c*e_3
			var p2 = new Plane(1f, 2f, 3f, 4f);

			Motor p1p2 = p1 * p2;
			Assert.AreEqual(0f, p1p2.Scalar);
			Assert.AreEqual(-5f, p1p2.e01);
			Assert.AreEqual(10f, p1p2.e02);
			Assert.AreEqual(-5f, p1p2.e03);
			Assert.AreEqual(3f, p1p2.e12);
			Assert.AreEqual(2f, p1p2.e31);
			Assert.AreEqual(1f, p1p2.e23);
			Assert.AreEqual(-16f, p1p2.e0123);
		}

		[TestMethod]
		public void Point_Times_Point()
		{
			// x*e_032 + y*e_013 + z*e_021 + e_123
			var p1 = new Point(1f, 2f, 3f);
			var p2 = new Point(-2f, 1f, 4f);

			Translator p1p2 = p1 * p2;
			Assert.AreEqual(-3f, p1p2.e01, Epsilon);
			Assert.AreEqual(-1f, p1p2.e02, Epsilon);
			Assert.AreEqual(1f, p1p2.e03, Epsilon);

			Point p3 = p1p2.Sqrt()[p2];
			Assert.AreEqual(1f, p3.X, Epsilon);
			Assert.AreEqual(2f, p3.Y, Epsilon);
			Assert.AreEqual(3f, p3.Z, Epsilon);
		}


		[TestMethod]
		public void Point_Div_Point()
		{
			var p1 = new Point(1f, 2f, 3f);
			Translator t = p1 / p1;
			Assert.AreEqual(0f, t.e01);
			Assert.AreEqual(0f, t.e02);
			Assert.AreEqual(0f, t.e03);
		}

		[TestMethod]
		public void Translator_Div_Translator()
		{
			var t1 = new Translator(3f, 1f, -2f, 3f);
			Translator t2 = t1 / t1;
			Assert.AreEqual(0f, t2.e01);
			Assert.AreEqual(0f, t2.e02);
			Assert.AreEqual(0f, t2.e03);
		}

		[TestMethod]
		public void Rotor_Times_Translator()
		{
			Rotor r = new Rotor(_mm_set_ps(1f, 0, 0, 1f));
			Translator t = new Translator(_mm_set_ps(1f, 0, 0, 0f));
			Motor m = r * t;
			Assert.AreEqual(1f, m.Scalar);
			Assert.AreEqual(0f, m.e01);
			Assert.AreEqual(0f, m.e02);
			Assert.AreEqual(1f, m.e03);
			Assert.AreEqual(0f, m.e23);
			Assert.AreEqual(0f, m.e31);
			Assert.AreEqual(1f, m.e12);
			Assert.AreEqual(1f, m.e0123);
		}

		[TestMethod]
		public void Translator_Times_Rotor()
		{
			Rotor r = new Rotor(_mm_set_ps(1f, 0, 0, 1f));
			Translator t = new Translator(_mm_set_ps(1f, 0, 0, 0f));
			Motor m = t * r;
			Assert.AreEqual(1f, m.Scalar);
			Assert.AreEqual(0f, m.e01);
			Assert.AreEqual(0f, m.e02);
			Assert.AreEqual(1f, m.e03);
			Assert.AreEqual(0f, m.e23);
			Assert.AreEqual(0f, m.e31);
			Assert.AreEqual(1f, m.e12);
			Assert.AreEqual(1f, m.e0123);
		}

		[TestMethod]
		public void Motor_Times_Rotor()
		{
			Rotor r1 = new Rotor(_mm_set_ps(1f, 2f, 3f, 4f));
			Translator t = new Translator(_mm_set_ps(3f, -2f, 1f, -3f));
			Rotor r2 = new Rotor(_mm_set_ps(-4f, 2f, -3f, 1f));
			Motor m1 = (t * r1) * r2;
			Motor m2 = t * (r1 * r2);
			Assert.AreEqual(m2, m1);
		}

		[TestMethod]
		public void Rotor_Times_Motor()
		{
			Rotor r1 = new Rotor(_mm_set_ps(1f, 2f, 3f, 4f));
			Translator t = new Translator(_mm_set_ps(3f, -2f, 1f, -3f));
			Rotor r2 = new Rotor(_mm_set_ps(-4f, 2f, -3f, 1f));
			Motor m1 = r2 * (r1 * t);
			Motor m2 = (r2 * r1) * t;
			Assert.AreEqual(m2, m1);
		}

		[TestMethod]
		public void Motor_Times_Translator()
		{
			Rotor r = new Rotor(_mm_set_ps(1f, 2f, 3f, 4f));
			Translator t1 = new Translator(_mm_set_ps(3f, -2f, 1f, -3f));
			Translator t2 = new Translator(_mm_set_ps(-4f, 2f, -3f, 1f));
			Motor m1 = (r * t1) * t2;
			Motor m2 = r * (t1 * t2);
			Assert.AreEqual(m2, m1);
		}

		[TestMethod]
		public void Translator_Times_Motor()
		{
			Rotor r = new Rotor(_mm_set_ps(1f, 2f, 3f, 4f));
			Translator t1 = new Translator(_mm_set_ps(3f, -2f, 1f, -3f));
			Translator t2 = new Translator(_mm_set_ps(-4f, 2f, -3f, 1f));
			Motor m1 = t2 * (r * t1);
			Motor m2 = (t2 * r) * t1;
			Assert.AreEqual(m2, m1);
		}

		[TestMethod]
		public void Motor_Times_Motor()
		{
			var m1 = new Motor(2, 3, 4, 5, 6, 7, 8, 9);
			var m2 = new Motor(6, 7, 8, 9, 10, 11, 12, 13);
			Motor m3 = m1 * m2;
			Assert.AreEqual(-86f, m3.Scalar);
			Assert.AreEqual(36f, m3.e23);
			Assert.AreEqual(32f, m3.e31);
			Assert.AreEqual(52f, m3.e12);
			Assert.AreEqual(-38f, m3.e01);
			Assert.AreEqual(-76f, m3.e02);
			Assert.AreEqual(-66f, m3.e03);
			Assert.AreEqual(384f, m3.e0123);
		}

		[TestMethod]
		public void Motor_Div_Motor()
		{
			var m1 = new Motor(2, 3, 4, 5, 6, 7, 8, 9);
			Motor m2 = m1 / m1;
			Assert.AreEqual(1f, m2.Scalar, Epsilon);
			Assert.AreEqual(0f, m2.e23);
			Assert.AreEqual(0f, m2.e31);
			Assert.AreEqual(0f, m2.e12);
			Assert.AreEqual(0f, m2.e01);
			Assert.AreEqual(0f, m2.e02, Epsilon);
			Assert.AreEqual(0f, m2.e03, Epsilon);
			Assert.AreEqual(0f, m2.e0123, Epsilon);
		}

	}
}