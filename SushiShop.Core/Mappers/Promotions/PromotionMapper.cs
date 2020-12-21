using SushiShop.Core.Data.Dtos.Promotions;
using SushiShop.Core.Data.Models.Promotions;
using SushiShop.Core.Mappers.Common;
using SushiShop.Core.Mappers.Products;
using System;

namespace SushiShop.Core.Mappers.Promotions
{
    public static class PromotionMapper
    {
        public static Promotion Map(this PromotionDto dto) =>
            new Promotion(dto.Id,
                          dto.PageTitle!,
                          dto.LongTitle!,
                          dto.Alias!,
                          dto.IntroText!,
                          dto.Content!,
                          ToNullableDateTimeOffset(dto.PubDate),
                          ToNullableDateTimeOffset(dto.UnpubDate),
                          DateTimeOffset.FromUnixTimeSeconds(dto.CreatedOn),
                          dto.Url!,
                          dto.SaleShowOnHome,
                          dto.SaleSquareImage!.Map(),
                          dto.SaleRectangularImage!.Map(),
                          dto.CityMulti!,
                          dto.SaleProduct?.Map());

        public static DateTimeOffset? ToNullableDateTimeOffset(int unixTimeSeconds)
        {
            if (unixTimeSeconds == 0)
            {
                return null;
            }

            return DateTimeOffset.FromUnixTimeSeconds(unixTimeSeconds);
        }
    }
}