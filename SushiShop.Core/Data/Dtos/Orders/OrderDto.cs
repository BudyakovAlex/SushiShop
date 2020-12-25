using Newtonsoft.Json;
using SushiShop.Core.Data.Dtos.Cart;
using SushiShop.Core.Data.Dtos.Common;
using System;

namespace SushiShop.Core.Data.Dtos.Orders
{
    public class OrderDto
    {
        [JsonProperty("orderId")]
        public long Id { get; set; }

        [JsonProperty("orderPrice")]
        public long Price { get; set; }

        [JsonProperty("needDelivery")]
        public bool IsDeliveryNeeded { get; set; }

        [JsonProperty("deliveryAmount")]
        public double DeliveryAmount { get; set; }

        [JsonProperty("discountAmount")]
        public double DiscountAmount { get; set; }

        [JsonProperty("orderTotal")]
        public double Total { get; set; }

        [JsonProperty("orderComment")]
        public string? Comment { get; set; }

        [JsonProperty("phone")]
        public string? Phone { get; set; }

        [JsonProperty("name")]
        public string? Name { get; set; }

        [JsonProperty("paid")]
        public bool IsPaid { get; set; }

        [JsonProperty("delivered")]
        public bool IsDelivered { get; set; }

        [JsonProperty("paidSum")]
        public double PaidSum { get; set; }

        [JsonProperty("theTimeAtWhichTheCustomerWantsToReceiveAnOrder")]
        public DateTime PreferredDeliveryTime { get; set; }

        [JsonProperty("cutleryCount")]
        public long CutleryCount { get; set; }

        [JsonProperty("wasSendToKeeper")]
        public bool WasSendToKeeper { get; set; }

        [JsonProperty("needCallback")]
        public bool IsCallbackNeeded { get; set; }

        [JsonProperty("cashBack")]
        public long CashBack { get; set; }

        [JsonProperty("shopId")]
        public long ShopId { get; set; }

        [JsonProperty("orderStateTitle")]
        public string? OrderStateTitle { get; set; }

        [JsonProperty("orderStateAlias")]
        public string? OrderStateAlias { get; set; }

        [JsonProperty("shopAddress")]
        public string? ShopAddress { get; set; }

        [JsonProperty("paymentMethodTitle")]
        public string? PaymentMethodTitle { get; set; }

        [JsonProperty("delivery")]
        public OrderDeliveryDto? Delivery { get; set; }

        [JsonProperty("pickupPoint")]
        public PickupPointDto? PickupPoint { get; set; }

        [JsonProperty("currency")]
        public CurrencyDto? Currency { get; set; }

        [JsonProperty("products")]
        public CartProductDto[]? Products { get; set; }

        [JsonProperty("paymentLink")]
        public string? PaymentLink { get; set; }

        [JsonProperty("canRepeat")]
        public bool CanRepeat { get; set; }

        [JsonProperty("orderDateTime")]
        public DateTime OrderDateTime { get; set; }

        [JsonProperty("orderDateFormated")]
        public string? OrderDateFormated { get; set; }

        [JsonProperty("receiveMethod")]
        public string? ReceiveMethod { get; set; }

        [JsonProperty("deliveryAddress")]
        public string? DeliveryAddress { get; set; }

        [JsonProperty("deliveryLatitude")]
        public double? DeliveryLatitude { get; set; }

        [JsonProperty("deliveryLongitude")]
        public double? DeliveryLongitude { get; set; }
    }
}
