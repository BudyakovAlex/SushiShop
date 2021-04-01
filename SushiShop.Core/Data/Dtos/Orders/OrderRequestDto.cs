using Newtonsoft.Json;
using System;

namespace SushiShop.Core.Data.Dtos.Orders
{
    public class OrderRequestDto
    {
        public Guid BasketId { get; set; }

        public string? City { get; set; }

        public string? ClientName { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Comment { get; set; }

        public int Cutlery { get; set; }

        public long? ShopId { get; set; }

        [JsonProperty("orderClientTime")]
        public DateTime? PreferedDateTime { get; set; }

        public string? PaymentMethod { get; set; }

        [JsonProperty("deliveryInfo")]
        public OrderDeliveryRequestDto? OrderDelivery { get; set; }

        [JsonProperty("cashBack")]
        public decimal CashbackFrom { get; set; }

        [JsonProperty("needCallback")]
        public bool ShouldCallMeBack { get; set; }

        [JsonProperty("useBonuses")]
        public bool ShouldUseBonuses { get; set; }

        public decimal BonusesCount { get; set; }
    }
}
