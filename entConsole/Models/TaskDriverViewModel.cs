using entConsole.Lib.DbAccess;
using entConsole.Lib.Util;
using entConsole.Models.Conf;
using Microsoft.AspNetCore.Html;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace entConsole.Models
{
    public class TaskDriverViewModel
    {
        public List<TaskDriverStru> dataList = new List<TaskDriverStru>();
        /// <summary>
        /// 当前页面数量 (当前table中一共有多少页)
        /// </summary>
        public int PageNum = 1;
        /// <summary>
        /// 当前点击页数
        /// </summary>
        public int clickpagenow;
        /// <summary>
        /// 允许显示的可选择页数
        /// </summary>
        public int allowPageNum = 5;
        /// <summary>
        /// 
        /// </summary>
        public Dictionary<string, string> paraDic = new Dictionary<string, string>();

        #region 读取TaskDriverStru
        /// <summary>
        /// Sqlite加载TableStatusStru
        /// </summary>
        /// <param name="options"></param>
        /// <param name="pageClick"></param>
        /// <param name="sqlDic"></param>
        /// <returns></returns>
        public List<TaskDriverStru> GetTaskDriver(CommConf options, int pageClick, ref Dictionary<string, string> sqlDic)
        {
            // 查询数据总数
            List<TaskDriverStru> tempDataList = new List<TaskDriverStru>();
            string errorMsg = string.Empty;
            CommonMethod comm = new CommonMethod();
            string sql = @"SELECT * FROM TaskStatus" + SqlStr(sqlDic) + " order by taskid desc;";

            DbConnStru SqlitePath = options.DbConnList.FirstOrDefault(o => o.key == "SqliteConn_1");

            var list = comm.Select<TaskDriverStru>(sql, SqlitePath);
            if (!string.IsNullOrEmpty(errorMsg))
            {
                return null;
            }

            foreach (TaskDriverStru item in list)
            {
                tempDataList.Add(item);
            }

            sqlDic["PageShowNum"] = options.AttriList.FirstOrDefault(o => o.key == "PageShowNum").value;
            sqlDic["allowPageNum"] = options.AttriList.FirstOrDefault(l => l.key == "allowPageNum").value;
            sqlDic["clickpagenow"] = pageClick.ToString();

            dataList = comm.TablePage<TaskDriverStru>(tempDataList, ref sqlDic);
            return dataList;
        }
        #endregion


        public List<TaskDriverStru> GetTaskDriver1(CommConf options, int pageClick, ref Dictionary<string, string> sqlDic)
        {
            // 查询数据总数
            List<TaskDriverStru> tempDataList = new List<TaskDriverStru>();
            string errorMsg = string.Empty;
            string sql = @"SELECT * FROM TaskStatus" + SqlStr(sqlDic) + " order by taskid desc;";
            string AppBasePath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            string SqlitePath = Path.Combine(AppBasePath, "finchina.db3");
            SqliteAccess sqliteAccess = new SqliteAccess(SqlitePath);
            DataTable dt = sqliteAccess.QueryDt(sql, out errorMsg);
            CommonMethod comm = new CommonMethod();

            if (!string.IsNullOrEmpty(errorMsg))
            {
                return null;
            }

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                TaskDriverStru obj = new TaskDriverStru();
                obj.Id = ConvertUtil.ParseString(dt.Rows[i]["Id"]);
                obj.taskId = ConvertUtil.ParseString(dt.Rows[i]["taskId"]);
                obj.userName = ConvertUtil.ParseString(dt.Rows[i]["userName"]);
                obj.TaskName = ConvertUtil.ParseString(dt.Rows[i]["TaskName"]);
                //obj.Tables = ConvertUtil.ParseString(dt.Rows[i]["tableName"]);
                obj.taskStatus = ConvertUtil.ParseString(dt.Rows[i]["taskStatus"]);
                //obj.TMSP = ConvertUtil.ParseString(dt.Rows[i]["TMSP"]);
                //obj.Init = ConvertUtil.ParseString(dt.Rows[i]["Init"]) == "1" ? true : false;
                tempDataList.Add(obj);
            }
            sqlDic["PageShowNum"] = options.AttriList.FirstOrDefault(o => o.key == "PageShowNum").value;
            sqlDic["allowPageNum"] = options.AttriList.FirstOrDefault(l => l.key == "allowPageNum").value;
            sqlDic["clickpagenow"] = pageClick.ToString();
            dataList = comm.TablePage<TaskDriverStru>(tempDataList, ref sqlDic);

            return dataList;
        }



        /// <summary>
        /// 拼接查询条件
        /// </summary>
        /// <param name="dic"></param>
        /// <returns></returns>
        public string SqlStr(Dictionary<string, string> dic)
        {
            string SqlStr = "";
            if (dic.Count != 0)
            {
                SqlStr = " where ";
                foreach (var item in dic)
                {
                    SqlStr += item.Key + "='" + item.Value + "' and ";
                }
                SqlStr = SqlStr.Substring(0, SqlStr.Length - 4);
            }
            return SqlStr;
        }


        /// <summary>
        /// table分页返回数据处理
        /// </summary>
        /// <param name="list"></param>
        /// <param name="clickpage"></param>
        /// <param name="everypage"></param>
        /// <returns></returns>
        public List<TaskDriverStru> TablePage(List<TaskDriverStru> list, int clickpage, int everypage)
        {
            if (list.Count == 0)
                return new List<TaskDriverStru>();
            clickpagenow = clickpage;
            PageNum = (int)Math.Ceiling((double)list.Count / (double)everypage);
            if (clickpagenow > PageNum)
                clickpagenow = PageNum;
            int begin = (clickpagenow - 1) * everypage;
            List<TaskDriverStru> newlist = new List<TaskDriverStru>();

            if (begin < list.Count && begin + everypage >= list.Count)
            {
                for (int i = begin; i < list.Count; i++)
                {
                    newlist.Add(list[i]);
                }
            }
            else
            {
                for (int i = begin; i < begin + everypage; i++)
                {
                    newlist.Add(list[i]);
                }
            }
            return newlist;
        }



            /// <summary>
            /// 获取数据总分页数
            /// </summary>
            /// <returns></returns>
            public int GetPageNum()
        {
            return PageNum;
        }

        /// <summary>
        /// 获取当前点击页
        /// </summary>
        /// <returns></returns>
        public int GetClickPage()
        {
            return clickpagenow;
        }
    }
}
