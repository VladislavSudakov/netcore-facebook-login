using Newtonsoft.Json;

namespace SimpleSoft.AspNetCore.FacebookGraphApi.Models
{
    internal class UserProfileResponse
    {
        [JsonProperty("id")]
        public string UserId { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonIgnore]
        public string ImageUrl => Picture.data.url;

        [JsonProperty("picture")]
        public dynamic Picture { get; set; }
    }
}