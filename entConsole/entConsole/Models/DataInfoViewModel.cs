using Dapper;
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
* Content: DataInfoViewModel
******************************************************************/
namespace entConsole.Models
{
    public class DataInfoViewModel
    {
        #region 属性
        /// <summary>
        /// 当前table页显示数据
        /// </summary>
        public List<DataInfoStru> dataInfo = new List<DataInfoStru>();
        /// <summary>
        /// 当前页面数量 (当前table中一共有多少页)
        /// </summary>
        //public int PageNum = 1;
        /// <summary>
        /// 当前点击页数
        /// </summary>
        //public int clickpagenow;
        /// <summary>
        /// 允许显示的可选择页数
        /// </summary>
        //public int allowPageNum = 5;
        /// <summary>
        /// Url参数列表
        /// </summary>
        public Dictionary<string, string> paraDic = new Dictionary<string, string>();
        #endregion

        #region 读取DataInfoStru
        /// <summary>
        /// Sqlite加载DataInfoStru
        /// </summary>
        /// <param name="options"></param>
        /// <param name="pageclick">当前点击页</param>
        /// <param name="sqlDic">过滤器条件</param>
        /// <returns></returns>
        public List<DataInfoStru> GetDataInfo(CommConf options,int pageClick, ref Dictionary<string, string> sqlDic)
        {
            //查询数据总数
            List<DataInfoStru> tempDataInfo = new List<DataInfoStru>();
            CommonMethod comm = new CommonMethod();
            string errorMsg = string.Empty;
            string sql = @"SELECT * FROM TaskRunLog" + SqlStr(sqlDic) + " order by RecordTime desc;";
            DbConnStru SqlitePath = options.DbConnList.FirstOrDefault(o => o.key == "SqliteConn_1");

            var list= comm.Select<DataInfoStru>(sql, SqlitePath);
            if (!string.IsNullOrEmpty(errorMsg))
            {
                return null;
            }

            foreach (DataInfoStru item in list)
            {
                tempDataInfo.Add(item);
            }

            sqlDic["PageShowNum"] = options.AttriList.FirstOrDefault(o => o.key == "PageShowNum").value;
            sqlDic["allowPageNum"] = options.AttriList.FirstOrDefault(l => l.key == "allowPageNum").value;
            sqlDic["clickpagenow"] = pageClick.ToString();
            
            dataInfo = comm.TablePage<DataInfoStru>(tempDataInfo, ref sqlDic);
            return dataInfo;
        }
        #endregion



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
                    if (item.Key == "infoTime")
                    {
                        SqlStr += item.Key + " >='" + item.Value + "' and ";
                    }
                    else
                    {
                        SqlStr += item.Key + "='" + item.Value + "' and ";
                    }
                    
                }
                SqlStr = SqlStr.Substring(0, SqlStr.Length - 4);
            }
            return SqlStr;
        }
    }
}
