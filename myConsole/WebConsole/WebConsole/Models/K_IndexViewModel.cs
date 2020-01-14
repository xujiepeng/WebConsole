using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebConsole.Models.Common;
using WebConsole.Models.Common.HttpHelper;
using WebConsole.Models.Model;

namespace WebConsole.Models
{
    public class K_IndexViewModel
    {
        public string GetData(string stockcode, string stockname)
        {
            HttpsGet httpget = new HttpsGet();
            string str = httpget.GetUrlResponse("https://www.highcharts.com/samples/data/aapl-ohlcv.json").ToString();
            return str;
        }

        /// <summary>
        /// 获取单只股票数据
        /// </summary>
        /// <param name="code">查询代码</param>
        /// <param name="period">从某天开始向前取N天</param>
        /// <param name="type">周期类型</param>
        /// <param name="date">获取某天之前的数据</param>
        /// <returns></returns>
        public List<List<string>> GetSingleStock(string code = null, string period = null, string type = null, string date = null)
        {
            string EndDate = date == null ? DateTime.Now.ToString("yyyy-MM-dd") : Convert.ToDateTime(date).ToString("yyyy-MM-dd");
            string BeginDate = Convert.ToDateTime(EndDate).AddDays(-1450).ToString("yyyy-MM-dd");
            code = code == null ? "300083" : code;
            type = type == null ? "1d" : type;
            period = period == null ? "120" : period;

            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("code", code + ".XSHG");
            dic.Add("unit", type);
            dic.Add("date", BeginDate);
            dic.Add("end_date", EndDate);
            dic.Add("fq_ref_date", EndDate);
            SortedList<string, SingleStockStru> stocklist = GetJQData.get_price_period(dic);

            List<string> stock = new List<string>();
            List<List<string>> s_list = new List<List<string>>();
            int count = 0;
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
    }
}
