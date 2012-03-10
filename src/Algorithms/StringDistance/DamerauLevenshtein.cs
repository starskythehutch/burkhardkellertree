namespace Algorithms.StringDistance
{
    using System;
    using System.Collections.Generic;

    public static class DamerauLevenshtein
    {
        public static double Distance(this string source, string target)
        {
            if (String.IsNullOrEmpty(source))
            {
                return String.IsNullOrEmpty(target) ? 0 : target.Length;
            }

            if (String.IsNullOrEmpty(target))
            {
                return source.Length;
            }

            int m = source.Length;
            int n = target.Length;
            var H = new int[m + 2,n + 2];

            int INF = m + n;
            H[0, 0] = INF;
            for (int i = 0; i <= m; i++)
            {
                H[i + 1, 1] = i;
                H[i + 1, 0] = INF;
            }
            for (int j = 0; j <= n; j++)
            {
                H[1, j + 1] = j;
                H[0, j + 1] = INF;
            }

            var sd = new SortedDictionary<char, int>();
            foreach (Char Letter in (source + target))
            {
                if (!sd.ContainsKey(Letter))
                {
                    sd.Add(Letter, 0);
                }
            }

            for (int i = 1; i <= m; i++)
            {
                int DB = 0;
                for (int j = 1; j <= n; j++)
                {
                    int i1 = sd[target[j - 1]];
                    int j1 = DB;

                    if (source[i - 1] == target[j - 1])
                    {
                        H[i + 1, j + 1] = H[i, j];
                        DB = j;
                    }
                    else
                    {
                        H[i + 1, j + 1] = Math.Min(H[i, j], Math.Min(H[i + 1, j], H[i, j + 1])) + 1;
                    }

                    H[i + 1, j + 1] = Math.Min(H[i + 1, j + 1], H[i1, j1] + (i - i1 - 1) + 1 + (j - j1 - 1));
                }

                sd[source[i - 1]] = i;
            }

            return H[m + 1, n + 1];
        }

        public static double Similarity(this string source, string target)
        {
            double distance = Distance(source, target);

            double longestWordLength = source.Length > target.Length ? source.Length : target.Length;

            return distance / longestWordLength;
        }
    }
}