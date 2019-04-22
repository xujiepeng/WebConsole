using System.Text;
using System.Net.Mail;
using System;
using System.ComponentModel;

/******************************************************************
 * Author: miaoxin 
 * Date: 2017-12-26
 * Content: 邮件类
 ******************************************************************/

namespace entConsole.Lib.Util
{
    /// <summary>
    /// 邮件类
    /// </summary>
    public class MailUtil
    {
        #region 异/同步发送邮件
        /// <summary>
        /// 异/同步发送邮件
        /// 注：如果是页面调用异步方法，则需要设置页面 Page 的属性 Async="true"
        /// </summary>
        /// <param name="strMailFromAddress">发件人邮箱地址</param>
        /// <param name="strMailDisplayName">发件人显示名称</param>
        /// <param name="strMailFromPwd">发件人邮箱密码</param>
        /// <param name="arrMailToAddress">收件人邮箱地址</param>
        /// <param name="arrMailCcAddress">抄送人邮箱地址(可为 null)</param>
        /// <param name="arrMailBccAddress">密送人邮箱地址(可为 null)</param>
        /// <param name="strSmtpServer">Smtp服务器地址("mail.finchina.com")</param>
        /// <param name="strSubject">邮件标题</param>
        /// <param name="strBody">邮件正文(可为 空)</param>
        /// <param name="arrMailAttachment">附件(可为 null)</param>
        /// <param name="boolIsBodyHtml">邮件正文是否HTML格式(false|true)</param>
        /// <param name="mailPriority">邮件优先级(MailPriority.Low|MailPriority.Normal|MailPriority.High)</param>
        /// <param name="mailEncoding">邮件标题和正文编码(可为 null;为 null时默认System.Text.Encoding.UTF8)</param>
        /// <param name="Async">是否以异步发送邮件(true|false)</param>
        public static bool SendMail(string strMailFromAddress, string strMailDisplayName, string strMailFromPwd, string[] arrMailToAddress, string[] arrMailCcAddress, string[] arrMailBccAddress, string strSmtpServer, string strSubject, string strBody, string[] arrMailAttachment, bool boolIsBodyHtml, MailPriority mailPriority, Encoding mailEncoding, bool Async)
        {
            bool bool_Result = false;

            #region 参数验证

            //发件人地址不能为空
            if (string.IsNullOrEmpty(strMailFromAddress))
            {
                Log4NetUtil.Error(typeof(MailUtil), "发件人地址不能为空");
                return bool_Result;
            }

            //发件人显示名称为空时，默认显示发件人地址
            if (string.IsNullOrEmpty(strMailDisplayName))
            {
                strMailDisplayName = strMailFromAddress;
            }

            //发件人密码不能为空
            if (string.IsNullOrEmpty(strMailFromPwd))
            {
                Log4NetUtil.Error(typeof(MailUtil), "发件人密码不能为空");
                return bool_Result;
            }

            //收件人地址不能为空
            if (arrMailToAddress == null)
            {
                Log4NetUtil.Error(typeof(MailUtil), "收件人地址不能为空");
                return bool_Result;
            }

            //Smtp服务器地址不能为空
            if (string.IsNullOrEmpty(strSmtpServer))
            {
                Log4NetUtil.Error(typeof(MailUtil), "Smtp服务器地址不能为空");
                return bool_Result;
            }

            //邮件标题不能为空
            if (string.IsNullOrEmpty(strSubject))
            {
                Log4NetUtil.Error(typeof(MailUtil), "邮件标题不能为空");
                return bool_Result;
            }

            //默认邮件编码为 UTF8
            if (mailEncoding == null)
            {
                mailEncoding = System.Text.Encoding.UTF8;
            }

            //默认邮件正文不是HTML格式
            //if (boolIsBodyHtml == null)
            //{
            //    boolIsBodyHtml = false;
            //}           

            //默认邮件优先级为 正常
            //if (mailPriority == null)
            //{
            //    mailPriority = MailPriority.Normal;
            //}

            //默认以异步方式发送邮件
            //if (Async == null)
            //{
            //    Async = true;
            //}
            #endregion

            #region MailMessage

            MailMessage mailMsg = new MailMessage();

            //发件人地址
            try
            {
                mailMsg.From = new MailAddress(strMailFromAddress, strMailDisplayName, mailEncoding);
            }
            catch (Exception ex)
            {
                Log4NetUtil.Error(typeof(MailUtil), ex.ToString());
                return bool_Result;
            }

            //收件人地址
            for (int i = 0; i < arrMailToAddress.Length; i++)
            {
                mailMsg.To.Add(arrMailToAddress[i]);
            }

            //抄送人地址
            if (arrMailCcAddress != null)
            {
                for (int i = 0; i < arrMailCcAddress.Length; i++)
                {
                    mailMsg.CC.Add(arrMailCcAddress[i]);
                }
            }

            //密送人地址
            if (arrMailBccAddress != null)
            {
                for (int i = 0; i < arrMailBccAddress.Length; i++)
                {
                    mailMsg.Bcc.Add(arrMailBccAddress[i]);
                }
            }

            //邮件标题
            mailMsg.Subject = strSubject;

            //邮件标题编码 
            mailMsg.SubjectEncoding = mailEncoding;

            //邮件正文
            mailMsg.Body = strBody;

            //邮件正文编码 
            mailMsg.BodyEncoding = mailEncoding;

            //含有附件
            if (arrMailAttachment != null)
            {
                for (int i = 0; i < arrMailAttachment.Length; i++)
                {
                    mailMsg.Attachments.Add(new Attachment(arrMailAttachment[i]));
                }
            }

            //邮件优先级
            mailMsg.Priority = mailPriority;

            //邮件正文是否是HTML格式
            mailMsg.IsBodyHtml = boolIsBodyHtml;

            #endregion

            #region SmtpClient

            SmtpClient smtpClient = new SmtpClient();

            //验证发件人用户名和密码
            smtpClient.Credentials = new System.Net.NetworkCredential(strMailFromAddress, strMailFromPwd);

            //Smtp服务器地址
            smtpClient.Host = strSmtpServer;

            object userState = mailMsg;

            try
            {
                if (Async)
                {
                    //绑定回调函数
                    smtpClient.SendCompleted += new SendCompletedEventHandler(SendCompletedCallback);

                    //异步发送邮件
                    smtpClient.SendAsync(mailMsg, userState);
                    bool_Result = true;
                }
                else
                {
                    //同步发送邮件
                    smtpClient.Send(mailMsg);
                    bool_Result = true;
                }
            }
            catch (Exception ex)
            {
                Log4NetUtil.Error(typeof(MailUtil), ex.ToString());
                return false;
            }

            return bool_Result;

            #endregion

        }

        /// <summary>
        /// 异步发送邮件回调函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
        {
            MailMessage mailMsg = (MailMessage)e.UserState;

            if (e.Cancelled)
            {
                //发送被取消
                Log4NetUtil.Error(typeof(MailUtil), "发送邮件被取消：标题->" + mailMsg.Subject);
                return;
            }

            if (e.Error != null)
            {
                //出现错误
                Log4NetUtil.Error(typeof(MailUtil), "发送邮件出错：标题->" + mailMsg.Subject + ";异常信息->" + e.Error.ToString());
                return;
            }
        }
        #endregion

        #region 异/同步发送邮件
        /// <summary>
        /// 异/同步发送邮件
        /// </summary>
        /// <param name="strMailFromAddress">发件人邮箱地址</param>
        /// <param name="strMailFromPwd">发件人邮箱密码</param>
        /// <param name="arrMailToAddress">收件人邮箱地址</param>
        /// <param name="strSmtpServer">Smtp服务器地址("mail.finchina.com")</param>
        /// <param name="strSubject">邮件标题</param>
        /// <param name="strBody">邮件正文(可为 空)</param>
        public static bool SendMail(string strMailFromAddress, string strMailFromPwd, string[] arrMailToAddress, string strSmtpServer, string strSubject, string strBody)
        {
            return SendMail(strMailFromAddress, null, strMailFromPwd, arrMailToAddress, null, null, strSmtpServer, strSubject, strBody, null, false, MailPriority.Normal, System.Text.Encoding.UTF8, true);
        }
        #endregion

        #region 发送报警邮件
        /// <summary>
        /// 发送报警邮件
        /// </summary>
        /// <param name="strMailFromAddress">发件人邮箱地址</param>
        /// <param name="strMailDisplayName">发件人显示名称</param>
        /// <param name="strMailFromPwd">发件人邮箱密码</param>
        /// <param name="strSmtpServer">Smtp服务器地址("mail.finchina.com")</param>
        /// <param name="strMailToAddress">收件人邮箱地址</param>
        /// <param name="strMailCcAddress">抄送人邮箱地址(可为 null)</param>
        /// <param name="strMailBccAddress">密送人邮箱地址(可为 null)</param>
        /// <param name="strSubject">邮件标题</param>
        /// <param name="strBody">邮件正文(可为 空)</param>
        /// <returns></returns>
        public static bool SendAlertMail(string strMailFromAddress, string strMailDisplayName, string strMailFromPwd, string strSmtpServer, string strMailToAddress, string strMailCcAddress, string strMailBccAddress, string strSubject, string strBody)
        {
            string[] arrAlertMailToAddress = null;
            string[] arrAlertMailCcAddress = null;
            string[] arrAlertMailBccAddress = null;

            if (!string.IsNullOrEmpty(strMailToAddress))
            {
                arrAlertMailToAddress = strMailToAddress.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            }

            if (!string.IsNullOrEmpty(strMailCcAddress))
            {
                arrAlertMailCcAddress = strMailCcAddress.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            }

            if (!string.IsNullOrEmpty(strMailBccAddress))
            {
                arrAlertMailBccAddress = strMailBccAddress.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            }

            return SendMail(strMailFromAddress, strMailDisplayName, strMailFromPwd, arrAlertMailToAddress, arrAlertMailCcAddress, arrAlertMailBccAddress, strSmtpServer, strSubject, strBody, null, false, MailPriority.High, System.Text.Encoding.UTF8, true);
        }

        /// <summary>
        /// 发送报警邮件（同步）
        /// </summary>
        /// <param name="strMailFromAddress">发件人邮箱地址</param>
        /// <param name="strMailDisplayName">发件人显示名称</param>
        /// <param name="strMailFromPwd">发件人邮箱密码</param>
        /// <param name="strSmtpServer">Smtp服务器地址("mail.finchina.com")</param>
        /// <param name="strMailToAddress">收件人邮箱地址</param>
        /// <param name="strMailCcAddress">抄送人邮箱地址(可为 null)</param>
        /// <param name="strMailBccAddress">密送人邮箱地址(可为 null)</param>
        /// <param name="strSubject">邮件标题</param>
        /// <param name="strBody">邮件正文(可为 空)</param>
        /// <returns></returns>
        public static bool SendAlertMailSync(string strMailFromAddress, string strMailDisplayName, string strMailFromPwd, string strSmtpServer, string strMailToAddress, string strMailCcAddress, string strMailBccAddress, string strSubject, string strBody)
        {
            string[] arrAlertMailToAddress = null;
            string[] arrAlertMailCcAddress = null;
            string[] arrAlertMailBccAddress = null;

            if (!string.IsNullOrEmpty(strMailToAddress))
            {
                arrAlertMailToAddress = strMailToAddress.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            }

            if (!string.IsNullOrEmpty(strMailCcAddress))
            {
                arrAlertMailCcAddress = strMailCcAddress.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            }

            if (!string.IsNullOrEmpty(strMailBccAddress))
            {
                arrAlertMailBccAddress = strMailBccAddress.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            }

            return SendMail(strMailFromAddress, strMailDisplayName, strMailFromPwd, arrAlertMailToAddress, arrAlertMailCcAddress, arrAlertMailBccAddress, strSmtpServer, strSubject, strBody, null, false, MailPriority.High, System.Text.Encoding.UTF8, false);
        }
        #endregion
    }
}
