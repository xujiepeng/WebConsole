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
        public List<MemberList_DynamicStru> GetData(CommConf options)
        {
            string errorMsg;
            SqliteAccess conn = new SqliteAccess(options.AttriList.FirstOrDefault(o => o.key == "DBLink").value);
            DataTable dt = conn.QueryDt("select * from MemberList_Dynamic order by ID", out errorMsg);
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

            return DataList;
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
    }
}
