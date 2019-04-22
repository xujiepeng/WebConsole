using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using entConsole.Lib.Util;
using entConsole.Models.Conf;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace entConsole.Controllers
{
    public class ErrorController : Controller
    {
        private readonly CommConf _appConf;
        public ErrorController(IOptions<CommConf> options)
        {
            _appConf = options.Value;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Index(string Msg)
        {
            var feature = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var error = feature?.Error;
            //记录日志
            Log4NetUtil.Error(this, Convert.ToString(error)+ Msg);
            //发送报警邮件
            //MailHelper.SendAlertMail(_appConf.MailList.FirstOrDefault<MailStru>(), error.ToString());
            //微信报警
            //WeChatHelper.SendAlertWX(_appConf.WeChatList.FirstOrDefault<WeChatStru>());
            ViewData["Msg"] = Msg;
            return View();
        }
    }
}