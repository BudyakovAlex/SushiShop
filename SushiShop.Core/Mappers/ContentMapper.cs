using System;
using SushiShop.Core.Data.Dtos.Common;
using SushiShop.Core.Data.Models.Common;

namespace SushiShop.Core.Mappers
{
    public static class ContentMapper
    {
        public static Content Map(this ContentDto dto)
        {
            return new Content(
                dto.Id,
                dto.Title,
                dto.Alias,
                dto.Content,
                DateTimeOffset.FromUnixTimeSeconds(dto.CreatedOn),
                DateTimeOffset.FromUnixTimeSeconds(dto.EditedOn));
        }
    }
}