using SushiShop.Core.Data.Dtos.Common;
using SushiShop.Core.Data.Models.Common;

namespace SushiShop.Core.Mappers
{
    public static class MetroMapper
    {
        public static Metro Map(this MetroDto dto)
        {
            return new Metro(
                dto.Distance,
                dto.Line,
                dto.Name,
                dto.Measurement);
        }
    }
}