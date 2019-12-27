using fishbuy.Data;
using fishbuy.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fishbuy.Models
{
    public class PostSmall
    {
        public int PostId { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Post 的第一张图片，若没有则为空
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 价格
        /// </summary>
        public decimal Price { get; set; }

        public static PostSmall FromPost(Post post, string imageServer)
        {
            return new PostSmall
            {
                PostId = post.PostId,
                Title = post.Title,
                Price = post.Price,
                Status = post.Status,
                ImageUrl = post.MediaLink.FirstOrDefault(it => it.ResType == ResType.Image.ToString())?.ResUri.AddServerAddress(imageServer)
            };
        }
    }
}
