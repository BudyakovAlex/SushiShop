using Newtonsoft.Json;
using SushiShop.Core.Data.Dtos.Common;

namespace SushiShop.Core.Data.Dtos.Orders
{
    public partial class OrderDeliveryDto
    {
        [JsonProperty("address")]
        public string? Address { get; set; }

        [JsonProperty("cordinates")]
        public CoordinatesDto? Coordinates { get; set; }
    }
}