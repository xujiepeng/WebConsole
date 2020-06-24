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
        /// Calculates Williams %R indicator
        /// 威廉姆%R WR指标，用于判断超买超卖
        /// </summary>
        /// <param name="highs">Signal representing price highs</param>
        /// <param name="lows">Signal representing price lows</param>
        /// <param name="closes">Signal representing closing prices</param>
        /// <param name="periods">Number of periods</param>
        /// <returns>Object containing operation results</returns>
        public static WilliamsRResult WilliamsR(IEnumerable<double> highs, IEnumerable<double> lows, IEnumerable<double> closes, int periods)
        {
            var outputValues = new List<double>();

            for (int i = periods - 1; i < highs.Count(); i++)
            {
                //计算过去periods天中收盘最高价的 最高价
                double highestHigh = highs.Skip(i - periods + 1).Take(periods).Max();
                //计算过去periods天中收盘最低价的 最低价
                double lowestLow = lows.Skip(i - periods + 1).Take(periods).Min();
                //取当天收盘价
                double currentClose = closes.ElementAt(i);
                //计算威廉R值
                double value = ((highestHigh - currentClose) / (highestHigh - lowestLow)) * -100;
                outputValues.Add(value);
            }

            var result = new WilliamsRResult()
            {
                StartIndexOffset = periods - 1,
                Values = outputValues
            };

            return result;
        }
    }
}
