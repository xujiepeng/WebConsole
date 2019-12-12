using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebConsole.Models.Model
{
    public class QTStrategyStru
    {
        public int id
        {
            get;
            set;
        }
        /// <summary>
        /// 策略名称
        /// </summary>
        public string strategyname
        {
            get;
            set;
        }
        /// <summary>
        /// 策略编号
        /// </summary>
        public string strategynumber
        {
            get;
            set;
        }
        /// <summary>
        /// 策略路径
        /// </summary>
        public string strategypath
        {
            get;
            set;
        }
        /// <summary>
        /// 策略创建时间
        /// </summary>
        public string creattime
        {
            get;
            set;
        }
        /// <summary>
        /// 策略描述
        /// </summary>
        public string describe
        {
            get;
            set;
        }
        /// <summary>
        /// 备注信息
        /// </summary>
        public string remark
        {
            get;
            set;
        }
        /// <summary>
        /// 是否启用
        /// </summary>
        public string states
        {
            get;
            set;
        }
        /// <summary>
        /// 是否正在执行
        /// </summary>
        public string isrun
        {
            get;
            set;
        }
    }
}
