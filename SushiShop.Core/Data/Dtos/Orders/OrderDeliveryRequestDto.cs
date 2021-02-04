using Newtonsoft.Json;

namespace SushiShop.Core.Data.Dtos.Orders
{
    public class OrderDeliveryRequestDto : OrderDeliveryDto
    {
        public string? Flat { get; set; }

        public string? Section { get; set; }

        public string? Floor { get; set; }

        [JsonProperty("intercom")]
        public string? IntercomCode { get; set; }
    }
}
