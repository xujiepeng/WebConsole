using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebConsole.Models;
using WebConsole.Models.Model;
using WebConsole.Models.Model.Conf;

namespace WebConsole.Controllers
{
    public class MemberList_StaticController : Controller
    {
        private readonly CommConf _appConf;
        public MemberList_StaticController(IOptions<CommConf> options)        {            _appConf = options.Value;        }

        public IActionResult Index()
        {
            MemberList_StaticViewModel viewModelData = new MemberList_StaticViewModel();
            viewModelData.a = "a";
            
            var resultModel = new MemberList_StaticViewModel
            {
                a = viewModelData.a,
                dataInfoList = viewModelData.GetDate(_appConf),
                c = "c"
            };

            return View(resultModel);
        }

        public IActionResult MemberAdd()
        {
            return View();
        }

        /// <summary>
        /// 接收前端传递来的，待修改页面数据,原样返回
        /// </summary>
        /// <param name="username"></param>
        /// <param name="sex"></param>
        /// <param name="tel"></param>
        /// <param name="addr"></param>
        /// <param name="Static"></param>
        /// <returns></returns>
        public IActionResult MemberEdit(string username,string sex,string tel,string addr,string states)
        {
            MemberList_StaticStru MemberList_Static = new MemberList_StaticStru();
            MemberList_Static.username = username;
            MemberList_Static.sex = sex;
            MemberList_Static.tel = tel;
            MemberList_Static.addr = addr;
            MemberList_Static.states = states;
            var resultModel = new MemberList_StaticViewModel
            {
                memberlist_static = MemberList_Static
            };
            return View(resultModel);
        }
        

        /// <summary>
        /// 新增数据，add
        /// </summary>
        /// <param name="username"></param>
        /// <param name="sex"></param>
        /// <param name="tel"></param>
        /// <param name="addr"></param>
        /// <param name="Static"></param>
        /// <returns></returns>
        public bool Add(string username, string sex, string tel, string addr, string states)
        {
            MemberList_StaticStru MemberList_StaticStru = new MemberList_StaticStru();
            MemberList_StaticStru.username = username;
            MemberList_StaticStru.sex = sex;
            MemberList_StaticStru.tel = tel;
            MemberList_StaticStru.addr = addr;
            MemberList_StaticStru.states = states;
            MemberList_StaticViewModel dataInfo = new MemberList_StaticViewModel();

            if (dataInfo.AddData(MemberList_StaticStru))
                return true;
            else
                return false;
        }

        /// <summary>
        /// 修改数据，Edit/Update
        /// </summary>
        /// <param name="username"></param>
        /// <param name="sex"></param>
        /// <param name="tel"></param>
        /// <param name="addr"></param>
        /// <param name="Static"></param>
        /// <returns></returns>
        public bool Update(string username, string sex, string tel, string addr, string states)
        {
            MemberList_StaticStru MemberList_StaticStru = new MemberList_StaticStru();
            MemberList_StaticStru.username = username;
            MemberList_StaticStru.sex = sex;
            MemberList_StaticStru.tel = tel;
            MemberList_StaticStru.addr = addr;
            MemberList_StaticStru.states = states;
            MemberList_StaticViewModel dataInfo = new MemberList_StaticViewModel();

            if (dataInfo.UpdateData(MemberList_StaticStru))
                return true;
            else
                return false;
        }

        /// <summary>
        /// 删除数据，delete
        /// </summary>
        /// <param name="id"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        public bool Delete(string id, string username)
        {
            MemberList_StaticStru MemberList_StaticStru = new MemberList_StaticStru();
            MemberList_StaticStru.ID = Convert.ToInt32(id);
            MemberList_StaticStru.username = username;
            MemberList_StaticViewModel dataInfo = new MemberList_StaticViewModel();

            if (dataInfo.DeleteData(MemberList_StaticStru))
                return true;
            else
                return false;
        }

        /// <summary>
        /// 批量删除数据，delete
        /// </summary>
        /// <param name="id"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        public bool DeleteAll(string[] ids)
        {
            MemberList_StaticViewModel dataInfo = new MemberList_StaticViewModel();

            if (dataInfo.DeleteDataAll(ids))
                return true;
            else
                return false;
        }

        /// <summary>
        /// 按条件搜索
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pageclick"></param>
        /// <param name="RecordTime"></param>
        /// <param name="userName"></param>
        /// <param name="Static"></param>
        /// <param name="begintime"></param>
        /// <param name="endtime"></param>
        /// <returns></returns>
        public MemberList_StaticViewModel Sreach(string id, string pageclick, string RecordTime, string userName, string states, string begintime, string endtime)
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
            if (!string.IsNullOrEmpty(states))
            {
                paraDic.Add("States", states.Trim());
            }
            if (!string.IsNullOrEmpty(begintime))
            {
                paraDic.Add("begintime", begintime.Trim());
            }
            if (!string.IsNullOrEmpty(endtime))
            {
                paraDic.Add("endtime", endtime.Trim());
            }
            MemberList_StaticViewModel viewModelData = new MemberList_StaticViewModel();

            var resultModel = new MemberList_StaticViewModel
            {
                dataInfoList = viewModelData.GetDateSreach(_appConf, ref paraDic)
            };
            return resultModel;

            
        }
    }
}
