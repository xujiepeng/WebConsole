/******************************************************************
* Author: yangzx 
* Date: 2019-3-20
* Content: 表 配置结构
******************************************************************/
namespace entConsole.Models
{
    public class DbColTypeConfigStru
    {
        /// <summary>
        /// Id
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// DBType
        /// </summary>
        public string DBType { get; set; }
        /// <summary>
        /// ColId
        /// </summary>
        public string ColId { get; set; }
        /// <summary>
        /// 默认值
        /// </summary>
        public string DefaultValue { get; set; }
        /// <summary>
        /// 列类型
        /// </summary>
        public string ColType { get; set; }
        /// <summary>
        /// ColLength
        /// </summary>
        public string ColLength { get; set; }
        /// <summary>
        /// Ver
        /// </summary>
        public string Ver { get; set; }
        /// <summary>
        /// 是否可用
        /// </summary>
        public string IsEnable { get; set; }
        /// <summary>
        /// 录入日期
        /// </summary>
        public string EntryDate { get; set; }
    }
}
