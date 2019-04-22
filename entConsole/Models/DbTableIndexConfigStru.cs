/******************************************************************
* Author: yangzx 
* Date: 2019-3-20
* Content: 表 配置结构
******************************************************************/

namespace entConsole.Models
{
    public class DbTableIndexConfigStru
    {
        /// <summary>
        /// Id
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 存活标记
        /// </summary>
        public string indexName { get; set; }
        /// <summary>
        /// 存活标记
        /// </summary>
        public string tableId { get; set; }
        /// <summary>
        /// 存活标记
        /// </summary>
        public string indexColName { get; set; }
        /// <summary>
        /// 存活标记
        /// </summary>
        public string Ver { get; set; }
        /// <summary>
        /// 存活标记
        /// </summary>
        public string isEnable { get; set; }
        /// <summary>
        /// 存活标记
        /// </summary>
        public string entryDate { get; set; }
    }
}