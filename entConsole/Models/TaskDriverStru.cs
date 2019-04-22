/******************************************************************
* Author: yangzx 
* Date: 2019-3-20
* Content: TaskDriverStru
******************************************************************/
namespace entConsole.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class TaskDriverStru
    {
        /// <summary>
        /// Id
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 任务ID
        /// </summary>
        public string taskId { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string userName { get; set; }
        /// <summary>
        /// 任务名
        /// </summary>
        public string TaskName { get; set; }
        /// <summary>
        /// 表名
        /// </summary>
        public string Tables { get; set; }
        /// <summary>
        /// 任务状态
        /// </summary>
        public string taskStatus { get; set; }
        /// <summary>
        /// 时间戳
        /// </summary>
        public string TMSP1 { get; set; }
        /// <summary>
        /// 时间戳
        /// </summary>
        public string TMSP2 { get; set; }
        /// <summary>
        /// 时间戳
        /// </summary>
        public string TMSP3 { get; set; }
        /// <summary>
        /// 是否初始化
        /// </summary>
        public bool Init { get; set; }
    }
}
