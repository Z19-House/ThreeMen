using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fishbuy.Dtos
{
    public class AccessTokenDto
    {
        public string AccessToken { get; set; }
        public string Expires { get; set; }
        public string RefreshToken { get; set; }

    }
}
