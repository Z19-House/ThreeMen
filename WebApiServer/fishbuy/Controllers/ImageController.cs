using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using fishbuy.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace fishbuy.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly string[] LimitedImageTypes = { ".jpg", ".jpeg", ".gif", ".png", ".bmp" };
        private readonly string ImageFolderPath;

        public ImageController(IConfiguration config)
        {
            ImageFolderPath = config.GetSection("AppSettings:ImageFolder").Value;
        }

        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param name="formFile"></param>
        /// <returns></returns>
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPost("upload")]
        public async Task<ActionResult<Media>> UploadImage(IFormFile formFile)
        {
            if (!LimitedImageTypes.Contains(Path.GetExtension(formFile.FileName).ToLower()))
            {
                return BadRequest(new { error = "Not supported image format." });
            }
            if (formFile.Length > 2097152)
            {
                return BadRequest(new { error = "File larger then 2MB." });
            }

            // Todo: 使用哈希值检测文件是否已存在

            using (var stream = new FileStream(Path.Combine(ImageFolderPath, formFile.FileName), FileMode.Create))
            {
                await formFile.CopyToAsync(stream);
            }

            return new Media
            {
                ResType = "image",
                ResUri = formFile.FileName
            };
        }
    }
}