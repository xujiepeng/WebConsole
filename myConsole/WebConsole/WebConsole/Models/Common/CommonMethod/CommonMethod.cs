using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebConsole.Models.Common
{
    public class CommonMethod
    {
        /// <summary>
        /// 拼接查询条件(包含时间)
        /// </summary>
        /// <param name="dic"></param>
        /// <returns></returns>
        public static string SqlStrWithTime(Dictionary<string, string> dic)
        {
            string SqlStr = "";
            if (dic.Count != 0)
            {
                SqlStr = " where ";
                foreach (var item in dic)
                {
                    if (item.Key == "begintime")
                    {
                        SqlStr += "DateTime >='" + item.Value + "' and ";
                    }
                    else if (item.Key == "endtime")
                    {
                        SqlStr += "DateTime <='" + item.Value + "' and ";
                    }
                    else if (item.Key.ToLower() == "username")
                    {
                        SqlStr += item.Key + " like '%" + item.Value + "%' and ";
                    }
                    else if (item.Key.ToLower() == "states")
                    {
                        if (item.Value != "ALL")
                            SqlStr += item.Key + "='" + item.Value + "' and ";
                        else
                            SqlStr += " 1 = 1 and ";
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
