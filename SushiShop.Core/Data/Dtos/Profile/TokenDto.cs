using Newtonsoft.Json;

namespace SushiShop.Core.Data.Dtos.Profile
{
    public class TokenDto
    {
        [JsonProperty("token")]
        public string? Token { get; set; }

        [JsonProperty("header")]
        public string? Header { get; set; }

        [JsonProperty("headerPreffix")]
        public string? HeaderPreffix { get; set; }

        [JsonProperty("expired")]
        public int Expired { get; set; }

        [JsonProperty("expiredAtDateTime")]
        public string? ExpiredAtDateTime { get; set; }
    }
}
