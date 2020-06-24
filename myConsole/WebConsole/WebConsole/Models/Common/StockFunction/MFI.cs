using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebConsole.Models.Common.Entities;
/******************************************************************
 * Author: 
 * Date: 
 * Content: 资金流量指标
 * Link: https://baike.baidu.com/item/MFI%E6%8C%87%E6%A0%87/1690931?fr=aladdin
 ******************************************************************/
namespace WebConsole.Models.Common
{
    public sealed partial class AnalysisEngine
    {
        private class MFIMoneyFlow
        {
            /// <summary>
            /// 流入现金总量
            /// </summary>
            public double PositiveMoneyFlow { get; set; }
            /// <summary>
            /// 流出现金总量
            /// </summary>
            public double NegativeMoneyFlow { get; set; }
        }

        /// <summary>
        /// Calculates Money Flow Index (MFI) indicator
        /// 资金流量指标
        /// </summary>
        /// <param name="highs">Signal representing price highs</param>
        /// <param name="lows">Signal representing price lows</param>
        /// <param name="closes">Signal representing closing prices</param>
        /// <param name="periods">Number of periods</param>
        /// <param name="volume">Signal representing daily volumes</param>
        /// <returns>Object containing operation results</returns>
        public static MFIResult MFI(IEnumerable<double> highs, IEnumerable<double> lows, IEnumerable<double> closes, IEnumerable<int> volume, int periods)
        {
            //求起始日的典型价
            double lastTypicalPrice = (highs.ElementAt(0) + lows.ElementAt(0) + closes.ElementAt(0)) / 3;

            var moneyFlowList = new List<MFIMoneyFlow>();

            for (int i = 1; i < highs.Count(); i++)
            {
                //取最高 最低 收盘价均值 作为典型价
                double typicalPrice = (highs.ElementAt(i) + lows.ElementAt(i) + closes.ElementAt(i)) / 3;
                bool up = typicalPrice > lastTypicalPrice;
                lastTypicalPrice = typicalPrice;
                //典型价*成交量=资金流量
                double rawMoneyFlow = typicalPrice * volume.ElementAt(i);

                var moneyFlow = new MFIMoneyFlow();
                //如果今日资金流量大于昨日，记为流入，否则记为流出
                if (up)
                {
                    moneyFlow.PositiveMoneyFlow = rawMoneyFlow;
                }
                else
                {
                    moneyFlow.NegativeMoneyFlow = rawMoneyFlow;
                }

                moneyFlowList.Add(moneyFlow);
            }

            var moneyFlowIndexList = new List<double>();

            for (int i = 0; i < moneyFlowList.Count - periods + 1; i++)
            {
                //取14天的数据，作为计算周期
                var range = moneyFlowList.GetRange(i, periods);
                //统计14天内资金流入总量 和 流出总量
                double positiveMoneyFlow = range.Sum(x => x.PositiveMoneyFlow);
                double negativeMoneyFlow = range.Sum(x => x.NegativeMoneyFlow);
                //流入流出比 = 流入资金/流出资金
                double moneyFlowRatio = positiveMoneyFlow / negativeMoneyFlow;
                //为了更好地在坐标上显示，对moneyFlowRatio进行数据处理
                double moneyFlowIndex = 100 - 100 / (1 + moneyFlowRatio);
                //把每天的MFI数据放入列表
                moneyFlowIndexList.Add(moneyFlowIndex);
            }

            var result = new MFIResult()
            {
                Values = moneyFlowIndexList,
                StartIndexOffset = periods
            };

            return result;
        }
    }
}
