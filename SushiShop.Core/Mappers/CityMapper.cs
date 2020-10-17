using SushiShop.Core.Data.Dtos.City;
using SushiShop.Core.Data.Models;

namespace SushiShop.Core.Mappers
{
    public static class CityMapper
    {
        public static City Map(this CityDto dto)
        {
            return new City(dto.Id,
                dto.City,
                dto.ShopsCount,
                dto.Latitude,
                dto.Latitude,
                dto.TimeZone,
                dto.PriceGroup,
                dto.Phone,
                dto.CityGroups,
                dto.Currency?.Map());
        }
    }
}