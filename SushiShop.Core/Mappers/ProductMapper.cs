using SushiShop.Core.Data.Dtos.Products;
using SushiShop.Core.Data.Models.Products;
using System;

namespace SushiShop.Core.Mappers
{
    public static class ProductMapper
    {
        public static Product Map(this ProductDto productDto)
        {
            return new Product(
                productDto.Id,
                productDto.Uid,
                productDto.PageTitle!,
                productDto.Alias,
                productDto.Parent,
                DateTimeOffset.FromUnixTimeSeconds(productDto.PublishDate),
                DateTimeOffset.FromUnixTimeSeconds(productDto.UnpublishDate),
                productDto.IntroText,
                DateTimeOffset.FromUnixTimeSeconds(productDto.CreationDate),
                productDto.Params?.Map(),
                productDto.PriceGroup,
                productDto.Currency!.Map(),
                productDto.Price,
                productDto.OldPrice,
                productDto.MainImageInfo!.Map(),
                productDto.OptionalImageInfo!.Map(),
                productDto.CountInBasket,
                productDto.IsRefreshNeeded);
        }
    }
}
