using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WebConsole.Models.Model;
using WebConsole.Models.Model.Conf;

namespace WebConsole.Models
{
    public class MemberList_DynamicViewModel
    {
        public List<MemberList_DynamicStru> data;
        static string DBLink = "";
        public string code;
        public string msg;
        public string count;
        public MemberList_DynamicStru memberlist_dynamic = new MemberList_DynamicStru();

        /// <summary>
        /// 查询数据
        /// </summary>
        /// <returns></returns>
        public List<MemberList_DynamicStru> GetData(CommConf options, Dictionary<string, string> Para, ref string count)
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
            string strsql = string.Format(@"select * from(select * from (select * from MemberList_Dynamic {0} order by id asc limit {1})
            order by id desc limit {2}) order by id asc", strPara, int.Parse(Para["page"]) * int.Parse(Para["limit"]), Para["limit"]);
            DataTable dt = conn.QueryDt(strsql, out errorMsg);
            MemberList_DynamicStru obj = new MemberList_DynamicStru();
            List<MemberList_DynamicStru> DataList = new List<MemberList_DynamicStru>();
            foreach (DataRow item in dt.Rows)
            {
                obj = new MemberList_DynamicStru();
                obj.id = int.Parse(item["id"].ToString());
                obj.username = item["username"].ToString();
                obj.sex = item["sex"].ToString();
                obj.tel = item["tel"].ToString();
                obj.addr = item["addr"].ToString();
                obj.states = item["states"].ToString();
                DataList.Add(obj);
            }
            //是否是条件查询，统计条数
            if (ispara == false)
            {
                strsql = "select count(*) from MemberList_Dynamic";
            }
            else
            {
                strsql = string.Format(@"select count(*) from MemberList_Dynamic {0}", strPara);
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
        public bool UpdateData(CommConf options, MemberList_DynamicStru objstru)
        {
            string errorMsg;
            SqliteAccess conn = new SqliteAccess(options.AttriList.FirstOrDefault(o => o.key == "DBLink").value);
            string str = string.Format("update MemberList_Dynamic set sex = '{0}', tel = '{1}', addr = '{2}', states = '{3}' where username='{4}'", objstru.sex, objstru.tel, objstru.addr, objstru.states, objstru.username);
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
        public bool DeleteData(CommConf options, MemberList_DynamicStru objstru)
        {
            string errorMsg;
            SqliteAccess conn = new SqliteAccess(options.AttriList.FirstOrDefault(o => o.key == "DBLink").value);
            string str = string.Format("delete from MemberList_Dynamic where id='{0}'", objstru.id);
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
            string str = string.Format("delete from MemberList_Dynamic where id in ({0})", deleteNom);
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
