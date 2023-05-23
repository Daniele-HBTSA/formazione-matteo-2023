using FinanceApp.Models;
using FinanceApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace FinanceApp.Controllers
{
    public class MovimentiController : ControllerBase
    {
        public IAziendeService aziendeService { get; set; }
        public IMovimentiService movimentiService { get; set; }

        public MovimentiController(IAziendeService aziendeService, IMovimentiService movimentiService)
        {
            this.aziendeService = aziendeService;
            this.movimentiService = movimentiService;
        }

        //Da togliere se non serve più
        [HttpGet("mostraelenco")]
        public async Task<List<AziendaDTO>> MostraElencoAziende()
        {
            return await aziendeService.ElencoAziende();
        }

        //Mostra tutti i movimenti dell'azienda
        [HttpGet("mostramovimenti/{idAzienda}")]
        public async Task<ActionResult<List<MovimentoDTO>>> MostraMovimentiAzienda(int idAzienda)
        {
            try
            {
                return Ok(await movimentiService.MostraMovimenti(idAzienda));

            } catch (Exception ex)
            {
                return NotFound(false);
            }
        }

        //Mostra movimenti dell'azienda per idmovimento
        [HttpGet("mostramovimenti/{idMovimento}")]
        public async Task<ActionResult<MovimentoDTO>> SelezionaMovimento(int idMovimento)
        {
            try
            {
                return Ok(await movimentiService.SelezionaMovimento(idMovimento));

            } catch (Exception ex)
            {
                return NotFound(false);
            }
        }

        //Aggiungi nuovo movimento
        [HttpPost("aggiungi")]
        public async Task<ActionResult> AggiungiMovimento([FromBody] MovimentoDTO nuovoMovimento)
        {
            try
            {
                MovimentoDTO movimentoAggiunto = await movimentiService.NuovoMovimento(nuovoMovimento);
                if (movimentoAggiunto != null)
                {
                    return Ok(await aziendeService.CalcolaSaldoAzienda(nuovoMovimento.IdAzienda));

                } else
                {
                    return Ok(false);
                }
            } catch (Exception ex)
            {
                return BadRequest(false);
            }
        }

        //Rimuovi movimento
        [HttpDelete("rimuovi/{idMovimento}")]
        public async Task<ActionResult<MovimentoDTO>> CancellaMovimento(int idMovimento)
        {
            try
            {
                return Ok(await movimentiService.RimuoviMovimento(idMovimento));

            } catch(Exception ex)
            {
                return NotFound(false);
            }
        }
    }
}
