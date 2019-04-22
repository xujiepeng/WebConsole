/******************************************************************
* Author: yangzx 
* Date: 2019-3-20
* Content: 表 配置结构
******************************************************************/

namespace entConsole.Models
{
    public class DownLoadAccountStru
    {
        /// <summary>
        /// Id
        /// </summary>
        public string userName { get; set; }
        /// <summary>
        /// 存活标记
        /// </summary>
        public string Pwd { get; set; }
        /// <summary>
        /// 密码是否被加密
        /// </summary>
        public bool pwdIsEncrypt { get; set; }
        /// <summary>
        /// 数据库链接标识，唯一
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 数据库类型
        /// </summary>
        public string DbType { get; set; }
        /// <summary>
        /// 链接字符串
        /// </summary>
        public string ConnStr { get; set; }
        /// <summary>
        /// 链接字符串是否被加密
        /// </summary>
        public bool connStrIsEncrypt { get; set; }
        /// <summary>
        /// 数据库字符集编码
        /// </summary>
        public string Encoding { get; set; }
    }
}
