using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;
using WebConsole.Models.Model;

namespace WebConsole.Models
{
    public class MemberList_StaticViewModel
    {
        public object a;
        public List<MemberList_StaticStru> staticstru;
        public string c;
        public MemberList_StaticStru memberlist_static = new MemberList_StaticStru();

        /// <summary>
        /// 查询数据
        /// </summary>
        /// <returns></returns>
        public List<MemberList_StaticStru> GetDate()
        {
            string errorMsg;
            SqliteAccess conn = new SqliteAccess(@"E:\MyGit\WebConsole\myConsole\doc\ConsoleData.db3");
            DataTable dt = conn.QueryDt("select * from MemberList_Static order by ID", out errorMsg);
            MemberList_StaticStru obj = new MemberList_StaticStru();
            staticstru = new List<MemberList_StaticStru>();
            foreach (DataRow item in dt.Rows)
            {
                obj.ID = int.Parse(item["id"].ToString());
                obj.username = item["username"].ToString();
                obj.sex = item["sex"].ToString();
                obj.tel = item["tel"].ToString();
                obj.addr = item["addr"].ToString();
                obj.Static = item["Static"].ToString();
                staticstru.Add(obj);
                obj = new MemberList_StaticStru();
            }
            return staticstru;
        }

        /// <summary>
        /// 新增数据
        /// </summary>
        /// <returns></returns>
        public bool AddData(MemberList_StaticStru objstru)
        {
            string errorMsg;
            SqliteAccess conn = new SqliteAccess(@"E:\MyGit\WebConsole\myConsole\doc\ConsoleData.db3");
            string str = string.Format("insert into MemberList_Static (username,sex,tel,addr,Static) values('{0}','{1}','{2}','{3}','{4}')", objstru.username, objstru.sex, objstru.tel, objstru.addr, objstru.Static);
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
        public bool UpdateData(MemberList_StaticStru objstru)
        {
            string errorMsg;
            SqliteAccess conn = new SqliteAccess(@"E:\MyGit\WebConsole\myConsole\doc\ConsoleData.db3");
            string str = string.Format("update MemberList_Static set sex = '{0}', tel = '{1}', addr = '{2}', Static = '{3}' where username='{4}'", objstru.sex, objstru.tel, objstru.addr, objstru.Static, objstru.username);
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
        public bool DeleteData(MemberList_StaticStru objstru)
        {
            string errorMsg;
            SqliteAccess conn = new SqliteAccess(@"E:\MyGit\WebConsole\myConsole\doc\ConsoleData.db3");
            string str = string.Format("delete from MemberList_Static where id='{0}'", objstru.ID);
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