using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using fishbuy.Dtos;
using fishbuy.Models;
using fishbuy.Repositories;
using fishbuy.Services;
using fishbuy.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace fishbuy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        private readonly ILogger<UserController> _logger;
        private readonly IAuthService _service;

        public AuthController(ILogger<UserController> logger, IAuthRepository repo, IConfiguration config, IAuthService service)
        {
            _logger = logger;
            _repo = repo;
            _service = service;
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="user">登录用户信息</param>
        /// <returns></returns>
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPost("signin")]
        public async Task<ActionResult<AccessTokenDto>> SignIn([FromBody] UserForAuthDto user)
        {
            _logger.LogInformation(nameof(SignIn) + ": " + user);
            var userFromRepo = await _repo.SignIn(user);
            if (userFromRepo == null)
            {
                return Unauthorized();
            }

            var refreshToken = _service.GenerateRefreshToken();
            await _repo.SaveRefreshToken(userFromRepo.UserId, refreshToken);

            return new AccessTokenDto
            {
                AccessToken = _service.GenerateToken(new Claim[] {
                    new Claim(ClaimTypes.NameIdentifier, userFromRepo.UserId.ToString()),
                    new Claim(ClaimTypes.Name, userFromRepo.Username)
                }),
                Expires = DateTime.UtcNow.AddDays(7).ToString(),
                RefreshToken = refreshToken
            };
        }

        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="user">注册用户信息</param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("signup")]
        public async Task<ActionResult> SignUp([FromBody] UserForAuthDto user)
        {
            _logger.LogInformation(nameof(SignUp) + ": " + user);

            // 检查是否有相同用户名
            if (await _repo.UserExists(user.Username))
            {
                return BadRequest(new { error = "Username already exists." });
            }

            var registeredUser = await _repo.Register(user);
            if (registeredUser == null)
            {
                return BadRequest(new { error = "User register fail!" });
            }
            registeredUser.PasswordHash = null;
            return new CreatedResult(nameof(SignUp), registeredUser);
        }

        /// <summary>
        /// 刷新 token
        /// </summary>
        /// <param name="accessTokenDto"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("refresh_token")]
        public async Task<ActionResult<AccessTokenDto>> RefreshToken([FromBody] AccessTokenDto accessTokenDto)
        {
            var principal = _service.GetPrincipalFromExpiredToken(accessTokenDto.AccessToken);
            var userId = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userId, out int uid))
            {
                return BadRequest(new { error = "Unknow user ID." });
            }
            var savedRefreshToken = await _repo.GetRefreshToken(uid, accessTokenDto.RefreshToken);
            if (string.IsNullOrEmpty(savedRefreshToken))
            {
                return BadRequest(new { error = "Invalid refresh token" });
            }

            var newAccessToken = _service.GenerateToken(principal.Claims);
            var newRefreshToken = _service.GenerateRefreshToken();
            await _repo.DeleteRefreshToken(uid, savedRefreshToken);
            await _repo.SaveRefreshToken(uid, newRefreshToken);

            return new AccessTokenDto
            {
                AccessToken = newAccessToken,
                Expires = DateTime.UtcNow.AddDays(7).ToString(),
                RefreshToken = newRefreshToken
            };
        }


    }
}