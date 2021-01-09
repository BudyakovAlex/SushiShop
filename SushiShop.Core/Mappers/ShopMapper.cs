using SushiShop.Core.Data.Dtos.Shops;
using SushiShop.Core.Data.Models.Common;
using SushiShop.Core.Data.Models.Shops;
using System;
using System.Linq;

namespace SushiShop.Core.Mappers
{
    public static class ShopMapper
    {
        public static Shop Map(this ShopDto dto)
        {
            return new Shop(
                dto.Id,
                dto.Coordinates!.Map(),
                dto.TimeZone,
                dto.MondayWorkingTime,
                dto.TuesdayWorkingTime,
                dto.WednesdayWorkingTime,
                dto.FridayWorkingTime,
                dto.SaturdayWorkingTime,
                dto.SundayWorkingTime,
                dto.ThursdayWorkingTime,
                dto.City,
                dto.Phone,
                dto.IsFranchisePoint,
                dto.IsDelivery,
                dto.MerchantId,
                dto.IsPizzaPoint,
                dto.IsOrderAvailable,
                dto.CanSendOrderToRKeeper,
                dto.Email,
                dto.AddressId,
                dto.DeliveryZones?.Select(deliveryZone => deliveryZone.Map()).ToArray() ?? Array.Empty<DeliveryZone>(),
                DateTimeOffset.FromUnixTimeSeconds(dto.DeliveryTime),
                dto.PageTitle,
                dto.LongTitle,
                dto.ParentId,
                dto.DriveWay,
                dto.ShopWorkingInfo?.Map(),
                dto.Phones ?? Array.Empty<string>(),
                dto.Metro?.Select(metro => metro.Map()).ToArray() ?? Array.Empty<Metro>(),
                dto.Images?.Select(image => image.Map()).ToArray() ?? Array.Empty<ImageInfo>());
        }
    }
}
