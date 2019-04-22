using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using entConsole.Models;
using entConsole.Models.Conf;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Http;

namespace entConsole.Controllers
{
    public class HomeController : Controller
    {
        private readonly CommConf _appConf;
        public HomeController(IOptions<CommConf> options)        {            _appConf = options.Value;        }

        public IActionResult Index(IFormCollection collection)
        {
            string a = collection["infoTime"].ToString();
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pageclick"></param>
        /// <param name="infoTime"></param>
        /// <param name="userName"></param>
        /// <param name="Level"></param>
        /// <returns></returns>
        public IActionResult DataInfo(string id, string pageclick, string infoTime, string userName, string Level)
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

            DataInfoViewModel dataInfo = new DataInfoViewModel();
            if (string.IsNullOrEmpty(pageclick))
            {
                pageclick = "1";
            }
            var resultModel = new DataInfoViewModel
            {
                dataInfo = dataInfo.GetDataInfo(_appConf, Convert.ToInt32(pageclick), ref paraDic),
                //PageNum = dataInfo.GetPageNum(),
                //clickpagenow = dataInfo.GetClickPage(),
                //allowPageNum = Convert.ToInt32(_appConf.AttriList.FirstOrDefault(l => l.key == "allowPageNum").value),
                paraDic = paraDic
            };
            return View(resultModel);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userName"></param>
        /// <param name="tablename"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public IActionResult TableStatus(string id, string pageclick, string userName, string tablename, string status)
        {
            Dictionary<string, string> paraDic = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(userName))
            {
                paraDic.Add("userName", userName);
            }
            if (!string.IsNullOrEmpty(tablename))
            {
                paraDic.Add("tablename", tablename);
            }
            if (!string.IsNullOrEmpty(status))
            {
                paraDic.Add("status", status);
            }

            TableStatusViewModel dataInfo = new TableStatusViewModel();
            if (string.IsNullOrEmpty(pageclick))
            {
                pageclick = "1";
            }
            var resultModel = new TableStatusViewModel
            {
                table = dataInfo.GetTableStatus(_appConf, Convert.ToInt32(pageclick), ref paraDic),
                //PageNum = dataInfo.GetPageNum(),
                //clickpagenow = dataInfo.GetClickPage(),
                //allowPageNum = Convert.ToInt32(_appConf.AttriList.FirstOrDefault(l => l.key == "allowPageNum").value),
                paraDic = paraDic
            };
            return View(resultModel);
            //return RedirectToAction("Index", "Error", new { Msg = "TableStatus对象为Null" });
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


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(string id)
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
