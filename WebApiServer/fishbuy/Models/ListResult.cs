using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fishbuy.Models
{
    public class ListResult<T>
    {
        public T Data { get; set; }
        public int Count { get; set; }
    }
}
