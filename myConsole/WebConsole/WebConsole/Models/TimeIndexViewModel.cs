using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using WebConsole.Models.Common.HttpHelper;
using WebConsole.Models.Model;

namespace WebConsole.Models
{
    public class TimeIndexViewModel
    {
        /// <summary>
        /// 获取股票分时价格数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetPrice(string stockcode, string stockname)
        {
            string stockLink = string.Format("http://data.gtimg.cn/flashdata/hushen/minute/{0}.js?maxage=111", stockcode);
            HttpsGet httpget = new HttpsGet();
            string str = httpget.GetUrlResponse(stockLink).ToString();
            string[] array = str.Split("\\n\\\n");
            //价格列表
            List<double> list_price = new List<double>();
            //成交量列表
            List<int> list_vol = new List<int>();
            //时间列表
            List<string> list_time = new List<string>();
            //总时长240分钟
            int count = 60 * 4;
            //剔除array中前两位和最后一位，不足240的用Null补齐
            for (int i = 2; i < count + 2; i++)
            {
                if (i < array.Length - 3)
                {
                    list_price.Add(double.Parse(array[i].Split(' ')[1]));
                    list_time.Add(array[i].Split(' ')[0].Insert(2, ":"));
                    //累计值减去前一周期值，计算当前成交量
                    if (list_vol.Count > 0)
                    {
                        list_vol.Add(int.Parse(array[i].Split(' ')[2]) - int.Parse(array[i - 1].Split(' ')[2]));
                    }
                    else
                    {
                        list_vol.Add(int.Parse(array[i].Split(' ')[2]));
                    }
                }
                else
                {
                    list_price.Add(double.NaN);
                    list_vol.Add(0);
                    list_time.Add(" ");
                }
            }
            //股价数据列表
            List<StockStru> LineList = new List<StockStru>();
            StockStru l1 = new StockStru();
            l1.name = stockcode;
            l1.date = array[1];
            l1.list_time = list_time;
            l1.list_price = list_price;
            LineList.Add(l1);

            //成交量数据列表
            StockStru l2 = new StockStru();
            l2.name = "Stock-Vol";
            l2.list_vol = list_vol;
            LineList.Add(l2);
            

            string jsonData = JsonConvert.SerializeObject(LineList);
            return jsonData;
        }



        /// <summary>
        /// 查询股票分时成交量数据
        /// </summary>
        /// <param name="stockcode">股票名称</param>
        /// <param name="stockname">股票代码</param>
        /// <returns></returns>
        public string GetVol(string stockcode, string stockname)
        {
            string stockLink = string.Format("http://data.gtimg.cn/flashdata/hushen/minute/{0}.js?maxage=111", stockcode);
            HttpsGet httpget = new HttpsGet();
            string str = httpget.GetUrlResponse(stockLink).ToString();
            string[] array = str.Split("\\n\\\n");
            //价格列表
            List<double> list_price = new List<double>();
            //成交量列表
            List<int> list_vol = new List<int>();
            //时间列表
            List<string> list_time = new List<string>();
            //总时长240分钟
            int count = 60 * 4;
            //剔除array中前两位和最后一位，不足240的用Null补齐
            for (int i = 2; i < count + 2; i++)
            {
                if (i < array.Length - 3)
                {
                    //成交时间列表
                    list_time.Add(array[i].Split(' ')[0]);
                    //累计值减去前一周期值，计算当前成交量
                    if (list_vol.Count > 0)
                    {
                        list_vol.Add(int.Parse(array[i].Split(' ')[2]) - int.Parse(array[i - 1].Split(' ')[2]));
                    }
                    else
                    {
                        list_vol.Add(int.Parse(array[i].Split(' ')[2]));
                    }
                }
                else
                {
                    list_vol.Add(0);
                    list_time.Add(".");
                }
            }

            StockStru result = new StockStru();
            result.name = "成交量";
            //构建“时间-成交量”键值对，加入到list_vol_time列表
            for (int i = 0; i < list_vol.Count; i++)
            {
                List<int> item = new List<int>();
                //没有对应数据的时间，向后顺延一个单位
                if (list_time[i] == ".")
                {
                    if (i == 0)
                    {
                        item.Add(0);
                    }
                    else
                    {
                        item.Add(result.list_vol_time[i - 1][0] + 1);
                    }
                }
                else
                {
                    item.Add(Convert.ToInt32(list_time[i]));
                }
                item.Add(list_vol[i]);
                result.list_vol_time.Add(item);
            }

            string jsonData = JsonConvert.SerializeObject(result.list_vol_time);
            return jsonData;
        }
    }
}
