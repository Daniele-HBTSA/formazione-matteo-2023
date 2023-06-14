using FinanceApp.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace FinanceApp.Utils.Security
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly JwtSettings _jwtSettings;

        public JwtMiddleware(RequestDelegate next, IOptions<JwtSettings> _jwtSettings)
        {
            _next = next;
            this._jwtSettings = _jwtSettings.Value;
        }

        public async Task Invoke(HttpContext context, FinanceAppContext appContext)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
            {
                try
                {
                    var jwtToken = JwtTokenManager.GetToken(token, _jwtSettings.Secret);
                    var idAzienda = int.Parse(jwtToken.Claims.First(element => element.Type.Equals("Id")).Value);

                    Aziende? user = await appContext.Aziende.FirstOrDefaultAsync(element => element.ID_AZIENDA == idAzienda);

                    if (user == null)
                        throw new Exception($"Doesn't exist an user with userID {idAzienda}");

                    SecurityInfo info = new SecurityInfo
                    {
                        Origin = "OK",
                        Message = "OK"
                    };

                    context.Items["Token"] = token;
                    context.Items["SecurityInfo"] = info;
                }
                catch (SecurityTokenExpiredException ex)
                {
                    SecurityInfo info = new SecurityInfo
                    {
                        Origin = ex.Data["Origin"].ToString(),
                        Message = ex.Message
                    };

                    context.Items["SecurityInfo"] = info;
                }
                catch (Exception ex)
                {
                    SecurityInfo info = new SecurityInfo
                    {
                        Origin = "GenericError",
                        Message = ex.Message,
                    };

                    context.Items["SecurityInfo"] = info;
                }
            }
            else
            {
                SecurityInfo info = new SecurityInfo
                {
                    Origin = "TokenMissing",
                    Message = "token not found!"
                };

                context.Items["SecurityInfo"] = info;
            }

            await _next(context);
        }
    }
}
