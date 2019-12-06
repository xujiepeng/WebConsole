using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebConsole.Models;
using WebConsole.Models.Common.HttpHelper;
using WebConsole.Models.Model;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebConsole.Controllers
{
    public class TimeIndexController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 获取股票分时数据
        /// 回传前端 画价格分时折线图
        /// </summary>
        /// <param name="stockcode"></param>
        /// <param name="stockname"></param>
        /// <returns></returns>
        public string DrawLine(string stockcode, string stockname)
        {
            if (string.IsNullOrEmpty(stockcode) && string.IsNullOrEmpty(stockname))
            {
                stockcode = "sh000001";
            }

            TimeIndexViewModel dataInfo = new TimeIndexViewModel();
            return dataInfo.GetPrice(stockcode, stockname);
        }


        /// <summary>
        /// 查询股票分时数据
        /// 回传前端 画成交量分时柱状图
        /// </summary>
        /// <param name="stockcode">股票名称</param>
        /// <param name="stockname">股票代码</param>
        /// <returns></returns>
        public string DrawVol(string stockcode, string stockname)
        {
            if (string.IsNullOrEmpty(stockcode) && string.IsNullOrEmpty(stockname))
            {
                stockcode = "sh000001";
            }
            TimeIndexViewModel dataInfo = new TimeIndexViewModel();
            return dataInfo.GetVol(stockcode, stockname);
        }
    }
}
