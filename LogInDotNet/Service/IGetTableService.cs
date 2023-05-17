using LogInDotNet.Model;

namespace LogInDotNet.Service
{
    public interface IGetTableService
    {
        public Task<List<UserDTO>> GetUsersTable();
    }
}
