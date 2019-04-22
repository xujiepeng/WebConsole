/******************************************************************
* Author: yangzx 
* Date: 2019-3-20
* Content: 表 配置结构
******************************************************************/

namespace entConsole.Models
{
    public class CommonConfigStru
    {
        /// <summary>
        /// Sftp标识，唯一
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Sftp地址
        /// </summary>
        public string ftpUri { get; set; }
        /// <summary>
        /// Sftp端口
        /// </summary>
        public string ftpPort { get; set; }
        /// <summary>
        /// Sftp用户
        /// </summary>
        public string ftpUserID { get; set; }
        /// <summary>
        /// Sftp密码
        /// </summary>
        public string ftpPassword { get; set; }
        /// <summary>
        /// Sftp密码是否被加密
        /// </summary>
        public bool isEncrypt { get; set; }
        /// <summary>
        /// Https接口地址
        /// </summary>
        public string httpsAddr { get; set; }
    }
}
