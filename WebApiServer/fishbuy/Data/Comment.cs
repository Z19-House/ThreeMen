using System;
using System.Collections.Generic;

namespace fishbuy.Data
{
    public partial class Comment
    {
        public string CommentId { get; set; }
        public int PostId { get; set; }
        public string UpTime { get; set; }
        public string Content { get; set; }
        public int UserId { get; set; }

        public virtual Post Post { get; set; }
        public virtual User User { get; set; }
    }
}
