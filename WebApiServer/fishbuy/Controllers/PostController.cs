using fishbuy.Data;
using fishbuy.Models;
using fishbuy.Repositories;
using fishbuy.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace fishbuy.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly ILogger<PostController> _logger;
        private readonly IPostRepository _repo;
        private readonly string _imageServer;

        public PostController(ILogger<PostController> logger, FishbuyContext context, IConfiguration config, IPostRepository repo)
        {
            _logger = logger;
            _repo = repo;
            _imageServer = config.GetSection("Server:Images").Value;
        }

        /// <summary>
        /// 获取所有发布内容
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<ActionResult<ListResult<List<PostMedium>>>> GetAllPosts(DateTime beforeDateTime, int skip = 0, int take = 20)
        {
            var posts = await _repo.GetPosts(beforeDateTime, skip, take);
            var list = new List<PostMedium>();
            foreach (var item in posts)
            {
                list.Add(PostMedium.FromPost(item, _imageServer));
            }
            return new ListResult<List<PostMedium>>
            {
                Data = list,
                Count = await _repo.GetPostCount(it => it.UpTime < beforeDateTime)
            };
        }

        /// <summary>
        /// 获取某条发布内容
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("{postId}")]
        public async Task<ActionResult<PostLarge>> GetPost(int postId)
        {
            var item = await _repo.GetPost(postId);
            if (item == null)
            {
                return NotFound();
            }
            return PostLarge.FromPost(await _repo.GetPost(postId), _imageServer);
        }

        /// <summary>
        /// 发布内容
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [HttpPost("new")]
        public async Task<ActionResult<PostLarge>> PostNew([FromBody] PostForUpload post)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userId, out int uid))
            {
                return BadRequest(new { error = "Unknow user ID." });
            }
            post.UserId = uid;
            return PostLarge.FromPost(await _repo.SavePost(post), _imageServer);
        }

        /// <summary>
        /// 编辑内容
        /// </summary>
        /// <param name="postId"></param>
        /// <param name="post"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPut("{postId}")]
        public async Task<ActionResult<PostLarge>> EditPost(int postId, [FromBody] PostForUpload post)
        {
            return PostLarge.FromPost(await _repo.UpdatePost(postId, post), _imageServer);
        }

        /// <summary>
        /// 删除内容
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpDelete("{postId}")]
        public async Task<ActionResult<PostLarge>> DeletePost(int postId)
        {
            return PostLarge.FromPost(await _repo.DeletePost(postId), _imageServer);
        }

        /// <summary>
        /// 发布评论
        /// </summary>
        /// <param name="postId"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("{postId}/comment/new")]
        public async Task<ActionResult<CommentLarge>> AddComment(int postId, [FromBody] string content)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userId, out int uid))
            {
                return BadRequest(new { error = "Unknow user ID." });
            }
            return CreatedAtAction(nameof(AddComment), CommentLarge.FromComment(await _repo.SaveComment(postId, uid, content), _imageServer));
        }

        /// <summary>
        /// 删除评论
        /// </summary>
        /// <param name="commentId"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpDelete("{postId}/comment/{commentId}")]
        public async Task<ActionResult<CommentLarge>> DeleteComment(string commentId)
        {
            var comment = await _repo.DeleteComment(commentId);
            if (comment == null)
            {
                return NotFound();
            }
            return CommentLarge.FromComment(comment, _imageServer);
        }



    }
}
