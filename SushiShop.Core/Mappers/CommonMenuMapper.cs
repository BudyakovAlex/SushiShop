using SushiShop.Core.Data.Dtos.Common;
using SushiShop.Core.Data.Models.Common;
using System;

namespace SushiShop.Core.Mappers
{
    public static class CommonMenuMapper
    {
        public static CommonMenu Map(this CommonMenuDto dto)
        {
            return new CommonMenu(
                dto.Id,
                dto.Title,
                dto.Alias,
                dto.ExtraData?.Map(),
                DateTimeOffset.FromUnixTimeSeconds(dto.CreatedOn),
                DateTimeOffset.FromUnixTimeSeconds(dto.EditedOn));
        }
    }
}