using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

/// <summary>
/// 数据验证方法
/// </summary>
namespace WebConsole.Models.Common
{
    public class Verify
    {
        /// <summary>
        /// <br>转换成\r\n
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static string html_txt_rn(string txt)
        {
            if (string.IsNullOrEmpty(txt))
            {
                return string.Empty;
            }
            else
            {
                return txt.Replace("<br>", "\r\n");
            }
        }

        /// <summary>
        /// 文本中的\n转换成<br>
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static string txt_html_n(string txt)
        {
            if (string.IsNullOrEmpty(txt))
            {
                return string.Empty;
            }
            else
            {
                return txt.Replace("\n", "<br>");
            }
        }

        /// <summary>
        /// \r\n转换成<br>
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static string txt_html_rn(string txt)
        {
            if (string.IsNullOrEmpty(txt))
            {
                return string.Empty;
            }
            else
            {
                return txt.Replace("\r\n", "<br>");
            }
        }

        /// <summary>
        /// 文本中的<br>转换成\n
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static string html_txt_n(string txt)
        {
            if (string.IsNullOrEmpty(txt))
            {
                return string.Empty;
            }
            else
            {
                return txt.Replace("<br>", "\n");
            }
        }
        
    }
}
