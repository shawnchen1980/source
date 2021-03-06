﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace onlineExam.Utilities
{
    public static class GradeHelper
    {
        public static  int CalScore(object answers, object ranswers, object scores)
        {
            string ans = Convert.ToString(answers);
            if (string.IsNullOrEmpty(ans)) return 0;
            Char dl = '|';
            string[] ansArr = ans.Split(dl).ToArray();
            string[] ransArr = Convert.ToString(ranswers).Split(dl).ToArray();
            int[] scoresArr = Convert.ToString(scores).Split(dl).Select(x => Convert.ToInt32(x)).ToArray();
            int finalScore = scoresArr.Select((x, i) => ansArr[i] == ransArr[i] ? x : 0).Sum();
            return finalScore;

            //return 0;
        }
        public static int CalScoreRange(object answers, object ranswers, object scores,int startIndex, int length)
        {
            string ans = Convert.ToString(answers);
            if (string.IsNullOrEmpty(ans)) return 0;
            if (length <= 0) return 0;
            Char dl = '|';
            string[] ansArr = ans.Split(dl).Skip(startIndex).Take(length).ToArray();
            string[] ransArr = Convert.ToString(ranswers).Split(dl).Skip(startIndex).Take(length).ToArray();
            int[] scoresArr = Convert.ToString(scores).Split(dl).Select(x => Convert.ToInt32(x)).Skip(startIndex).Take(length).ToArray();
            int finalScore = scoresArr.Select((x, i) => ansArr[i] == ransArr[i] ? x : 0).Sum();
            return finalScore;
        }
    }
}