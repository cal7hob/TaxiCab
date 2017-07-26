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
    public static partial class Operations
    {
        public static string Capitalize(this string source)
        {
            return source.ToUpper();
        }

        public static string[] SplitIntoIndividualElements(string source)
        {
            string[] stringCollection = new string[source.Length];

            for (int i = 0; i < stringCollection.Length; i++)
            {
                stringCollection[i] = source[i].ToString();
            }

            return stringCollection;
        }

        public static string MergeIndividualElementsIntoString(IEnumerable<string> source)
        {
            string returnString = "";

            for (int i = 0; i < source.Count(); i++)
            {
                returnString += source.ElementAt<string>(i);
            }
            return returnString;
        }

        public static List<string> ListPrefixes(this string source)
        {
            List<string> prefixes = new List<string>();

            for (int i = 0; i < source.Length; i++)
            {
                prefixes.Add(source.Substring(0, i));
            }

            return prefixes;
        }

        public static List<string> ListBiGrams(this string source)
        {
            return ListNGrams(source, 2);
        }

        public static List<string> ListTriGrams(this string source)
        {
            return ListNGrams(source, 3);
        }

        public static List<string> ListNGrams(this string source, int n)
        {
            List<string> nGrams = new List<string>();

            if (n > source.Length)
            {
                return null;
            }
            else if (n == source.Length)
            {
                nGrams.Add(source);
                return nGrams;
            }
            else
            {
                for (int i = 0; i < source.Length - n; i++)
                {
                    nGrams.Add(source.Substring(i, n));
                }

                return nGrams;
            }
        }
    }
}
