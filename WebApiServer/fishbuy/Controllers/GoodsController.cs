using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using fishbuy.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace fishbuy.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class GoodsController : ControllerBase
    {
        private readonly ILogger<GoodsController> _logger;
        private readonly FishbuyContext _context;

        public GoodsController(ILogger<GoodsController> logger, FishbuyContext context)
        {
            _logger = logger;
            _context = context;
        }

        /// <summary>
        /// 获取所有发布内容
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public ActionResult<string> GetAllGoods()
        {
            return Ok();
        }

        /// <summary>
        /// 获取某条发布内容
        /// </summary>
        /// <param name="goodsId"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("{goodsId}")]
        public ActionResult<string> GetGoods(string goodsId)
        {
            return Ok();
        }

        /// <summary>
        /// 发布内容
        /// </summary>
        /// <param name="goods"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost("new")]
        public ActionResult<string> PostGoods([FromBody]object goods)
        {
            return Ok();
        }

        /// <summary>
        /// 编辑内容
        /// </summary>
        /// <param name="goodsId"></param>
        /// <param name="goods"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPut("{goodsId}")]
        public ActionResult<string> EditGoods(string goodsId, [FromBody]object goods)
        {
            return Ok();
        }

        /// <summary>
        /// 删除内容
        /// </summary>
        /// <param name="goodsId"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpDelete("{goodsId}")]
        public ActionResult<string> DeleteGoods(string goodsId)
        {
            return Ok();
        }

        /// <summary>
        /// 发布评论
        /// </summary>
        /// <param name="goodsId"></param>
        /// <param name="comment"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost("{goodsId}/comment/new")]
        public ActionResult<string> AddComment(string goodsId, [FromBody]object comment)
        {
            return Ok();
        }

        /// <summary>
        /// 删除评论
        /// </summary>
        /// <param name="goodsId"></param>
        /// <param name="commentId"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpDelete("{goodsId}/comment/{commentId}")]
        public ActionResult<string> DeleteComment(string goodsId, string commentId)
        {
            return Ok();
        }



    }
}
