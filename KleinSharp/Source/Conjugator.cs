using System;
using System.Runtime.CompilerServices;

namespace KleinSharp
{
	public interface IConjugator<T> where T : unmanaged
	{
		unsafe void Conjugate(T* input, T* output, int length);

		T this[T input] { get; }
		Span<T> this[ReadOnlySpan<T> input, Span<T> output] { get; }
		T[] this[ReadOnlySpan<T> input] { get; }
	}

	internal class Conjugator
	{
		[MethodImpl(MethodImplOptions.AggressiveOptimization)]
		public static unsafe Span<TElement> Apply<TSelf, TElement>(in TSelf self, ReadOnlySpan<TElement> input, Span<TElement> buffer)
			where TSelf : IConjugator<TElement>
			where TElement : unmanaged
		{
			if (input.Length != buffer.Length)
				throw new ArgumentOutOfRangeException(nameof(input));

			fixed (TElement* src = input)
			fixed (TElement* dst = buffer)
			{
				self.Conjugate(src, dst, input.Length);
			}

			return buffer.Slice(0, input.Length);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static TElement[] Apply<TSelf, TElement>(in TSelf self, ReadOnlySpan<TElement> input)
			where TSelf : IConjugator<TElement>
			where TElement : unmanaged
		{
			var output = new TElement[input.Length];
			Apply(self, input, output);
			return output;
		}
	}
}