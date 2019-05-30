using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace onlineExam.Utilities
{
    static public class SeqGenerator
    {
        static Random random = new Random();
        public static int GenerateRandomNum(int max)
        {
            return random.Next(max);
        }
        public static List<int> GenerateRandom(int count)
        {
            // generate count random values.
            HashSet<int> candidates = new HashSet<int>();
            int i = 0;
            while (i < count)
            {
                // May strike a duplicate.
                candidates.Add(i++);
            }

            // load them in to a list.
            List<int> result = new List<int>();
            result.AddRange(candidates);

            // shuffle the results:
            i = result.Count;
            while (i > 1)
            {
                i--;
                int k = random.Next(i + 1);
                int value = result[k];
                result[k] = result[i];
                result[i] = value;
            }
            return result;
        }
    }
}