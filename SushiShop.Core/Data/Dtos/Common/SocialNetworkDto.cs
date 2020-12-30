using Newtonsoft.Json;

namespace SushiShop.Core.Data.Dtos.Common
{
    public class SocialNetworkDto
    {
        public string? Url { get; set; }

        [JsonProperty("image")]
        public string? ImageUrl { get; set; }
    }
}
