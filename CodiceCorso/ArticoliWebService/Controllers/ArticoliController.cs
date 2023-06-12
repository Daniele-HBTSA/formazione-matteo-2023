using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArticoliWebService.Dtos;
using ArticoliWebService.Models;
using ArticoliWebService.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ArticoliWebService.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/articoli")]
    [Authorize(Roles = "ADMIN, USER")]
    public class ArticoliController : Controller
    {
        private IArticoliRepository articolirepository;
        private readonly IMapper mapper;

        public ArticoliController(IArticoliRepository articolirepository, IMapper mapper)
        {
            this.articolirepository = articolirepository;
            this.mapper = mapper;
        }

        [HttpGet("cerca/descrizione/{filter}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ArticoliDto>))]
        public async Task<ActionResult<IEnumerable<ArticoliDto>>> GetArticoliByDesc(string filter,
            [FromQuery(Name = "cat")] string IdCat, [FromQuery(Name = "prz")] double prezzo)
        {
            var articoliDto = new List<ArticoliDto>();

            var articoli = await this.articolirepository.SelArticoliByDescrizione(filter, IdCat);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (articoli.Count == 0)
            {
                return NotFound(
                    new ErrMsg(string.Format("Non è stato trovato alcun articolo con il filtro '{0}'", filter), 
                    this.HttpContext.Response.StatusCode));
            }

            foreach(var articolo in articoli)
            {
                articoliDto.Add(GetArticoliDto(articolo));
            }

            return Ok(articoliDto);
        }

        [HttpGet("cerca/codice/{CodArt}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ArticoliDto))]
        [AllowAnonymous]
        public async Task<ActionResult<ArticoliDto>> GetArticoloByCode(string CodArt)
        {
            bool retVal = await this.articolirepository.ArticoloExists(CodArt);

            if (!retVal)
            {
                return NotFound(
                    new ErrMsg(string.Format("Non è stato trovato l'articolo con il codice '{0}'", CodArt), 
                    this.HttpContext.Response.StatusCode));
            }

            var articolo = await this.articolirepository.SelArticoloByCodice(CodArt);

            return Ok(this.GetArticoliDto(articolo));
        }

        [HttpGet("cerca/barcode/{Ean}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(ArticoliDto))]
        public async Task<ActionResult<ArticoliDto>> GetArticoloByEan(string Ean)
        {
            var articolo = await this.articolirepository.SelArticoloByEan(Ean);

            if (articolo == null)
            {
                return NotFound(
                    new ErrMsg(string.Format("Non è stato trovato l'articolo con il barcode '{0}'", Ean), 
                    this.HttpContext.Response.StatusCode));
            }

            return Ok(this.GetArticoliDto(articolo));
        }

        [HttpPost("inserisci")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Articoli))]
        [ProducesResponseType(400)]
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult<Articoli>> SaveArticoli([FromBody] Articoli articolo)
        {
            if (articolo == null)
            {
                return BadRequest(new ErrMsg("Dati Nuovo Articolo Assenti", this.HttpContext.Response.StatusCode));
            }

            if (articolo.IdIva == -1)
            {
                return BadRequest(new ErrMsg("Aliquota Iva non selezionata", this.HttpContext.Response.StatusCode));
            }

            //Contolliamo se l'articolo è presente
            var isPresent = await articolirepository.ArticoloExists(articolo.CodArt);

            if (isPresent)
            {
                //ModelState.AddModelError("", $"Articolo {articolo.CodArt} è presente in anagrafica! Impossibile utilizzare il metodo POST!");
                return StatusCode(422, new ErrMsg($"Articolo {articolo.CodArt} è presente in anagrafica! Impossibile utilizzare il metodo POST!",
                    this.HttpContext.Response.StatusCode));
            }

            //Verifichiamo che i dati siano corretti
            if (!ModelState.IsValid)
            {
                string ErrVal = "";

                foreach (var modelState in ModelState.Values) 
                {
                    foreach (var modelError in modelState.Errors) 
                    {
                        ErrVal += modelError.ErrorMessage + " - "; 
                    }
                }
                return BadRequest(new ErrMsg(ErrVal, 400));
            }

            articolo.DataCreazione = DateTime.Today;

            var retVal = await articolirepository.InsArticoli(articolo);

            if (!retVal)
            {
                //ModelState.AddModelError("", $"Ci sono stati problemi nell'inserimento dell'Articolo {articolo.CodArt}. ");
                return StatusCode(500, new ErrMsg($"Ci sono stati problemi nell'inserimento dell'Articolo {articolo.CodArt}.", 500));
            }

            return Ok(new InfoMsg(DateTime.Today, "Inserimento Articolo Eseguita con successo!"));

        }

        [HttpPut("modifica")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Articoli))]
        [ProducesResponseType(400)]
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult<Articoli>> UpdateArticoli([FromBody] Articoli articolo)
        {
            if (articolo == null)
            {
                return BadRequest(new ErrMsg("Dati Articolo Assenti", this.HttpContext.Response.StatusCode));
            }

            
            if (articolo.IdIva == -1)
            {
                return BadRequest(new ErrMsg("Aliquota Iva non selezionata", this.HttpContext.Response.StatusCode));
            }
            

            //Contolliamo se l'articolo è presente
            var isPresent = await articolirepository.ArticoloExists(articolo.CodArt);

            if (!isPresent)
            {
                //ModelState.AddModelError("", $"Articolo {articolo.CodArt} NON presente in anagrafica! Impossibile utilizzare il metodo PUT!");
                return StatusCode(422, new ErrMsg($"Articolo {articolo.CodArt} NON presente in anagrafica! Impossibile utilizzare il metodo PUT!", this.HttpContext.Response.StatusCode));
            }

            //Verifichiamo che i dati siano corretti
            if (!ModelState.IsValid)
            {
                string ErrVal = "";

                foreach (var modelState in ModelState.Values) 
                {
                    foreach (var modelError in modelState.Errors) 
                    {
                        ErrVal += modelError.ErrorMessage + " - "; 
                    }
                }
                
                return BadRequest(new ErrMsg(ErrVal, 400));
            }

            articolo.DataCreazione = DateTime.Today;

            var retVal = await articolirepository.UpdArticoli(articolo);

            if (!retVal)
            {
                //ModelState.AddModelError("", $"Ci sono stati problemi nella modifica dell'Articolo {articolo.CodArt}.  ");
                return StatusCode(500, new ErrMsg($"Ci sono stati problemi nella modifica dell'Articolo {articolo.CodArt}.  ", 500));
            }

            return Ok(new InfoMsg(DateTime.Today, "Modifica Articolo Eseguita con successo!"));
        }

        [HttpDelete("elimina/{codart}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(InfoMsg))]
        [ProducesResponseType(400, Type = typeof(ErrMsg))]
        [ProducesResponseType(422, Type = typeof(ErrMsg))]
        [ProducesResponseType(500, Type = typeof(ErrMsg))]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> DeleteArticoli(string codart)
        {
            if (codart == "123Test")
            {
                return BadRequest(new ErrMsg($"Impossibile Eseguire l'Eliminazione dell'articolo {codart}!", 
                    this.HttpContext.Response.StatusCode));
            }

            if (codart == "")
            {
                return BadRequest(new ErrMsg($"E' necessario inserire il codice dell'articolo da eliminare!",
                    this.HttpContext.Response.StatusCode));
            }

            Articoli articolo =  await articolirepository.SelArticoloByCodice2(codart);

            if (articolo == null)
            {
                return StatusCode(422, new ErrMsg($"Articolo {codart} NON presente in anagrafica! Impossibile Eliminare!",
                    this.HttpContext.Response.StatusCode));
            }

            var retVal = await articolirepository.DelArticoli(articolo);

            //verifichiamo che i dati siano stati regolarmente eliminati dal database
            if (!retVal)
            {
                return StatusCode(500, new ErrMsg($"Ci sono stati problemi nella eliminazione dell'Articolo {articolo.CodArt}.",
                    this.HttpContext.Response.StatusCode));
            }

            return Ok(new InfoMsg(DateTime.Today, $"Eliminazione articolo {codart} eseguita con successo!"));
        }

        private ArticoliDto GetArticoliDto(Articoli articolo)
        {
            var barcodeDto = new List<BarcodeDto>();

            foreach(var ean in articolo.barcode)
            {
                barcodeDto.Add(new BarcodeDto
                {
                    Barcode = ean.Barcode,
                    Tipo = ean.IdTipoArt
                });
            }

            ArticoliDto articoliDto =  mapper.Map<ArticoliDto>(articolo);

            articoliDto.Ean = barcodeDto;

            return articoliDto;
        }
    }
}