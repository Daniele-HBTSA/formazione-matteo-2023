using LogInDotNet.Model;

namespace LogInDotNet.Service
{
    public interface IGetTableService
    {
        public Task<UserDTO> GetUserTable(int userId);
    }
}
