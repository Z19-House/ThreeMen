using fishbuy.Data;
using fishbuy.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace fishbuy.Models
{
    public class MediaLarge : MediaSmall
    {
        /// <summary>
        /// 资源 Hash
        /// </summary>
        public string Hash { get; set; }

        /// <summary>
        /// 资源上传时间
        /// </summary>
        public DateTime UploadTime { get; set; }

        public static MediaLarge FromUploadedImage(UploadedImage uploadedImage, string imageServer)
        {
            return new MediaLarge
            {
                Hash = uploadedImage.Hash,
                ResUri = uploadedImage.FileName.AddServerAddress(imageServer),
                ResType = ResType.Image,
                UploadTime = uploadedImage.UploadTime
            };
        }
    }
}
