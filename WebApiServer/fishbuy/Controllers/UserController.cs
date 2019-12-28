using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using fishbuy.Models;
using fishbuy.Data;
using fishbuy.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using fishbuy.Repositories;

namespace fishbuy.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserRepository _repo;
        private readonly string _imageServer;

        public UserController(ILogger<UserController> logger, IConfiguration config, IUserRepository repo)
        {
            _logger = logger;
            _repo = repo;
            _imageServer = config.GetSection("Server:Images").Value;
        }

        /// <summary>
        /// 根据ID获取用户信息
        /// </summary>
        /// <param name="username">用户名或用户ID</param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{username}")]
        public async Task<ActionResult<UserLarge>> GetUserInfo(string username)
        {
            _logger.LogInformation(nameof(GetUserInfo) + ": " + username);
            var user = await _repo.GetUser(username);
            if (user == null)
            {
                return NotFound();
            }
            return UserLarge.FromUser(user, _imageServer);
        }

        /// <summary>
        /// 编辑用户信息
        /// </summary>
        /// <param name="newUserInfo"></param>
        /// <returns></returns>
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPut("edit")]
        public async Task<ActionResult<UserLarge>> EditUserInfo([FromBody] UserLarge newUserInfo)
        {
            _logger.LogInformation(nameof(EditUserInfo) + ": " + newUserInfo);

            var username = User.FindFirst(ClaimTypes.Name)?.Value;
            var user = await _repo.UpdateUser(username, newUserInfo);
            if (user == null)
            {
                return BadRequest(new { error = "Update user info failed." });
            }

            return UserLarge.FromUser(user, _imageServer);
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{username}")]
        public async Task<ActionResult<UserLarge>> DeleteUser(string username)
        {
            _logger.LogInformation(nameof(EditUserInfo) + ": " + username);

            var uname = User.FindFirst(ClaimTypes.Name)?.Value;
            var uid = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (uname != username && uid != username)
            {
                return Unauthorized();
            }
            var user = await _repo.GetUser(uid);
            if (user == null)
            {
                return NotFound();
            }
            var postsCount = await _repo.GetUserPostsCount(user.Username, DateTime.UtcNow);
            if (postsCount != 0)
            {
                return BadRequest(new { error = $"User has {postsCount} posts." });
            }
            var collectionCount = await _repo.GetUserCollectionCount(user.Username, true, DateTime.UtcNow);
            if (collectionCount != 0)
            {
                return BadRequest(new { error = $"User has {collectionCount} collection." });
            }
            return UserLarge.FromUser(await _repo.DeleteUser(user.Username), _imageServer);
        }

        /// <summary>
        /// 获取用户发布的内容
        /// </summary>
        /// <param name="beforeDateTime">获取该时间前的数据（Utc时间）</param>
        /// <param name="username">用户名或用户ID</param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{username}/posts")]
        public async Task<ActionResult<ListResult<List<PostMedium>>>> GetUserPosts(DateTime beforeDateTime, string username, int skip = 0, int take = 20)
        {
            _logger.LogInformation(nameof(GetUserPosts) + ": " + username);
            if (beforeDateTime == DateTime.MinValue)
            {
                beforeDateTime = DateTime.UtcNow;
            }
            var posts = await _repo.GetUserPosts(beforeDateTime, username, skip, take);
            if (posts == null)
            {
                return NotFound();
            }
            var list = new List<PostMedium>();
            foreach (var item in posts)
            {
                list.Add(PostMedium.FromPost(item, _imageServer));
            }

            return new ListResult<List<PostMedium>>
            {
                Data = list,
                Count = await _repo.GetUserPostsCount(username, beforeDateTime)
            };
        }

        /// <summary>
        /// 获取用户收藏
        /// </summary>
        /// <param name="beforeDateTime">获取该时间前的数据（Utc时间）</param>
        /// <param name="username">用户名或用户ID</param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{username}/collection")]
        public async Task<ActionResult<ListResult<List<PostMediumWithCollectionStatus>>>> GetUserColection(DateTime beforeDateTime, string username, int skip = 0, int take = 20)
        {
            _logger.LogInformation(nameof(GetUserColection) + ": " + username);

            if (beforeDateTime == DateTime.MinValue)
            {
                beforeDateTime = DateTime.UtcNow;
            }
            // 判断查询的用户与登录的用户是否相同，相同则显示私有收藏
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            int.TryParse(userId, out int uid);
            var uname = User.FindFirst(ClaimTypes.Name)?.Value;
            bool isAuthorized = false;
            if (username == uname || username == userId)
            {
                isAuthorized = true;
            }
            var collectPosts = await _repo.GetUserCollection(beforeDateTime, username, isAuthorized, skip, take);
            if (collectPosts == null)
            {
                return NotFound();
            }
            var list = new List<PostMediumWithCollectionStatus>();
            foreach (var item in collectPosts)
            {
                list.Add(PostMediumWithCollectionStatus.FromPost(item, uid, _imageServer));
            }

            return new ListResult<List<PostMediumWithCollectionStatus>>
            {
                Data = list,
                Count = await _repo.GetUserCollectionCount(username, isAuthorized, beforeDateTime)
            };
        }
    }
}
