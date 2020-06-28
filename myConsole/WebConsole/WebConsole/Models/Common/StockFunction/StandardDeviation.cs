using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebConsole.Models.Common
{
    public sealed partial class AnalysisEngine
    {
        /// <summary>
        /// 标准差
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static double StandardDeviation(IEnumerable<double> input)
        {
            double average = input.Average();

            var helperList = new List<double>();

            for (int i = 0; i < input.Count(); i++)
            {
                helperList.Add(Math.Pow(input.ElementAt(i) - average, 2));
            }

            return Math.Sqrt((helperList.Sum() / helperList.Count()));
        }
    }
}
