using FinanceApp.Context;
using FinanceApp.Models;
using FinanceApp.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FinanceApp.Repository.InterfacesImpl
{
    public class MovimentiRepository : IMovimentiRepository
    {
        public FinanceAppContext Context { get; set; }

        public MovimentiRepository(FinanceAppContext context)
        {
            Context = context;
        }

        //Selezionatori entità:

        public async Task<List<Movimenti>> SelezionaEntitaMovimentiPerAzienda(int idAzienda)
        {
            List<Movimenti>? listaDB = await Context.Movimenti.Where(element => element.ID_AZIENDA.Equals(idAzienda)).ToListAsync();

            if (listaDB == null)
            {
                throw new Exception("Lista non trovata");
            }

            return listaDB;
        }

        public async Task<Movimenti> SelezionaEntitaMovimentoPerID(int idMovimento)
        {
            Movimenti? infoDB = await Context.Movimenti.FirstOrDefaultAsync(element => element.ID_MOVIMENTO.Equals(idMovimento));

            if(infoDB == null)
            {
                throw new Exception("Movimento non trovato");
            }
            return infoDB;
        }

        public async Task<MovimentoDTO> UltimoMovimento()
        {

            Movimenti? ultimoMovimento = await Context.Movimenti.OrderBy(element => element.ID_MOVIMENTO).LastOrDefaultAsync();

            if (ultimoMovimento == null)
            {
                throw new Exception();
            }

            return await this.SelezionaMovimentoPerID(ultimoMovimento.ID_MOVIMENTO);
        }

        /*================================================================*/

        public async Task<List<MovimentoDTO>> SelezionaMovimentiPerAzienda(int idAzienda)
        {
            List<Movimenti> listaDB = await this.SelezionaEntitaMovimentiPerAzienda(idAzienda);
            listaDB.Where(element => element.ID_AZIENDA.Equals(idAzienda)).ToList();

            List<MovimentoDTO> listaMovimenti = new List<MovimentoDTO>();
            listaMovimenti = listaDB.Select(element => new MovimentoDTO
            {

                IdAzienda = element.ID_AZIENDA,
                IdMovimento = element.ID_MOVIMENTO,
                ValoreMovimento = element.VALORE_MOVIMENTO

            }).ToList();

            return listaMovimenti;
        }

        public async Task<MovimentoDTO> SelezionaMovimentoPerID(int idMovimento)
        {
            Movimenti infoDB = await this.SelezionaEntitaMovimentoPerID(idMovimento);

            MovimentoDTO movimento = new MovimentoDTO();
            movimento.IdAzienda = infoDB.ID_AZIENDA;
            movimento.IdMovimento = infoDB.ID_MOVIMENTO;
            movimento.ValoreMovimento = infoDB.VALORE_MOVIMENTO;

            return movimento;
        }

        public async Task<MovimentoDTO> AggiungiMovimento(MovimentoDTO datiMovimento)
        {
            Movimenti nuovoMovimento = new Movimenti();
            nuovoMovimento.ID_AZIENDA = datiMovimento.IdAzienda;
            nuovoMovimento.VALORE_MOVIMENTO = datiMovimento.ValoreMovimento;

            await Context.Movimenti.AddAsync(nuovoMovimento);
            await Context.SaveChangesAsync();

            return await this.UltimoMovimento();
        }

        public async Task<MovimentoDTO> EliminaMovimento(int idMovimento)
        {
            MovimentoDTO movimentoDaEliminare = await this.SelezionaMovimentoPerID(idMovimento);
            Movimenti entitaMovimento = await this.SelezionaEntitaMovimentoPerID(idMovimento);

            if(movimentoDaEliminare.IdMovimento != entitaMovimento.ID_MOVIMENTO)
            {
                throw new Exception();
            }

            Context.Movimenti.Remove(entitaMovimento);
            await Context.SaveChangesAsync();

            return movimentoDaEliminare;
        }
    }
}
