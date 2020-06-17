using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace KleinSharp.ConstSwizzleTool
{
	class Program
	{
		static void Main(string[] args)
		{
			static string Replacer(Match m)
			{
				var g = m.Groups;

				var p = g[1];
				var a = int.Parse(g[2].Value);
				var b = int.Parse(g[3].Value);
				var c = int.Parse(g[4].Value);
				var d = int.Parse(g[5].Value);
				var mask = (a << 6) | (b << 4) | (c << 2) | d;
				return $"_mm_swizzle_ps({g[1]}, {mask} /* {a}, {b}, {c}, {d} */";
			}

			var filePaths = Directory.GetFiles(@"c:\dev\KleinSharp\KleinSharp\Source", "*.cs", SearchOption.AllDirectories);

			var regex = new Regex(@"KLN_SWIZZLE\((\S+),\s*(\d),\s*(\d),\s*(\d),\s*(\d)");
			foreach (var filePath in filePaths)
			{
				var text = File.ReadAllText(filePath);
				text = regex.Replace(text, Replacer);
				File.WriteAllText(filePath, text, Encoding.UTF8);
			}
		}
	}
}