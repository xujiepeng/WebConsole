using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebConsole.Models;
using WebConsole.Models.Common;
using WebConsole.Models.Model;

namespace WebConsole.Controllers
{
    public class K_StockController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public string DrawCandle(string stockcode, string stockname)
        {
            K_IndexViewModel dataInfo = new K_IndexViewModel();
            return dataInfo.GetData(stockcode, stockname);
        }

        public IActionResult DrawEchart_K()
        {
            K_IndexViewModel dataInfo = new K_IndexViewModel();
            return Content(JsonConvert.SerializeObject(dataInfo.GetSingleStock()));
        }


        public IActionResult getdata(string code, string period, string type)
        {
            return Content(BLJJ(code, period, type));
        }

        /// <summary>
        /// 捕捞季节
        /// VAR1:=(2*CLOSE+HIGH+LOW)/3;
        /// VAR2:= EMA(EMA(EMA(VAR1, 3), 3), 3);
        /// J: (VAR2 - REF(VAR2, 1)) / REF(VAR2, 1) * 100;
        /// D: MA(J, 2);
        /// K: MA(J, 1);
        /// </summary>
        public static string BLJJ(string code = null, string period = null, string type = null, string date = null)
        {
            string EndDate = date == null ? DateTime.Now.ToString("yyyy-MM-dd") : Convert.ToDateTime(date).ToString("yyyy-MM-dd");
            if (date == null && DateTime.Now.Hour < 9)
            {
                EndDate = date == null ? DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") : Convert.ToDateTime(date).ToString("yyyy-MM-dd");
            }
            string BeginDate = Convert.ToDateTime(EndDate).AddDays(-1450).ToString("yyyy-MM-dd");
            code = code == null ? "300083" : code;
            type = type == null ? "1d" : type;
            period = period == null ? "30" : period;

            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("code", code + ".XSHG");
            dic.Add("unit", type);
            dic.Add("date", BeginDate);
            dic.Add("end_date", EndDate);
            dic.Add("fq_ref_date", EndDate);
            SortedList<string, SingleStockStru> stocklist = GetJQData.get_price_period(dic);
            List<decimal> testValues = new List<decimal>();
            List<decimal> TList = new List<decimal>();
            //(2*CLOSE+HIGH+LOW)/3
            foreach (var item in stocklist)
            {
                testValues.Add((2 * item.Value.close + item.Value.high + item.Value.low) / 3);
            }

            //J: (VAR2 - REF(VAR2, 1)) / REF(VAR2, 1) * 100
            List<decimal> pre_ema_list = StockFunction.EMA(StockFunction.EMA(StockFunction.EMA(testValues, 3), 3), 3);
            List<decimal> DList = new List<decimal>();
            List<decimal> KList = new List<decimal>();
            //根据给定显示周期，截取数据：D、J、Time
            if (pre_ema_list.Count > Convert.ToInt32(period))
            {
                for (int i = 0; i < Convert.ToInt32(period); i++)
                {
                    decimal J1 = (StockFunction.REF(pre_ema_list, i) - StockFunction.REF(pre_ema_list, i + 1)) / StockFunction.REF(pre_ema_list, i + 1) * 100;
                    decimal J2 = (StockFunction.REF(pre_ema_list, i + 1) - StockFunction.REF(pre_ema_list, i + 2)) / StockFunction.REF(pre_ema_list, i + 2) * 100;
                    DList.Add((J1 + J2) / 2);
                    KList.Add(J1);
                    TList.Add(Convert.ToDecimal(stocklist.Keys.ToList<string>()[stocklist.Count - i - 1]));
                }
            }
            else
            {
                for (int i = 0; i < pre_ema_list.Count; i++)
                {
                    decimal J1 = (StockFunction.REF(pre_ema_list, i) - StockFunction.REF(pre_ema_list, i + 1)) / StockFunction.REF(pre_ema_list, i + 1) * 100;
                    decimal J2 = (StockFunction.REF(pre_ema_list, i + 1) - StockFunction.REF(pre_ema_list, i + 2)) / StockFunction.REF(pre_ema_list, i + 2) * 100;
                    DList.Add(J1 + J2);
                    KList.Add(J1);
                    TList.Add(Convert.ToDecimal(stocklist.Keys.ToList<string>()[stocklist.Count - i - 1]));
                }
            }
            List<List<decimal>> ll = new List<List<decimal>>();
            DList.Reverse();
            KList.Reverse();
            TList.Reverse();
            ll.Add(DList);
            ll.Add(KList);
            ll.Add(TList);
            return JsonConvert.SerializeObject(ll);

        }
    }
}
