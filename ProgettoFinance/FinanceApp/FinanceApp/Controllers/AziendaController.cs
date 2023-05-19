using FinanceApp.Models;
using FinanceApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace FinanceApp.Controllers
{
    public class AziendaController : ControllerBase
    {
        public IAziendeService aziendeService { get; set; }
        public IMovimentiService movimentiService { get; set; }
        public IAuthenticationService authService { get; set; }

        public AziendaController(IAziendeService aziendeService, IMovimentiService movimentiService, IAuthenticationService authenticationService)
        {
            this.aziendeService = aziendeService;
            this.movimentiService = movimentiService;
            this.authService = authenticationService;
        }

        [HttpPost("login")]
        public async Task<ActionResult> TentaLogin(AziendaDTO azienda)
        {
            try
            {
                bool risposta = await authService.Autenticazione(azienda.AccountAzienda, azienda.PswAzienda);
                if (risposta)
                {
                    return Ok(true);
                }
                else
                {
                    return Ok(false);
                }
            } catch (Exception ex)
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
            } catch (Exception ex)
            {
                await Console.Out.WriteLineAsync($"Registrazione fallita {ex.Message}");
                return BadRequest(false);
            }
        }

        [HttpGet("mostraelenco")]
        public async Task<List<AziendaDTO>> MostraElencoAziende()
        {
            return await aziendeService.ElencoAziende();
        }

        //Mostra tutti i movimenti dell'azienda
        [HttpGet("mostramovimenti/{idAzienda}")]
        public async Task<List<MovimentoDTO>> MostraMovimentiAzienda(int idAzienda)
        {
            return await movimentiService.MostraMovimenti(idAzienda);
        }

        //Mostra movimenti dell'azienda per idmovimento
        [HttpGet("mostramovimenti/{idAzienda}/{idMovimento}")]
        public async Task<MovimentoDTO> SelezionaMovimento(int idAzienda, int idMovimento)
        {
            List<MovimentoDTO> listaMovimenti = await movimentiService.MostraMovimenti(idAzienda);
            if (listaMovimenti == null)
            {
                throw new Exception("Lista non trovata");
            }

            MovimentoDTO? movimentoScelto = listaMovimenti.FirstOrDefault(element => element.IdMovimento.Equals(idMovimento));
            if (movimentoScelto == null)
            {
                throw new Exception("Movimento non trovato");
            }

            return movimentoScelto;
        }

        //Aggiungi nuovo movimento
        [HttpPost("aggiungi")]
        public async Task<ActionResult> AggiungiMovimento([FromBody] MovimentoDTO nuovoMovimento)
        {
            bool rispostaAggiunta = await movimentiService.NuovoMovimento(nuovoMovimento);
            bool rispostaSaldo = false;
            if (rispostaAggiunta)
            {
                rispostaSaldo = await aziendeService.CalcolaSaldoAzienda(nuovoMovimento.IdAzienda);
            }

            return Ok(rispostaSaldo);
        }

        //Rimuovi movimento
        [HttpDelete("rimuovi/{idMovimento}")]
        public async Task<ActionResult> CancellaMovimento(int idMovimento)
        {
            bool risposta = await movimentiService.RimuoviMovimento(idMovimento);
            return Ok(risposta);
        }

        //aggiorna saldo
        [HttpGet("aggiornasaldo")]
        public async Task<ActionResult> AggiornaSaldo(int idAzienda)
        {
            bool risposta = await aziendeService.CalcolaSaldoAzienda(idAzienda);
            return Ok(risposta);
        }
    }
}
