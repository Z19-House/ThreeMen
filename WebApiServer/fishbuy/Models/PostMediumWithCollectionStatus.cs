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
    public class PostMediumWithCollectionStatus : PostMedium
    {
        /// <summary>
        /// 0 - 共有
        /// 1 - 私有
        /// </summary>
        public int IsPrivacy { get; set; }

        public new static PostMediumWithCollectionStatus FromPost(Post post, string imageServer)
        {
            return new PostMediumWithCollectionStatus
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
                ImageUrl = post.MediaLink.FirstOrDefault(it => it.ResType == ResType.Image.ToString())?.ResUri.AddServerAddress(imageServer),
                IsPrivacy = post.Collection.FirstOrDefault(it => it.UserId == post.User.UserId)?.Privacy ?? 0
            };
        }
    }
}
