using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace entConsole.Models
{
    public class DbTableConfigStru
    {
        /// <summary>
        /// Id
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 表名称
        /// </summary>
        public string tableName { get; set; }
        /// <summary>
        /// 表中文
        /// </summary>
        public string tableName_CN { get; set; }
        /// <summary>
        /// Iname
        /// </summary>
        public string Iname { get; set; }
        /// <summary>
        /// 表类型
        /// </summary>
        public string tableType { get; set; }
        /// <summary>
        /// 数据过滤器
        /// </summary>
        public string DataFilter { get; set; }
        /// <summary>
        /// 是否初始化
        /// </summary>
        public string isOrigin { get; set; }
        /// <summary>
        /// 版本
        /// </summary>
        public string Ver { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public string isEnable { get; set; }
        /// <summary>
        /// 录入日期
        /// </summary>
        public string entryDate { get; set; }
    }
}
