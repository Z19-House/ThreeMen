using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using fishbuy.Dtos;
using fishbuy.Models;
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
        /// <param name="user">登录用户信息</param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPost("signin")]
        public ActionResult SignIn([FromBody]UserForLoginDto user)
        {
            _logger.LogInformation(nameof(SignIn) + ": " + user);
            if (_context.User.FirstOrDefault(u => u.UserName == user.Username || u.UserId.ToString() == user.Username)
                ?.Password == user.Password.GetMd5Hash())
            {
                //generate token
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_config.GetSection("AppSettings:Token").Value);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]{
                    new Claim(ClaimTypes.NameIdentifier, user.Username)
                }),
                    Expires = DateTime.Now.AddDays(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);
                return Ok(new { tokenString });
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
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpPost("signup")]
        public ActionResult SignUp([FromBody]UserForRegisterDto user)
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
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPut("{username}")]
        public ActionResult EditUserInfo(string username, [FromBody]UserForRegisterDto user)
        {
            _logger.LogInformation(nameof(EditUserInfo) + ": " + user);

            var user2 = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

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
