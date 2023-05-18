using FinanceApp.Models;

namespace FinanceApp.Repository
{
    public interface IMovimentiRepository
    {
        public Task<List<MovimentoDTO>> SelezionaMovimenti();
        public Task<MovimentoDTO> SelezionaMovimentoPerID(int IdAzienda);
        public Task<bool> AggiungiMovimento(MovimentoDTO nuovoMovimento);
        public Task<bool> EliminaMovimento(int IdMovimento);
    }
}
