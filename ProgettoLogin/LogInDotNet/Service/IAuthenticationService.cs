using LogInDotNet.Model;

namespace LogInDotNet.Service
{
    public interface IAutenticationService
    {
        public Task<bool> authenticate(UserDTO userInfo);
    }
}
