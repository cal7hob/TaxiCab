/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FuzzyString
{
	public static partial class ComparisonMetrics
	{
		public static int LevenshteinDistance(this string source, string target)
		{
			if (source.Length == 0) { return target.Length; }
			if (target.Length == 0) { return source.Length; }

			int distance = 0;

			if (source[source.Length - 1] == target[target.Length - 1]) { distance = 0; }
			else { distance = 1; }

			return Math.Min(Math.Min(LevenshteinDistance(source.Substring(0, source.Length - 1), target) + 1,
									 LevenshteinDistance(source, target.Substring(0, target.Length - 1))) + 1,
									 LevenshteinDistance(source.Substring(0, source.Length - 1), target.Substring(0, target.Length - 1)) + distance);
		}

		public static double NormalizedLevenshteinDistance(this string source, string target)
		{
			int unnormalizedLevenshteinDistance = source.LevenshteinDistance(target);

			return unnormalizedLevenshteinDistance - source.LevenshteinDistanceLowerBounds(target);
		}

		public static int LevenshteinDistanceUpperBounds(this string source, string target)
		{
			// If the two strings are the same length then the Hamming Distance is the upper bounds of the Levenshtien Distance.
			if (source.Length == target.Length) { return source.HammingDistance(target); }

			// Otherwise, the upper bound is the length of the longer string.
			else if (source.Length > target.Length) { return source.Length; }
			else if (target.Length > source.Length) { return target.Length; }

			return 9999;
		}

		public static int LevenshteinDistanceLowerBounds(this string source, string target)
		{
			// If the two strings are the same length then the lower bound is zero.
			if (source.Length == target.Length) { return 0; }

			// If the two strings are different lengths then the lower bounds is the difference in length.
			else { return Math.Abs(source.Length - target.Length); }
		}

	}
}
