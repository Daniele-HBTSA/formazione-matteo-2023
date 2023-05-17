using LogInDotNet.Context;
using LogInDotNet.Model;
using LogInDotNet.Repository;
using LogInDotNet.Repository.InterfacesImpl;
using System.Data.SqlClient;

namespace LogInDotNet.Service.InterfacesImpl
{
    public class AuthenticationService : IAutenticationService
    {
        public IUserRepository UserRepository { get; set; }

        public AuthenticationService(IUserRepository userRepository)
        {
            UserRepository = userRepository;

        }

        //Controlla se i dati utente esistono nel DB
        public async Task<bool> authenticate(UserDTO userInfo) //Riceve i dati dal controller
        {
            List<UserDTO> usersList = await UserRepository.SelectUsers(); //Riceve di dati dalla repository

            if (usersList.Exists(element => element.UserName.Contains(userInfo.UserName)))
            {
                if (usersList.Exists(element => element.UserPsw.Equals(userInfo.UserPsw)))
                {
                    return true;
                }
                else
                {
                    await Console.Out.WriteLineAsync("Password errata");
                    return false;
                }
            }
            else
            {
                await Console.Out.WriteLineAsync("Utente non trovato");
                return false;
            }
        }
    }
}
