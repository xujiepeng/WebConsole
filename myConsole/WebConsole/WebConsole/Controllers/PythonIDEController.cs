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
        public string RunText(string code, string para)
        {
            PythonIDEViewModel p = new PythonIDEViewModel();
            return JsonConvert.SerializeObject(p.RunCode(code, para));
        }

        /// <summary>
        /// 运行程序
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public string RunContent(string code, string para)
        {
            PythonIDEViewModel p = new PythonIDEViewModel();
            return JsonConvert.SerializeObject(p.RunCode(code, para));
        }

        /// <summary>
        /// 运行程序
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public string RunScript(string code, string para)
        {
            string path = @"E:\MyGit\WebConsole\myConsole\WebConsole\WebConsole\wwwroot\old\Document\Script\test2.py";
            string para1 = "\"Form C#:\"";
            string para2 = "\"Form C++++:\"";
            string strcmd = string.Format("{0} {1} {2}", path, para1, para2);
            string cmdresult = PythonIDEViewModel.run_cmd("python.exe", strcmd);
            return JsonConvert.SerializeObject(cmdresult);
        }
    }
}
