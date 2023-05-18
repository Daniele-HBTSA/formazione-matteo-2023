using LogInDotNet.Context;
using LogInDotNet.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;

namespace LogInDotNet.Repository.InterfacesImpl
{
    public class UserRepository : IUserRepository
    {
        public LogInContext LogInContext { get; set; } //Connessione col DB

        public UserRepository(LogInContext context)
        {
            LogInContext = context;
        }


        //Seleziona tutti gli utenti
        public async Task<List<UserDTO>> SelectUsers()
        {
            List<Users> users = new List<Users>();
            List<UserDTO> result = new List<UserDTO>();

            try
            {
                users = await LogInContext.Users.ToListAsync();

                /*
                result = users.Select(x => new UserDTO(x.UserName, x.UserPsw)
                {
                    UserName = x.UserName,
                    UserPsw = x.UserPsw

                }).ToList();
                */

                foreach (var user in users)
                {
                    UserDTO userDTO = new UserDTO();
                    userDTO.UserName = user.UserName;
                    userDTO.UserPsw = user.UserPsw;
                    result.Add(userDTO);
                }
            }
            catch (SqlException ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
            }

            return result;
        }

        //Aggiunge nuovo utente al DB
        public async Task<Boolean> AddUser(UserDTO user)
        {
            Users newUser = new Users();
            newUser.UserName = user.UserName;
            newUser.UserPsw = user.UserPsw;

            await LogInContext.Users.AddAsync(newUser); //Aggiunge nuovo utente al db
            LogInContext.SaveChanges();
            return true;
        }

        //Seleziona tabella per ID
        public async Task<UserDTO> GetDataById(int userId)
        {
            Users userData = await LogInContext.Users.FirstOrDefaultAsync(element => element.UserID.Equals(userId));

            UserDTO requestedData = new UserDTO();
            requestedData.UserId = userData.UserID;
            requestedData.UserName = userData.UserName;
            requestedData.UserPsw = userData.UserPsw;
            return requestedData;

        }
    }
}
