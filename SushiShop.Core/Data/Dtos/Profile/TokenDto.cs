using Newtonsoft.Json;
using System;

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
        public DateTime Expired { get; set; }

        [JsonProperty("expiredAtDateTime")]
        public DateTime ExpiredAtDateTime { get; set; }
    }
}