using LogInDotNet.Model;
using LogInDotNet.Repository;
using LogInDotNet.Repository.InterfacesImpl;

namespace LogInDotNet.Service.InterfacesImpl
{
    public class GetTableService : IGetTableService
    {
        public IUserRepository UserRepository { get; set; }

        public GetTableService(IUserRepository userRepository)
        {
            UserRepository = userRepository;
        }

        public async Task<List<UserDTO>> GetUsersTable()
        {
            return await UserRepository.SelectUsers();
        }
    }
}
