using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using static KleinSharp.Math;

namespace KleinSharp.Perf
{
	class Program
	{
		[MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.AggressiveOptimization)]
		static (float, float, float) Deconstruct()
		{
			var r = new Random();
			var p = new Point(r.Next(-10, 10), r.Next(-10, 10), r.Next(-10, 10));

			var sx = 0f;
			var sy = 0f;
			var sz = 0f;

			for (long i = 0; i < long.MaxValue; ++i)
			{
				var (x, y, z) = p;
				sx += x;
				sy += y;
				sz += z;
			}

			return (sx, sy, sz);
		}

		[MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.AggressiveOptimization)]
		static Line AddConstElements()
		{
			Line line = default;

			for (long i = 0; i < long.MaxValue; ++i)
			{
				// .NET Core 3.1 on x64 7700K doesn't seem to fold all these constant expression yet.
				line = -e01 + 2 * e02 - 3 * e03 + 4 * e12 - 5 * e23 + 6 * e31;
			}

			return line;
		}

		[MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.AggressiveOptimization)]
		static Line CreateLineElement()
		{
			Line l = default;

			for (long i = 0; i < long.MaxValue; ++i)
			{
				l = Line(-1, 2, -3, 4, -5, 6);
			}

			return l;
		}

		static void AttachDebuggerOnKey()
		{
			Console.ReadKey();
			Console.WriteLine("Breaking...");
			Debugger.Break();
		}

		static void Main(string[] args)
		{
			Console.WriteLine($"Current process id = {Process.GetCurrentProcess().Id}");

			var thread = new Thread(AttachDebuggerOnKey);
			thread.Start();

			// Console.Title = Deconstruct().ToString();
			// Console.Title = AddConstElements().ToString();
			Console.Title = CreateLineElement().ToString();
		}
	}
}
