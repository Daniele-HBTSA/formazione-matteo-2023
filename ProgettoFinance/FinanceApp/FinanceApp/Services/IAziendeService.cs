using FinanceApp.Models;

namespace FinanceApp.Services
{
    public interface IAziendeService
    {
        public Task<List<AziendaDTO>> ElencoAziende();
        public Task<AziendaDTO> NuovaAzienda(AziendaDTO datiAzienda);
        public Task<int> CalcolaSaldoAzienda(int idAzienda);
    }
}
