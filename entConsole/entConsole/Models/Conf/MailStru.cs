/******************************************************************
 * Author: miaoxin 
 * Date: 2018-12-12  
 * Content: 邮件配置结构
 ******************************************************************/

namespace entConsole.Models.Conf
{
    /// <summary>
    /// 邮件配置结构
    /// </summary>
    public class MailStru
    {
        /// <summary>
        /// Key
        /// </summary>
        public string key { get; set; }
        /// <summary>
        /// 邮件开关
        /// </summary>
        public bool isEnable { get; set; }
        /// <summary>
        /// 邮件发件人邮箱
        /// </summary>
        public string mailFromAddress { get; set; }
        /// <summary>
        /// 显示名称
        /// </summary>
        public string mailDisplayName { get; set; }
        /// <summary>
        /// 邮件发件人邮箱密码
        /// </summary>
        public string mailFromPwd { get; set; }
        /// <summary>
        /// 邮箱密码是否加密
        /// </summary>
        public bool isPwdEncrypt { get; set; }
        /// <summary>
        /// Smtp服务器地址
        /// </summary>
        public string smtpServer { get; set; }
        /// <summary>
        /// 邮件接收人
        /// </summary>
        public string mailToAddress { get; set; }
        /// <summary>
        /// 邮件抄送人
        /// </summary>
        public string mailCcAddress { get; set; }
        /// <summary>
        /// 邮件密送人
        /// </summary>
        public string mailBccAddress { get; set; }
        /// <summary>
        /// 邮件签名
        /// </summary>
        public string mailSign { get; set; }
        /// <summary>
        /// 每日报警数量上限
        /// </summary>
        public int maxAlertNum { get; set; }
    }
}
