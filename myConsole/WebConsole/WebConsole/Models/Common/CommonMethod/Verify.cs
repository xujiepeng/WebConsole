using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            return txt.Replace("<br>", "\r\n");
        }

        /// <summary>
        /// \n转换成<br>
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static string txt_html_n(string txt)
        {
            return txt.Replace("\n", "<br>");
        }

        /// <summary>
        /// \r\n转换成<br>
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static string txt_html_rn(string txt)
        {
            return txt.Replace("\r\n", "<br>");
        }

        /// <summary>
        /// \r\n转换成<br>
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static string html_txt_n(string txt)
        {
            return txt.Replace("<br>", "\n");
        }
    }
}
