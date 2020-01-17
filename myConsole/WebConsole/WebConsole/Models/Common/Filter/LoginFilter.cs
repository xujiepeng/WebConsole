using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebConsole.Models.Common
{
    public class LoginFilter : ActionFilterAttribute
    {
        public LoginFilter(int Order = 0)
        {
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            int a = 1;
            a++;
        }
    }
}
