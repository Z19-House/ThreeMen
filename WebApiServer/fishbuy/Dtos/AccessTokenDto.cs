using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fishbuy.Dtos
{
    public class AccessTokenDto
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("expires_in")]
        public string Expires { get; set; }

        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }

    }
}
