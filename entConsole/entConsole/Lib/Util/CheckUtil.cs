using System.Text.RegularExpressions;

/******************************************************************
 * Author: miaoxin 
 * Date: 2018-10-16
 * Content: 参数校验类
 ******************************************************************/

namespace entConsole.Lib.Util
{
    /// <summary>
    /// 参数校验类
    /// </summary>
    public class CheckUtil
    {
        #region 单引号处理,避免操作数据库时出现错误
        /// <summary>
        /// 单引号处理,避免操作数据库时出现错误
        /// </summary>
        /// <param name="strPara"></param>
        /// <returns></returns>
        public static string CheckQuotes(string strPara)
        {
            if (string.IsNullOrEmpty(strPara))
            {
                return "";
            }

            return strPara.Replace("'", "''");
        }
        #endregion

        #region 正则表达式匹配 Email 格式字符串
        /// <summary>
        /// 正则表达式匹配 Email 格式字符串
        /// </summary>
        /// <param name="strPara"></param>
        /// <returns></returns>
        public static bool CheckEmail(string strPara)
        {
            if (string.IsNullOrEmpty(strPara))
            {
                return false;
            }

            Regex reg = new Regex(@"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$");

            return reg.IsMatch(strPara);
        }
        #endregion

        #region 正则表达式匹配 Url 格式字符串
        /// <summary>
        /// 正则表达式匹配 Url 格式字符串
        /// </summary>
        /// <param name="strPara"></param>
        /// <returns></returns>
        public static bool CheckUrl(string strPara)
        {
            if (string.IsNullOrEmpty(strPara))
            {
                return false;
            }

            Regex reg = new Regex(@"^[a-zA-z]+://[^\s]+$");

            return reg.IsMatch(strPara);
        }
        #endregion

        #region 正则表达式匹配 IP 格式字符串
        /// <summary>
        /// 正则表达式匹配 IP 格式字符串
        /// </summary>
        /// <param name="strPara"></param>
        /// <returns></returns>
        public static bool CheckIP(string strPara)
        {
            if (string.IsNullOrEmpty(strPara))
            {
                return false;
            }

            Regex reg = new Regex(@"^\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}$");

            return reg.IsMatch(strPara);
        }
        #endregion

        #region 正则表达式匹配 数字 格式字符串
        /// <summary>
        /// 正则表达式匹配 数字 格式字符串
        /// </summary>
        /// <param name="strPara"></param>
        /// <returns></returns>
        public static bool CheckNumber(string strPara)
        {
            if (string.IsNullOrEmpty(strPara))
            {
                return false;
            }

            Regex reg = new Regex(@"^-?\d+\.?\d*$");

            return reg.IsMatch(strPara);
        }

        /// <summary>
        /// 正则表达式匹配 数字 格式字符串
        /// </summary>
        /// <param name="strPara"></param>
        /// <returns></returns>
        public static bool CheckNo(string strPara)
        {
            if (string.IsNullOrEmpty(strPara))
            {
                return false;
            }

            Regex reg = new Regex(@"^[0-9]\d*$");

            return reg.IsMatch(strPara);
        }
        #endregion

        #region 正则表达式匹配 日期 格式字符串
        /// <summary>
        /// 正则表达式匹配 日期 格式字符串 (yyyy-MM-dd)
        /// </summary>
        /// <param name="strPara"></param>
        /// <returns></returns>
        public static bool CheckDate(string strPara)
        {
            if (string.IsNullOrEmpty(strPara))
            {
                return false;
            }

            Regex reg = new Regex(@"^[1-9][0-9]{3}-[0|1][0-9]-[0-3][0-9]$");

            return reg.IsMatch(strPara);
        }

        /// <summary>
        /// 正则表达式匹配 日期 格式字符串 (yyyy-MM-dd HH:mm)
        /// </summary>
        /// <param name="strPara"></param>
        /// <returns></returns>
        public static bool CheckDateTime(string strPara)
        {
            if (string.IsNullOrEmpty(strPara))
            {
                return false;
            }

            Regex reg = new Regex(@"^[1-9][0-9]{3}-[0|1][0-9]-[0-3][0-9] [0-2][0-9]:[0-5][0-9]$");

            return reg.IsMatch(strPara);
        }

        /// <summary>
        /// 正则表达式匹配 日期 格式字符串 (yyyy-MM-dd HH:mm:ss)
        /// </summary>
        /// <param name="strPara"></param>
        /// <returns></returns>
        public static bool CheckDateTime2(string strPara)
        {
            if (string.IsNullOrEmpty(strPara))
            {
                return false;
            }

            Regex reg = new Regex(@"^[1-9][0-9]{3}-[0|1][0-9]-[0-3][0-9] [0-2][0-9]:[0-5][0-9]:[0-5][0-9]$");

            return reg.IsMatch(strPara);
        }

        /// <summary>
        /// 正则表达式匹配 日期 格式字符串 (MM-dd HH:mm:ss)
        /// </summary>
        /// <param name="strPara"></param>
        /// <returns></returns>
        public static bool CheckDateTime3(string strPara)
        {
            if (string.IsNullOrEmpty(strPara))
            {
                return false;
            }

            Regex reg = new Regex(@"^[0|1][0-9]-[0-3][0-9] [0-2][0-9]:[0-5][0-9]:[0-5][0-9]$");

            return reg.IsMatch(strPara);
        }

        /// <summary>
        /// 正则表达式匹配 日期 格式字符串（yyyy-MM-ddTHH:mm:ss.SSSZ）
        /// </summary>
        /// <param name="strPara"></param>
        /// <returns></returns>
        public static bool CheckDateTime4(string strPara)
        {
            if (string.IsNullOrEmpty(strPara))
            {
                return false;
            }

            Regex reg = new Regex(@"^[1-9][0-9]{3}-[0|1][0-9]-[0-3][0-9][T][0-2][0-9]:[0-5][0-9]:[0-5][0-9][/.][0-9]{3}[Z]$");

            return reg.IsMatch(strPara);
        }

        /// <summary>
        /// 正则表达式匹配 时间 格式字符串 (HH:mm)
        /// </summary>
        /// <param name="strPara"></param>
        /// <returns></returns>
        public static bool CheckTime(string strPara)
        {
            if (string.IsNullOrEmpty(strPara))
            {
                return false;
            }

            Regex reg = new Regex(@"^[0-2][0-9]:[0-5][0-9]$");

            return reg.IsMatch(strPara);
        }

        /// <summary>
        /// 正则表达式匹配 时间 格式字符串 (HH:mm:ss)
        /// </summary>
        /// <param name="strPara"></param>
        /// <returns></returns>
        public static bool CheckTime2(string strPara)
        {
            if (string.IsNullOrEmpty(strPara))
            {
                return false;
            }

            Regex reg = new Regex(@"^[0-2][0-9]:[0-5][0-9]:[0-5][0-9]$");

            return reg.IsMatch(strPara);
        }

        /// <summary>
        /// 正则表达式匹配 频率 格式字符串 (HH:mm-HH:mm|mm)
        /// </summary>
        /// <param name="strPara"></param>
        /// <returns></returns>
        public static bool CheckFrequency(string strPara)
        {
            if (string.IsNullOrEmpty(strPara))
            {
                return false;
            }

            Regex reg = new Regex(@"^[0-2][0-9]:[0-5][0-9]-[0-2][0-9]:[0-5][0-9]\|[1-9]\d*$");

            return reg.IsMatch(strPara);
        }

        /// <summary>
        /// 正则表达式匹配 频率 格式字符串 (HH:mm:ss-HH:mm:ss|ss)
        /// </summary>
        /// <param name="strPara"></param>
        /// <returns></returns>
        public static bool CheckFrequency2(string strPara)
        {
            if (string.IsNullOrEmpty(strPara))
            {
                return false;
            }

            Regex reg = new Regex(@"^[0-2][0-9]:[0-5][0-9]:[0-5][0-9]-[0-2][0-9]:[0-5][0-9]:[0-5][0-9]\|[1-9]\d*$");

            return reg.IsMatch(strPara);
        }

        #endregion

        #region 正则表达式匹配 正整数(包括0) 格式字符串
        /// <summary>
        /// 正则表达式匹配 正整数(包括0) 格式字符串
        /// </summary>
        /// <param name="strPara"></param>
        /// <returns></returns>
        public static bool CheckPlusInt(string strPara)
        {
            if (string.IsNullOrEmpty(strPara))
            {
                return false;
            }

            Regex reg = new Regex(@"^[1-9]\d*|0$");

            return reg.IsMatch(strPara);
        }
        #endregion

        #region 正则表达式匹配 负整数(包括0) 格式字符串
        /// <summary>
        /// 正则表达式匹配 负整数(包括0) 格式字符串
        /// </summary>
        /// <param name="strPara"></param>
        /// <returns></returns>
        public static bool CheckSubtractInt(string strPara)
        {
            if (string.IsNullOrEmpty(strPara))
            {
                return false;
            }

            Regex reg = new Regex(@"^-[1-9]\d*|0$");

            return reg.IsMatch(strPara);
        }
        #endregion

        #region 正则表达式匹配 整数(包括0) 格式字符串
        /// <summary>
        /// 正则表达式匹配 整数(包括0) 格式字符串
        /// </summary>
        /// <param name="strPara"></param>
        /// <returns></returns>
        public static bool CheckInt(string strPara)
        {
            if (string.IsNullOrEmpty(strPara))
            {
                return false;
            }

            Regex reg = new Regex(@"^-?[1-9]\d*|0$");

            return reg.IsMatch(strPara);
        }
        #endregion

        #region 正则表达式匹配 正浮点数(包括0，0.00) 格式字符串
        /// <summary>
        /// 正则表达式匹配 正浮点数(包括0，0.00) 格式字符串
        /// </summary>
        /// <param name="strPara"></param>
        /// <returns></returns>
        public static bool CheckPlusFloat(string strPara)
        {
            if (string.IsNullOrEmpty(strPara))
            {
                return false;
            }

            Regex reg = new Regex(@"^[1-9]\d*\.\d*|0\.\d*|0$");

            return reg.IsMatch(strPara);
        }
        #endregion

        #region 正则表达式匹配 负浮点数(包括0，0.00) 格式字符串
        /// <summary>
        /// 正则表达式匹配 负浮点数(包括0，0.00) 格式字符串
        /// </summary>
        /// <param name="strPara"></param>
        /// <returns></returns>
        public static bool CheckSubtractFloat(string strPara)
        {
            if (string.IsNullOrEmpty(strPara))
            {
                return false;
            }

            Regex reg = new Regex(@"^-[1-9]\d*\.\d*|-0\.\d*[1-9]\d*|0\.0+|0$");

            return reg.IsMatch(strPara);
        }
        #endregion

        #region 正则表达式匹配 浮点数(包括0，0.00) 格式字符串
        /// <summary>
        /// 正则表达式匹配 浮点数(包括0，0.00) 格式字符串
        /// </summary>
        /// <param name="strPara"></param>
        /// <returns></returns>
        public static bool CheckFloat(string strPara)
        {
            if (string.IsNullOrEmpty(strPara))
            {
                return false;
            }

            Regex reg = new Regex(@"^-?[1-9]\d*\.\d*|-?0\.\d*[1-9]\d*|0\.0+|0$");

            return reg.IsMatch(strPara);
        }
        #endregion

        #region 正则表达式匹配 汉字
        /// <summary>
        /// 正则表达式匹配 汉字
        /// </summary>
        /// <param name="strPara"></param>
        /// <returns></returns>
        public static bool CheckChinese(string strPara)
        {
            if (string.IsNullOrEmpty(strPara))
            {
                return false;
            }

            Regex reg = new Regex(@"[\u4e00-\u9fa5]");

            return reg.IsMatch(strPara);
        }
        #endregion

        #region 正则表达式取得 字符字节数
        /// <summary>
        /// 正则表达式取得 字符字节数
        /// </summary>
        /// <param name="strPara"></param>
        /// <returns></returns>
        public static int GetCharacterByte(string strPara)
        {
            if (string.IsNullOrEmpty(strPara))
            {
                return 0;
            }

            //匹配双字节字符(包括汉字在内)
            Regex reg = new Regex(@"[^\x00-\xff]");

            if (reg.IsMatch(strPara))
            {
                return 2;
            }
            else
            {
                return 1;
            }
        }
        #endregion

        #region 正则表达式替换 所有html标签(包括&nbsp;)
        /// <summary>
        /// 正则表达式匹配 所有html标签(包括&nbsp;)
        /// </summary>
        /// <param name="strPara"></param>
        /// <returns></returns>
        public static string ReplaceHtmlTag(string strPara)
        {
            if (string.IsNullOrEmpty(strPara))
            {
                return "";
            }

            return Regex.Replace(strPara, @"<[^>]+>", "")
                .Replace("&nbsp;", " ")
                .Replace("&ldquo;", "“")
                .Replace("&rdquo;", "”")
                .Replace("&lt;", "<")
                .Replace("&gt;", ">")
                .Replace("&quot;", "\"")
                .Replace("&amp;", "&");
        }
        #endregion

        #region 正则表达式匹配 MAC 地址
        /// <summary>
        /// 正则表达式匹配 MAC 地址
        /// </summary>
        /// <param name="strPara"></param>
        /// <returns></returns>
        public static bool CheckMac(string strPara)
        {
            if (string.IsNullOrEmpty(strPara))
            {
                return false;
            }

            Regex reg = new Regex(@"^[A-F0-9]{2}(-[A-F0-9]{2}){5}$");

            return reg.IsMatch(strPara);
        }
        #endregion
    }
}
