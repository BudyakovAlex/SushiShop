using Newtonsoft.Json;
using SushiShop.Core.Data.Dtos.Common;

namespace SushiShop.Core.Data.Dtos.Shops
{
    public class DeliveryZoneDto
    {
        [JsonProperty("title")]
        public string? Title { get; set; }

        [JsonProperty("path")]
        public CoordinatesDto[]? Path { get; set; }

        [JsonProperty("price")]
        public decimal Price { get; set; }

        [JsonProperty("active")]
        public bool IsActive { get; set; }
    }
}