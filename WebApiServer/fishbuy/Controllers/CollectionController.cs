using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using fishbuy.Models;
using fishbuy.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace fishbuy.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class CollectionController : ControllerBase
    {
        private readonly ICollectionRepository _repo;

        public CollectionController(ICollectionRepository repo)
        {
            _repo = repo;
        }

        /// <summary>
        /// 获取收藏状态
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        /// <response code="200">收藏信息</response>
        /// <response code="404">用户未收藏</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{postId}")]
        public async Task<ActionResult<CollectionInfo>> GetCollectionStatus(int postId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userId, out int uid))
            {
                return BadRequest(new { error = "Unknow user ID." });
            }
            var col = await _repo.GetCollection(uid, postId);
            if (col == null)
            {
                return NotFound();
            }

            return CollectionInfo.FromCollection(col);
        }

        /// <summary>
        /// 添加或修改收藏
        /// </summary>
        /// <param name="postId"></param>
        /// <param name="privacy"></param>
        /// <returns></returns>
        /// <response code="201">收藏添加或修改成功</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [HttpPost("{postId}")]
        public async Task<ActionResult<CollectionInfo>> AddOrEditCollection(int postId, [FromQuery] int privacy)
        {
            // 收藏存在则修改，否则添加
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userId, out int uid))
            {
                return BadRequest(new { error = "Unknow user ID." });
            }
            var col = await _repo.GetCollection(uid, postId);
            if (col == null)
            {
                return CollectionInfo.FromCollection(await _repo.SaveCollection(uid, postId, privacy));
            }
            else
            {
                return CollectionInfo.FromCollection(await _repo.UpdateCollection(uid, postId, privacy));
            }
        }

        /// <summary>
        /// 删除收藏
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        /// <response code="200">删除收藏成功</response>
        /// <response code="404">未找到收藏信息</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{postId}")]
        public async Task<ActionResult<CollectionInfo>> DeleteCollection(int postId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userId, out int uid))
            {
                return BadRequest(new { error = "Unknow user ID." });
            }
            return CollectionInfo.FromCollection(await _repo.DeleteCollection(uid, postId));
        }




    }
}