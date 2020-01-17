using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebConsole.Models.Model
{
    public class StockStru
    {
        /// <summary>
        /// 股票名称
        /// </summary>
        public string name
        {
            get;
            set;
        }
        /// <summary>
        /// 股票名称
        /// </summary>
        public string code
        {
            get;
            set;
        }
        /// <summary>
        /// 股票日期
        /// </summary>
        public string date
        {
            get;
            set;
        }
        /// <summary>
        /// 股票时间列表
        /// </summary>
        public List<string> list_time
        {
            get;
            set;
        }
        /// <summary>
        /// 股票时间数组
        /// </summary>
        public string[] array_time
        {
            get;
            set;
        }
        /// <summary>
        /// 成交量列表
        /// </summary>
        public List<int> list_vol
        {
            get;
            set;
        }
        /// <summary>
        /// 成交量数组
        /// </summary>
        public int[] array_vol
        {
            get;
            set;
        }
        /// <summary>
        /// 股票价格列表
        /// </summary>
        public List<double> list_price
        {
            get;
            set;
        }
        /// <summary>
        /// 股票价格数组
        /// </summary>
        public double[] array_price
        {
            get;
            set;
        }
        /// <summary>
        /// 成交量-时间列表
        /// </summary>
        public List<List<int>> list_vol_time = new List<List<int>>();

        /// <summary>
        /// list集合
        /// </summary>
        public List<List<decimal>> list_list_data = new List<List<decimal>>();
    }
}
