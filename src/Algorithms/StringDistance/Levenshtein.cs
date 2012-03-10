namespace Algorithms.StringDistance
{
    using System;

    /// <summary>
    /// Contains approximate string matching
    /// </summary>
    static class Levenshtein
    {
        /// <summary>
        /// Compute the distance between two strings.
        /// </summary>
        public static double Distance(string source, string target)
        {
            int n = source.Length;
            int m = target.Length;
            int[,] d = new int[n + 1, m + 1];

            // Step 1
            if (n == 0)
            {
                return m;
            }

            if (m == 0)
            {
                return n;
            }

            // Step 2
            for (int i = 0; i <= n; d[i, 0] = i++)
            {
            }

            for (int j = 0; j <= m; d[0, j] = j++)
            {
            }

            // Step 3
            for (int i = 1; i <= n; i++)
            {
                //Step 4
                for (int j = 1; j <= m; j++)
                {
                    // Step 5
                    int cost = (target[j - 1] == source[i - 1]) ? 0 : 1;

                    // Step 6
                    d[i, j] = Math.Min(
                        Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
                        d[i - 1, j - 1] + cost);
                }
            }
            // Step 7
            return d[n, m];
        }

        public static double Similarity(this string source, string target)
        {
            double distance = Distance(source, target);

            int longestWordLength = source.Length > target.Length ? source.Length : target.Length;

            return distance / longestWordLength;
        }
    }
}
