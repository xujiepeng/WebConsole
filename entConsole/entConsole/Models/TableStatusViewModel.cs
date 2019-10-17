using entConsole.Lib.DbAccess;
using entConsole.Lib.Util;
using entConsole.Models.Conf;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

/******************************************************************
* Author: yangzx 
* Date: 2019-3-20
* Content: TableStatusViewModel
******************************************************************/
namespace entConsole.Models
{
    public class TableStatusViewModel
    {
        #region 属性
        /// <summary>
        /// 当前table页显示数据
        /// </summary>
        public List<TableStatusStru> table = new List<TableStatusStru>();
        /// <summary>
        /// 
        /// </summary>
        public Dictionary<string, string> paraDic = new Dictionary<string, string>();
        #endregion

        #region 读取TableStatusStru
        /// <summary>
        /// Sqlite加载TableStatusStru
        /// </summary>
        /// <param name="options"></param>
        /// <param name="sqlDic"></param>
        /// <returns></returns>
        public List<TableStatusStru> GetTableStatus(CommConf options, int pageClick, ref Dictionary<string, string> sqlDic)
        {
            // 查询数据总数
            List<TableStatusStru> tempDataList = new List<TableStatusStru>();
            string errorMsg = string.Empty;
            CommonMethod comm = new CommonMethod();
            string sql = @"select a.taskid,a.UserName,b.laststarttime,b.TotalDataPacketNum,
                            b.TotalRecordNum,b.FinishDataPacketNum,b.FinishRecordNum,b.TaskStatus
                            from taskinfo a left join taskstatus b on a.ID = b.id " 
                            + SqlStr(sqlDic) + "order by laststarttime desc";
            //string sql = @"SELECT * FROM TableStatus " + SqlStr(sqlDic);
            DbConnStru SqlitePath = options.DbConnList.FirstOrDefault(o => o.key == "SqliteConn_1");

            var list = comm.Select<TableStatusStru>(sql, SqlitePath);
            if (!string.IsNullOrEmpty(errorMsg))
            {
                return null;
            }

            foreach (TableStatusStru item in list)
            {
                tempDataList.Add(item);
            }

            sqlDic["PageShowNum"] = options.AttriList.FirstOrDefault(o => o.key == "PageShowNum").value;
            sqlDic["allowPageNum"] = options.AttriList.FirstOrDefault(l => l.key == "allowPageNum").value;
            sqlDic["clickpagenow"] = pageClick.ToString();
            
            table = comm.TablePage<TableStatusStru>(tempDataList, ref sqlDic);
            return table;
        }
        #endregion




        //public List<TableStatusStru> GetTableStatus1(CommConf options, int pageClick, ref Dictionary<string, string> sqlDic)
        //{
        //    // 查询数据总数
        //    List<TableStatusStru> tempDataList = new List<TableStatusStru>();
        //    string errorMsg = string.Empty;

        //    string sql = @"SELECT * FROM TableStatus " + SqlStr(sqlDic);
        //    string AppBasePath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
        //    string SqlitePath = Path.Combine(AppBasePath, "finchina.db3");
        //    SqliteAccess sqliteAccess = new SqliteAccess(SqlitePath);
        //    DataTable dt = sqliteAccess.QueryDt(sql, out errorMsg);

        //    if (!string.IsNullOrEmpty(errorMsg))
        //    {
        //        return null;
        //    }

        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {
        //        TableStatusStru obj = new TableStatusStru();
        //        obj.userName = ConvertUtil.ParseString(dt.Rows[i]["userName"]);
        //        obj.tableName = ConvertUtil.ParseString(dt.Rows[i]["tableName"]);
        //        obj.lastRunTime = ConvertUtil.ParseString(dt.Rows[i]["lastRunTime"]);
        //        obj.totalNum = ConvertUtil.ParseString(dt.Rows[i]["totalNum"]);
        //        obj.successNum = ConvertUtil.ParseString(dt.Rows[i]["successNum"]);
        //        obj.Status = ConvertUtil.ParseString(dt.Rows[i]["Status"]);
        //        obj.Describe = ConvertUtil.ParseString(dt.Rows[i]["Describe"]);
        //        tempDataList.Add(obj);
        //    }
        //    sqlDic["PageShowNum"] = options.AttriList.FirstOrDefault(o => o.key == "PageShowNum").value;
        //    sqlDic["allowPageNum"] = options.AttriList.FirstOrDefault(l => l.key == "allowPageNum").value;
        //    sqlDic["clickpagenow"] = pageClick.ToString();
        //    CommonMethod comm = new CommonMethod();
        //    table = comm.TablePage<TableStatusStru>(tempDataList, ref sqlDic);
        //    //table = TablePage(tempDataList, pageclick, Convert.ToInt32(options.AttriList.FirstOrDefault(o => o.key == "PageShowNum").value));
        //    return table;
        //}
        




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
        //public List<TableStatusStru> TablePage(List<TableStatusStru> list, int clickpage, int everypage)
        //{
        //    if (list.Count == 0)
        //        return new List<TableStatusStru>();
        //    clickpagenow = clickpage;
        //    //int everypage = 2;
        //    PageNum = (int)Math.Ceiling((double)list.Count / (double)everypage);
        //    if (clickpagenow > PageNum)
        //        clickpagenow = PageNum;
        //    int begin = (clickpagenow - 1) * everypage;
        //    List<TableStatusStru> newlist = new List<TableStatusStru>();

        //    if (begin < list.Count && begin + everypage >= list.Count)
        //    {
        //        for (int i = begin; i < list.Count; i++)
        //        {
        //            newlist.Add(list[i]);
        //        }
        //    }
        //    else
        //    {
        //        for (int i = begin; i < begin + everypage; i++)
        //        {
        //            newlist.Add(list[i]);
        //        }
        //    }
        //    return newlist;
        //}

        
    }
}
