using LogInDotNet.Model;

namespace LogInDotNet.Repository
{
    public interface IUserRepository
    {
        public Task<List<UserDTO>> SelectUsers();
        public Task<bool> AddUser(UserDTO user);

        public Task<UserDTO> GetDataById(int userId);
    }
}
