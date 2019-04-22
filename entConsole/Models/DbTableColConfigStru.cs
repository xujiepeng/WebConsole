/******************************************************************
* Author: yangzx 
* Date: 2019-3-20
* Content: 表 配置结构
******************************************************************/
namespace entConsole.Models
{
    public class DbTableColConfigStru
    {
        /// <summary>
        /// Id
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 存活标记
        /// </summary>
        public string tableId { get; set; }
        /// <summary>
        /// Id
        /// </summary>
        public string colName { get; set; }
        /// <summary>
        /// 存活标记
        /// </summary>
        public string iColName { get; set; }
        /// <summary>
        /// Id
        /// </summary>
        public string ColName_CN { get; set; }
        /// <summary>
        /// 存活标记
        /// </summary>
        public string isPk { get; set; }
        /// <summary>
        /// Id
        /// </summary>
        public string isIgnore { get; set; }
        /// <summary>
        /// 存活标记
        /// </summary>
        public string isKey { get; set; }
        /// <summary>
        /// Id
        /// </summary>
        public string sortCol { get; set; }
        /// <summary>
        /// 存活标记
        /// </summary>
        public string isNotNull { get; set; }
        /// <summary>
        /// Id
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
