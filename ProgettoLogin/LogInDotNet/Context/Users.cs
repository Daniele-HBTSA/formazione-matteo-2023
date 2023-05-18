using System;
using System.Collections.Generic;

namespace LogInDotNet.Context
{
    public partial class Users
    {
        public int UserID { get; set; }
        public string UserName { get; set; } = null!;
        public string UserPsw { get; set; } = null!;       
    }
}
