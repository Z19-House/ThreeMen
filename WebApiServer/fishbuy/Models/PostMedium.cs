using fishbuy.Data;
using fishbuy.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fishbuy.Models
{
    /// <summary>
    /// Post 的简略信息
    /// </summary>
    public class PostMedium
    {
        public int PostId { get; set; }
        public int UserId { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Post 的第一张图片，若没有则为空
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 标签
        /// </summary>
        public string Tags { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 价格
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 用户昵称
        /// </summary>
        public string UserNickname { get; set; }

        /// <summary>
        /// 用户头像
        /// </summary>
        public string UserImageUrl { get; set; }

        public static PostMedium FromPost(Post post, string imageServer)
        {
            return new PostMedium
            {
                UserId = post.UserId,
                UserNickname = post.User.Nickname,
                UserImageUrl = post.User.ImageUrl.AddServerAddress(imageServer),
                PostId = post.PostId,
                Title = post.Title,
                Content = post.Content,
                Price = post.Price,
                Status = post.Status,
                Address = post.Address,
                Tags = post.Tags,
                ImageUrl = post.MediaLink.FirstOrDefault(it => it.ResType == "image")?.ResUri.AddServerAddress(imageServer)
            };
        }
    }
}
