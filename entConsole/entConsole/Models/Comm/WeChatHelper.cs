using entConsole.Lib.Util;
using entConsole.Models.Conf;
using System;

/******************************************************************
 * Author: miaoxin 
 * Date: 2018-12-12
 * Content: 微信发送帮助类
 ******************************************************************/

namespace entConsole.Models.Comm
{
    /// <summary>
    /// 微信发送帮助类
    /// </summary>
    public class WeChatHelper
    {
        /// <summary>
        /// 微信发送数量
        /// </summary>
        private static int int_WeChatNum = 0;
        /// <summary>
        /// 数量校验日期
        /// </summary>
        private static int int_WeChatDate = 0;
        /// <summary>
        /// 锁
        /// </summary>
        private static object padlock = new object();

        #region 微信报警
        /// <summary>
        /// 微信报警
        /// </summary>
        /// <param name="wcConfig">微信配置</param>
        /// <param name="strMsg">信息</param>
        /// <param name="boolFlag">true-成功;false-失败</param>
        /// <returns></returns>
        public static bool SendAlertWX(WeChatStru wcConfig, string strMsg = "系统异常，详情请查看日志", bool boolFlag = false)
        {
            bool bool_Result = false;
            //内容
            string str_Content = string.Empty;
            //地址
            string str_Url = string.Empty;
            //返回内容
            string str_HttpResult = string.Empty;

            //邮件开关处于打开状态才发送邮件
            if (wcConfig.isEnable)
            {
                lock (padlock)
                {
                    if (int_WeChatDate != ConvertUtil.ParseInt(DateTime.Now))
                    {
                        int_WeChatNum = 0;
                        int_WeChatDate = ConvertUtil.ParseInt(DateTime.Now);
                    }

                    //超过指定数量
                    if (int_WeChatNum > wcConfig.maxAlertNum)
                    {
                        return false;
                    }

                    if (string.IsNullOrEmpty(strMsg) || string.IsNullOrEmpty(wcConfig.urlPath))
                    {
                        return false;
                    }

                    if (boolFlag) //成功消息
                    {
                        str_Content = "[呲牙] ";
                    }
                    else //失败消息
                    {
                        str_Content = "[衰] ";
                    }

                    str_Content += strMsg + "\r\n";

                    str_Url = string.Format(wcConfig.urlPath, str_Content);
                    str_HttpResult = HttpUtil.HttpGet(str_Url);

                    if (str_HttpResult.IndexOf("ok") > 0)
                    {
                        bool_Result = true;
                        int_WeChatNum++;
                    }

                    return bool_Result;
                }
            }
            else
            {
                return false;
            }
        }
        #endregion
    }
}
