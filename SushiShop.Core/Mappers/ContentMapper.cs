using SushiShop.Core.Data.Dtos.Common;
using SushiShop.Core.Data.Models.Common;

namespace SushiShop.Core.Mappers
{
    public static class ContentMapper
    {
        public static Content Map(this ContentDto contentDto)
        {
            return new Content(
                contentDto.Id,
                contentDto.Title,
                contentDto.Alias,
                contentDto.Content,
                contentDto.CreatedOn,
                contentDto.EditedOn);
        }
    }
}