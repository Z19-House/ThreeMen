using System;
using System.Collections.Generic;

namespace fishbuy.Models
{
    public partial class Goods
    {
        public Goods()
        {
            Comment = new HashSet<Comment>();
        }

        public int GoodsId { get; set; }
        public int UserId { get; set; }
        public string UpTime { get; set; }
        public string EditTime { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Tags { get; set; }
        public string Status { get; set; }
        public decimal Price { get; set; }
        public string Address { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<Comment> Comment { get; set; }
    }
}
