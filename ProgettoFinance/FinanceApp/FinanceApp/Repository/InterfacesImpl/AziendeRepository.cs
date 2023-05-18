using FinanceApp.Context;
using FinanceApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
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

        public async Task<List<AziendaDTO>?> SelezionaAziende()
        {
            List<AziendaDTO> listaAziende = new List<AziendaDTO>();

            try
            {
                List<Aziende> listaDB = await Context.Aziende.ToListAsync();
                listaAziende = listaDB.Select(element => new AziendaDTO
                {

                    AccountAzienda = element.ACCOUNT_AZIENDA,
                    PswAzienda = element.PASSWORD_AZIENDA,
                    NomeAzienda = element.NOME_AZIENDA,
                    SaldoAzienda = element.SALDO_AZIENDA
                    
                }).ToList();

                return listaAziende;

            } catch (SqlException ex) 
            {
                return null;
            }
        }

        public async Task<AziendaDTO> SelezionaAziendaPerNome(string nomeAzienda)
        {
            Aziende? infoDB = await Context.Aziende.FirstOrDefaultAsync(element => element.NOME_AZIENDA.Contains(nomeAzienda));
            
            AziendaDTO? azienda = new AziendaDTO();
            azienda.AccountAzienda = infoDB.ACCOUNT_AZIENDA;
            azienda.PswAzienda = infoDB.PASSWORD_AZIENDA;
            azienda.NomeAzienda = infoDB.NOME_AZIENDA;
            azienda.SaldoAzienda = infoDB.SALDO_AZIENDA;

            return azienda;
        }

        public async Task<bool> AggiungiAzienda(AziendaDTO datiAzienda)
        {
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

            } catch (SqlException ex)
            {
                return false;
            }

        }

        //Somma di tutti i movimenti
        public Task<bool> ModificaCapitale()
        {
            throw new NotImplementedException();
        }

        //Recupera la somma di tutti i movimenti e aggiorna il capitale.

    }
}
