using fishbuy.Data;
using fishbuy.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fishbuy.Models
{
    public class MediaSmall
    {
        /// <summary>
        /// 资源类型
        /// </summary>
        public ResType ResType { get; set; }

        /// <summary>
        /// 资源链接
        /// </summary>
        public string ResUri { get; set; }

        public static MediaSmall FromMediaLink(MediaLink mediaLink, string imageServer)
        {
            return new MediaSmall
            {
                ResType = Enum.Parse<ResType>(mediaLink.ResType),
                ResUri = mediaLink.ResUri.AddServerAddress(imageServer)
            };
        }
    }
}
