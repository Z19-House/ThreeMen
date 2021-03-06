﻿using System;
using System.Collections.Generic;

namespace fishbuy.Data
{
    public partial class Post
    {
        public Post()
        {
            Collection = new HashSet<Collection>();
            Comment = new HashSet<Comment>();
            MediaLink = new HashSet<MediaLink>();
        }

        public int PostId { get; set; }
        public int UserId { get; set; }
        public DateTime UpTime { get; set; }
        public DateTime EditTime { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Tags { get; set; }
        public string Status { get; set; }
        public decimal Price { get; set; }
        public string Address { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<Collection> Collection { get; set; }
        public virtual ICollection<Comment> Comment { get; set; }
        public virtual ICollection<MediaLink> MediaLink { get; set; }
    }
}
