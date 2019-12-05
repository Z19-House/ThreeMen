using System;
using System.Collections.Generic;

namespace fishbuy.Models
{
    public partial class MediaLink
    {
        public string MediaId { get; set; }
        public int PostId { get; set; }
        public string ResType { get; set; }
        public string ResUri { get; set; }

        public virtual Post Post { get; set; }
    }
}
