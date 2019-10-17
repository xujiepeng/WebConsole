using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using entConsole.Models;
using entConsole.Models.Conf;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace entConsole.Controllers
{
    public class CommonConfigController : Controller
    {
        private readonly CommConf _appConf;
        public CommonConfigController(IOptions<CommConf> options)        {            _appConf = options.Value;        }

        // GET: /<controller>/
        public IActionResult Index(string id, string pageclick, string RecordTime, string userName, string Level)
        {
            Dictionary<string, string> paraDic = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(userName))
            {
                paraDic.Add("userName", userName);
            }
            if (!string.IsNullOrEmpty(RecordTime))
            {
                paraDic.Add("RecordTime", RecordTime);
            }
            if (!string.IsNullOrEmpty(Level))
            {
                paraDic.Add("Level", Level);
            }

            CommonConfigViewModel dataInfo = new CommonConfigViewModel();
            if (string.IsNullOrEmpty(pageclick))
            {
                pageclick = "1";
            }
            var resultModel = new CommonConfigViewModel
            {
                dataInfo = dataInfo.GetSftpDataInfo(_appConf, Convert.ToInt32(pageclick), ref paraDic),
                paraDic = paraDic
            };
            return View(resultModel);
        }

        public IActionResult Sftp(string id, string pageclick, string RecordTime, string userName, string Level)
        {
            Dictionary<string, string> paraDic = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(userName))
            {
                paraDic.Add("userName", userName);
            }
            if (!string.IsNullOrEmpty(RecordTime))
            {
                paraDic.Add("RecordTime", RecordTime);
            }
            if (!string.IsNullOrEmpty(Level))
            {
                paraDic.Add("Level", Level);
            }

            CommonConfigViewModel dataInfo = new CommonConfigViewModel();
            if (string.IsNullOrEmpty(pageclick))
            {
                pageclick = "1";
            }
            var resultModel = new CommonConfigViewModel
            {
                dataInfo = dataInfo.GetSftpDataInfo(_appConf, Convert.ToInt32(pageclick), ref paraDic),
                paraDic = paraDic
            };
            return View(resultModel);
        }

        public IActionResult HttpsAddr(string id, string pageclick, string RecordTime, string userName, string Level)
        {
            Dictionary<string, string> paraDic = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(userName))
            {
                paraDic.Add("userName", userName);
            }
            if (!string.IsNullOrEmpty(RecordTime))
            {
                paraDic.Add("RecordTime", RecordTime);
            }
            if (!string.IsNullOrEmpty(Level))
            {
                paraDic.Add("Level", Level);
            }

            CommonConfigViewModel dataInfo = new CommonConfigViewModel();
            if (string.IsNullOrEmpty(pageclick))
            {
                pageclick = "1";
            }
            var resultModel = new CommonConfigViewModel
            {
                dataInfo = dataInfo.GetHttpsAddrDataInfo(_appConf, Convert.ToInt32(pageclick), ref paraDic),
                paraDic = paraDic
            };
            return View(resultModel);
        }
    }
}
