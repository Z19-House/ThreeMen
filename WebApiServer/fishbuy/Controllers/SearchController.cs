using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using fishbuy.Data;
using fishbuy.Models;
using fishbuy.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace fishbuy.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ILogger<SearchController> _logger;
        private readonly ISearchRepository _repo;
        private readonly string _imageServer;

        public SearchController(ILogger<SearchController> logger, ISearchRepository repo, IConfiguration config)
        {
            _logger = logger;
            _repo = repo;
            _imageServer = config.GetSection("Server:Images").Value;
        }

        /// <summary>
        /// 按标题搜索商品
        /// </summary>
        /// <param name="keywords"></param>
        /// <param name="beforeDateTime"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("posts/{keywords}")]
        public async Task<ActionResult<ListResult<List<PostMedium>>>> SearchPostsByTitle(string keywords, DateTime beforeDateTime, int skip = 0, int take = 20)
        {
            if (beforeDateTime == DateTime.MinValue)
            {
                beforeDateTime = DateTime.UtcNow;
            }
            var posts = await _repo.GetPostsByTitle(keywords, beforeDateTime, skip, take);

            var list = new List<PostMedium>();
            foreach (var item in posts)
            {
                list.Add(PostMedium.FromPost(item, _imageServer));
            }

            return new ListResult<List<PostMedium>>
            {
                Data = list,
                Count = await _repo.GetPostsByTitleCount(keywords, beforeDateTime)
            };
        }

        /// <summary>
        /// 按标签搜索商品
        /// </summary>
        /// <param name="keywords"></param>
        /// <param name="beforeDateTime"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("tags/{keywords}")]
        public async Task<ActionResult<ListResult<List<PostMedium>>>> SearchPostsByTags(string keywords, DateTime beforeDateTime, int skip = 0, int take = 20)
        {
            if (beforeDateTime == DateTime.MinValue)
            {
                beforeDateTime = DateTime.UtcNow;
            }
            var posts = await _repo.GetPostsByTag(keywords, beforeDateTime, skip, take);

            var list = new List<PostMedium>();
            foreach (var item in posts)
            {
                list.Add(PostMedium.FromPost(item, _imageServer));
            }

            return new ListResult<List<PostMedium>>
            {
                Data = list,
                Count = await _repo.GetPostsByTagCount(keywords, beforeDateTime)
            };
        }

        /// <summary>
        /// 搜索用户
        /// </summary>
        /// <param name="keywords"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("user/{keywords}")]
        public async Task<ActionResult<ListResult<List<UserMedium>>>> SearchUsers(string keywords, int skip = 0, int take = 20)
        {
            var posts = await _repo.GetUsersByNickname(keywords, skip, take);

            var list = new List<UserMedium>();
            foreach (var item in posts)
            {
                list.Add(UserMedium.FromUser(item, _imageServer));
            }

            return new ListResult<List<UserMedium>>
            {
                Data = list,
                Count = await _repo.GetUsersByNicknameCount(keywords)
            };
        }




    }
}