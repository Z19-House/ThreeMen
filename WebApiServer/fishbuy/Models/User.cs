using System;
using System.Collections.Generic;

namespace fishbuy.Models
{
    public partial class User
    {
        public User()
        {
            Comment = new HashSet<Comment>();
            Goods = new HashSet<Goods>();
        }

        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Nickname { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Birthdate { get; set; }
        public string Sex { get; set; }
        public string Address { get; set; }

        public virtual ICollection<Comment> Comment { get; set; }
        public virtual ICollection<Goods> Goods { get; set; }
    }
}
