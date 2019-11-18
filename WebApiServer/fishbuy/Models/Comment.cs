using System;
using System.Collections.Generic;

namespace fishbuy.Models
{
    public partial class Comment
    {
        public string CommentId { get; set; }
        public int GoodsId { get; set; }
        public string UpTime { get; set; }
        public string Comment1 { get; set; }
        public int UserId { get; set; }

        public virtual Goods Goods { get; set; }
        public virtual User User { get; set; }
    }
}
