using System;
using System.Collections.Generic;

namespace FinanceApp.Context
{
    public partial class Aziende
    {
        public Aziende()
        {
            Movimenti = new HashSet<Movimenti>();
        }

        public int ID_AZIENDA { get; set; }
        public string ACCOUNT_AZIENDA { get; set; } = null!;
        public string PASSWORD_AZIENDA { get; set; } = null!;
        public string NOME_AZIENDA { get; set; } = null!;
        public int? CAPITALE_AZIENDA { get; set; }

        public virtual ICollection<Movimenti> Movimenti { get; set; }
    }
}
