// ReSharper disable InconsistentNaming

using System.Runtime.CompilerServices;

namespace KleinSharp
{
	/// <summary>
	/// Import with `using static Basic` allows you to form elements by scaling and adding the various basis elements together.
	/// <br/>
	/// For example, to create a point (1,2,3), <c>use E1 + 2*E2 + 3*E3</c>
	/// <br/>
	/// It also offers constructors methods, so that you don't have to call <c>new</c> everywhere.
	/// <br/>
	/// For example, to create the X-axis line, use <c>Branch(1, 0, 0)</c>
	/// </summary>
	/// <remarks>
	/// If performance is your concern, then create elements directly using the constructor, don't add separately scaled basis elements together.
	/// </remarks>
	public static class Basis
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Plane Plane(float a, float b, float c, float d) => new Plane(a, b, c, d);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Direction Direction(float x, float y, float z) => new Direction(x, y, z);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Point Point(float x, float y, float z) => new Point(x, y, z);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Point Point(float x, float y, float z, float w) => new Point(x, y, z, w);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Branch Branch(float a, float b, float c) => new Branch(a, b, c);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IdealLine IdealLine(float a, float b, float c) => new IdealLine(a, b, c);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Line Line(float a, float b, float c, float d, float e, float f) => new Line(a, b, c, d, e, f);

		/// <summary>
		/// e1 is the plane x=0, i.e. the YZ plane
		/// </summary>
		public static readonly Plane e1 = new Plane(1, 0, 0, 0);

		/// <summary>
		/// e2 is the plane y=0, i.e. the ZX plane
		/// </summary>
		public static readonly Plane e2 = new Plane(0, 1, 0, 0);

		/// <summary>
		/// e3 is the plane z=0, i.e. the XY plane
		/// </summary>
		public static readonly Plane e3 = new Plane(0, 0, 1, 0);

		/// <summary>
		/// e0 is the plane w=0, i.e. the ideal plane (aka plane at infinity)
		/// </summary>
		public static readonly Plane e0 = new Plane(0, 0, 0, 1);

		/// <summary>
		/// E1 is the X-direction (an alias of of e032)
		/// </summary>
		public static readonly Point E1 = new Point(1, 0, 0);

		/// <summary>
		/// e032 is the X-direction (an alias of E1)
		/// </summary>
		public static readonly Point e032 = E1;

		/// <summary>
		/// E2 is the Y-direction (an alias of e013)
		/// </summary>
		public static readonly Point E2 = new Direction(0, 1, 0);

		/// <summary>
		/// e013 is the X-direction (an alias of E1)
		/// </summary>
		public static readonly Point e013 = E2;

		/// <summary>
		/// E3 is the Z-direction (an alias of e021)
		/// </summary>
		public static readonly Point E3 = new Point(0, 0, 1);

		/// <summary>
		/// e021 is the Z-direction (an alias of E3)
		/// </summary>
		public static readonly Point e021 = E3;

		/// <summary>
		/// E0 is the origin (an alias of e123)
		/// </summary>
		public static readonly Point E0 = new Point(0, 0, 0, 1);

		/// <summary>
		/// e123 is the origin (an alias of E0)
		/// </summary>
		public static readonly Point e123 = E0;

		/// <summary>
		/// e23 is the X-axis line
		/// </summary>
		public static readonly Branch e23 = new Branch(1, 0, 0);

		/// <summary>
		/// e31 is the Y-axis line
		/// </summary>
		public static readonly Branch e31 = new Branch(0, 1, 0);

		/// <summary>
		/// e12 is the Z-axis line
		/// </summary>
		public static readonly Branch e12 = new Branch(0, 0, 1);

		/// <summary>
		/// e01 is the ideal line in the plane x=0, i.e. the ideal line in the YZ plane
		/// </summary>
		public static readonly IdealLine e01 = new IdealLine(1, 0, 0);

		/// <summary>
		/// e02 is the ideal line in the plane y=0, i.e. the ideal line in the ZX plane
		/// </summary>
		public static readonly IdealLine e02 = new IdealLine(0, 1, 0);

		/// <summary>
		/// e03 is the ideal line in the plane z=0, i.e. the ideal line in the XY plane
		/// </summary>
		public static readonly IdealLine e03 = new IdealLine(0, 0, 1);
	}
}