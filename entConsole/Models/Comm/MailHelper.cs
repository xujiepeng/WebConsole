using entConsole.Lib.Util;
using entConsole.Models.Conf;
using System;
using System.Net.Mail;

/******************************************************************
 * Author: miaoxin 
 * Date: 2018-12-12
 * Content: 邮件发送帮助类
 ******************************************************************/

namespace entConsole.Models.Comm
{
    /// <summary>
    /// 邮件发送帮助类
    /// </summary>
    public class MailHelper
    {
        /// <summary>
        /// 邮件发送数量
        /// </summary>
        private static int int_MailNum = 0;
        /// <summary>
        /// 数量校验日期
        /// </summary>
        private static int int_MailDate = 0;
        /// <summary>
        /// 锁
        /// </summary>
        private static object padlock = new object();

        #region 发送报警邮件
        /// <summary>
        /// 发送报警邮件
        /// </summary>
        /// <param name="mailStru">邮件配置</param>
        /// <param name="strTitle">标题</param>
        /// <param name="strContent">内容</param>
        /// <returns></returns>
        public static bool SendAlertMail(MailStru mailStru, string strTitle, string strContent)
        {
            bool bool_Result = false;
            string[] arrAlertMailToAddress = null;
            string[] arrAlertMailCcAddress = null;
            string[] arrAlertMailBccAddress = null;

            if (mailStru == null || string.IsNullOrEmpty(strTitle))
            {
                return bool_Result;
            }

            //邮件开关处于打开状态才发送邮件
            if (mailStru.isEnable)
            {
                lock (padlock)
                {
                    if (int_MailDate != ConvertUtil.ParseInt(DateTime.Now))
                    {
                        int_MailNum = 0;
                        int_MailDate = ConvertUtil.ParseInt(DateTime.Now);
                    }

                    //超过指定数量
                    if (int_MailNum > mailStru.maxAlertNum)
                    {
                        return false;
                    }

                    if (!string.IsNullOrEmpty(mailStru.mailToAddress))
                    {
                        arrAlertMailToAddress = mailStru.mailToAddress.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    }

                    if (!string.IsNullOrEmpty(mailStru.mailCcAddress))
                    {
                        arrAlertMailCcAddress = mailStru.mailCcAddress.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    }

                    if (!string.IsNullOrEmpty(mailStru.mailBccAddress))
                    {
                        arrAlertMailBccAddress = mailStru.mailBccAddress.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    }

                    //异步发送邮件
                    bool_Result = MailUtil.SendMail(mailStru.mailFromAddress
                        , mailStru.mailDisplayName
                        , mailStru.isPwdEncrypt ? EncryptUtil.DesDecrypt_Default(mailStru.mailFromPwd) : mailStru.mailFromPwd
                        , arrAlertMailToAddress
                        , arrAlertMailCcAddress
                        , arrAlertMailBccAddress
                        , mailStru.smtpServer
                        , strTitle
                        , strContent + "\r\n" + mailStru.mailSign
                        , null
                        , false
                        , MailPriority.High
                        , System.Text.Encoding.UTF8
                        , true);

                    if (bool_Result)
                    {
                        int_MailNum++;
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

        #region 发送报警邮件
        /// <summary>
        /// 发送报警邮件
        /// </summary>
        /// <param name="mailStru">邮件配置</param>
        /// <param name="strContent">内容</param>
        /// <returns></returns>
        public static bool SendAlertMail(MailStru mailStru, string strContent)
        {
            bool bool_Result = false;
            string[] arrAlertMailToAddress = null;
            string[] arrAlertMailCcAddress = null;
            string[] arrAlertMailBccAddress = null;
            //邮件标题
            string strTitle = "cibApp 异常";

            if (mailStru == null)
            {
                return bool_Result;
            }

            //邮件开关处于打开状态才发送邮件
            if (mailStru.isEnable)
            {
                lock (padlock)
                {
                    if (int_MailDate != ConvertUtil.ParseInt(DateTime.Now))
                    {
                        int_MailNum = 0;
                        int_MailDate = ConvertUtil.ParseInt(DateTime.Now);
                    }

                    //超过指定数量
                    if (int_MailNum > mailStru.maxAlertNum)
                    {
                        return false;
                    }

                    if (!string.IsNullOrEmpty(mailStru.mailToAddress))
                    {
                        arrAlertMailToAddress = mailStru.mailToAddress.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    }

                    if (!string.IsNullOrEmpty(mailStru.mailCcAddress))
                    {
                        arrAlertMailCcAddress = mailStru.mailCcAddress.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    }

                    if (!string.IsNullOrEmpty(mailStru.mailBccAddress))
                    {
                        arrAlertMailBccAddress = mailStru.mailBccAddress.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    }

                    //异步发送邮件
                    bool_Result = MailUtil.SendMail(mailStru.mailFromAddress
                        , mailStru.mailDisplayName
                        , mailStru.isPwdEncrypt ? EncryptUtil.DesDecrypt_Default(mailStru.mailFromPwd) : mailStru.mailFromPwd
                        , arrAlertMailToAddress
                        , arrAlertMailCcAddress
                        , arrAlertMailBccAddress
                        , mailStru.smtpServer
                        , strTitle
                        , strContent + "\r\n" + mailStru.mailSign
                        , null
                        , false
                        , MailPriority.High
                        , System.Text.Encoding.UTF8
                        , true);

                    if (bool_Result)
                    {
                        int_MailNum++;
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
