using FinanceApp.Models;
using FinanceApp.Repository.Interfaces;
using FinanceApp.Services.Interfaces;

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

        public async Task<MovimentoDTO> SelezionaMovimento(int idMovimento)
        {
            return await movimentiRepository.SelezionaMovimentoPerID(idMovimento);
        }

        public async Task<MovimentoDTO> NuovoMovimento(MovimentoDTO nuovoMovimento)
        {
            return await movimentiRepository.AggiungiMovimento(nuovoMovimento); ;
        }

        public async Task<MovimentoDTO> RimuoviMovimento(int idMovimento)
        {
            return await movimentiRepository.EliminaMovimento(idMovimento);
        }
    }
}
