using System;
using System.Collections.Generic;

namespace fishbuy.Models
{
    public partial class RefreshToken
    {
        public string Token { get; set; }
        public int UserId { get; set; }
        public DateTime Expires { get; set; }

        public virtual User User { get; set; }
    }
}
