using fishbuy.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace fishbuy.Utils
{
    public static class ImageUrlExtension
    {
        public static string AddServerAddress(this string url, string imageServer)
        {
            if (!url.IsNullOrEmpty() && !url.StartsWith("http"))
            {
                return Path.Combine(imageServer, url);
            }
            return url;
        }
    }
}
