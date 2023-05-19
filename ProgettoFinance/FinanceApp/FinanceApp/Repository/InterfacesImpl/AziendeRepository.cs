using FinanceApp.Context;
using FinanceApp.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanceApp.Repository.InterfacesImpl
{
    public class AziendeRepository : IAziendeRepository
    {
        public FinanceAppContext Context { get; set; }

        public AziendeRepository(FinanceAppContext context)
        {
            Context = context;
        }

        //Selezionatori entità:
        public async Task<List<Aziende>> SelezionaEntitaAziende()
        {
            List<Aziende>? listaDB = await Context.Aziende.ToListAsync();

            if (listaDB == null)
            {
                throw new Exception("Azienda non trovata");
            }

            return listaDB;
        }

        public async Task<Aziende> SelezionaEntitaAziendaPerID(int idAzienda)
        {
            Aziende? infoDB = await Context.Aziende.FirstOrDefaultAsync(element => element.ID_AZIENDA.Equals(idAzienda));

            if (infoDB == null)
            {
                throw new Exception("Azienda non trovata");
            }
            return infoDB;
        }

        /*================================================================*/

        public async Task<List<AziendaDTO>> SelezionaAziende()
        {
            List<AziendaDTO> listaAziende = new List<AziendaDTO>();

            List<Aziende> listaDB = await this.SelezionaEntitaAziende();
            listaAziende = listaDB.Select(element => new AziendaDTO
            {
                AccountAzienda = element.ACCOUNT_AZIENDA,
                PswAzienda = element.PASSWORD_AZIENDA,
                NomeAzienda = element.NOME_AZIENDA,
                SaldoAzienda = element.SALDO_AZIENDA

            }).ToList();

            return listaAziende;
        }

        public async Task<AziendaDTO> SelezionaAziendaPerID(int idAzienda)
        {
            Aziende infoDB = await this.SelezionaEntitaAziendaPerID(idAzienda);

            AziendaDTO azienda = new AziendaDTO();
            azienda.AccountAzienda = infoDB.ACCOUNT_AZIENDA;
            azienda.PswAzienda = infoDB.PASSWORD_AZIENDA;
            azienda.NomeAzienda = infoDB.NOME_AZIENDA;
            azienda.SaldoAzienda = infoDB.SALDO_AZIENDA;

            return azienda;
        }

        public async Task<bool> AggiungiAzienda(AziendaDTO datiAzienda)
        {
            await Console.Out.WriteLineAsync(datiAzienda.ToString());
            Aziende nuovaAzienda = new Aziende();
            nuovaAzienda.ACCOUNT_AZIENDA = datiAzienda.AccountAzienda;
            nuovaAzienda.PASSWORD_AZIENDA = datiAzienda.PswAzienda;
            nuovaAzienda.NOME_AZIENDA = datiAzienda.NomeAzienda;
            nuovaAzienda.SALDO_AZIENDA = datiAzienda.SaldoAzienda;

            try
            {
                await Context.Aziende.AddAsync(nuovaAzienda);
                await Context.SaveChangesAsync();
                return true;

            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                return false;
            }
        }

        public async Task<bool> AggiornaSaldo(int idAzienda, int nuovoSaldo)
        {
            Aziende aziendaDB = await this.SelezionaEntitaAziendaPerID(idAzienda);
            aziendaDB.SALDO_AZIENDA += nuovoSaldo;

            try
            {
                Context.Aziende.Update(aziendaDB);
                await Context.SaveChangesAsync();
                return true;

            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                return false;
            }
        }
    }
}
