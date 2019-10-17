using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace entConsole.Models
{
    public class InterfaceConfStru
    {
        /// <summary>
        /// Id
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 配置名称
        /// </summary>
        public string confName { get; set; }
        /// <summary>
        /// 接口
        /// </summary>
        public string Interface { get; set; }
        /// <summary>
        /// req类型
        /// </summary>
        public string reqType { get; set; }
        /// <summary>
        /// 参数
        /// </summary>
        public string param { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Describe { get; set; }
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
