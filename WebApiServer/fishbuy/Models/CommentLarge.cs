using fishbuy.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fishbuy.Models
{
    public class CommentLarge
    {
        /// <summary>
        /// 评论 Id
        /// </summary>
        public string CommentId { get; set; }

        /// <summary>
        /// 评论时间
        /// </summary>
        public string UpTime { get; set; }

        /// <summary>
        /// 评论内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 评论用户
        /// </summary>
        public UserMedium User { get; set; }

        public static CommentLarge FromComment(Comment comment, string imageServer)
        {
            return new CommentLarge
            {
                CommentId = comment.CommentId,
                UpTime = comment.UpTime,
                Content = comment.Content,
                User = UserMedium.FromUser(comment.User, imageServer)
            };
        }
    }
}
