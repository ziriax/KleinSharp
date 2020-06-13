using System;
using System.Diagnostics;

namespace KleinSharp.Perf
{
	class Program
	{
		static void Main(string[] args)
		{
			var r = new Random();
			var p = new Point(r.Next(-10, 10), r.Next(-10, 10), r.Next(-10, 10));

			Console.WriteLine($"Current process id = {Process.GetCurrentProcess().Id}");

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

			Console.Title = $"{sx} {sy} {sz}";
		}
	}
}
