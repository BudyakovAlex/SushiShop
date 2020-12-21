using SushiShop.Core.Data.Dtos.Cities;
using SushiShop.Core.Data.Enums;
using SushiShop.Core.Data.Models.Cities;
using SushiShop.Core.Mappers.Common;
using System;

namespace SushiShop.Core.Mappers.Cities
{
    public static class CityMapper
    {
        public static City Map(this CityDto dto)
        {
            return new City(
                dto.Id,
                dto.City!,
                dto.ShopsCount,
                dto.Latitude,
                dto.Latitude,
                dto.TimeZone!,
                Enum.Parse<PriceGroup>(dto.PriceGroup!, ignoreCase: true),
                dto.Phone!,
                dto.CityGroups!,
                dto.Currency!.Map());
        }
    }
}