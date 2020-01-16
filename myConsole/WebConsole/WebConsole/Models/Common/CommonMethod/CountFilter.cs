using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebConsole.Models.Common
{
    public class CountFilter : IResultFilter
    {
        public bool IsCheck { get; set; }

        #region 执行action前执行这个方法

        public void OnResultExecuted(ResultExecutedContext context)
        {
            int a = 1;
        }
        public void OnResultExecuting(ResultExecutingContext context)
        {

        }
        #endregion


    }
}
