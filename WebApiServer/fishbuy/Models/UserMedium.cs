using fishbuy.Data;
using fishbuy.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace fishbuy.Models
{
    public class UserMedium
    {
        /// <summary>
        /// 用户 Id
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        [Required]
        [StringLength(32, MinimumLength = 2, ErrorMessage = "You must specify a nickname between 2 and 32 characters.")]
        public string Nickname { get; set; }

        /// <summary>
        /// 手机
        /// </summary>
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        public string ImageUrl { get; set; }

        public static UserMedium FromUser(User user, string imageServer)
        {
            if (user == null)
            {
                return null;
            }
            return new UserMedium
            {
                UserId = user.UserId,
                Username = user.Username,
                Nickname = user.Nickname,
                Phone = user.Phone,
                ImageUrl = user.ImageUrl.AddServerAddress(imageServer)
            };
        }
    }
}
