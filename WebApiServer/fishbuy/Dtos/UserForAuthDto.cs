using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace fishbuy.Dtos
{
    public class UserForAuthDto
    {
        [Required]
        [StringLength(16, MinimumLength = 2, ErrorMessage = "You must specify a username between 2 and 16 characters.")]
        public string Username { get; set; }
        [Required]
        [StringLength(32, MinimumLength = 6, ErrorMessage = "You must specify a password between 6 and 32 characters.")]
        public string Password { get; set; }
    }
}
