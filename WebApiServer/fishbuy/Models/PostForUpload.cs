using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fishbuy.Models
{
    public class PostForUpload
    {
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Tags { get; set; }
        public string Status { get; set; }
        public decimal Price { get; set; }
        public string Address { get; set; }

        public List<MediaSmall> Medias { get; set; }
    }
}
