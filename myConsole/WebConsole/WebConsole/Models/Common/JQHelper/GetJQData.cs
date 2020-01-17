using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebConsole.Models.Model;
using WebConsole.Models.Model.Conf;

namespace WebConsole.Models.Common
{
    public class GetJQData
    {
        /// <summary>
        /// 获取单只股票数据
        /// </summary>
        /// <param name="dic"></param>
        /// <returns></returns>
        public static SortedList<string, SingleStockStru> get_price_period(Dictionary<string, string> dic)
        {
            var url = "https://dataapi.joinquant.com/apis";
            string securityInfo = string.Empty;
            using (var client = new HttpClient())
            {
                //需要添加System.Web.Extensions
                //生成JSON请求信息
                string json = JsonConvert.SerializeObject(new
                {
                    method = "get_token",
                    mob = dic["JQUserName"],
                    pwd = dic["JQPassWord"]
                });
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                //POST请求并等待结果
                var result = client.PostAsync(url, content).Result;
                //读取返回的TOKEN
                string token1 = result.Content.ReadAsStringAsync().Result;
                string body = JsonConvert.SerializeObject(new
                {
                    method = "get_price_period",
                    token = token1,
                    code = dic["code"],
                    unit = dic["unit"],
                    date = dic["date"],
                    end_date = dic["end_date"],
                    fq_ref_date = dic["fq_ref_date"]
                }
                );
                var bodyContent = new StringContent(body, Encoding.UTF8, "application/json");
                //POST请求并等待结果
                result = client.PostAsync(url, bodyContent).Result;
                securityInfo = result.Content.ReadAsStringAsync().Result;
            }
            var s = securityInfo.Split('\n');
            SortedList<string, SingleStockStru> stocklist = new SortedList<string, SingleStockStru>();
            foreach (var item in s)
            {
                var i = item.Split(',');
                if (i[0].ToString() != "date")
                {
                    SingleStockStru stock = new SingleStockStru();
                    stock.open = Convert.ToDecimal(i[1]);
                    stock.close = Convert.ToDecimal(i[2]);
                    stock.high = Convert.ToDecimal(i[3]);
                    stock.low = Convert.ToDecimal(i[4]);
                    stocklist.Add(Convert.ToDateTime(i[0]).ToString("yyyyMMdd"), stock);
                }
            }
            return stocklist;
        }

        public static SingleStockStru get_security_info(Dictionary<string, string> dic)
        {
            var url = "https://dataapi.joinquant.com/apis";
            string securityInfo = string.Empty;
            using (var client = new HttpClient())
            {
                //需要添加System.Web.Extensions
                //生成JSON请求信息
                string json = JsonConvert.SerializeObject(new
                {
                    method = "get_token",
                    mob = dic["JQUserName"],
                    pwd = dic["JQPassWord"]
                });
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                //POST请求并等待结果
                var result = client.PostAsync(url, content).Result;
                //读取返回的TOKEN
                string token1 = result.Content.ReadAsStringAsync().Result;
                string body = JsonConvert.SerializeObject(new
                {
                    method = "get_security_info",
                    token = token1,
                    code = dic["code"]
                }
                );
                var bodyContent = new StringContent(body, Encoding.UTF8, "application/json");
                //POST请求并等待结果
                result = client.PostAsync(url, bodyContent).Result;
                securityInfo = result.Content.ReadAsStringAsync().Result;
            }
            var s = securityInfo.Split('\n');
            SingleStockStru stock = new SingleStockStru();
            foreach (var item in s)
            {
                var i = item.Split(',');
                if (i[0].ToString() != "date")
                {
                    stock = new SingleStockStru();
                    stock.open = Convert.ToDecimal(i[1]);
                    stock.close = Convert.ToDecimal(i[2]);
                    stock.high = Convert.ToDecimal(i[3]);
                    stock.low = Convert.ToDecimal(i[4]);
                }
            }
            return stock;
        }
    }

}
