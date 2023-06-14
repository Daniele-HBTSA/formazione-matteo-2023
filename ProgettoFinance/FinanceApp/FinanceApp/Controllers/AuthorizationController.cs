using FinanceApp.Models;
using FinanceApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Session;

namespace FinanceApp.Controllers
{
    public class AuthorizationController : ControllerBase
    {

        public IAuthenticationService authService { get; set; }

        public AuthorizationController(IAuthenticationService authService)
        {
            this.authService = authService; 
        }

        [HttpPost("login")]
        public async Task<ActionResult<AziendaDTO>> TentaLogin([FromBody]AziendaDTO azienda)
        {
            try
            {
                AziendaDTO utenteLoggato = await authService.Autenticazione(azienda.AccountAzienda, azienda.PswAzienda);
                utenteLoggato.TokenPersonale = await authService.GetToken(utenteLoggato.IdAzienda);
                return Ok(utenteLoggato);

            }
            catch (Exception ex)
            {
                return Unauthorized("Non autorizzato");
            }
        }

        [HttpPost("registrati")]
        public async Task<ActionResult> TentaRegistrazione([FromBody]AziendaDTO nuovaAzienda)
        {
            try
            {
                if (await authService.Registrazione(nuovaAzienda))
                {
                    return Ok(true);
                }
                else
                {
                    return Ok(false);
                }
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync($"Registrazione fallita {ex.Message}");
                return BadRequest(false);
            }
        }
    }
}
