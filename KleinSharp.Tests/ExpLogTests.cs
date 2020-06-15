using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static KleinSharp.Math;

namespace KleinSharp.Tests
{
	[TestClass]
	public class ExpLogTests
	{
		public const float Epsilon = 1e-6f;

		[TestMethod]
		public void Rotor_ExpLog()
		{
			var r = new Rotor(MathF.PI * 0.5f, 0.3f, -3f, 1f);
			Branch b = Log(r);
			Rotor r2 = Exp(b);

			Assert.AreEqual(r.Scalar, r2.Scalar, Epsilon);
			Assert.AreEqual(r.e12, r2.e12, Epsilon);
			Assert.AreEqual(r.e31, r2.e31, Epsilon);
			Assert.AreEqual(r.e23, r2.e23, Epsilon);
		}

		[TestMethod]
		public void Rotor_Sqrt()
		{
			var r1 = new Rotor(MathF.PI * 0.5f, 0.3f, -3f, 1f);
			Rotor r2 = Sqrt(r1);
			Rotor r3 = r2 * r2;
			Assert.AreEqual(r3.Scalar, r1.Scalar, Epsilon);
			Assert.AreEqual(r3.e12, r1.e12, Epsilon);
			Assert.AreEqual(r3.e31, r1.e31, Epsilon);
			Assert.AreEqual(r3.e23, r1.e23, Epsilon);
		}

		[TestMethod]
		public void Motor_ExpLogSqrt()
		{
			// Construct a Motor from a Translator and Rotor
			var r = new Rotor(MathF.PI * 0.5f, 0.3f, -3f, 1f);
			var t = new Translator(12f, -2f, 0.4f, 1f);
			Motor m1 = r * t;
			Line l = Log(m1);
			Motor m2 = Exp(l);

			Assert.AreEqual(m2.Scalar, m1.Scalar, Epsilon);
			Assert.AreEqual(m2.e12, m1.e12, Epsilon);
			Assert.AreEqual(m2.e31, m1.e31, Epsilon);
			Assert.AreEqual(m2.e23, m1.e23, Epsilon);
			Assert.AreEqual(m2.e01, m1.e01, Epsilon);
			Assert.AreEqual(m2.e02, m1.e02, Epsilon);
			Assert.AreEqual(m2.e03, m1.e03, Epsilon);
			Assert.AreEqual(m2.e0123, m1.e0123, Epsilon);

			Motor m3 = Sqrt(m1) * Sqrt(m1);
			Assert.AreEqual(m3.Scalar, m1.Scalar, Epsilon);
			Assert.AreEqual(m3.e12, m1.e12, Epsilon);
			Assert.AreEqual(m3.e31, m1.e31, Epsilon);
			Assert.AreEqual(m3.e23, m1.e23, Epsilon);
			Assert.AreEqual(m3.e01, m1.e01, Epsilon);
			Assert.AreEqual(m3.e02, m1.e02, Epsilon);
			Assert.AreEqual(m3.e03, m1.e03, Epsilon);
			Assert.AreEqual(m3.e0123, m1.e0123, Epsilon);
		}

		[TestMethod]
		public void Motor_Slerp()
		{
			// Construct a Motor from a Translator and Rotor
			var r = new Rotor(MathF.PI * 0.5f, 0.3f, -3f, 1f);
			var t = new Translator(12f, -2f, 0.4f, 1f);
			Motor m1 = r * t;
			Line l = Log(m1);
			// Divide the Motor action into three equal steps
			Line step = l / 3;
			Motor m_step = Exp(step);
			Motor m2 = m_step * m_step * m_step;
			Assert.AreEqual(m2.Scalar, m1.Scalar, Epsilon);
			Assert.AreEqual(m2.e12, m1.e12, Epsilon);
			Assert.AreEqual(m2.e31, m1.e31, Epsilon);
			Assert.AreEqual(m2.e23, m1.e23, Epsilon);
			Assert.AreEqual(m2.e01, m1.e01, Epsilon);
			Assert.AreEqual(m2.e02, m1.e02, Epsilon);
			Assert.AreEqual(m2.e03, m1.e03, Epsilon);
			Assert.AreEqual(m2.e0123, m1.e0123, Epsilon);
		}

		[TestMethod]
		public void Motor_Blend()
		{
			var r1 = new Rotor(MathF.PI * 0.5f, 0, 0, 1f);
			var t1 = new Translator(1f, 0f, 0f, 1f);
			Motor m1 = r1 * t1;

			var r2 = new Rotor(MathF.PI * 0.5f, 0.3f, -3f, 1f);
			var t2 = new Translator(12f, -2f, 0.4f, 1f);
			Motor m2 = r2 * t2;

			Motor motion = m2 * ~m1;
			Line step = Log(motion) / 4f;
			Motor motor_step = Exp(step);

			// Applying motor_step 0 times to m1 is m1.
			// Applying motor_step 4 times to m1 is m2 * ~m1;
			Motor result = motor_step * motor_step * motor_step * motor_step * m1;
			Assert.AreEqual(m2.Scalar, result.Scalar, Epsilon);
			Assert.AreEqual(m2.e12, result.e12, Epsilon);
			Assert.AreEqual(m2.e31, result.e31, Epsilon);
			Assert.AreEqual(m2.e23, result.e23, Epsilon);
			Assert.AreEqual(m2.e01, result.e01, Epsilon);
			Assert.AreEqual(m2.e02, result.e02, Epsilon);
			Assert.AreEqual(m2.e03, result.e03, Epsilon);
			Assert.AreEqual(m2.e0123, result.e0123, Epsilon);
		}
	}
}