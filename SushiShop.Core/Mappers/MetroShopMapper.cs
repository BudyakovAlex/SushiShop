using SushiShop.Core.Data.Dtos.Shops;
using SushiShop.Core.Data.Models.Shops;

namespace SushiShop.Core.Mappers
{
    public static class MetroShopMapper
    {
        public static MetroShop Map(this MetroShopDto dto)
        {
            return new MetroShop(
                dto.Distance,
                dto.Line,
                dto.Name,
                dto.Measurement,
                dto.Shop?.Map());
        }
    }
}
