using fishbuy.Data;
using fishbuy.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fishbuy.Models
{
    public class UserMediumWithPostCount : UserMedium
    {
        /// <summary>
        /// 用户发布的商品数
        /// </summary>
        public int PostsCount { get; set; }

        public List<PostSmall> Posts { get; set; }

        public new static UserMediumWithPostCount FromUser(User user, string imageServer)
        {
            if (user == null)
            {
                return null;
            }
            var posts = user.Post.OrderByDescending(it => it.UpTime).Take(3);
            var list = new List<PostSmall>();
            foreach (var item in posts)
            {
                list.Add(PostSmall.FromPost(item, imageServer));
            }
            return new UserMediumWithPostCount
            {
                UserId = user.UserId,
                Username = user.Username,
                Nickname = user.Nickname,
                Phone = user.Phone,
                ImageUrl = user.ImageUrl.AddServerAddress(imageServer),
                UserGroup = user.UserGroup,
                PostsCount = user.Post.Count(),
                Posts = list
            };
        }

    }
}
