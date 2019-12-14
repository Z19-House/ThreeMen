using fishbuy.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fishbuy.Models
{
    /// <summary>
    /// Post 的详情
    /// </summary>
    public class PostLarge
    {
        public int PostId { get; set; }

        /// <summary>
        /// 首次发布时间
        /// </summary>
        public DateTime UpTime { get; set; }

        /// <summary>
        /// 最后编辑时间
        /// </summary>
        public DateTime EditTime { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

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
        /// 发布用户
        /// </summary>
        public UserMedium User { get; set; }

        /// <summary>
        /// 评论
        /// </summary>
        public List<CommentLarge> Comments { get; set; }

        /// <summary>
        /// 媒体资源，类型及链接
        /// </summary>
        public List<MediaSmall> Medias { get; set; }

        public static PostLarge FromPost(Post post, string imageServer)
        {
            var cms = new List<CommentLarge>();
            foreach (var cm in post.Comment)
            {
                cms.Add(CommentLarge.FromComment(cm, imageServer));
            }
            var mds = new List<MediaSmall>();
            foreach (var md in post.MediaLink)
            {
                mds.Add(MediaSmall.FromMediaLink(md, imageServer));
            }
            return new PostLarge
            {
                PostId = post.PostId,
                UpTime = post.UpTime,
                EditTime = post.EditTime,
                Title = post.Title,
                Content = post.Content,
                Price = post.Price,
                Status = post.Status,
                Address = post.Address,
                Tags = post.Tags,
                User = UserMedium.FromUser(post.User, imageServer),
                Comments = cms,
                Medias = mds
            };
        }
    }
}
