using fishbuy.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace fishbuy.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly ILogger<PostController> _logger;
        private readonly FishbuyContext _context;

        public PostController(ILogger<PostController> logger, FishbuyContext context)
        {
            _logger = logger;
            _context = context;
        }

        /// <summary>
        /// 获取所有发布内容
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public ActionResult<List<Post>> GetAllPosts(int skip = 0, int take = 20)
        {
            var list = new List<Post>();
            for (int i = 1; i <= take; i++)
            {
                list.Add(new Post
                {
                    UserId = _context.User.FirstOrDefault()?.UserId ?? 1,
                    PostId = skip + i,
                    Title = $"第{skip + i}个",
                    Content = $"第{skip + i}个描述文字",
                    UpTime = DateTime.Now.AddDays(-1).ToString("yyyy/MM/dd"),
                    EditTime = DateTime.Now.ToString("yyyy/MM/dd"),
                    Address = "河南省洛阳市",
                    Price = 100 * (skip + i),
                    Status = "on_sale",
                    Tags = "Good",
                    MediaLink = new List<MediaLink>
                    {
                        new MediaLink
                        {
                            PostId=skip + i,
                            MediaId=Guid.NewGuid().ToString(),
                            ResType="image",
                            ResUri="https://i.loli.net/2019/12/05/3BQpGcF2Cg9NAaT.png"
                        },
                        new MediaLink
                        {
                            PostId=skip + i,
                            MediaId=Guid.NewGuid().ToString(),
                            ResType="image",
                            ResUri="https://i.loli.net/2019/12/05/SXp9wcBiHqrCODu.png"
                        }
                    },
                    User = _context.User.FirstOrDefault(),
                });

            }
            return list;
        }

        /// <summary>
        /// 获取某条发布内容
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("{postId}")]
        public ActionResult<Post> GetPost(int postId)
        {
            return new Post
            {
                UserId = 1,
                PostId = postId,
                Title = $"第{postId}个",
                Content = $"第{postId}个描述文字",
                UpTime = DateTime.Now.AddDays(-1).ToString("yyyy/MM/dd"),
                EditTime = DateTime.Now.ToString("yyyy/MM/dd"),
                Address = "河南省洛阳市",
                Price = 100 * postId,
                Status = "on_sale",
                Tags = "Good",
                User = _context.User.FirstOrDefault(),
                MediaLink = new List<MediaLink>
                    {
                        new MediaLink
                        {
                            PostId=postId,
                            MediaId=Guid.NewGuid().ToString(),
                            ResType="image",
                            ResUri="https://i.loli.net/2019/12/05/3BQpGcF2Cg9NAaT.png"
                        },
                        new MediaLink
                        {
                            PostId=postId,
                            MediaId=Guid.NewGuid().ToString(),
                            ResType="image",
                            ResUri="https://i.loli.net/2019/12/05/SXp9wcBiHqrCODu.png"
                        }
                    },
                Comment = new List<Comment>()
                {
                    new Comment
                    {
                        PostId= postId ,
                        UserId=_context.User.FirstOrDefault()?.UserId ?? 1,
                        CommentId=Guid.NewGuid().ToString(),
                        UpTime=DateTime.Now.AddHours(-1).ToString("yyyy/MM/dd"),
                        Content="第1个评论"
                    },
                    new Comment
                    {
                        PostId= postId ,
                        UserId=_context.User.FirstOrDefault()?.UserId ?? 1,
                        CommentId=Guid.NewGuid().ToString(),
                        UpTime=DateTime.Now.AddHours(-1).ToString("yyyy/MM/dd"),
                        Content="第2个评论"
                    }
                }
            };
        }

        /// <summary>
        /// 发布内容
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [HttpPost("new")]
        public ActionResult<string> PostNew([FromBody]object post)
        {
            return CreatedAtAction(nameof(PostNew), post);
        }

        /// <summary>
        /// 编辑内容
        /// </summary>
        /// <param name="postId"></param>
        /// <param name="post"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPut("{postId}")]
        public ActionResult<string> EditPost(string postId, [FromBody]object post)
        {
            return Ok();
        }

        /// <summary>
        /// 删除内容
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpDelete("{postId}")]
        public ActionResult<string> DeletePost(string postId)
        {
            return Ok();
        }

        /// <summary>
        /// 发布评论
        /// </summary>
        /// <param name="postId"></param>
        /// <param name="comment"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [HttpPost("{postId}/comment/new")]
        public ActionResult<string> AddComment(string postId, [FromBody]object comment)
        {
            return CreatedAtAction(nameof(AddComment), comment);
        }

        /// <summary>
        /// 删除评论
        /// </summary>
        /// <param name="commentId"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpDelete("{postId}/comment/{commentId}")]
        public ActionResult<string> DeleteComment(string commentId)
        {
            return Ok();
        }



    }
}
