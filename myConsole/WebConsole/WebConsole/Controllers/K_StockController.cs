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

        public IActionResult Draw_KLine(string stockcode, string exchange, string period, string type, string enddate)
        {
            try
            {
                K_StockViewModel dataInfo = new K_StockViewModel();
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
                K_StockViewModel dataInfo = new K_StockViewModel();
                return Content(dataInfo.BLJJ(stockcode, exchange, period, type, enddate));
            }
            catch
            {
                return Content("false");
            }
            
        }

        
    }
}
