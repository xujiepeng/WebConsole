using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebConsole.Models.Common.Entities;

namespace WebConsole.Models.Common
{
    public sealed partial class AnalysisEngine
    {
        /// <summary>
        /// Calculates Exponential Moving Average (EMA) indicator
        /// 移动平均指数
        /// </summary>
        /// <param name="input">Input signal</param>
        /// <param name="period">Number of periods</param>
        /// <returns>Object containing operation results</returns>
        public static EMAResult EMA(IEnumerable<double> input, int period)
        {
            var returnValues = new List<double>();

            double multiplier = (2.0 / (period + 1));
            double initialSMA = input.Take(period).Average();

            returnValues.Add(initialSMA);

            var copyInputValues = input.ToList();

            for (int i = period; i < copyInputValues.Count; i++)
            {
                var resultValue = (copyInputValues[i] - returnValues.Last()) * multiplier + returnValues.Last();

                returnValues.Add(resultValue);
            }

            var result = new EMAResult()
            {
                Values = returnValues,
                StartIndexOffset = period - 1
            };

            return result;
        }

        /// <summary>
        /// Calculates Exponential Moving Average (EMA) indicator
        /// </summary>
        /// <param name="input">Input signal</param>
        /// <param name="period">Number of periods</param>
        /// <returns>Object containing operation results</returns>
        public static List<decimal> EMA(IEnumerable<decimal> input, int period)
        {
            var returnValues = new List<decimal>();

            decimal multiplier = (decimal)(2.0 / (period + 1));
            decimal initialSMA = input.Take(period).Average();

            returnValues.Add(initialSMA);

            var copyInputValues = input.ToList();

            for (int i = period; i < copyInputValues.Count; i++)
            {
                var resultValue = (copyInputValues[i] - returnValues.Last()) * multiplier + returnValues.Last();

                returnValues.Add(resultValue);
            }

            return returnValues;
        }
    }
}
