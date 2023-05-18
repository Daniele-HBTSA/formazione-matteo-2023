using FinanceApp.Models;

namespace FinanceApp.Repository
{
    public interface IAziendeRepository
    {
        public Task<List<AziendaDTO>> SelezionaAziende();
        public Task<AziendaDTO> SelezionaAziendaPerNome(string nomeAzienda);
        public Task<Boolean> AggiungiAzienda(AziendaDTO nuovaAzienda);
        public Task<Boolean> ModificaCapitale();
    }
}
