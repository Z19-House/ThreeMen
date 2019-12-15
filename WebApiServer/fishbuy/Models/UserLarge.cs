using fishbuy.Data;
using fishbuy.Utils;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace fishbuy.Models
{
    /// <summary>
    /// 用户详情
    /// </summary>
    public class UserLarge
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
        /// 出生日期
        /// </summary>
        [DataType(DataType.Date)]
        public string BirthDate { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string Sex { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        public string ImageUrl { get; set; }

        public static UserLarge FromUser(User user, string imageServer)
        {
            return new UserLarge
            {
                UserId = user.UserId,
                Username = user.Username,
                Nickname = user.Nickname,
                Phone = user.Phone,
                BirthDate = user.BirthDate,
                Sex = user.Sex,
                Address = user.Address,
                ImageUrl = user.ImageUrl.AddServerAddress(imageServer)
            };
        }

    }
}
