using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebConsole.Models.Common.HttpHelper;

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
    }
}
