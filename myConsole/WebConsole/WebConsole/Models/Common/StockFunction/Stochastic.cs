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
        /// Calculates Stochastic indicator计算随机指标
        /// KD随机指标
        /// </summary>
        /// <param name="highs">Signal representing price highs</param>
        /// <param name="lows">Signal representing price lows</param>
        /// <param name="closes">Signal representing closing prices</param>
        /// <param name="kPeriods">Number of periods for %K line</param>
        /// <param name="dPeriods">Number of periods for %D line</param>
        /// <returns>Object containing operation results</returns>
        public static StochasticResult Stochastic(IEnumerable<double> highs, IEnumerable<double> lows, IEnumerable<double> closes, int kPeriods, int dPeriods)
        {
            int startKIndex = kPeriods - 1;
            int startDIndex = startKIndex + 2;

            var outputKLine = new List<double>();
            var outputDLine = new List<double>();

            for (int i = kPeriods - 1; i < highs.Count(); i++)
            {
                //取过去kPeriods天中收盘最高价 中的最高价
                double highestHigh = highs.Skip(i + 1 - kPeriods)
                        .Take(kPeriods)
                        .Max();
                //取过去kPeriods天中收盘最低价 中的最低价
                double lowestLow = lows.Skip(i + 1 - kPeriods)
                        .Take(kPeriods)
                        .Min();
                //取当天收盘价
                double currentClose = closes.ElementAt(i);
                //计算K线
                double k = (currentClose - lowestLow) / (highestHigh - lowestLow) * 100;
                outputKLine.Add(k);
            }
            //对计算出来的K线值，计算以dPeriods为周期的简单移动均线，作为D线
            if (outputDLine != null)
            {
                var dLineSMA = SMA(outputKLine, dPeriods);

                foreach (var item in dLineSMA.Values)
                {
                    outputDLine.Add(item);
                }
            }

            var result = new StochasticResult()
            {
                KLine = outputKLine,
                DLine = outputDLine,
                DStartIndexOffset = startDIndex,
                KStartIndexOffset = startKIndex
            };

            return result;
        }
    }
}
