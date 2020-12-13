using SushiShop.Core.Data.Models.Cart;
using SushiShop.Core.Data.Models.Common;
using System;

namespace SushiShop.Core.Data.Models.Orders
{
    public class Order
    {
        public Order(
            long id,
            long price,
            bool isDeliveryNeeded,
            double deliveryAmount,
            double discountAmount,
            double total,
            string? comment,
            string? phone,
            string? name,
            bool isPaid,
            bool isDelivered,
            double paidSum,
            DateTimeOffset preferredDeliveryTime,
            long cutleryCount,
            bool wasSendToKeeper,
            bool isCallbackNeeded,
            long cashBack,
            long shopId,
            string? orderStateTitle,
            string? orderStateAlias,
            string? shopAddress,
            string? paymentMethodTitle,
            OrderDelivery? delivery,
            PickupPoint? pickupPoint,
            Currency? currency,
            CartProduct[]? products,
            string? paymentLink,
            bool canRepeat,
            DateTimeOffset orderDateTime,
            string? orderDateFormated,
            string? receiveMethod,
            string? deliveryAddress,
            double? deliveryLatitude,
            double? deliveryLongitude)
        {
            Id = id;
            Price = price;
            IsDeliveryNeeded = isDeliveryNeeded;
            DeliveryAmount = deliveryAmount;
            DiscountAmount = discountAmount;
            Total = total;
            Comment = comment;
            Phone = phone;
            Name = name;
            IsPaid = isPaid;
            IsDelivered = isDelivered;
            PaidSum = paidSum;
            PreferredDeliveryTime = preferredDeliveryTime;
            CutleryCount = cutleryCount;
            WasSendToKeeper = wasSendToKeeper;
            IsCallbackNeeded = isCallbackNeeded;
            CashBack = cashBack;
            ShopId = shopId;
            OrderStateTitle = orderStateTitle;
            OrderStateAlias = orderStateAlias;
            ShopAddress = shopAddress;
            PaymentMethodTitle = paymentMethodTitle;
            Delivery = delivery;
            PickupPoint = pickupPoint;
            Currency = currency;
            Products = products;
            PaymentLink = paymentLink;
            CanRepeat = canRepeat;
            OrderDateTime = orderDateTime;
            OrderDateFormated = orderDateFormated;
            ReceiveMethod = receiveMethod;
            DeliveryAddress = deliveryAddress;
            DeliveryLatitude = deliveryLatitude;
            DeliveryLongitude = deliveryLongitude;
        }

        public long Id { get; }

        public long Price { get; }

        public bool IsDeliveryNeeded { get; }

        public double DeliveryAmount { get; }

        public double DiscountAmount { get; }

        public double Total { get; }

        public string? Comment { get; }

        public string? Phone { get; }

        public string? Name { get; }

        public bool IsPaid { get; }

        public bool IsDelivered { get; }

        public double PaidSum { get; }

        public DateTimeOffset PreferredDeliveryTime { get; }

        public long CutleryCount { get; }

        public bool WasSendToKeeper { get; }

        public bool IsCallbackNeeded { get; }

        public long CashBack { get; }

        public long ShopId { get; }

        public string? OrderStateTitle { get; }

        public string? OrderStateAlias { get; }

        public string? ShopAddress { get; }

        public string? PaymentMethodTitle { get; }

        public OrderDelivery? Delivery { get; }

        public PickupPoint? PickupPoint { get; }

        public Currency? Currency { get; }

        public CartProduct[]? Products { get; }

        public string? PaymentLink { get; }

        public bool CanRepeat { get; }

        public DateTimeOffset OrderDateTime { get; }

        public string? OrderDateFormated { get; }

        public string? ReceiveMethod { get; }

        public string? DeliveryAddress { get; }

        public double? DeliveryLatitude { get; }

        public double? DeliveryLongitude { get; }
    }
}