/******************************************************************
* Author: yangzx 
* Date: 2019-3-20
* Content: DataInfoStru表 配置结构
******************************************************************/

namespace entConsole.Models
{
    /// <summary>
    /// DataInfoStru表 配置结构
    /// </summary>
    public class DataInfoStru
    {
        /// <summary>
        /// Id
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string userName { get; set; }
        /// <summary>
        /// 日志等级
        /// </summary>
        public string Level { get; set; }
        /// <summary>
        /// 数据流日志时间
        /// </summary>
        public string RecordTime { get; set; }
        /// <summary>
        /// 数据流日志内容
        /// </summary>
        public string Info { get; set; }
        /// <summary>
        /// 任务ID
        /// </summary>
        public string TaskID { get; set; }
    }
}
