using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using fishbuy.Models;
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
        public async Task<ActionResult<CollectionInfo>> GetCollectionStatus(string postId)
        {
            return new CollectionInfo
            {
                CollectionTime = DateTime.UtcNow,
                Privacy = 0
            };
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
        public async Task<ActionResult<CollectionInfo>> AddOrEditCollection(string postId, [FromQuery] int privacy)
        {
            //Todo：收藏存在则修改，否则添加

            return new CollectionInfo
            {
                CollectionTime = DateTime.UtcNow,
                Privacy = privacy
            };
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
        public async Task<ActionResult> DeleteCollection(string postId)
        {
            return Ok();
        }




    }
}