/******************************************************************
 * Author: miaoxin 
 * Date: 2018-12-12 
 * Content: 通用配置结构
 ******************************************************************/

using System.Collections.Generic;

namespace entConsole.Models.Conf
{
    /// <summary>
    /// 通用配置结构
    /// </summary>
    public class CommConf
    {
        /// <summary>
        /// 属性列表
        /// </summary>
        public List<AttriStru> AttriList { get; set; }
        /// <summary>
        /// 数据库连接列表
        /// </summary>
        public List<DbConnStru> DbConnList { get; set; }
        /// <summary>
        /// Ftp 列表
        /// </summary>
        public List<FtpStru> FtpList { get; set; }
        /// <summary>
        /// 邮件列表
        /// </summary>
        public List<MailStru> MailList { get; set; }
        /// <summary>
        /// 微信列表
        /// </summary>
        public List<WeChatStru> WeChatList { get; set; }
    }
}
