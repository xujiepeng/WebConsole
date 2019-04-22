using Dapper;
using entConsole.Lib.DbAccess;
using entConsole.Lib.Util;
using entConsole.Models.Conf;
using Microsoft.AspNetCore.Html;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/******************************************************************
* Author: yangzx 
* Date: 2019-3-20
* Content: 通用方法封装类
******************************************************************/
namespace entConsole.Models
{
    public class CommonMethod
    {

        /// <summary>
        /// 获取当前页要显示的数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="clickpage"></param>
        /// <param name="everypage"></param>
        /// <returns></returns>
        public List<T> TablePage<T>(List<T> list, ref Dictionary<string, string> paraDic)
        {
            if (list.Count == 0)
                return new List<T>();
            //每页显示的数据条数
            int everyPage = Convert.ToInt32(paraDic["PageShowNum"]);
            //计算总页数
            paraDic["PageNum"] = ((int)Math.Ceiling((double)list.Count / (double)everyPage)).ToString() ;
            if (Convert.ToInt32( paraDic["clickpagenow"]) > Convert.ToInt32(paraDic["PageNum"]))
                paraDic["clickpagenow"] = paraDic["PageNum"];
            //当前显示页的第一条数据
            int beginData = (Convert.ToInt32(paraDic["clickpagenow"]) - 1) * everyPage;
            List<T> showList = new List<T>();
            //获取当前显示页要显示的所有数据
            if (beginData < list.Count && beginData + everyPage >= list.Count)
            {
                for (int i = beginData; i < list.Count; i++)
                {
                    showList.Add(list[i]);
                }
            }
            else
            {
                for (int i = beginData; i < beginData + everyPage; i++)
                {
                    showList.Add(list[i]);
                }
            }
            return showList;
        }


        /// <summary>
        /// 实现分页样式
        /// </summary>
        /// <param name="clickpagenow"></param>
        /// <param name="PageNum"></param>
        /// <param name="allowPageNum"></param>
        /// <param name="action"></param>
        /// <param name="paraDic"></param>
        /// <returns></returns>
        public static HtmlString PaginationPager(int clickpagenow, int PageNum, int allowPageNum, string controller, string action, Dictionary<string, string> paraDic)
        {
            string paraStr = "";
            foreach (var item in paraDic)
            {
                paraStr += "&" + item.Key + "=" + item.Value;
            }

            StringBuilder stringBuilder = new StringBuilder(15000);
            stringBuilder.Append("<div>");
            stringBuilder.Append("<div>");
            stringBuilder.Append("<nav aria-label=\"Page navigation\">");
            stringBuilder.Append("<ul class=\"pagination\" style=\"float:right\">");

            //当前页数信息标识
            stringBuilder.Append("<li style=\"float:left; margin-right:30px; margin-top:5px\">");
            stringBuilder.Append("<form asp-controller=\"" + controller + "\" asp-action=\"" + action + "\" method=\"get\">");
            stringBuilder.Append("当前第 ");
            stringBuilder.Append(clickpagenow);
            stringBuilder.Append(" 页，共 ");
            stringBuilder.Append(PageNum);
            stringBuilder.Append(" 页，");

            //自定义页数跳转
            stringBuilder.Append("<input type=\"text\" style=\"width:30px;margin-right:5px;\" name=\"pageclick\"/>");
            foreach (var item in paraDic)
            {
                stringBuilder.Append("<input type=\"hidden\" name =\""+ item.Key + "\" value=\"" + item.Value + "\"/>");
                paraStr += "&" + item.Key + "=" + item.Value;
            }
            stringBuilder.Append("<input type=\"submit\" value=\"跳转\" />");
            stringBuilder.Append("</form>");
            stringBuilder.Append("</li>");

            //上一页图标
            if (clickpagenow - 1 > 0)
            {
                int Previous = clickpagenow - 1;
                stringBuilder.Append("<li>");
                stringBuilder.Append("<a href=\"/" + controller + "/" + action + "/id=1?pageclick=");
                stringBuilder.Append(Previous + paraStr);
                stringBuilder.Append("\" aria-label=\"Previous\">");
                stringBuilder.Append("<span aria-hidden=\"true\">&laquo;</span>");
                stringBuilder.Append("</li>");
            }

            //生成中间页面标签
            if (PageNum > allowPageNum)
            {
                if (clickpagenow >= (int)Math.Ceiling(Convert.ToDouble(allowPageNum) / 2) && PageNum >= clickpagenow + 2)
                {
                    for (int i = clickpagenow - 2; i <= clickpagenow + 2; i++)
                    {
                        stringBuilder.Append("<li><a href=\"/" + controller + "/" + action + "/id =1?pageclick=");
                        stringBuilder.Append(i);
                        stringBuilder.Append(paraStr + "\">");
                        stringBuilder.Append(i);
                        stringBuilder.Append("</a></li>");
                    }
                }
                else if (clickpagenow >= (int)Math.Ceiling(Convert.ToDouble(allowPageNum) / 2) && PageNum < clickpagenow + 2)
                {
                    for (int i = PageNum - allowPageNum + 1; i <= PageNum; i++)
                    {
                        stringBuilder.Append("<li><a href=\"/" + controller + "/" + action + "/id=1?pageclick=");
                        stringBuilder.Append(i + paraStr);
                        stringBuilder.Append("\">");
                        stringBuilder.Append(i);
                        stringBuilder.Append("</a></li>");
                    }
                }
                else
                {
                    for (int i = 1; i <= allowPageNum; i++)
                    {
                        stringBuilder.Append("<li><a href=\"/" + controller + "/" + action + "/id=1?pageclick=");
                        stringBuilder.Append(i + paraStr);
                        stringBuilder.Append("\">");
                        stringBuilder.Append(i);
                        stringBuilder.Append("</a></li>");
                    }
                }
            }
            else
            {
                for (int i = 1; i <= PageNum; i++)
                {
                    stringBuilder.Append("<li><a href=\"/" + controller + "/" + action + "/id=1?pageclick=");
                    stringBuilder.Append(i + paraStr);
                    stringBuilder.Append("\">");
                    stringBuilder.Append(i);
                    stringBuilder.Append("</a></li>");
                }
            }

            //下一页图标
            if (clickpagenow + 1 <= PageNum)
            {
                int next = clickpagenow + 1;
                stringBuilder.Append("<li>");
                stringBuilder.Append("<a href=\"/" + controller + "/" + action + "/id=1?pageclick=");
                stringBuilder.Append(next+ paraStr);
                stringBuilder.Append("\" aria-label=\"Next\">");
                stringBuilder.Append("<span aria-hidden=\"true\">&raquo;</span>");
                stringBuilder.Append("</a>");
                stringBuilder.Append("</li>");
            }

            stringBuilder.Append("</ul>");
            stringBuilder.Append("</nav>");
            stringBuilder.Append("</div>");
            stringBuilder.Append("</div>");

            return new HtmlString(stringBuilder.ToString());
        }

        /// <summary>
        /// ORM数据映射
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="connConfig"></param>
        /// <returns></returns>
        public IEnumerable<T> Select<T>(string sql, DbConnStru connConfig)
        {
            IEnumerable<T> list = null;

            using (IDbConnection conn = DapperAcc.GetConnection(connConfig))
            {
                try
                {
                    //传统sql in (1, 2, 3)写法
                    //conn.Query<TestTable>("SELECT * FROM TestTable  WHERE id IN (@ids) ",new { ids = IDs.ToArray()})
                    list = conn.Query<T>(sql);
                    //list = conn.Query<DataInfoStru>(sql, new { id = ID });
                }
                catch (Exception ex)
                {
                    Log4NetUtil.Error(this, "Select->" + ex.ToString());
                }
            }
            return list;
        }




    }
}
