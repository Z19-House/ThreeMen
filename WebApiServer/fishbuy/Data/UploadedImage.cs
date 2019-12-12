using System;
using System.Collections.Generic;

namespace fishbuy.Data
{
    public partial class UploadedImage
    {
        public string Hash { get; set; }
        public string FileName { get; set; }
        public DateTime UploadTime { get; set; }
    }
}
