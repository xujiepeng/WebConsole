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
    public class TableStatusController : Controller
    {
        private readonly CommConf _appConf;
        public TableStatusController(IOptions<CommConf> options)        {            _appConf = options.Value;        }
        // GET: /<controller>/
        public IActionResult Index(string id, string pageclick, string userName, string taskId, string taskStatus)
        {
            Dictionary<string, string> paraDic = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(userName))
            {
                paraDic.Add("userName", userName);
            }
            if (!string.IsNullOrEmpty(taskId))
            {
                paraDic.Add("taskId", taskId);
            }
            if (!string.IsNullOrEmpty(taskStatus))
            {
                paraDic.Add("taskStatus", taskStatus);
            }

            TableStatusViewModel dataInfo = new TableStatusViewModel();
            if (string.IsNullOrEmpty(pageclick))
            {
                pageclick = "1";
            }
            var resultModel = new TableStatusViewModel
            {
                table = dataInfo.GetTableStatus(_appConf, Convert.ToInt32(pageclick), ref paraDic),
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
    }
}
