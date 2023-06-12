using System;
using System.Collections.Generic;

namespace FinanceApp.Context
{
    public partial class Movimenti
    {
        public int ID_MOVIMENTO { get; set; }
        public int ID_AZIENDA { get; set; }
        public int VALORE_MOVIMENTO { get; set; }

        public virtual Aziende ID_AZIENDANavigation { get; set; } = null!;
    }
}
