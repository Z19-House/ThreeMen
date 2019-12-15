using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace fishbuy.Models
{
    public class UserChangePassword
    {
        /// <summary>
        /// 注册时的用户名
        /// </summary>
        [Required]
        [StringLength(16, MinimumLength = 2, ErrorMessage = "You must specify a username between 2 and 16 characters.")]
        public string Username { get; set; }

        /// <summary>
        /// 老密码
        /// </summary>
        [Required]
        [StringLength(32, MinimumLength = 6, ErrorMessage = "You must specify a password between 6 and 32 characters.")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        /// <summary>
        /// 新密码
        /// </summary>
        [Required]
        [StringLength(32, MinimumLength = 6, ErrorMessage = "You must specify a password between 6 and 32 characters.")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
    }
}
