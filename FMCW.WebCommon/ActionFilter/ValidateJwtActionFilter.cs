using System;
using System.Linq;
using FMCW.Common.Jwt;
using FMCW.Common.Results;
using FMCW.WebCommon.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;

namespace FMCW.WebCommon.ActionFilter
{
    public class NoTokenCheckAttribute : Attribute { }

    /// <summary>
    /// requiere FMCW.Common.Jwt.JwtManager y FMCW.Common.Jwt.JwtConfiguration
    /// </summary>
    public class ValidateJwtActionFilter : Attribute, IActionFilter
    {
        private readonly IJwtManager _jwtManager;

        public ValidateJwtActionFilter(IJwtManager jwtManager)
        {
            _jwtManager = jwtManager;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {

        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ActionDescriptor is ControllerActionDescriptor controllerActionDescriptor)
            {
                var actionAttributes = controllerActionDescriptor.MethodInfo.GetCustomAttributes(inherit: true).ToList();

                if (actionAttributes.Any(a => a is NoTokenCheckAttribute))
                {
                    return;
                }

                if (context.HttpContext.Request.Headers.TryGetValue("Authorization", out StringValues authorizationToken))
                {
                    if (!authorizationToken.ToString().Contains("Bearer"))
                    {
                        context.Result = context.Result = new JsonResult(StringResult.Error("Authorization header must be 'Bearer xxxxxxxx'"))
                        {
                            StatusCode = StatusCodes.Status401Unauthorized
                        };
                        return;
                    }

                    var jwt = authorizationToken.ToString().Replace("Bearer ", "");
                    var result = _jwtManager.ValidateToken(jwt);
                    if (result.Success)
                    {
                        var controller = context.Controller as BaseController;
                        controller.Jwt = jwt;
                        controller.IdUsuario = result.ResultOk;
                    }
                    else
                    {
                        context.Result = new JsonResult(result)
                        {
                            StatusCode = StatusCodes.Status401Unauthorized
                        };
                        return;
                    }
                }
                else
                {
                    context.Result = new JsonResult(StringResult.Error("Authorization header is missing"))
                    {
                        StatusCode = StatusCodes.Status401Unauthorized
                    };
                    return;
                }
            }

        }

    }
}
