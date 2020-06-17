using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using JitBuddy;

namespace KleinSharp.Disassembly
{
	class Program
	{
		static void Main(string[] args)
		{
			var skip = new HashSet<string>
			{
				"ToString"
			};

			// Disassemble all operators on all structs
			var assembly = typeof(Math).Assembly;
			var methods = assembly.GetTypes()
				.Where(t => t.IsValueType)
				.SelectMany(t => t.GetMethods(BindingFlags.Public | BindingFlags.Static | BindingFlags.CreateInstance | BindingFlags.Instance))
				.Where(m => m.DeclaringType?.Assembly == assembly)
				.Where(m => !skip.Contains(m.Name) && !m.Name.StartsWith("get_"));

			// string GetParams(MethodInfo method)
			// {
			// 	return string.Join(" ,", method.GetParameters()
			//
			// }
			using var writer = File.CreateText("dump.asm");

			foreach (var method in methods)
			{
				var methodSignature = $"# [struct {method.DeclaringType.Name}] {method}".Replace("System.Runtime.Intrinsics.Vector128`1[System.Single]", "m128").Replace("KleinSharp.", "");

				if (method.MethodImplementationFlags == 0)
				{
					Console.WriteLine($"{methodSignature} is missing [MethodImpl], skipping");
				}

				var asm = method.ToAsm();
				if (!string.IsNullOrWhiteSpace(asm))
				{
					writer.WriteLine(methodSignature);
					writer.WriteLine(asm);
					writer.WriteLine("# -----------------------------------------------------------------------------------");
					writer.WriteLine();
				}
			}
		}
	}
}
