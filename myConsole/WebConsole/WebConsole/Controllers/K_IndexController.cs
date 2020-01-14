using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebConsole.Models;
using WebConsole.Models.Common.HttpHelper;

namespace WebConsole.Controllers
{
    public class K_IndexController : Controller
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
    }
}
