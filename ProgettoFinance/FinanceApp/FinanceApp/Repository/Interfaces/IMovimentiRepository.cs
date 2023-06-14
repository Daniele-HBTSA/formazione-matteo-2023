using FinanceApp.Models;

namespace FinanceApp.Repository.Interfaces
{
    public interface IMovimentiRepository
    {
        public Task<List<MovimentoDTO>> SelezionaMovimentiPerAzienda(int idAzienda);
        public Task<MovimentoDTO> SelezionaMovimentoPerID(int IdAzienda);
        public Task<MovimentoDTO> AggiungiMovimento(MovimentoDTO nuovoMovimento);
        public Task<MovimentoDTO> EliminaMovimento(int IdMovimento);
    }
}
