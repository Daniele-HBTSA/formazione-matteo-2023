using LogInDotNet.Model;

namespace LogInDotNet.Service
{
    public interface IRegistrationService
    {
        public Task<bool> NewUser(UserDTO user);
    }
}
