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
            string? franchisePoint,
            string? delivery,
            string? email,
            long addressId,
            DeliveryZone[]? deliveryZones,
            DateTimeOffset deliveryTime,
            string? pageTitle,
            string? longTitle,
            long parentId,
            ShopWorkingInfo? shopWorkingInfo,
            string[]? phones,
            Metro[] metro)
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
            FranchisePoint = franchisePoint;
            Delivery = delivery;
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

        public string? FranchisePoint { get; }

        public string? Delivery { get; }

        public string? Email { get; }

        public long AddressId { get; }

        public DeliveryZone[]? DeliveryZones { get; }

        public DateTimeOffset DeliveryTime { get; }

        public string? PageTitle { get; }

        public string? LongTitle { get; }

        public long ParentId { get; }

        public ShopWorkingInfo? ShopWorkingInfo { get; }

        public string[]? Phones { get; }

        public Metro[] Metro { get; }
    }
}
