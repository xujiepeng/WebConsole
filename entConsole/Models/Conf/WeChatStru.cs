/******************************************************************
 * Author: miaoxin 
 * Date: 2018-12-12 
 * Content: 微信配置结构
 ******************************************************************/

namespace entConsole.Models.Conf
{
    /// <summary>
    /// 微信配置结构
    /// </summary>
    public class WeChatStru
    {
        /// <summary>
        /// Key
        /// </summary>
        public string key { get; set; }
        /// <summary>
        /// 微信开关
        /// </summary>
        public bool isEnable { get; set; }
        /// <summary>
        /// 接口地址
        /// </summary>
        public string urlPath { get; set; }
        /// <summary>
        /// 每日报警数量上限
        /// </summary>
        public int maxAlertNum { get; set; }
    }
}
