using FinanceApp.Models;

namespace FinanceApp.Services
{
    public interface IAuthenticationService
    {
        public Task<bool> Autenticazione(string username, string password);
        public Task<bool> Registrazione(AziendaDTO nuovaAzienda);
    }
}
