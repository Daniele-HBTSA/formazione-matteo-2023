using FinanceApp.Models;
using FinanceApp.Repository;

namespace FinanceApp.Services.InterfacesImpl
{
    public class MovimentiService : IMovimentiService
    {
        public IMovimentiRepository movimentiRepository { get; set; }

        public MovimentiService(IMovimentiRepository repository)
        {
            this.movimentiRepository = repository;
        }

        public async Task<List<MovimentoDTO>> MostraMovimenti(int IdAzienda)
        {
            return await movimentiRepository.SelezionaMovimentiPerAzienda(IdAzienda);
        }

        public async Task<bool> NuovoMovimento(MovimentoDTO nuovoMovimento)
        {
            if(nuovoMovimento != null)
            {
                bool risposta = await movimentiRepository.AggiungiMovimento(nuovoMovimento);
                return risposta;

            } else 
            {
                return false;
            }
        }

        public async Task<bool> RimuoviMovimento(int idMovimento)
        {
            bool risposta = await movimentiRepository.EliminaMovimento(idMovimento);
            return risposta;
        }
    }
}
