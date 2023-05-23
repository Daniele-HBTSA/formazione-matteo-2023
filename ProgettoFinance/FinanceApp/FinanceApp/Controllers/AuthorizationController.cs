using FinanceApp.Models;
using FinanceApp.Services;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<ActionResult> TentaLogin(AziendaDTO azienda)
        {
            try
            {
                bool risposta = await authService.Autenticazione(azienda.AccountAzienda, azienda.PswAzienda);
                return Ok(risposta);

            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync($"Login fallito {ex.Message}");
                return BadRequest(false);
            }
        }

        [HttpPost("registrati")]
        public async Task<ActionResult> TentaRegistrazione([FromBody] AziendaDTO nuovaAzienda)
        {
            try
            {
                bool risposta = await authService.Registrazione(nuovaAzienda);
                if (risposta)
                {
                    await Console.Out.WriteLineAsync("Registrazione avvenuta con successo.");
                    return Ok(true);
                }
                else
                {
                    await Console.Out.WriteLineAsync("Utente già esistente.");
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
