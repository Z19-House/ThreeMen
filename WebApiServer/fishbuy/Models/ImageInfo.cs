using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fishbuy.Models
{
    public class ImageInfo
    {
        /// <summary>
        /// 图片 hash 值
        /// </summary>
        public string Hash { get; set; }

        /// <summary>
        /// 上传及访问的文件名
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 图片上传时间
        /// </summary>
        public DateTime UploadTime { get; set; }
    }
}
