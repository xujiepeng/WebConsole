using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebConsole.Models.Common.Entities;

namespace WebConsole.Models.Common
{
    /// <summary>
    /// Provides methods for technical analysis
    /// </summary>
    public sealed partial class AnalysisEngine
    {
        /// <summary>
        /// Calculates Bollinger Bands indicator
        /// 布林带
        /// </summary>
        /// <param name="input">Input signal</param>
        /// <param name="periods">Number of periods</param>
        /// <param name="standardDeviations">Number of standard deviations</param>
        /// <param name="calculateBandwidth">Determines whether the bandwidth line should be calculated</param>
        /// <param name="calculatePercentB">Determines whether the percent B line should be calculated</param>
        /// <returns>Object containing operation results</returns>
        public static BollingerBandsResult BollingerBands(IEnumerable<double> input, int periods, int standardDeviations, bool calculateBandwidth, bool calculatePercentB)
        {
            List<double> upperBand = new List<double>();
            List<double> lowerBand = new List<double>();
            List<double> middleBand = null;
            List<double> bandwidth = null;
            List<double> percentB = null;
            //是否计算布林带宽度
            if (calculateBandwidth)
            {
                bandwidth = new List<double>();
            }
            //是否计算价格在布林带中位置
            if (calculatePercentB)
            {
                percentB = new List<double>();
            }
            //20天简单移动均线作为布林带中线
            var middleBandSMA = SMA(input, periods);
            middleBand = middleBandSMA.Values;

            var stdevList = new List<double>();
            var inputHelperList = input.ToList();
            //计算过去20天 的标准差
            for (int i = 0; i < input.Count() - periods + 1; i++)
            {
                stdevList.Add(AnalysisEngine.StandardDeviation(inputHelperList.GetRange(i, periods)));
            }

            for (int i = 0; i < middleBand.Count; i++)
            {
                //获取当天布林带中线的值
                var middleValue = middleBand.ElementAt(i);
                //获取当天的标准差
                var stdev = stdevList.ElementAt(i);
                //上轨 = 20天简单移动均值 + standardDeviations倍的标准差
                var upperBandValue = middleValue + stdev * standardDeviations;
                //上轨 = 20天简单移动均值 - standardDeviations倍的标准差
                var lowerBandValue = middleValue - stdev * standardDeviations;
                upperBand.Add(upperBandValue);
                lowerBand.Add(lowerBandValue);
                //计算布林带带宽
                if (bandwidth != null)
                {
                    bandwidth.Add(upperBandValue - lowerBandValue);
                }
                //计算价格在布林带中位置
                if (percentB != null)
                {
                    var price = input.ElementAt(i + periods - 1);
                    percentB.Add((price - lowerBandValue) / (upperBandValue - lowerBandValue));
                }
            }

            var result = new BollingerBandsResult()
            {
                Bandwidth = bandwidth,
                LowerBand = lowerBand,
                MiddleBand = middleBand,
                PercentB = percentB,
                StartIndexOffset = periods - 1,
                UpperBand = upperBand
            };

            return result;
        }

        /// <summary>
        /// Calculates Bollinger Bands indicator
        /// </summary>
        /// <param name="input">Input signal</param>
        /// <param name="periods">Number of periods</param>
        /// <param name="standardDeviations">Number of standard deviations</param>
        /// <returns>Object containing operation results</returns>
        public static BollingerBandsResult BollingerBands(IEnumerable<double> input, int periods, int standardDeviations)
        {
            return BollingerBands(input, periods, standardDeviations, false, false);
        }
    }
}
