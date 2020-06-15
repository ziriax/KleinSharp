using System;
using System.Runtime.Intrinsics;
using KleinSharp;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using __m128 = System.Runtime.Intrinsics.Vector128<float>;
using static KleinSharp.Simd;
using static KleinSharp.Math;

namespace Tests
{
	[TestClass]
	public class SandwichTests
	{
		public const float Epsilon = 1e-5f;

		[TestMethod]
		public void Simd_Sandwich()
		{
			__m128 a = _mm_set_ps(4f, 3f, 2f, 1f);
			__m128 b = _mm_set_ps(-1f, -2f, -3f, -4f);
			var ab = Detail.sw02(a, b);
			Assert.AreEqual(9f, ab.GetElement(0));
			Assert.AreEqual(2f, ab.GetElement(1));
			Assert.AreEqual(3f, ab.GetElement(2));
			Assert.AreEqual(4f, ab.GetElement(3));
		}

		[TestMethod]
		public void Reflect_Plane()
		{
			var p1 = new Plane(3f, 2f, 1f, -1f);
			var p2 = new Plane(1f, 2f, -1f, -3f);
			Plane p3 = p1[p2];

			Assert.AreEqual(30f, p3.e0);
			Assert.AreEqual(22f, p3.e1);
			Assert.AreEqual(-4f, p3.e2);
			Assert.AreEqual(26f, p3.e3);
		}

		[TestMethod]
		public void Reflect_Line()
		{
			var p = new Plane(3f, 2f, 1f, -1f);
			// a*e01 + b*e01 + c*e02 + d*e23 + e*e31 + f*e12
			var l1 = new Line(1f, -2f, 3f, 6f, 5f, -4f);
			Line l2 = p[l1];
			Assert.AreEqual(28f, l2.e01);
			Assert.AreEqual(-72f, l2.e02);
			Assert.AreEqual(32f, l2.e03);
			Assert.AreEqual(104f, l2.e12);
			Assert.AreEqual(26f, l2.e31);
			Assert.AreEqual(60f, l2.e23);
		}

		[TestMethod]
		public void Reflect_Point()
		{
			var p1 = new Plane(3f, 2f, 1f, -1f);
			var p2 = new Point(4f, -2f, -1f);
			Point p3 = p1[p2];
			Assert.AreEqual(-26f, p3.e021);
			Assert.AreEqual(-52f, p3.e013);
			Assert.AreEqual(20f, p3.e032);
			Assert.AreEqual(14f, p3.e123);
		}

		[TestMethod]
		public void Rotor_Line()
		{
			// Make an unnormalized Rotor to verify correctness
			var data = new[] { 1f, 4f, -3f, 2f };
			var r = Rotor.LoadNormalized(data);

			// a*e01 + b*e01 + c*e02 + d*e23 + e*e31 + f*e12
			var l1 = new Line(-1f, 2f, -3f, -6f, 5f, 4f);
			var l2 = r[l1];
			Assert.AreEqual(-110f, l2.e01);
			Assert.AreEqual(20f, l2.e02);
			Assert.AreEqual(10f, l2.e03);
			Assert.AreEqual(-240f, l2.e12);
			Assert.AreEqual(102f, l2.e31);
			Assert.AreEqual(-36f, l2.e23);
		}

		[TestMethod]
		public void Rotor_Point()
		{
			var r = Rotor.FromAngleAxis(MathF.PI * 0.5f, 0, 0, 1f);
			var p1 = new Point(1, 0, 0);
			Point p2 = r[p1];
			Assert.AreEqual(0f, p2.X);
			Assert.AreEqual(-1f, p2.Y, Epsilon);
			Assert.AreEqual(0f, p2.Z);
		}

		[TestMethod]
		public void Translator_Point()
		{
			var t = new Translator(1f, 0f, 0f, 1f);
			var p1 = new Point(1, 0, 0);
			Point p2 = t[p1];
			Assert.AreEqual(1f, p2.X);
			Assert.AreEqual(0f, p2.Y);
			Assert.AreEqual(1f, p2.Z);
		}

		[TestMethod]
		public void Translator_Line()
		{
			var data = new[] { 0f, -5f, -2f, 2f };
			Translator t = Translator.LoadNormalized(data);
			// a*e01 + b*e01 + c*e02 + d*e23 + e*e31 + f*e12
			var l1 = new Line(-1f, 2f, -3f, -6f, 5f, 4f);
			var l2 = t[l1];
			Assert.AreEqual(35f, l2.e01);
			Assert.AreEqual(-14f, l2.e02);
			Assert.AreEqual(71f, l2.e03);
			Assert.AreEqual(4f, l2.e12);
			Assert.AreEqual(5f, l2.e31);
			Assert.AreEqual(-6f, l2.e23);
		}

		[TestMethod]
		public void Construct_Motor()
		{
			var r = new Rotor(MathF.PI * 0.5f, 0, 0, 1f);
			var t = new Translator(1f, 0f, 0f, 1f);
			Motor m = r * t;
			var p1 = new Point(1, 0, 0);
			Point p2 = m[p1];
			Assert.AreEqual(0f, p2.X);
			Assert.AreEqual(-1f, p2.Y, Epsilon);
			Assert.AreEqual(1f, p2.Z, Epsilon);

			// Rotation and translation about the same axis commutes
			m = t * r;
			p2 = m[p1];
			Assert.AreEqual(0f, p2.X);
			Assert.AreEqual(-1f, p2.Y, Epsilon);
			Assert.AreEqual(1f, p2.Z, Epsilon);

			Line l = Log(m);
			Assert.AreEqual(0f, l.e23);
			Assert.AreEqual(0.7854, l.e12, Epsilon);
			Assert.AreEqual(0f, l.e31);
			Assert.AreEqual(0f, l.e01);
			Assert.AreEqual(0f, l.e02);
			Assert.AreEqual(-0.5, l.e03, Epsilon);
		}

		[TestMethod]
		public void Construct_Motor_ViaScrewAxis()
		{
			var m = new Motor(MathF.PI * 0.5f, 1f, new Line(0f, 0f, 0f, 0f, 0f, 1f));
			var p1 = new Point(1, 0, 0);
			Point p2 = m[p1];
			Assert.AreEqual(0f, p2.X, Epsilon);
			Assert.AreEqual(1f, p2.Y, Epsilon);
			Assert.AreEqual(1f, p2.Z, Epsilon);
		}

		[TestMethod]
		public void Motor_Plane()
		{
			var m = new Motor(1f, 4f, 3f, 2f, 5f, 6f, 7f, 8f);
			var p1 = new Plane(3f, 2f, 1f, -1f);
			Plane p2 = m[p1];
			Assert.AreEqual(78f, p2.X);
			Assert.AreEqual(60f, p2.Y);
			Assert.AreEqual(54f, p2.Z);
			Assert.AreEqual(358f, p2.W);
		}

		[TestMethod]
		public void Motor_Plane_Variadic()
		{
			var m = new Motor(1f, 4f, 3f, 2f, 5f, 6f, 7f, 8f);

			var ps = new[]
			{
				new Plane(3f, 2f, 1f, -1f),
				new Plane(3f, 2f, 1f, -1f)
			};

			var ps2 = m[ps];

			for (int i = 0; i != 2; ++i)
			{
				Assert.AreEqual(78f, ps2[i].X);
				Assert.AreEqual(60f, ps2[i].Y);
				Assert.AreEqual(54f, ps2[i].Z);
				Assert.AreEqual(358f, ps2[i].W);
			}
		}

		[TestMethod]
		public void Motor_Point()
		{
			var m = new Motor(1f, 4f, 3f, 2f, 5f, 6f, 7f, 8f);
			var p1 = new Point(-1f, 1f, 2f);
			Point p2 = m[p1];
			Assert.AreEqual(-12f, p2.X);
			Assert.AreEqual(-86f, p2.Y);
			Assert.AreEqual(-86f, p2.Z);
			Assert.AreEqual(30f, p2.W);
		}

		[TestMethod]
		public void Motor_Point_variadic()
		{
			var m = new Motor(1f, 4f, 3f, 2f, 5f, 6f, 7f, 8f);
			var ps = new[]
			{
				new Point( -1f, 1f, 2f),
				new Point(-1f, 1f, 2f )
			};

			var ps2 = m[ps];

			for (int i = 0; i != 2; ++i)
			{
				Assert.AreEqual(-12f, ps2[i].X);
				Assert.AreEqual(-86f, ps2[i].Y);
				Assert.AreEqual(-86f, ps2[i].Z);
				Assert.AreEqual(30f, ps2[i].W);
			}
		}

		[TestMethod]
		public void Motor_Line()
		{
			var m = new Motor(2f, 4f, 3f, -1f, -5f, -2f, 2f, -3f);
			// a*e01 + b*e01 + c*e02 + d*e23 + e*e31 + f*e12
			var l1 = new Line(-1f, 2f, -3f, -6f, 5f, 4f);
			var l2 = m[l1];
			Assert.AreEqual(6f, l2.e01);
			Assert.AreEqual(522f, l2.e02);
			Assert.AreEqual(96f, l2.e03);
			Assert.AreEqual(-214f, l2.e12);
			Assert.AreEqual(-148f, l2.e31);
			Assert.AreEqual(-40f, l2.e23);
		}

		[TestMethod]
		public void Motor_Line_variadic()
		{
			var m = new Motor(2f, 4f, 3f, -1f, -5f, -2f, 2f, -3f);
			// a*e01 + b*e01 + c*e02 + d*e23 + e*e31 + f*e12

			var ls = new[]
			{
				new Line(-1f, 2f, -3f, -6f, 5f, 4f),
				new Line(-1f, 2f, -3f, -6f, 5f, 4f)
			};

			var ls2 = m[ls, new Line[2]];

			for (int i = 0; i != 2; ++i)
			{
				Assert.AreEqual(6f, ls2[i].e01);
				Assert.AreEqual(522f, ls2[i].e02);
				Assert.AreEqual(96f, ls2[i].e03);
				Assert.AreEqual(-214f, ls2[i].e12);
				Assert.AreEqual(-148f, ls2[i].e31);
				Assert.AreEqual(-40f, ls2[i].e23);
			}
		}

		[TestMethod]
		public void Motor_Origin()
		{
			var r = new Rotor(MathF.PI * 0.5f, 0, 0, 1f);
			var t = new Translator(1f, 0f, 0f, 1f);
			Motor m = r * t;
			Point p = m[origin];

			Assert.AreEqual(0f, p.X);
			Assert.AreEqual(0f, p.Y);
			Assert.AreEqual(1f, p.Z, Epsilon);
		}

		/*
		[TestMethod]
		public void Motor_ToMatrix()
		{
			var m = Motor(1f, 4f, 3f, 2f, 5f, 6f, 7f, 8f);
			__m128 p1 = _mm_set_ps(1f, 2f, 1f, -1f);
			mat4x4 m_mat = m.as_mat4x4();
			__m128 p2 = m_mat(p1);
			float buf[4];
			_mm_storeu_ps(buf, p2);

			Assert.AreEqual(-12f, buf[0]);
			Assert.AreEqual(-86f, buf[1]);
			Assert.AreEqual(-86f, buf[2]);
			Assert.AreEqual(30f, buf[3]);
		}

		[TestMethod]
		public void Motor_ToMatrix3x4()
		{
			var m = Motor(1f, 4f, 3f, 2f, 5f, 6f, 7f, 8f);
			m.normalize();
			__m128 p1 = _mm_set_ps(1f, 2f, 1f, -1f);
			mat3x4 m_mat = m.as_mat3x4();
			__m128 p2 = m_mat(p1);
			float buf[4];
			_mm_storeu_ps(buf, p2);

			Assert.AreEqual(-12f / 30f, buf[0], Epsilon);
			Assert.AreEqual(-86f / 30f, buf[1], Epsilon);
			Assert.AreEqual(-86f / 30f, buf[2], Epsilon);
			Assert.AreEqual(1f, buf[3]);
		}
		*/

		[TestMethod]
		public void Normalize_Motor()
		{
			var m = new Motor(1f, 4f, 3f, 2f, 5f, 6f, 7f, 8f).Normalized();
			Motor norm = m * ~m;
			Assert.AreEqual(1f, norm.Scalar, Epsilon);
			Assert.AreEqual(0f, norm.e0123, Epsilon);
		}

		[TestMethod]
		public void Motor_Sqrt()
		{
			Motor m = new Motor(MathF.PI * 0.5f, 3f, new Line(3f, 1f, 2f, 4f, -2f, 1f)).Normalized();

			Motor m2 = Sqrt(m);
			m2 = m2 * m2;
			Assert.AreEqual(m2.Scalar, m.Scalar, Epsilon);
			Assert.AreEqual(m2.e01, m.e01, Epsilon);
			Assert.AreEqual(m2.e02, m.e02, Epsilon);
			Assert.AreEqual(m2.e03, m.e03, Epsilon);
			Assert.AreEqual(m2.e23, m.e23, Epsilon);
			Assert.AreEqual(m2.e31, m.e31, Epsilon);
			Assert.AreEqual(m2.e12, m.e12, Epsilon);
			Assert.AreEqual(m2.e0123, m.e0123, Epsilon);
		}

		[TestMethod]
		public void Rotor_sqrt()
		{
			var r = new Rotor(MathF.PI * 0.5f, 1, 2, 3);

			Rotor r2 = Sqrt(r);
			r2 = r2 * r2;
			Assert.AreEqual(r.Scalar, r2.Scalar, Epsilon);
			Assert.AreEqual(r.e23, r2.e23, Epsilon);
			Assert.AreEqual(r.e31, r2.e31, Epsilon);
			Assert.AreEqual(r.e12, r2.e12, Epsilon);
		}

		[TestMethod]
		public void normalize_Rotor()
		{
			Rotor r = new Rotor(_mm_set_ps(4f, -3f, 3f, 28f)).Normalized();
			Rotor norm = r * ~r;
			Assert.AreEqual(1f, norm.Scalar, Epsilon);
			Assert.AreEqual(0f, norm.e12, Epsilon);
			Assert.AreEqual(0f, norm.e31, Epsilon);
			Assert.AreEqual(0f, norm.e23, Epsilon);
		}
	}
}