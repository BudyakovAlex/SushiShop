using SushiShop.Core.Data.Enums;
using System;

namespace SushiShop.Core.Data.Models.Orders
{
    public class OrderRequest
    {
        public OrderRequest(Guid basketId,
                            string? city,
                            string? clientName,
                            string? phoneNumber,
                            string? comment,
                            int cutlery,
                            long? shopId,
                            DateTime? preferedDateTime,
                            PaymentMethod paymentMethod,
                            OrderDeliveryRequest? orderDelivery,
                            decimal cashbackFrom,
                            bool shouldCallMeBack,
                            bool shouldUseBonuses,
                            decimal bonusesCount)
        {
            BasketId = basketId;
            City = city;
            ClientName = clientName;
            PhoneNumber = phoneNumber;
            Comment = comment;
            Cutlery = cutlery;
            ShopId = shopId;
            PreferedDateTime = preferedDateTime;
            PaymentMethod = paymentMethod;
            OrderDelivery = orderDelivery;
            CashbackFrom = cashbackFrom;
            ShouldCallMeBack = shouldCallMeBack;
            ShouldUseBonuses = shouldUseBonuses;
            BonusesCount = bonusesCount;
        }

        public Guid BasketId { get; }

        public string? City { get; }

        public string? ClientName { get; }

        public string? PhoneNumber { get; }

        public string? Comment { get; }

        public int Cutlery { get; }

        public long? ShopId { get; }

        public DateTime? PreferedDateTime { get; }

        public PaymentMethod PaymentMethod { get; }

        public OrderDeliveryRequest? OrderDelivery { get; }

        public decimal CashbackFrom { get; }

        public bool ShouldCallMeBack { get; }

        public bool ShouldUseBonuses { get; }

        public decimal BonusesCount { get; }
    }
}