using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Models;
using Services;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Collections.Generic;
using System.IO.Pipelines;

namespace SicurezzaBackEnd.Security
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {

        private readonly IUserService userService;

        public BasicAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
        {
            this.userService = userService;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
                return AuthenticateResult.Fail("Authorization header mancante");

            Utenti utente = null;
            bool isOk = false;

            try
            {
                AuthenticationHeaderValue? authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
                    if (authHeader == null) 
                        return AuthenticateResult.Fail("Header non valido");
                var credentialBytes = Convert.FromBase64String(authHeader.Parameter ?? "");
                var credentials = Encoding.UTF8.GetString(credentialBytes).Split(":");

                var username = credentials[0];
                var password = credentials[1];

                isOk = await userService.Authenticate(username, password);
                
                if (isOk)
                {
                    utente = await userService.GetUser(username);
                }
                else
                {
                    return AuthenticateResult.Fail("Nome utente o password errati");
                }
            } 
            catch
            {
                return AuthenticateResult.Fail("Header non valido");
            }

            ICollection<Profili> userProfiles = utente.Profili;

            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, utente.UserId));

            foreach (var Profile in userProfiles)
            {
                claims.Add(new Claim(ClaimTypes.Role, Profile.Tipo));
            }

            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return AuthenticateResult.Success(ticket);
        }
    }
}
