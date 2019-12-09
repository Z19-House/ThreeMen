using fishbuy.Data;
using System.ComponentModel.DataAnnotations;

namespace fishbuy.Models
{
    /// <summary>
    /// 注册和登录时使用
    /// </summary>
    public class UserSmall
    {
        /// <summary>
        /// 注册时的用户名
        /// </summary>
        [Required]
        [StringLength(16, MinimumLength = 2, ErrorMessage = "You must specify a username between 2 and 16 characters.")]
        public string Username { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Required]
        [StringLength(32, MinimumLength = 6, ErrorMessage = "You must specify a password between 6 and 32 characters.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public static UserSmall FromUser(User user)
        {
            return new UserSmall
            {
                Username = user.Username
            };
        }
    }
}
