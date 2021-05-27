using FMCW.Common.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace FMCW.WebCommon.ActionFilter
{
    public class LogExceptionActionFilter : IExceptionFilter
    {
        private readonly ILogger _logger;

        public LogExceptionActionFilter(ILogger logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            context.Result = new JsonResult(BoolResult.Error(context.Exception))
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };
            var mensaje = @$"Ha habido una exception no controlado.
                {context.Exception}
                {context.HttpContext.Request.Method} {context.HttpContext.Request.QueryString}
                {context.HttpContext.Request.Headers.Aggregate("headers: ", (s, ns) => s + ns.Key + ": " + ns.Value + " - ")}
                ";
            _logger.LogCritical(mensaje);
        }
    }
}
