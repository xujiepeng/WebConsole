using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using entConsole.Models;
using entConsole.Models.Conf;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace entConsole.Controllers
{
    public class TaskDriverController : Controller
    {
        private readonly CommConf _appConf;
        public TaskDriverController(IOptions<CommConf> options)        {            _appConf = options.Value;        }

        // GET: /<controller>/
        public IActionResult Index(string id, string pageclick, string infoTime, string userName, string Level)
        {
            Dictionary<string, string> paraDic = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(userName))
            {
                paraDic.Add("userName", userName);
            }
            if (!string.IsNullOrEmpty(infoTime))
            {
                paraDic.Add("infoTime", infoTime);
            }
            if (!string.IsNullOrEmpty(Level))
            {
                paraDic.Add("Level", Level);
            }


            TaskDriverViewModel taskDriver = new TaskDriverViewModel();
            if (string.IsNullOrEmpty(pageclick))
            {
                pageclick = "1";
            }
            var resultModel = new TaskDriverViewModel
            {
                dataList = taskDriver.GetTaskDriver(_appConf, Convert.ToInt32(pageclick), ref paraDic),
                paraDic = paraDic
            };
            return View(resultModel);
        }


        public IActionResult Edit(string id, string pageclick, string infoTime, string userName, string Level)
        {
            
            return View();
        }





        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult TaskDriver(string id, string pageclick, string infoTime, string userName, string Level)
        {
            Dictionary<string, string> paraDic = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(userName))
            {
                paraDic.Add("userName", userName);
            }
            if (!string.IsNullOrEmpty(infoTime))
            {
                paraDic.Add("infoTime", infoTime);
            }
            if (!string.IsNullOrEmpty(Level))
            {
                paraDic.Add("Level", Level);
            }


            TaskDriverViewModel taskDriver = new TaskDriverViewModel();
            if (string.IsNullOrEmpty(pageclick))
            {
                pageclick = "1";
            }
            var resultModel = new TaskDriverViewModel
            {
                dataList = taskDriver.GetTaskDriver(_appConf, Convert.ToInt32(pageclick), ref paraDic),
                //PageNum = taskDriver.GetPageNum(),
                //clickpagenow = taskDriver.GetClickPage(),
                //PageNum = Convert.ToInt32( paraDic["PageNum"]),
                //clickpagenow = Convert.ToInt32(paraDic["clickpagenow"]),
                //allowPageNum = Convert.ToInt32(_appConf.AttriList.FirstOrDefault(l => l.key == "allowPageNum").value),
                //paraDic["allowPageNum"] = Convert.ToInt32(_appConf.AttriList.FirstOrDefault(l => l.key == "allowPageNum").value).ToString(),
                paraDic = paraDic
            };
            return View(resultModel);
        }
    }
}
