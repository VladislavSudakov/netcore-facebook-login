using Newtonsoft.Json;

namespace SimpleSoft.AspNetCore.FacebookGraphApi.Models
{
    internal class AccessTokenResponse
    {
        [JsonProperty("access_token")]
        public string Token { get; set; }
    }
}
