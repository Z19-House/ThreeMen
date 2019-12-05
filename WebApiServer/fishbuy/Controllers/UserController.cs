using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using fishbuy.Models;
using fishbuy.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace fishbuy.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly FishbuyContext _context;

        public UserController(ILogger<UserController> logger, FishbuyContext context)
        {
            _logger = logger;
            _context = context;
        }

        /// <summary>
        /// 根据ID获取用户信息
        /// </summary>
        /// <param name="username">用户名或用户ID</param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{username}")]
        public ActionResult<User> GetUserInfo(string username)
        {
            _logger.LogInformation(nameof(GetUserInfo) + ": " + username);
            var user = _context.User.FirstOrDefault(u => u.UserName == username || u.UserId.ToString() == username);
            if (user == null)
            {
                return NotFound();
            }
            user.Password = string.Empty;
            return user;
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="username">用户名或用户ID</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPost("signin")]
        public ActionResult SignIn(string username, string password)
        {
            _logger.LogInformation(nameof(SignIn) + ": " + new { username, password });
            if (_context.User.FirstOrDefault(u => u.UserName == username || u.UserId.ToString() == username)?.Password == password.GetMd5Hash())
            {
                return Ok();
            }
            else
            {
                return Unauthorized();
            }
        }

        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="user">注册用户信息</param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [HttpPost("signup")]
        public ActionResult SignUp([FromBody] User user)
        {
            _logger.LogInformation(nameof(SignUp) + ": " + user);

            // 检查是否有相同用户名
            if (_context.User.FirstOrDefault(u => u.UserName == user.UserName) != null)
            {
                return Forbid("用户名已存在！");
            }

            // 新增用户
            _context.Add(new User
            {
                UserId = _context.User.Max(u => u.UserId) + 1,
                UserName = user.UserName,
                Nickname = user.Nickname,
                Password = user.Password.GetMd5Hash(),
                Phone = user.Phone,
                BirthDate = user.BirthDate,
                Sex = user.Sex,
                Address = user.Address,
            });
            _context.SaveChanges();
            return Ok();
        }

        /// <summary>
        /// 编辑用户信息
        /// </summary>
        /// <param name="username"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPut("{username}")]
        public ActionResult EditUserInfo(string username, [FromBody]object user)
        {
            _logger.LogInformation(nameof(EditUserInfo) + ": " + user);
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
        public ActionResult<List<Post>> GetUserGoods(string username)
        {
            _logger.LogInformation(nameof(GetUserGoods) + ": " + username);
            return Ok();
        }
    }
}
