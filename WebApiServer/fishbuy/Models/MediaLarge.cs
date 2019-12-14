using fishbuy.Data;
using fishbuy.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace fishbuy.Models
{
    public class MediaLarge
    {
        /// <summary>
        /// 资源 Hash
        /// </summary>
        public string Hash { get; set; }

        /// <summary>
        /// 资源类型
        /// </summary>
        public ResType ResType { get; set; }

        /// <summary>
        /// 资源链接
        /// </summary>
        public string ResUri { get; set; }

        /// <summary>
        /// 资源上传时间
        /// </summary>
        public DateTime UploadTime { get; set; }

        public static MediaLarge FromUploadedImage(UploadedImage uploadedImage)
        {
            return new MediaLarge
            {
                Hash = uploadedImage.Hash,
                ResUri = uploadedImage.FileName,
                ResType = ResType.Image,
                UploadTime = uploadedImage.UploadTime
            };
        }
    }
}
