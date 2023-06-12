using FinanceApp.Context;
using FinanceApp.Models;
using FinanceApp.Utils.Enums;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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

            if (listaDB.Count == 0)
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

        public async Task<AziendaDTO> UltimaAzienda()
        {
            Aziende? ultimoAzienda = await Context.Aziende.OrderBy(element => element.ID_AZIENDA).LastOrDefaultAsync();

            if (ultimoAzienda == null)
            {
                throw new Exception();
            }

            return await this.SelezionaAziendaPerID(ultimoAzienda.ID_AZIENDA);
        }

        /*================================================================*/

        public async Task<List<AziendaDTO>> SelezionaAziende()
        {
            List<AziendaDTO> listaAziende = new List<AziendaDTO>();

            List<Aziende> listaDB = await this.SelezionaEntitaAziende();
            listaAziende = listaDB.Select(element => new AziendaDTO
            {
                IdAzienda = element.ID_AZIENDA,
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
            azienda.IdAzienda = infoDB.ID_AZIENDA;
            azienda.AccountAzienda = infoDB.ACCOUNT_AZIENDA;
            azienda.PswAzienda = infoDB.PASSWORD_AZIENDA;
            azienda.NomeAzienda = infoDB.NOME_AZIENDA;
            azienda.SaldoAzienda = infoDB.SALDO_AZIENDA;

            return azienda;
        }

        public async Task<AziendaDTO> AggiungiAzienda(AziendaDTO datiAzienda)
        {
            await Console.Out.WriteLineAsync(datiAzienda.ToString());
            Aziende nuovaAzienda = new Aziende();
            nuovaAzienda.ACCOUNT_AZIENDA = datiAzienda.AccountAzienda;
            nuovaAzienda.PASSWORD_AZIENDA = datiAzienda.PswAzienda;
            nuovaAzienda.NOME_AZIENDA = datiAzienda.NomeAzienda;
            nuovaAzienda.SALDO_AZIENDA = datiAzienda.SaldoAzienda;

            await Context.Aziende.AddAsync(nuovaAzienda);
            await Context.SaveChangesAsync();
            return await this.UltimaAzienda();
        }
        public async Task<int> AggiornaSaldo(MovimentoDTO movimento, Operazione tipoOperazione)
        {
            Aziende aziendaDB = await this.SelezionaEntitaAziendaPerID(movimento.IdAzienda);
            if (tipoOperazione.Equals(Operazione.ADDIZIONE))
            {
                aziendaDB.SALDO_AZIENDA += movimento.ValoreMovimento;
                Context.Aziende.Update(aziendaDB);
                await Context.SaveChangesAsync();
            }
            else if (tipoOperazione.Equals(Operazione.SOTTRAZIONE))
            {
                aziendaDB.SALDO_AZIENDA -= movimento.ValoreMovimento;
                Context.Aziende.Update(aziendaDB);
                await Context.SaveChangesAsync();
            }

            return aziendaDB.SALDO_AZIENDA.GetValueOrDefault();
        }

        //public async Task<int> AggiornaSaldo(int idAzienda, int nuovoSaldo)
        //{
        //    Aziende aziendaDB = await this.SelezionaEntitaAziendaPerID(idAzienda);
        //    if(nuovoSaldo != 0)
        //    {
        //        aziendaDB.SALDO_AZIENDA += nuovoSaldo;

        //    }

        //    Context.Aziende.Update(aziendaDB);
        //    await Context.SaveChangesAsync();
        //    return aziendaDB.SALDO_AZIENDA.GetValueOrDefault();

        //}
    }
}
