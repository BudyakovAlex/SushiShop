using System;
using SushiShop.Core.Data.Dtos.Promotions;
using SushiShop.Core.Data.Models.Promotions;

namespace SushiShop.Core.Mappers
{
    public static class PromotionMapper
    {
        public static Promotion Map(this PromotionDto dto) =>
            new Promotion(
                dto.Id,
                dto.PageTitle!,
                dto.LongTitle!,
                dto.Alias!,
                dto.IntroText!,
                dto.Content!,
                DateTimeOffset.FromUnixTimeSeconds(dto.PubDate),
                DateTimeOffset.FromUnixTimeSeconds(dto.UnpubDate),
                DateTimeOffset.FromUnixTimeSeconds(dto.CreatedOn),
                dto.Url!,
                dto.SaleShowOnHome,
                dto.SaleSquareImage!.Map(),
                dto.SaleRectangularImage!.Map(),
                dto.CityMulti!,
                dto.SaleProductId);
    }
}
