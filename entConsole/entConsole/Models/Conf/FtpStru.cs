/******************************************************************
 * Author: miaoxin 
 * Date: 2019-3-18  
 * Content: FTP 配置结构
 ******************************************************************/

namespace entConsole.Models.Conf
{
    /// <summary>
    /// Ftp配置结构
    /// </summary>
    public class FtpStru
    {
        /// <summary>
        /// Key
        /// </summary>
        public string key { get; set; }
        /// <summary>
        /// FTP地址
        /// </summary>
        public string ftpUri { get; set; }
        /// <summary>
        /// FTP端口
        /// </summary>
        public int ftpPort { get; set; }        
        /// <summary>
        /// FTP用户名
        /// </summary>
        public string ftpUserID { get; set; }
        /// <summary>
        /// FTP密码
        /// </summary>
        public string ftpPassword { get; set; }
        /// <summary>
        /// FTP密码是否被加密
        /// </summary>
        public bool isPwdEncrypt { get; set; }        
    }
}
