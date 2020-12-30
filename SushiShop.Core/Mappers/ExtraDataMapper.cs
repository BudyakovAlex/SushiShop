using SushiShop.Core.Data.Dtos.Common;
using SushiShop.Core.Data.Models.Common;

namespace SushiShop.Core.Mappers
{
    public static class ExtraDataMapper
    {
        public static ExtraData Map(this ExtraDataDto dto)
        {
            return new ExtraData(dto.Data, dto.Type);
        }
    }
}
