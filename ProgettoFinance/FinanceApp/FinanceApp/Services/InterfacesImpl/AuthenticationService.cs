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

        public async Task<bool> Autenticazione(string username, string password)
        {
            List<AziendaDTO> listaAziende = await this.SelezionaElencoEntitaAziende();

            if (listaAziende.Exists(element => element.AccountAzienda.Equals(username)))
            {
                if(listaAziende.Exists(element => element.PswAzienda.Equals(password)))
                {
                    return true;

                } else
                {
                    await Console.Out.WriteLineAsync("Password sbagliata");
                    return false;
                }
            } else
            {
                await Console.Out.WriteLineAsync("Account sbagliato");
                return false;
            }
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
