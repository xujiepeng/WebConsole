using entConsole.Models.Conf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace entConsole.Models
{
    public class DbColTypeConfigViewModel
    {
        #region 属性
        /// <summary>
        /// 当前table页显示数据
        /// </summary>
        public List<DbColTypeConfigStru> dataInfo = new List<DbColTypeConfigStru>();
        /// <summary>
        /// Url参数列表
        /// </summary>
        public Dictionary<string, string> paraDic = new Dictionary<string, string>();
        #endregion


        #region Sqlite加载显示的数据
        /// <summary>
        /// Sqlite加载显示的数据
        /// </summary>
        /// <param name="options"></param>
        /// <param name="pageclick">当前点击页</param>
        /// <param name="sqlDic">过滤器条件</param>
        /// <returns></returns>
        public List<DbColTypeConfigStru> GetDataInfo(CommConf options, int pageClick, ref Dictionary<string, string> sqlDic)
        {
            //查询数据总数
            List<DbColTypeConfigStru> tempDataInfo = new List<DbColTypeConfigStru>();
            CommonMethod comm = new CommonMethod();
            string errorMsg = string.Empty;
            string sql = @"SELECT * FROM DbColTypeConfig";

            DbConnStru SqlitePath = options.DbConnList.FirstOrDefault(o => o.key == "SqliteConn_1");

            var list = comm.Select<DbColTypeConfigStru>(sql, SqlitePath);
            if (!string.IsNullOrEmpty(errorMsg))
            {
                return null;
            }

            foreach (DbColTypeConfigStru item in list)
            {
                tempDataInfo.Add(item);
            }

            sqlDic["PageShowNum"] = options.AttriList.FirstOrDefault(o => o.key == "PageShowNum").value;
            sqlDic["allowPageNum"] = options.AttriList.FirstOrDefault(l => l.key == "allowPageNum").value;
            sqlDic["clickpagenow"] = pageClick.ToString();

            dataInfo = comm.TablePage<DbColTypeConfigStru>(tempDataInfo, ref sqlDic);
            return dataInfo;
        }
        #endregion
    }
}
