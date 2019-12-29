using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using fishbuy.Data;
using fishbuy.Models;
using fishbuy.Repositories;
using fishbuy.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace fishbuy.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class QRCodeController : ControllerBase
    {
        private readonly ILogger<QRCodeController> _logger;
        private readonly IQRCodeRepository _repo;
        private readonly IAuthRepository _authRepo;
        private readonly IAuthService _service;
        private readonly IUserRepository _userRepo;

        public QRCodeController(ILogger<QRCodeController> logger, IQRCodeRepository repo,
            IAuthRepository authRepo, IAuthService service, IUserRepository userRepo)
        {
            _logger = logger;
            _repo = repo;
            _authRepo = authRepo;
            _service = service;
            _userRepo = userRepo;
        }

        /// <summary>
        /// 获取二维码登录使用的信息
        /// </summary>
        /// <returns></returns>
        [HttpGet("code")]
        public async Task<ActionResult<QRToken>> GetQRCodeToken()
        {
            return await _repo.Generate();
        }

        /// <summary>
        /// 允许二维码登录
        /// </summary>
        /// <param name="token">仅使用 Token</param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("code")]
        public async Task<ActionResult> PermitSignIn([FromBody] QRToken token)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userId, out int uid))
            {
                return BadRequest(new { error = "Unknow user ID." });
            }
            var item = await _repo.PermitByToken(token.Token, uid);
            if (item == null)
            {
                return BadRequest(new { error = "No such code or expired." });
            }
            return Ok();
        }

        /// <summary>
        /// 检查二维码登录状态
        /// </summary>
        /// <param name="token">仅使用 Id</param>
        /// <returns></returns>
        [HttpPost("status")]
        public async Task<ActionResult<Token>> CheckStatus([FromBody] QRToken token)
        {
            const int delayTime = 2000;
            var item = await _repo.GetById(token.Id);
            if (item == null)
            {
                return BadRequest(new { error = "No such code." });
            }
            for (int i = 0; i < 60000 / delayTime; i++)
            {
                // 获取最新数据
                _repo.GetLatest(ref item);
                if (item.Expires < DateTime.UtcNow)
                {
                    return BadRequest(new { error = "Code expired." });
                }
                else if (item.Status == 1)
                {
                    var user = await _userRepo.GetUser(item.UserId.ToString());
                    var refreshToken = _service.GenerateRefreshToken();

                    // Return jwt token.
                    return new Token
                    {
                        AccessToken = _service.GenerateToken(new Claim[]
                        {
                            new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                            new Claim(ClaimTypes.Name, user.Username),
                            new Claim(ClaimTypes.Role, user.UserGroup.ToString()),
                        }),
                        Expires = DateTime.UtcNow.AddDays(7).ToString(),
                        RefreshToken = refreshToken,
                        UserId = item.UserId
                    };
                }
                else
                {
                    await Task.Delay(delayTime);
                }
            }
            return StatusCode(408, new { error = "Request time out." });

        }



    }
}