using SushiShop.Core.Data.Dtos.Shops;
using SushiShop.Core.Data.Models.Shops;
using System;

namespace SushiShop.Core.Mappers
{
    public static class ShopWorkingInfoMapper
    {
        public static ShopWorkingInfo Map(this ShopWorkingInfoDto dto)
        {
            return new ShopWorkingInfo(
                dto.AvailabilityStatus!,
                dto.IsAvailable,
                dto.AvailabilityColor!,
                Enum.Parse<DayOfWeek>(dto.DayOfWeek, ignoreCase: true),
                dto.DeliveryStatus,
                dto.PickUpStatus);
        }
    }
}