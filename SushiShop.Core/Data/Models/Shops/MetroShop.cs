using SushiShop.Core.Data.Models.Common;
using System;

namespace SushiShop.Core.Data.Models.Shops
{
    public class MetroShop : Shop
    {
        public MetroShop(
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
            Metro[] metro,
            Shop? shop) : base(id, latitude, longitude, timeZone, mondayWorkingTime, tuesdayWorkingTime, wednesdayWorkingTime, fridayWorkingTime, saturdayWorkingTime, sundayWorkingTime, thursdayWorkingTime, city, phone, franchisePoint, delivery, email, addressId, deliveryZones, deliveryTime, pageTitle, longTitle, parentId, shopWorkingInfo, phones, metro)
        {
            Shop = shop;
        }

        public Shop? Shop { get; }
    }
}
