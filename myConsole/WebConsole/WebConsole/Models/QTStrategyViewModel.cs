using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WebConsole.Models.Model.Conf;

namespace WebConsole.Models.Model
{
    public class QTStrategyViewModel
    {
        public List<QTStrategyStru> data;
        public string code;
        public string msg;
        public string count;
        public QTStrategyStru qtstrategy = new QTStrategyStru();

        /// <summary>
        /// 查询数据
        /// </summary>
        /// <returns></returns>
        public List<QTStrategyStru> GetData(CommConf options, Dictionary<string, string> Para, ref string count)
        {
            string errorMsg;
            SqliteAccess conn = new SqliteAccess(options.AttriList.FirstOrDefault(o => o.key == "DBLink").value);
            bool ispara = false;
            //拼接sql查询条件
            string strPara = " where 1=1 and ";
            foreach (var item in Para)
            {
                if (!string.IsNullOrEmpty(item.Value) && item.Key != "page" && item.Key != "limit")
                {
                    strPara += item.Key + "='" + item.Value + "' and ";
                    ispara = true;
                }
            }
            strPara = strPara.Substring(0, strPara.Length - 4);
            string strsql = string.Format(@"select * from(select * from (select * from QTStrategy {0} order by id asc limit {1})
            order by id desc limit {2}) order by id asc", strPara, int.Parse(Para["page"]) * int.Parse(Para["limit"]), Para["limit"]);
            DataTable dt = conn.QueryDt(strsql, out errorMsg);
            QTStrategyStru obj = new QTStrategyStru();
            List<QTStrategyStru> DataList = new List<QTStrategyStru>();
            foreach (DataRow item in dt.Rows)
            {
                obj = new QTStrategyStru();
                obj.id = int.Parse(item["id"].ToString());
                obj.strategyname = item["strategyname"].ToString();
                obj.strategynumber = item["strategynumber"].ToString();
                obj.strategypath = item["strategypath"].ToString();
                obj.creattime = item["creattime"].ToString();
                obj.describe = item["describe"].ToString();
                obj.remark = item["remark"].ToString();
                obj.states = item["states"].ToString();
                obj.isrun = item["isrun"].ToString();
                DataList.Add(obj);
            }
            //是否是条件查询，统计条数
            if (ispara == false)
            {
                strsql = "select count(*) from QTStrategy";
            }
            else
            {
                strsql = string.Format(@"select count(*) from QTStrategy {0}", strPara);
            }
            DataTable dtcount = conn.QueryDt(strsql, out errorMsg);
            count = dtcount.Rows[0][0].ToString();
            return DataList;
        }

        /// <summary>
        /// 新增数据
        /// </summary>
        /// <returns></returns>
        public bool AddData(CommConf options, MemberList_DynamicStru objstru)
        {
            string errorMsg;
            SqliteAccess conn = new SqliteAccess(options.AttriList.FirstOrDefault(o => o.key == "DBLink").value);
            string str = string.Format("insert into MemberList_Dynamic (username,sex,tel,addr,states) values('{0}','{1}','{2}','{3}','{4}')", objstru.username, objstru.sex, objstru.tel, objstru.addr, objstru.states);
            int count = conn.Execute(str, out errorMsg);
            if (count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 编辑数据
        /// </summary>
        /// <returns></returns>
        public bool UpdateData(CommConf options, QTStrategyStru objstru)
        {
            string errorMsg;
            SqliteAccess conn = new SqliteAccess(options.AttriList.FirstOrDefault(o => o.key == "DBLink").value);
            string str = string.Format("update QTStrategy set strategyname = '{0}', describe = '{1}', states = '{2}', remark = '{3}' where id='{4}'", objstru.strategyname, objstru.describe, objstru.states, objstru.remark, objstru.id);
            int count = conn.Execute(str, out errorMsg);
            if (count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <returns></returns>
        public bool DeleteData(CommConf options, QTStrategyStru objstru)
        {
            string errorMsg;
            SqliteAccess conn = new SqliteAccess(options.AttriList.FirstOrDefault(o => o.key == "DBLink").value);
            string str = string.Format("delete from QTStrategy where id='{0}'", objstru.id);
            int count = conn.Execute(str, out errorMsg);
            if (count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 批量删除数据
        /// </summary>
        /// <returns></returns>
        public bool DeleteDataAll(CommConf options, string[] ids)
        {
            string errorMsg;
            string deleteNom = "";
            foreach (var id in ids)
            {
                deleteNom += "'" + id + "',";
            }
            if (deleteNom.Length > 0)
            {
                deleteNom = deleteNom.Substring(0, deleteNom.Length - 1);
            }
            SqliteAccess conn = new SqliteAccess(options.AttriList.FirstOrDefault(o => o.key == "DBLink").value);
            string str = string.Format("delete from QTStrategy where id in ({0})", deleteNom);
            int count = conn.Execute(str, out errorMsg);
            if (count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
