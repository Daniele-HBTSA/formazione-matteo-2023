﻿using FinanceApp.Models;
using FinanceApp.Repository.Interfaces;
using FinanceApp.Services.Interfaces;
using FinanceApp.Utils.Enums;

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

        public async Task<int> CalcolaSaldoAzienda(MovimentoDTO movimento, Operazione tipoOperazione)
        {
            return await aziendeRepository.AggiornaSaldo(movimento, tipoOperazione);
        }
    }
}
