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
    public class SearchController : ControllerBase
    {
        private readonly ILogger<SearchController> _logger;
        private readonly FishbuyContext _context;

        public SearchController(ILogger<SearchController> logger, FishbuyContext context)
        {
            _logger = logger;
            _context = context;
        }

        /// <summary>
        /// 按标题搜索商品
        /// </summary>
        /// <param name="keywords"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("posts/{keywords}")]
        public ActionResult<List<object>> SearchGoodsByTitle(string keywords)
        {
            return Ok();
        }

        /// <summary>
        /// 按标签搜索商品
        /// </summary>
        /// <param name="keywords"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("tags/{keywords}")]
        public ActionResult<List<object>> SearchGoodsByTags(string keywords)
        {
            return Ok();
        }

        /// <summary>
        /// 搜索用户
        /// </summary>
        /// <param name="keywords"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("user/{keywords}")]
        public ActionResult<List<object>> SearchUsers(string keywords)
        {
            return Ok();
        }




    }
}