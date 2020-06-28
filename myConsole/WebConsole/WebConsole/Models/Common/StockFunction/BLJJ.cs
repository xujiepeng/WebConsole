using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebConsole.Models.Model;

namespace WebConsole.Models.Common
{
    public sealed partial class AnalysisEngine
    {
        /// <summary>
        /// 捕捞季节
        /// VAR1:=(2*CLOSE+HIGH+LOW)/3;
        /// VAR2:= EMA(EMA(EMA(VAR1, 3), 3), 3);
        /// J: (VAR2 - REF(VAR2, 1)) / REF(VAR2, 1) * 100;
        /// D: MA(J, 2);
        /// K: MA(J, 1);
        /// </summary>
        /// <param name="stocklist"></param>
        /// <param name="period"></param>
        /// <returns></returns>
        public static List<List<decimal>> BLJJ(SortedList<string, SingleStockStru> stocklist, string period = null)
        {
            List<decimal> testValues = new List<decimal>();
            List<decimal> TList = new List<decimal>();
            //(2*CLOSE+HIGH+LOW)/3
            foreach (var item in stocklist)
            {
                testValues.Add((2 * item.Value.close + item.Value.high + item.Value.low) / 3);
            }

            //J: (VAR2 - REF(VAR2, 1)) / REF(VAR2, 1) * 100
            List<decimal> pre_ema_list = AnalysisEngine.EMA(AnalysisEngine.EMA(AnalysisEngine.EMA(testValues, 3), 3), 3);
            List<decimal> DList = new List<decimal>();
            List<decimal> KList = new List<decimal>();
            List<List<decimal>> ll = new List<List<decimal>>();
            //根据给定显示周期，截取数据：D、J、Time
            if (pre_ema_list.Count > Convert.ToInt32(period))
            {
                for (int i = 0; i < Convert.ToInt32(period); i++)
                {
                    decimal J1 = (AnalysisEngine.REF(pre_ema_list, i) - AnalysisEngine.REF(pre_ema_list, i + 1)) / AnalysisEngine.REF(pre_ema_list, i + 1) * 100;
                    decimal J2 = (AnalysisEngine.REF(pre_ema_list, i + 1) - AnalysisEngine.REF(pre_ema_list, i + 2)) / AnalysisEngine.REF(pre_ema_list, i + 2) * 100;
                    DList.Add((J1 + J2) / 2);
                    KList.Add(J1);
                    TList.Add(Convert.ToDecimal(stocklist.Keys.ToList<string>()[stocklist.Count - i - 1]));
                }
            }
            else
            {
                for (int i = 0; i < pre_ema_list.Count; i++)
                {
                    decimal J1 = (AnalysisEngine.REF(pre_ema_list, i) - AnalysisEngine.REF(pre_ema_list, i + 1)) / AnalysisEngine.REF(pre_ema_list, i + 1) * 100;
                    decimal J2 = (AnalysisEngine.REF(pre_ema_list, i + 1) - AnalysisEngine.REF(pre_ema_list, i + 2)) / AnalysisEngine.REF(pre_ema_list, i + 2) * 100;
                    DList.Add(J1 + J2);
                    KList.Add(J1);
                    TList.Add(Convert.ToDecimal(stocklist.Keys.ToList<string>()[stocklist.Count - i - 1]));
                }
            }
            DList.Reverse();
            KList.Reverse();
            TList.Reverse();
            ll.Add(DList);
            ll.Add(KList);
            ll.Add(TList);
            return ll;

        }
    }
}
