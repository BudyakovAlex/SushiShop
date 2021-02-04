using SushiShop.Core.Data.Models.Common;
using System;

namespace SushiShop.Core.Data.Models.Cities
{
    public class AddressSuggestion
    {
        public AddressSuggestion(
            string? address,
            string? fullAddress,
            Guid fiasId,
            string? kladrId,
            long zipCode,
            bool isHouseAddress,
            long? shopId,
            decimal deliveryPrice,
            int deliveryTime,
            int cookingTime,
            Coordinates? coordinates)
        {
            Address = address;
            FullAddress = fullAddress;
            FiasId = fiasId;
            KladrId = kladrId;
            ZipCode = zipCode;
            IsHouseAddress = isHouseAddress;
            ShopId = shopId;
            DeliveryPrice = deliveryPrice;
            DeliveryTime = deliveryTime;
            CookingTime = cookingTime;
            Coordinates = coordinates;
        }

        public string? Address { get; }

        public string? FullAddress { get; }

        public Guid FiasId { get; }

        public string? KladrId { get; }

        public long ZipCode { get; }

        public bool IsHouseAddress { get; }

        public long? ShopId { get; }

        public decimal DeliveryPrice { get; }

        public int DeliveryTime { get; }

        public int CookingTime { get; }

        public Coordinates? Coordinates { get; }
    }
}
