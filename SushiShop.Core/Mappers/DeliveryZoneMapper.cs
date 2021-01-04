using SushiShop.Core.Data.Dtos.Shops;
using SushiShop.Core.Data.Models.Common;
using SushiShop.Core.Data.Models.Shops;
using System;
using System.Linq;

namespace SushiShop.Core.Mappers
{
    public static class DeliveryZoneMapper
    {
        public static DeliveryZone Map(this DeliveryZoneDto dto)
        {
            return new DeliveryZone(
                dto.Title,
                dto.Path?.Select(coordinates => coordinates.Map()).ToArray() ?? Array.Empty<Coordinates>(),
                dto.Price,
                dto.IsActive);
        }
    }
}