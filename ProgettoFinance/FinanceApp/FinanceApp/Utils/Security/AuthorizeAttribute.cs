using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace FinanceApp.Utils.Security
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            SecurityInfo? securityInfo = (SecurityInfo?)context.HttpContext.Items["SecurityInfo"];

            bool hasError = false;

            if (securityInfo != null)
            {
                if (securityInfo.Origin == "Authentication")
                {
                    hasError = true;
                    context.Result = new JsonResult(new { message = "Unauthorized" })
                    {
                        StatusCode = StatusCodes.Status401Unauthorized
                    };
                }
                if (securityInfo.Origin == "TokenMissing")
                {
                    hasError = true;
                    context.Result = new JsonResult(new { message = "Token Missing" })
                    {
                        StatusCode = StatusCodes.Status401Unauthorized
                    };
                }

                if (!hasError && securityInfo.Origin == "GenericError")
                {
                    hasError = true;
                    context.Result = new JsonResult(new { message = $"GenericError: {securityInfo.Message}" })
                    {
                        StatusCode = StatusCodes.Status500InternalServerError
                    };
                }
            }
        }
    }
}
