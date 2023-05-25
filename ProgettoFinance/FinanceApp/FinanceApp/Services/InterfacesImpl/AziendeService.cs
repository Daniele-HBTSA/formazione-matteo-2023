using FinanceApp.Models;
using FinanceApp.Repository;

namespace FinanceApp.Services.InterfacesImpl
{
    public class AziendeService : IAziendeService
    {
        public IAziendeRepository aziendeRepository { get; set; }
        public IMovimentiRepository movimentiRepository { get; set; }

        public AziendeService(IAziendeRepository aziendeRepository, IMovimentiRepository movimentiRepository)
        {
            this.aziendeRepository = aziendeRepository;
            this.movimentiRepository = movimentiRepository;
        }

        public async Task<List<AziendaDTO>> ElencoAziende()
        {
            return await aziendeRepository.SelezionaAziende();
        }

        public async Task<AziendaDTO> NuovaAzienda(AziendaDTO datiAzienda)
        {
            return await aziendeRepository.AggiungiAzienda(datiAzienda);
        }

        //Somma di tutti i movimenti
        public async Task<int> CalcolaSaldoAzienda(int idAzienda)
        {
            List<MovimentoDTO> elencoMovimenti = await movimentiRepository.SelezionaMovimentiPerAzienda(idAzienda);

            int nuovoSaldo = elencoMovimenti.Sum(element => element.ValoreMovimento);
            return await aziendeRepository.AggiornaSaldo(idAzienda, nuovoSaldo);
        }
    }
}
