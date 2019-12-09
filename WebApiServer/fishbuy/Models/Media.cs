﻿using fishbuy.Data;
using fishbuy.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace fishbuy.Models
{
    public class Media
    {
        /// <summary>
        /// 资源类型
        /// </summary>
        public string ResType { get; set; }

        /// <summary>
        /// 资源链接
        /// </summary>
        public string ResUri { get; set; }

        public static Media FromMediaLink(MediaLink mediaLink, string imageServer)
        {
            return new Media
            {
                ResType = mediaLink.ResType,
                ResUri = mediaLink.ResUri.AddServerAddress(imageServer)
            };
        }
    }
}
