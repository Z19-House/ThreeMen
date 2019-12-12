using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using fishbuy.Models;
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
            string md5HashString;

            using (var stream = new FileStream(Path.Combine(ImageFolderPath, formFile.FileName), FileMode.Create))
            {
                await formFile.CopyToAsync(stream);
                using (MD5 md5Hash = MD5.Create())
                {
                    // Convert the input string to a byte array and compute the hash.
                    byte[] data = md5Hash.ComputeHash(stream);

                    // Create a new Stringbuilder to collect the bytes
                    // and create a string.
                    StringBuilder sBuilder = new StringBuilder();

                    // Loop through each byte of the hashed data 
                    // and format each one as a hexadecimal string.
                    for (int i = 0; i < data.Length; i++)
                    {
                        sBuilder.Append(data[i].ToString("x2"));
                    }
                    md5HashString = sBuilder.ToString();
                }
            }

            return new Media
            {
                Hash = md5HashString,
                ResType = ResType.Image.ToString(),
                ResUri = formFile.FileName,
                UploadTime = DateTime.UtcNow
            };
        }

        /// <summary>
        /// 删除图片
        /// </summary>
        /// <param name="imageHash"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{imageHash}")]
        public async Task<ActionResult> DeleteImage(string imageHash)
        {
            return Ok();
        }
    }
}