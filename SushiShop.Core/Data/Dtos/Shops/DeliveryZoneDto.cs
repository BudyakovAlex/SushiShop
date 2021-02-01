using Newtonsoft.Json;
using SushiShop.Core.Data.Dtos.Common;

namespace SushiShop.Core.Data.Dtos.Shops
{
    public class DeliveryZoneDto
    {
        [JsonProperty("title")]
        public string? Title { get; set; }

        [JsonProperty("polygon")]
        public CoordinatesDto[]? Polygon { get; set; }

        public string? Path { get; set; }

        [JsonProperty("price")]
        public decimal Price { get; set; }

        [JsonProperty("active")]
        public bool IsActive { get; set; }
    }
}