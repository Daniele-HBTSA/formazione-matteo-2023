using FinanceApp.Models;

namespace FinanceApp.Services
{
    public interface IMovimentiService
    {
        public Task<List<MovimentoDTO>> MostraMovimenti(int idAzienda);
        public Task<MovimentoDTO> SelezionaMovimento(int idMovimento);
        public Task<MovimentoDTO> NuovoMovimento(MovimentoDTO nuovoMovimento);
        public Task<MovimentoDTO> RimuoviMovimento(int IdRimuvomento);
        
    }
}
