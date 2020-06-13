using System;

namespace KleinSharp
{
	public interface IConjugator<T> where T : unmanaged
	{
		unsafe void Conjugate(T* input, T* output, int count);

		/// <summary>
		/// We abuse the indexer for doing conjugation
		/// </summary>
		T this[T item] { get; }
	}

	public static class ConjugatorExt
	{
		public static unsafe void Conjugate<TConj, TItem>(this TConj self, Span<TItem> input, Span<TItem> output)
			where TItem : unmanaged
			where TConj : struct, IConjugator<TItem>
		{
			if (input.Length != output.Length)
				throw new ArgumentOutOfRangeException();

			fixed (TItem* i = input)
			fixed (TItem* o = output)
			{
				self.Conjugate(i, o, input.Length);
			}
		}

		public static unsafe TItem Conjugate<TConj, TItem>(this TConj self, TItem item)
			where TItem : unmanaged
			where TConj : struct, IConjugator<TItem>
		{
			TItem result;
			self.Conjugate(&item, &result, 1);
			return result;
		}
	}
}