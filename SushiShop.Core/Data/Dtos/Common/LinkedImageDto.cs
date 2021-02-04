using Newtonsoft.Json;

namespace SushiShop.Core.Data.Dtos.Common
{
    public class LinkedImageDto
    {
        public string? Url { get; set; }

        [JsonProperty("image")]
        public string? ImageUrl { get; set; }
    }
}
