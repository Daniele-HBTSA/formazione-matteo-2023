using System;
using System.Collections.Generic;

namespace EnterpriseHierarchy.Context
{
    public partial class ENTERPRISES
    {
        public ENTERPRISES()
        {
            ENT_MOVMENTS = new HashSet<ENT_MOVMENTS>();
        }

        public int ID_ENTERPRISE { get; set; }
        public string ENT_CODE { get; set; } = null!;
        public string? ENT_ADDRESS { get; set; }
        public int? ENT_PARENT_ID { get; set; }
        public int ENT_BALACE { get; set; }

        public virtual ICollection<ENT_MOVMENTS> ENT_MOVMENTS { get; set; }
    }
}
