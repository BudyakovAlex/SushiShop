using Newtonsoft.Json;

namespace SushiShop.Core.Data.Dtos.Profile
{
    public class TokenDto
    {
        public string? Token { get; set; }

        public string? Header { get; set; }

        public string? HeaderPreffix { get; set; }

        [JsonProperty("expired")]
        public long ExpiresAt { get; set; }

        public string? ExpiredAtDateTime { get; set; }
    }
}