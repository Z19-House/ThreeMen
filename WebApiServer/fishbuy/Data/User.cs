using System;
using System.Collections.Generic;

namespace fishbuy.Data
{
    public partial class User
    {
        public User()
        {
            Collection = new HashSet<Collection>();
            Comment = new HashSet<Comment>();
            Post = new HashSet<Post>();
            RefreshToken = new HashSet<RefreshToken>();
        }

        public int UserId { get; set; }
        public string Username { get; set; }
        public string Nickname { get; set; }
        public string PasswordHash { get; set; }
        public string Phone { get; set; }
        public string BirthDate { get; set; }
        public string Sex { get; set; }
        public string Address { get; set; }
        public string ImageUrl { get; set; }

        public virtual ICollection<Collection> Collection { get; set; }
        public virtual ICollection<Comment> Comment { get; set; }
        public virtual ICollection<Post> Post { get; set; }
        public virtual ICollection<RefreshToken> RefreshToken { get; set; }
    }
}
