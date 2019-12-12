using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fishbuy.Models
{
    /// <summary>
    /// 收藏状态
    /// </summary>
    public class CollectionInfo
    {
        /// <summary>
        /// 收藏时间
        /// </summary>
        public DateTime CollectionTime { get; set; }

        /// <summary>
        /// 是否是私有收藏
        /// 0 - 公开
        /// 1 - 私有
        /// </summary>
        public int Privacy { get; set; }

    }
}
