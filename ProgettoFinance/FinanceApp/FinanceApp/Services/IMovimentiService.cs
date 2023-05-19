using FinanceApp.Models;

namespace FinanceApp.Services
{
    public interface IMovimentiService
    {
        public Task<List<MovimentoDTO>> MostraMovimenti(int IdAzienda);
        public Task<bool> NuovoMovimento(MovimentoDTO nuovoMovimento);
        public Task<bool> RimuoviMovimento(int IdRimuvomento);
        
    }
}
