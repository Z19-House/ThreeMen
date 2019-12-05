using System;
using System.Collections.Generic;

namespace fishbuy.Models
{
    public partial class Media
    {
        public string MediaId { get; set; }
        public int GoodsId { get; set; }
        public string ResType { get; set; }
        public string ResUri { get; set; }

        public virtual Goods Goods { get; set; }
    }
}
