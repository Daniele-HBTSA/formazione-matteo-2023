using FinanceApp.Models;

namespace FinanceApp.Repository
{
    public interface IAziendeRepository
    {
        public Task<List<AziendaDTO>> SelezionaAziende();
        public Task<AziendaDTO> SelezionaAziendaPerID(int idAzienda);
        public Task<AziendaDTO> AggiungiAzienda(AziendaDTO datiAzienda);
        public Task<bool> AggiornaSaldo(int idAzienda, int nuovoCapitale);
    }
}
