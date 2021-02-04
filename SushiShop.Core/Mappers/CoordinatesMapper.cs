using SushiShop.Core.Data.Dtos.Common;
using SushiShop.Core.Data.Models.Common;

namespace SushiShop.Core.Mappers
{
    public static class CoordinatesMapper
    {
        public static Coordinates Map(this CoordinatesDto dto)
        {
            return new Coordinates(dto?.Longitude, dto?.Latitude);
        }

        public static CoordinatesDto Map(this Coordinates model)
        {
            return new CoordinatesDto
            {
                Latitude = model.Latitude,
                Longitude = model.Longitude
            };
        }
    }
}