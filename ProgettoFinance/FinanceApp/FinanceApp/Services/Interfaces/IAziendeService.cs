﻿using FinanceApp.Models;
using FinanceApp.Utils.Enums;

namespace FinanceApp.Services.Interfaces
{
    public interface IAziendeService
    {
        public Task<List<AziendaDTO>> ElencoAziende();
        public Task<AziendaDTO> NuovaAzienda(AziendaDTO datiAzienda);
        public Task<int> CalcolaSaldoAzienda(MovimentoDTO movimento, Operazione tipoOperazione);
    }
}
