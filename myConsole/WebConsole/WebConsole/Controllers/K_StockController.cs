using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using WebConsole.Models;
using WebConsole.Models.Common;
using WebConsole.Models.Model;
using WebConsole.Models.Model.Conf;

namespace WebConsole.Controllers
{
    public class K_StockController : Controller
    {
        private readonly CommConf _appConf;
        public K_StockController(IOptions<CommConf> options)        {            _appConf = options.Value;        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Draw_KLine(string stockcode, string exchange, string period, string type, string enddate)
        {
            try
            {
                K_StockViewModel dataInfo = new K_StockViewModel(_appConf);
                return Content(JsonConvert.SerializeObject(dataInfo.GetSingleStock(stockcode, exchange, period, type, enddate)));
            }
            catch
            {
                return Content("false");
            }
        }


        public IActionResult DrawBLJJ(string stockcode,string exchange, string period, string type, string enddate)
        {
            
            try
            {
                K_StockViewModel dataInfo = new K_StockViewModel(_appConf);
                return Content(dataInfo.BLJJ(stockcode, exchange, period, type, enddate));
            }
            catch
            {
                return Content("false");
            }
            
        }

        
    }
}
