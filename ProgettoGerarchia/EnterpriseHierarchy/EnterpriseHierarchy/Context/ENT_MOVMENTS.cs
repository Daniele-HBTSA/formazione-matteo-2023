using System;
using System.Collections.Generic;

namespace EnterpriseHierarchy.Context
{
    public partial class ENT_MOVMENTS
    {
        public int ID_MOVMENT { get; set; }
        public string? MOV_CAUSAL { get; set; }
        public int? MOV_COST { get; set; }
        public int? MOV_INCOME { get; set; }
        public int ID_ENTERPRISE { get; set; }

        public virtual ENTERPRISES ID_ENTERPRISENavigation { get; set; } = null!;
    }
}
