using System.Text;

namespace KleinSharp
{
	public static class Utils
	{
		public static StringBuilder AppendElement(this StringBuilder sb, float scale, string element)
		{
			if (scale < 0)
			{
				sb.Append(" - ");
				scale = -scale;
				// ReSharper disable once CompareOfFloatsByEqualityOperator
				if (scale != 1) sb.Append(scale);
				sb.Append(element);
				return sb;
			}

			if (scale > 0)
			{
				sb.Append(" + ");
				// ReSharper disable once CompareOfFloatsByEqualityOperator
				if (scale != 1) sb.Append(scale);
				sb.Append(element);
				return sb;
			}

			return sb;
		}
	}
}