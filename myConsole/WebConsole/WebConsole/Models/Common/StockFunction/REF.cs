using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebConsole.Models.Common
{
    public sealed partial class AnalysisEngine
    {
        /// <summary>
        /// 取前N个周期数据
        /// </summary>
        /// <param name="input"></param>
        /// <param name="period"></param>
        /// <returns></returns>
        public static decimal REF(List<decimal> input, int period)
        {
            return input[input.Count - period - 1];
        }
    }
}
