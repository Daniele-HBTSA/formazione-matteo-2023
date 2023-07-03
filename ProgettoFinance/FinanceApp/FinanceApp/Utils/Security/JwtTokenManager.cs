using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace FinanceApp.Utils.Security
{
    public class JwtTokenManager
    {
        public static JwtSecurityToken GetToken(string token, string secret)
        {
            return (JwtSecurityToken)Validate(token, secret);
        }

        private static SecurityToken Validate(string token, string secret)
        {
            try
            {
                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                byte[] key = Encoding.ASCII.GetBytes(secret);

                // Se il token è scaduto lancia una SecurityTokenExpiredException
                // viene catturata nel catch, viene aggiunto ex.Data.Add("Origin", "Authentication");
                // e viene poi catturata nel JwtMiddleware
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero

                }, out SecurityToken validatedToken);

                return validatedToken;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Origin", "Authentication");
                throw ex;
            }
        }
    }
}
