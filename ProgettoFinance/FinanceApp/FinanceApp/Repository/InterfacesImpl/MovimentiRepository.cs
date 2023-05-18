using FinanceApp.Context;
using FinanceApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace FinanceApp.Repository.InterfacesImpl
{
    public class MovimentiRepository : IMovimentiRepository
    {
        public FinanceAppContext Context { get; set; }

        public MovimentiRepository(FinanceAppContext context)
        {
            Context = context;
        }

        public async Task<List<MovimentoDTO>> SelezionaMovimenti()
        {
            List<MovimentoDTO> listaMovimenti = new List<MovimentoDTO>();

            try
            {
                List<Movimenti> listaDB = await Context.Movimenti.ToListAsync();
                listaMovimenti = listaDB.Select(element => new MovimentoDTO
                {

                }).ToList();

                return listaMovimenti;

            }
            catch (SqlException ex)
            {
                return null;
            }
        }

        public async Task<MovimentoDTO> SelezionaMovimentoPerID(int idAzienda)
        {
            Movimenti? infoDB = await Context.Movimenti.FirstOrDefaultAsync(element => element.ID_AZIENDA.Equals(idAzienda));

            MovimentoDTO? movimento = new MovimentoDTO();
            movimento.IdAzienda = infoDB.ID_AZIENDA;
            movimento.ValoreMovimento = infoDB.VALORE_MOVIMENTO;

            return movimento;
        }

        public async Task<bool> AggiungiMovimento(MovimentoDTO datiMovimento)
        {
            Movimenti nuovoMovimento = new Movimenti();
            nuovoMovimento.ID_AZIENDA = datiMovimento.IdAzienda;
            nuovoMovimento.VALORE_MOVIMENTO = datiMovimento.ValoreMovimento;

            try
            {
                await Context.Movimenti.AddAsync(nuovoMovimento);
                await Context.SaveChangesAsync();
                return true;

            }
            catch (SqlException ex)
            {
                return false;
            }
        }

        public async Task<bool> EliminaMovimento(int IdMovimento)
        {
            Movimenti? movimento = await Context.Movimenti.FirstOrDefaultAsync(element => element.ID_MOVIMENTO.Equals(IdMovimento));
            
            try
            {
                if(movimento != null)
                {
                    Context.Movimenti.Remove(movimento);
                    await Context.SaveChangesAsync();
                    return true;

                } else
                {
                    return false;
                }
            } catch (SqlException ex)
            {
                return false;
            }
        }
    }
}
