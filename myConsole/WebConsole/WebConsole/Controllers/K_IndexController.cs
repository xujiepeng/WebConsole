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
    public class K_IndexController : Controller
    {
        private readonly CommConf _appConf;
        public K_IndexController(IOptions<CommConf> options)        {            _appConf = options.Value;        }
        public IActionResult Index()
        {
            return View();
        }

        public string DrawCandle(string stockcode, string stockname)
        {
            K_IndexViewModel dataInfo = new K_IndexViewModel(_appConf);
            return dataInfo.GetData(stockcode, stockname);
        }

        public IActionResult DrawEchart_K()
        {
            K_IndexViewModel dataInfo = new K_IndexViewModel(_appConf);
            return Content(JsonConvert.SerializeObject(dataInfo.GetSingleStock()));
        }


        public IActionResult getdata(string code, string period, string type)
        {
            K_IndexViewModel dataInfo = new K_IndexViewModel(_appConf);
            return Content(JsonConvert.SerializeObject(dataInfo.BLJJ(code, period, type)));
        }

        
    }
}
