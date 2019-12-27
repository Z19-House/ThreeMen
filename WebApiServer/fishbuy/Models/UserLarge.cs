using fishbuy.Data;
using fishbuy.Utils;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace fishbuy.Models
{
    /// <summary>
    /// 用户详情
    /// </summary>
    public class UserLarge : UserMedium
    {
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

        public new static UserLarge FromUser(User user, string imageServer)
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
                ImageUrl = user.ImageUrl.AddServerAddress(imageServer),
                UserGroup = user.UserGroup
            };
        }

    }
}
