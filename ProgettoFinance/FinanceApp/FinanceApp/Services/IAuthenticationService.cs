using FinanceApp.Context;
using FinanceApp.Models;

namespace FinanceApp.Services
{
    public interface IAuthenticationService
    {
        public Task<AziendaDTO> Autenticazione(string username, string password);
        public Task<bool> Registrazione(AziendaDTO nuovaAzienda);
        public Task<Dictionary<string, string>> GetToken(int IdAzienda);
    }
}
