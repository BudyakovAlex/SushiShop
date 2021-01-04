using SushiShop.Core.Data.Models.Common;
using System;

namespace SushiShop.Core.Data.Models.Shops
{
    public class Shop
    {
        public Shop(
            long id,
            double latitude,
            double longitude,
            string? timeZone,
            string? mondayWorkingTime,
            string? tuesdayWorkingTime,
            string? wednesdayWorkingTime,
            string? fridayWorkingTime,
            string? saturdayWorkingTime,
            string? sundayWorkingTime,
            string? thursdayWorkingTime,
            string? city,
            string? phone,
            bool isFranchisePoint,
            bool isDelivery,
            bool isMerchant,
            bool isPizzaPoint,
            bool isOrderAvailable,
            bool canSendOrderToRKeeper,
            string? email,
            long addressId,
            DeliveryZone[]? deliveryZones,
            DateTimeOffset deliveryTime,
            string? pageTitle,
            string? longTitle,
            long parentId,
            ShopWorkingInfo? shopWorkingInfo,
            string[] phones,
            Metro[] metro,
            ImageInfo[] images)
        {
            Id = id;
            Latitude = latitude;
            Longitude = longitude;
            TimeZone = timeZone;
            MondayWorkingTime = mondayWorkingTime;
            TuesdayWorkingTime = tuesdayWorkingTime;
            WednesdayWorkingTime = wednesdayWorkingTime;
            FridayWorkingTime = fridayWorkingTime;
            SaturdayWorkingTime = saturdayWorkingTime;
            SundayWorkingTime = sundayWorkingTime;
            ThursdayWorkingTime = thursdayWorkingTime;
            City = city;
            Phone = phone;
            IsFranchisePoint = isFranchisePoint;
            IsDelivery = isDelivery;
            IsMerchant = isMerchant;
            IsPizzaPoint = isPizzaPoint;
            IsOrderAvailable = isOrderAvailable;
            CanSendOrderToRKeeper = canSendOrderToRKeeper;
            Email = email;
            AddressId = addressId;
            DeliveryZones = deliveryZones;
            DeliveryTime = deliveryTime;
            PageTitle = pageTitle;
            LongTitle = longTitle;
            ParentId = parentId;
            ShopWorkingInfo = shopWorkingInfo;
            Phones = phones;
            Metro = metro;
            Images = images;
        }

        public long Id { get; }

        public double Latitude { get; }

        public double Longitude { get; }

        public string? TimeZone { get; }

        public string? MondayWorkingTime { get; }

        public string? TuesdayWorkingTime { get; }

        public string? WednesdayWorkingTime { get; }

        public string? FridayWorkingTime { get; }

        public string? SaturdayWorkingTime { get; }

        public string? SundayWorkingTime { get; }

        public string? ThursdayWorkingTime { get; }

        public string? City { get; }

        public string? Phone { get; }

        public bool IsFranchisePoint { get; }

        public bool IsDelivery { get; }

        public bool IsMerchant { get; }

        public bool IsPizzaPoint { get; }

        public bool IsOrderAvailable { get; }

        public bool CanSendOrderToRKeeper { get; }

        public string? Email { get; }

        public long AddressId { get; }

        public DeliveryZone[]? DeliveryZones { get; }

        public DateTimeOffset DeliveryTime { get; }

        public string? PageTitle { get; }

        public string? LongTitle { get; }

        public long ParentId { get; }

        public ShopWorkingInfo? ShopWorkingInfo { get; }

        public string[] Phones { get; }

        public Metro[] Metro { get; }

        public ImageInfo[] Images { get; }
    }
}
