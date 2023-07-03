using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Models;
using Services;
using Microsoft.AspNetCore.Mvc;
using Security;
using GestUser.Dtos;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using ArticoliWebService.Dtos;
using GestUserWebApi.Dtos;
using SicurezzaBackEnd.Dtos;

namespace GestUser.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/user")]
    public class UserController : Controller
    {
        private readonly IUserService userRepository;
        private readonly IMapper mapper;

        public UserController(IUserService userRepository, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
        }
        
        [HttpPost("auth")]
        public async Task<ActionResult<JwtDTO>> authenticate([FromBody] UtentiDto user)
        {
            if(!await userRepository.Authenticate(user.UserId, user.Password))
            {
                return BadRequest();
            } else
            {
                JwtDTO tokenUtente = new JwtDTO(await userRepository.GetToken(user.UserId));
                return Ok(tokenUtente);
            }
        }

        [HttpGet("all")]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrMsg))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ActionResult<UtentiDto>))]
        public async Task<ActionResult<UtentiDto>> GetAllUser()
        {
            var clientiDto = new List<UtentiDto>();

            var utenti = await this.userRepository.GetAll();

            if (utenti.Count == 0)
            {
                return NotFound(new ErrMsg("Non è stato trovato alcun utente!", 
                    this.HttpContext.Response.StatusCode));
            }

            foreach(var utente in utenti)
            {
                clientiDto.Add(mapper.Map<UtentiDto>(utente));
            }

            return Ok(clientiDto);
        }
        
        [HttpGet("cerca/{userid}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UtentiDto))]
        public async Task<ActionResult<UtentiDto>> GetUser(String userid)
        {
            var utente = await this.userRepository.GetUser(userid);

            if (utente == null)
            {
                return NotFound(new ErrMsg(string.Format($"Non è stato trovato l'utente {userid}!"),
                    this.HttpContext.Response.StatusCode));
            }

            return Ok(mapper.Map<UtentiDto>(utente));
        }

        [HttpPost("inserisci")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(InfoMsg))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrMsg))]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ErrMsg))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrMsg))]
        public async Task<ActionResult<InfoMsg>> SaveUtente([FromBody] Utenti utente)
        {
            if (utente == null)
            {
                return BadRequest(new ErrMsg("E' necessario inserire i dati dell'utente", 
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

                return BadRequest(new ErrMsg(ErrVal, this.HttpContext.Response.StatusCode));
            }

            //Contolliamo se l'utente è presente
            var isPresent = await userRepository.GetUser(utente.UserId);

            if (isPresent != null)
            {
                return StatusCode(422, new ErrMsg($"Utente {utente.UserId} in uso! Impossibile inserire!", 
                    this.HttpContext.Response.StatusCode));
            }
            
            //Contolliamo se la fidelity è presente
            var isCodFid = await userRepository.GetUserByCodFid(utente.CodFidelity);

            if (isCodFid != null)
            {
                return StatusCode(422, new ErrMsg($"CodFid {utente.CodFidelity} in uso! Impossibile inserire!", 
                    this.HttpContext.Response.StatusCode));
            }

            foreach (var item in utente.Profili)
            {
                item.CodFidelity = utente.CodFidelity;
            }

            PasswordHasher Hasher = new PasswordHasher();

            //Criptiamo la Password
            utente.Password = Hasher.Hash(utente.Password);

            bool retVal = await userRepository.InsUtente(utente);
            
            //verifichiamo che i dati siano stati regolarmente inseriti nel database
            if (!retVal)
            {
                return StatusCode(500, new ErrMsg($"Ci sono stati problemi nell'inserimento dell'Utente {utente.UserId}.",
                    this.HttpContext.Response.StatusCode));
            }

            return Ok(new InfoMsg(DateTime.Today, $"Inserimento Utente {utente.UserId} eseguito con successo!"));
        }

        [HttpDelete("elimina/{userid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(InfoMsg))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrMsg))]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ErrMsg))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrMsg))]
        public async Task<ActionResult<InfoMsg>> DeleteUser(string UserId)
        {
            if (UserId == "")
            {
                return BadRequest(new ErrMsg("E' necessario inserire la userid dell'utente!",
                    this.HttpContext.Response.StatusCode));
            }

            //Contolliamo se l'articolo è presente (Usare il metodo senza Traking)
            var user = await userRepository.GetUserToDelete(UserId);

            if (user == null)
            {
                return StatusCode(422, new ErrMsg($"Utente {UserId} NON presente in anagrafica! Impossibile Eliminare!",
                    this.HttpContext.Response.StatusCode));
            }

            bool retVal = await userRepository.DelUtente(user);

            //verifichiamo che i dati siano stati regolarmente eliminati dal database
            if (!retVal)
            {
                //ModelState.AddModelError("", $"Ci sono stati problemi nella eliminazione dell'Articolo {articolo.CodArt}.  ");
                return StatusCode(500, new ErrMsg($"Ci sono stati problemi nella eliminazione dell'utente {user.UserId}.",
                    this.HttpContext.Response.StatusCode));
            }

            return Ok(new InfoMsg(DateTime.Today, $"Eliminazione utente {user.UserId} eseguita con successo!"));

        }

    }
}
