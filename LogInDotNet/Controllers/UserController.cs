using LogInDotNet.Context;
using LogInDotNet.Model;
using LogInDotNet.Repository;
using LogInDotNet.Service;
using LogInDotNet.Service.InterfacesImpl;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace LogInDotNet.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public IAutenticationService AuthService { get; set; }
        public IRegistrationService RegiService { get; set; }
        public IGetTableService GetTableService { get; set; }


        public UserController(IAutenticationService AuthService, IRegistrationService regiService, IGetTableService GetTableService)
        {
            this.AuthService = AuthService;
            this.RegiService = regiService;  
            this.GetTableService = GetTableService;
        }

        [Route("/newuser")]
        [HttpPost]
        public async Task<ActionResult> Registration(UserDTO user)
        {
            Boolean response = await RegiService.NewUser(user);
            return Ok(response); //Questo mi ritornerà sempre codice 200 anche se la connessione fallisce...
        }

        [Route("/login")]
        [HttpPost]
        public async Task<ActionResult> LogIn(UserDTO user)
        {
            Boolean response = await AuthService.authenticate(user);
            if (response)
            {
                return Ok(response);

            }
            else
            {
                return BadRequest();
            }
        }

        [Route("/homepage")]
        [HttpGet]
        public async Task<List<UserDTO>> ShowAllUsers()
        {
            List<UserDTO> result = await GetTableService.GetUsersTable();
            return result;
        }




    }
}
