using System;
using System.Collections.Generic;

namespace fishbuy.Data
{
    public partial class Collection
    {
        public int UserId { get; set; }
        public int PostId { get; set; }
        public DateTime CollectionTime { get; set; }
        public int Privacy { get; set; }

        public virtual Post Post { get; set; }
        public virtual User User { get; set; }
    }
}
