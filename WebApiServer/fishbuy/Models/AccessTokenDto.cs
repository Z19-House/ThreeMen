namespace fishbuy.Models
{
    public class AccessTokenDto
    {
        public string AccessToken { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        public string Expires { get; set; }

        public string RefreshToken { get; set; }

    }
}
