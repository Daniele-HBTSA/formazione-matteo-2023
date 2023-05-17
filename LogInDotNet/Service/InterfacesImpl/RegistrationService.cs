using LogInDotNet.Context;
using LogInDotNet.Model;
using LogInDotNet.Repository;
using LogInDotNet.Repository.InterfacesImpl;

namespace LogInDotNet.Service.InterfacesImpl
{
    public class RegistrationService : IRegistrationService
    {
        public IUserRepository UserRepository { get; set; }

        public RegistrationService(IUserRepository userRepository)
        {
            UserRepository = userRepository;

        }

        //Registra nuovo utente
        public async Task<bool> NewUser(UserDTO user) //Riceve info dal controller
        {

            List<UserDTO> usersList = await UserRepository.SelectUsers(); //Riceve dati da DB

            if (!usersList.Exists(element => element.UserName.Contains(user.UserName))) //Controlla che non esisti
            {
                bool response = await UserRepository.AddUser(user); //Manda i dati alla repository per aggiungerli al DB
                return true;

            }
            else
            {
                await Console.Out.WriteLineAsync("Utente già esistente!");
                return false;
            }
        }
    }
}

