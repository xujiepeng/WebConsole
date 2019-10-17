/******************************************************************
* Author: yangzx 
* Date: 2019-3-20
* Content: DataInfoStru表 配置结构
******************************************************************/

namespace entConsole.Models
{
    public class AttriConfStru
    {
        /// <summary>
        /// Id
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// confName
        /// </summary>
        public string confName { get; set; }
        /// <summary>
        /// Value
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// Describe
        /// </summary>
        public string Describe { get; set; }
        /// <summary>
        /// Ver
        /// </summary>
        public string Ver { get; set; }
        /// <summary>
        /// isEnable
        /// </summary>
        public string isEnable { get; set; }
        /// <summary>
        /// entryDate
        /// </summary>
        public string entryDate { get; set; }
    }
}