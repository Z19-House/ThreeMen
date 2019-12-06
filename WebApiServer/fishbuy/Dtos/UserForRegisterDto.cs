using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace fishbuy.Dtos
{
    public class UserForRegisterDto
    {
        [Required]
        [StringLength(16, MinimumLength = 2, ErrorMessage = "You must specify a username between 2 and 16 characters.")]
        public string UserName { get; set; }
        [Required]
        [StringLength(32, MinimumLength = 2, ErrorMessage = "You must specify a nickname between 2 and 32 characters.")]
        public string Nickname { get; set; }
        [Required]
        [StringLength(32, MinimumLength = 6, ErrorMessage = "You must specify a password between 6 and 32 characters.")]
        public string Password { get; set; }
        public string Phone { get; set; }
        public string BirthDate { get; set; }
        public string Sex { get; set; }
        public string Address { get; set; }
        public string ImageUrl { get; set; }

    }
}
