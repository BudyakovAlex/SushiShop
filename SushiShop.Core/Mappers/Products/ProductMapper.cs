using SushiShop.Core.Data.Dtos.Products;
using SushiShop.Core.Mappers.Common;
using System;
using Model = SushiShop.Core.Data.Models;

namespace SushiShop.Core.Mappers.Products
{
    public static class ProductMapper
    {
        public static Model.Products.Product Map(this ProductDto productDto)
        {
            return new Model.Products.Product(
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