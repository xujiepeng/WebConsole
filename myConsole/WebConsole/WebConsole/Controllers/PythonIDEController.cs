using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebConsole.Models;

namespace WebConsole.Controllers
{
    public class PythonIDEController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 运行程序
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public string Run(string code)
        {
            PythonIDEViewModel p = new PythonIDEViewModel();
            //return "123456";
            return JsonConvert.SerializeObject(p.RunCode(code, ""));
        }
    }
}
