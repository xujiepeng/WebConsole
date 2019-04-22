/******************************************************************
 * Author: miaoxin 
 * Date: 2018-12-12 
 * Content: 数据库连接配置结构
 ******************************************************************/

namespace entConsole.Models.Conf
{
    /// <summary>
    /// 数据库连接配置结构
    /// </summary>
    public class DbConnStru
    {
        /// <summary>
        /// Key
        /// </summary>
        public string key { get; set; }
        /// <summary>
        /// 数据库类型 sqlite,sqlserver,oracle,mysql
        /// </summary>
        public string dbType { get; set; }
        /// <summary>
        /// 链接字符串
        /// </summary>
        public string connStr { get; set; }
        /// <summary>
        /// 链接字符串是否被加密
        /// </summary>
        public bool isEncrypt { get; set; }
    }
}
