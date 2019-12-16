using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using fishbuy.Models;
using fishbuy.Repositories;
using fishbuy.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace fishbuy.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageRepository _repo;
        private readonly string[] LimitedImageTypes = { ".jpg", ".jpeg", ".gif", ".png", ".bmp" };
        private readonly string ImageFolderPath;

        public ImageController(IImageRepository repo, IConfiguration config)
        {
            _repo = repo;
            ImageFolderPath = config.GetSection("AppSettings:ImageFolder").Value;
        }

        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param name="formFile"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPost("upload")]
        public async Task<ActionResult<MediaLarge>> UploadImage(IFormFile formFile)
        {
            if (!LimitedImageTypes.Contains(Path.GetExtension(formFile.FileName).ToLower()))
            {
                return BadRequest(new { error = "Not supported image format." });
            }
            if (formFile.Length > 2097152)
            {
                return BadRequest(new { error = "File larger then 2MB." });
            }

            // 计算文件哈希值
            string md5HashString = formFile.OpenReadStream().GetMd5Hash();

            // 使用哈希值检测文件是否已存在，若文件不存在则保存
            if (!await _repo.ImageExists(md5HashString))
            {
                string fileName = $"{md5HashString}{Path.GetExtension(formFile.FileName)}";

                using (var stream = new FileStream(Path.Combine(ImageFolderPath, fileName), FileMode.Create))
                {
                    await formFile.CopyToAsync(stream);
                }

                await _repo.SaveImage(md5HashString, fileName, DateTime.UtcNow);
            }

            return MediaLarge.FromUploadedImage(await _repo.GetImage(md5HashString));
        }

        /// <summary>
        /// 删除图片
        /// </summary>
        /// <param name="imageName"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{imageName}")]
        public async Task<ActionResult> DeleteImage(string imageName)
        {
            // 若文件存在则删除
            if (await _repo.ImageExists(imageName))
            {
                var image = await _repo.GetImage(imageName);
                if (System.IO.File.Exists(Path.Combine(ImageFolderPath, image.FileName)))
                {
                    System.IO.File.Delete(Path.Combine(ImageFolderPath, image.FileName));
                }
                await _repo.DeleteImage(imageName);
            }
            else
            {
                return NotFound();
            }

            return Ok();
        }
    }
}