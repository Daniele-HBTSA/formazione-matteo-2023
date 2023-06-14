using FinanceApp.Models;
using FinanceApp.Utils.Enums;

namespace FinanceApp.Repository.Interfaces
{
    public interface IAziendeRepository
    {
        public Task<List<AziendaDTO>> SelezionaAziende();
        public Task<AziendaDTO> SelezionaAziendaPerID(int idAzienda);
        public Task<AziendaDTO> AggiungiAzienda(AziendaDTO datiAzienda);
        public Task<int> AggiornaSaldo(MovimentoDTO movimento, Operazione tipoOperazione);
    }
}
