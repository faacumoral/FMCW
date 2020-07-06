using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FMCW.Seguridad.API.Filters
{
    public class IPValidationActionFilter : IActionFilter
    {
        protected string AllowedHosts { get; set; }

        public IPValidationActionFilter(string allowed)
        {
            AllowedHosts = allowed;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var A = 5;
        }

        public void OnActionExecuted(ActionExecutedContext context) { }
    }
}
