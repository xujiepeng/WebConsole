using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebConsole.Models.Common;
using WebConsole.Models.Common.HttpHelper;
using WebConsole.Models.Model;
using WebConsole.Models.Model.Conf;

namespace WebConsole.Models
{
    public class K_StockViewModel
    {
        public static string JQUserName = string.Empty;
        public static string JQPassWord = string.Empty;
        public static string DefaultCode = string.Empty;
        public K_StockViewModel(CommConf options)
        {
            JQUserName = options.AttriList.FirstOrDefault(o => o.key == "JQUserName").value;            JQPassWord = options.AttriList.FirstOrDefault(o => o.key == "JQPassWord").value;
            DefaultCode = options.AttriList.FirstOrDefault(o => o.key == "DefaultCode").value;
        }
        /// <summary>
        /// 获取单只股票数据
        /// </summary>
        /// <param name="code">查询代码</param>
        /// <param name="period">从某天开始向前取N天</param>
        /// <param name="type">周期类型</param>
        /// <param name="date">获取某天之前的数据</param>
        /// <returns></returns>
        public List<List<string>> GetSingleStock(string code = null, string exchange = null, string period = null, string type = null, string date = null)
        {
            string EndDate = date == null ? DateTime.Now.ToString("yyyy-MM-dd") : Convert.ToDateTime(date).ToString("yyyy-MM-dd");
            if (date == null && Convert.ToUInt32(DateTime.Now.ToString("HHmm")) < 930)
            {
                EndDate = date == null ? DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") : Convert.ToDateTime(date).ToString("yyyy-MM-dd");
            }
            string BeginDate = Convert.ToDateTime(EndDate).AddDays(-1450).ToString("yyyy-MM-dd");
            code = code == null ? DefaultCode : code;
            type = type == null ? "1d" : type;
            period = period == null ? "30" : period;
            exchange = exchange == null ? ".XSHG" : exchange;

            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("code", code + exchange.ToUpper());
            dic.Add("unit", type);
            dic.Add("date", BeginDate);
            dic.Add("end_date", EndDate);
            dic.Add("fq_ref_date", EndDate);
            dic.Add("JQUserName", JQUserName);
            dic.Add("JQPassWord", JQPassWord);
            SortedList<string, SingleStockStru> stocklist = GetJQData.get_price_period(dic);

            List<string> stock = new List<string>();
            List<List<string>> s_list = new List<List<string>>();
            int count = 0;

            //筛选输出的数据范围
            foreach (var item in stocklist)
            {
                //如总数据小于周期，全部显示。否则只显示周期范围内数据
                if (stocklist.Count < Convert.ToInt32(period))
                {
                    stock = new List<string>();
                    stock.Add(Convert.ToDateTime(DateTime.ParseExact(item.Key, "yyyyMMdd", null)).ToString("yyyy/MM/dd"));
                    stock.Add(item.Value.open.ToString());
                    stock.Add(item.Value.close.ToString());
                    stock.Add(item.Value.low.ToString());
                    stock.Add(item.Value.high.ToString());
                    s_list.Add(stock);
                }
                else if (count < stocklist.Count - Convert.ToInt32(period))
                {
                    //数据不在要显示的周期内
                    count++;
                    continue;
                }
                else
                {
                    stock = new List<string>();
                    stock.Add(Convert.ToDateTime(DateTime.ParseExact(item.Key, "yyyyMMdd", null)).ToString("yyyy/MM/dd"));
                    stock.Add(item.Value.open.ToString());
                    stock.Add(item.Value.close.ToString());
                    stock.Add(item.Value.low.ToString());
                    stock.Add(item.Value.high.ToString());
                    s_list.Add(stock);
                }
            }
            return s_list;
        }

        /// <summary>
        /// 捕捞季节
        /// VAR1:=(2*CLOSE+HIGH+LOW)/3;
        /// VAR2:= EMA(EMA(EMA(VAR1, 3), 3), 3);
        /// J: (VAR2 - REF(VAR2, 1)) / REF(VAR2, 1) * 100;
        /// D: MA(J, 2);
        /// K: MA(J, 1);
        /// </summary>
        /// <param name="code">代码</param>
        /// <param name="exchange">市场</param>
        /// <param name="period">所要显示的周期</param>
        /// <param name="type">数据类型，日线，周线，月线</param>
        /// <param name="date">获取数据的起始时间</param>
        /// <returns></returns>
        public string BLJJ(string code = null, string exchange = null, string period = null, string type = null, string date = null)
        {
            string EndDate = date == null ? DateTime.Now.ToString("yyyy-MM-dd") : Convert.ToDateTime(date).ToString("yyyy-MM-dd");
            if (date == null && Convert.ToUInt32(DateTime.Now.ToString("HHmm")) < 930)
            {
                EndDate = date == null ? DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") : Convert.ToDateTime(date).ToString("yyyy-MM-dd");
            }
            string BeginDate = Convert.ToDateTime(EndDate).AddDays(-1450).ToString("yyyy-MM-dd");
            code = code == null ? DefaultCode : code;
            type = type == null ? "1d" : type;
            period = period == null ? "30" : period;
            exchange = exchange == null ? ".XSHG" : exchange;

            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("code", code + exchange.ToUpper());
            dic.Add("unit", type);
            dic.Add("date", BeginDate);
            dic.Add("end_date", EndDate);
            dic.Add("fq_ref_date", EndDate);
            dic.Add("JQUserName", JQUserName);
            dic.Add("JQPassWord", JQPassWord);
            SortedList<string, SingleStockStru> stocklist = GetJQData.get_price_period(dic);
            return JsonConvert.SerializeObject(AnalysisEngine.BLJJ(stocklist, period));

            //List<decimal> testValues = new List<decimal>();
            ////(2*CLOSE+HIGH+LOW)/3
            //foreach (var item in stocklist)
            //{
            //    testValues.Add((2 * item.Value.close + item.Value.high + item.Value.low) / 3);
            //}

            ////J: (VAR2 - REF(VAR2, 1)) / REF(VAR2, 1) * 100
            //List<decimal> pre_ema_list = StockFunction.EMA(StockFunction.EMA(StockFunction.EMA(testValues, 3), 3), 3);
            //List<decimal> DList = new List<decimal>();//d值线列表
            //List<decimal> KList = new List<decimal>();//k值线列表
            //List<decimal> TList = new List<decimal>();//x轴时间列表
            //List<List<decimal>> DataList = new List<List<decimal>>();//数据集合
            ////根据给定显示周期，截取数据：D、J、Time
            //if (pre_ema_list.Count > Convert.ToInt32(period))
            //{
            //    for (int i = 0; i < Convert.ToInt32(period); i++)
            //    {
            //        decimal J1 = (StockFunction.REF(pre_ema_list, i) - StockFunction.REF(pre_ema_list, i + 1)) / StockFunction.REF(pre_ema_list, i + 1) * 100;
            //        decimal J2 = (StockFunction.REF(pre_ema_list, i + 1) - StockFunction.REF(pre_ema_list, i + 2)) / StockFunction.REF(pre_ema_list, i + 2) * 100;
            //        DList.Add((J1 + J2) / 2);
            //        KList.Add(J1);
            //        TList.Add(Convert.ToDecimal(stocklist.Keys.ToList<string>()[stocklist.Count - i - 1]));
            //    }
            //}
            //else
            //{
            //    for (int i = 0; i < pre_ema_list.Count; i++)
            //    {
            //        decimal J1 = (StockFunction.REF(pre_ema_list, i) - StockFunction.REF(pre_ema_list, i + 1)) / StockFunction.REF(pre_ema_list, i + 1) * 100;
            //        decimal J2 = (StockFunction.REF(pre_ema_list, i + 1) - StockFunction.REF(pre_ema_list, i + 2)) / StockFunction.REF(pre_ema_list, i + 2) * 100;
            //        DList.Add(J1 + J2);
            //        KList.Add(J1);
            //        TList.Add(Convert.ToDecimal(stocklist.Keys.ToList<string>()[stocklist.Count - i - 1]));
            //    }
            //}
            
            //DList.Reverse();
            //KList.Reverse();
            //TList.Reverse();
            //DataList.Add(DList);
            //DataList.Add(KList);
            //DataList.Add(TList);
            //return JsonConvert.SerializeObject(DataList);
        }
    }
}
