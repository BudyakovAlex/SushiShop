using Newtonsoft.Json;

namespace SushiShop.Core.Data.Dtos.Cities
{
    public class AvailableReceiveMethodsDto
    {
        [JsonProperty("delivery")]
        public bool CanDelivery { get; set; }

        [JsonProperty("pickup")]
        public bool CanPickup { get; set; }
    }
}
