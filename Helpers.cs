using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace KleinSharp
{
	public static class Helpers
	{
		public static ReadOnlySpan<float> ToFloatSpan<T>(in T source) where T : struct
		{
			var span = MemoryMarshal.CreateReadOnlySpan(ref Unsafe.AsRef(source), 1);
			return MemoryMarshal.Cast<T, float>(span);
		}
	}
}