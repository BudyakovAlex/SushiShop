using Newtonsoft.Json;

namespace SushiShop.Core.Data.Dtos.Common
{
    public class ImageInfoDto
    {
        [JsonProperty("original")]
        public string? Original { get; set; }

        [JsonProperty("jpg")]
        public string? Jpg { get; set; }

        [JsonProperty("webp")]
        public string? Webp { get; set; }
    }
}