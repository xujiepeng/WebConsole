using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

/// <summary>
/// 访问量统计，只统计刷新次数
/// </summary>
namespace WebConsole.Models.Common
{
    public class CountFilter : ActionFilterAttribute
    {
        public CountFilter(int Order = 0)
        {
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            int a = 1;
            a++;
        }

    }
}
