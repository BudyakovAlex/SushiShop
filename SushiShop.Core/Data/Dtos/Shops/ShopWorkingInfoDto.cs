using Newtonsoft.Json;

namespace SushiShop.Core.Data.Dtos.Shops
{
    public class ShopWorkingInfoDto
    {
        [JsonProperty("timeState")]
        public string? AvailabilityStatus { get; set; }

        [JsonProperty("timeStateBool")]
        public bool IsAvailable { get; set; }

        [JsonProperty("timeStateColor")]
        public string? AvailabilityColor { get; set; }

        [JsonProperty("dayOfWeek")]
        public string? DayOfWeek { get; set; }

        [JsonProperty("timeStateDelivery")]
        public string? DeliveryStatus { get; set; }

        [JsonProperty("timeStatePickUp")]
        public string? PickUpStatus { get; set; }
    }
}
