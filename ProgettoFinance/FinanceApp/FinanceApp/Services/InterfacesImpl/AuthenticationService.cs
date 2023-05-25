using FinanceApp.Models;
using FinanceApp.Repository;

namespace FinanceApp.Services.InterfacesImpl
{
    public class AuthenticationService : IAuthenticationService
    {
        public IAziendeRepository aziendeRepository { get; set; }

        public AuthenticationService(IAziendeRepository repository) 
        {
            this.aziendeRepository = repository;
        }

        //Selezionatori entità:

        public async Task<List<AziendaDTO>> SelezionaElencoEntitaAziende()
        {
            List<AziendaDTO>? listaAziende = await aziendeRepository.SelezionaAziende();

            if(listaAziende == null)
            {
                throw new Exception("Lista non trovata");
            }

            return listaAziende;
        }

        /*================================================================*/

        public async Task<AziendaDTO> Autenticazione(string username, string password)
        {
            List<AziendaDTO> listaAziende = await this.SelezionaElencoEntitaAziende();
            AziendaDTO? aziendaCorrente = listaAziende.FirstOrDefault(element => element.AccountAzienda.Contains(username) && element.PswAzienda.Contains(password));
            
            if(aziendaCorrente == null)
            {
                throw new Exception();
            }

            return aziendaCorrente;

        }

        public async Task<bool> Registrazione(AziendaDTO nuovaAzienda)
        {
            List<AziendaDTO> listaAziende = await this.SelezionaElencoEntitaAziende();

            if (!listaAziende.Exists(element => element.AccountAzienda.Equals(nuovaAzienda.AccountAzienda)))
            {
                await aziendeRepository.AggiungiAzienda(nuovaAzienda);
                return true;

            } else
            {
                await Console.Out.WriteLineAsync("Account già esistente");
                return false;

            }
        }
    }
}
