using Newtonsoft.Json;

namespace SushiShop.Core.Data.Dtos.Orders
{
    public class OrderConfirmedDto
    {
        public long OrderNumber { get; set; }

        [JsonProperty("thanks")]
        public OrderConfirmationInfoDto? ConfirmationInfo { get; set; }
    }
}
