using System;
using System.Collections.Generic;
/******************************************************************
* Author: yangzx 
* Date: 2019-3-20
* Content: TableStatus表 配置结构
******************************************************************/

namespace entConsole.Models
{
    /// <summary>
    /// TableStatus表 配置结构
    /// </summary>
    public class TableStatusStru
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
        /// 表名
        /// </summary>
        public string tableName { get; set; }
        /// <summary>
        /// 表名
        /// </summary>
        public string taskId { get; set; }
        /// <summary>
        /// 最后入库时间
        /// </summary>
        public string lastStartTime { get; set; }
        /// <summary>
        /// 总条数
        /// </summary>
        public string totalDataPacketNum { get; set; }
        /// <summary>
        /// 总记录条数
        /// </summary>
        public string totalRecordNum { get; set; }
        /// <summary>
        /// 完成总条数
        /// </summary>
        public string finishDataPacketNum { get; set; }
        /// <summary>
        /// 完成记录数
        /// </summary>
        public string finishRecordNum { get; set; }
        /// <summary>
        /// 完成记录数
        /// </summary>
        public string taskStatus { get; set; }
    }
}
