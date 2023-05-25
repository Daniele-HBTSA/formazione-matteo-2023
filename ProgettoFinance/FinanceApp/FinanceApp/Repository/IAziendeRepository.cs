using FinanceApp.Models;

namespace FinanceApp.Repository
{
    public interface IAziendeRepository
    {
        public Task<List<AziendaDTO>> SelezionaAziende();
        public Task<AziendaDTO> SelezionaAziendaPerID(int idAzienda);
        public Task<AziendaDTO> AggiungiAzienda(AziendaDTO datiAzienda);
        public Task<int> AggiornaSaldo(int idAzienda, int nuovoCapitale);
    }
}
