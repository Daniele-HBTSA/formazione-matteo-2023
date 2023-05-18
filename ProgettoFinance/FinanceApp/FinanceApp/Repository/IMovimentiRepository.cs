using FinanceApp.Models;

namespace FinanceApp.Repository
{
    public interface IMovimentiRepository
    {
        public Task<List<MovimentoDTO>> SelezionaMovimenti();
        public Task<MovimentoDTO> SelezionaMovimentoPerCodice(string codiceMovimento);
        public Task<Boolean> AggiungiMovimento(MovimentoDTO nuovoMovimento);
        public Task<Boolean> EliminaMovimento(string codiceMovimento);
        public Task<Boolean> ModificaMovimento(string codiceMovimento, int nuovoValore);
    }
}
