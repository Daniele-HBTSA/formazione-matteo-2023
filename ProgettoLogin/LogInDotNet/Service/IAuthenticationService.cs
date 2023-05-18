using LogInDotNet.Model;

namespace LogInDotNet.Service
{
    public interface IAutenticationService
    {
        public Task<Boolean> authenticate(UserDTO userInfo);
    }
}
