using fishbuy.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

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
        public ActionResult<List<Goods>> GetAllGoods(int skip = 0, int take = 20)
        {
            var list = new List<Goods>();
            for (int i = 1; i <= take; i++)
            {
                list.Add(new Goods
                {
                    UserId = _context.User.FirstOrDefault()?.UserId ?? 1,
                    GoodsId = skip + i,
                    Title = $"第{skip + i}个",
                    Description = $"第{skip + i}个描述文字",
                    UpTime = DateTime.Now.AddDays(-1).ToString("yyyy/mm/dd"),
                    EditTime = DateTime.Now.ToString("yyyy/mm/dd"),
                    Address = "河南省洛阳市",
                    Price = 100 * (skip + i),
                    Status = "on_sale",
                    Tags = "Good",
                    Media = new List<Media>
                    {
                        new Media
                        {
                            GoodsId=skip + i,
                            MediaId=Guid.NewGuid().ToString(),
                            ResType="image",
                            ResUri="https://i.loli.net/2019/12/05/3BQpGcF2Cg9NAaT.png"
                        },
                        new Media
                        {
                            GoodsId=skip + i,
                            MediaId=Guid.NewGuid().ToString(),
                            ResType="image",
                            ResUri="https://i.loli.net/2019/12/05/SXp9wcBiHqrCODu.png"
                        }
                    },
                    User = _context.User.FirstOrDefault(),
                });

            }
            return list;
        }

        /// <summary>
        /// 获取某条发布内容
        /// </summary>
        /// <param name="goodsId"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("{goodsId}")]
        public ActionResult<Goods> GetGoods(int goodsId)
        {
            return new Goods
            {
                UserId = 1,
                GoodsId = goodsId,
                Title = $"第{goodsId}个",
                Description = $"第{goodsId}个描述文字",
                UpTime = DateTime.Now.AddDays(-1).ToString("yyyy/mm/dd"),
                EditTime = DateTime.Now.ToString("yyyy/mm/dd"),
                Address = "河南省洛阳市",
                Price = 100 * goodsId,
                Status = "on_sale",
                Tags = "Good",
                User = _context.User.FirstOrDefault(),
                Media = new List<Media>
                    {
                        new Media
                        {
                            GoodsId=goodsId,
                            MediaId=Guid.NewGuid().ToString(),
                            ResType="image",
                            ResUri="https://i.loli.net/2019/12/05/3BQpGcF2Cg9NAaT.png"
                        },
                        new Media
                        {
                            GoodsId=goodsId,
                            MediaId=Guid.NewGuid().ToString(),
                            ResType="image",
                            ResUri="https://i.loli.net/2019/12/05/SXp9wcBiHqrCODu.png"
                        }
                    },
                Comment = new List<Comment>()
                {
                    new Comment
                    {
                        GoodsId= goodsId ,
                        UserId=_context.User.FirstOrDefault()?.UserId ?? 1,
                        CommentId=Guid.NewGuid().ToString(),
                        UpTime=DateTime.Now.AddHours(-1).ToString("yyyy/mm/dd"),
                        Comment1="第1个评论"
                    },
                    new Comment
                    {
                        GoodsId= goodsId ,
                        UserId=_context.User.FirstOrDefault()?.UserId ?? 1,
                        CommentId=Guid.NewGuid().ToString(),
                        UpTime=DateTime.Now.AddHours(-1).ToString("yyyy/mm/dd"),
                        Comment1="第2个评论"
                    }
                }
            };
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
