using System;
using System.Collections.Generic;

namespace LogInDotNet.Context
{
    public partial class Users
    {
        public int UserID { get; set; }
        public string UserName { get; set; } = null!;
        public string UserPsw { get; set; } = null!;

        public Users() { }

        public override string ToString()
        {
            return string.Format("UserId: {0}, UserName: {1}, Password: {2}.", this.UserID, this.UserName, this.UserPsw);
        }
    }
}
