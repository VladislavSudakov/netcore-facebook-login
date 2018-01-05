using Newtonsoft.Json;

namespace SimpleSoft.AspNetCore.FacebookGraphApi.Models
{
    internal class DebugTokenResponse
    {
        public string AppId => Data.app_id;

        public bool IsTokenValid => Data.is_valid;

        public long ExpiresAt => Data.expires_at;

        [JsonProperty("data")]
        public dynamic Data { get; set; }
    }
}