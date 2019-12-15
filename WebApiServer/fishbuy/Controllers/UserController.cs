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

namespace fishbuy.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly FishbuyContext _context;
        private readonly IConfiguration _config;

        public UserController(ILogger<UserController> logger, FishbuyContext context, IConfiguration config)
        {
            _logger = logger;
            _context = context;
            _config = config;
        }

        /// <summary>
        /// 根据ID获取用户信息
        /// </summary>
        /// <param name="username">用户名或用户ID</param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{username}")]
        public ActionResult<UserLarge> GetUserInfo(string username)
        {
            _logger.LogInformation(nameof(GetUserInfo) + ": " + username);
            var user = _context.User.FirstOrDefault(u => u.Username == username || u.UserId.ToString() == username);
            if (user == null)
            {
                return NotFound();
            }
            return UserLarge.FromUser(user, _config.GetSection("Server:Images").Value);
        }

        /// <summary>
        /// 编辑用户信息
        /// </summary>
        /// <param name="username"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPut("{username}")]
        public ActionResult EditUserInfo(string username, [FromBody]UserLarge user)
        {
            _logger.LogInformation(nameof(EditUserInfo) + ": " + user);

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            return Ok();
        }

        /// <summary>
        /// 获取用户发布的内容
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{username}/posts")]
        public ActionResult<List<PostMedium>> GetUserGoods(string username)
        {
            _logger.LogInformation(nameof(GetUserGoods) + ": " + username);
            return Ok();
        }

        /// <summary>
        /// 获取用户收藏
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{username}/collection")]
        public ActionResult<List<PostMedium>> GetUserColection(string username)
        {
            _logger.LogInformation(nameof(GetUserColection) + ": " + username);

            //Todo:判断查询的用户与登录的用户是否相同，相同则显示私有收藏

            return Ok();
        }
    }
}
