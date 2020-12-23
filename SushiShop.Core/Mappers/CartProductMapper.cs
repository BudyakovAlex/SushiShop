using SushiShop.Core.Data.Dtos.Cart;
using SushiShop.Core.Data.Enums;
using SushiShop.Core.Data.Models.Cart;
using SushiShop.Core.Data.Models.Toppings;
using System;
using System.Linq;

namespace SushiShop.Core.Mappers
{
    public static class CartProductMapper
    {
        public static CartProduct Map(this CartProductDto productDto)
        {
            return new CartProduct(
                productDto.Id,
                productDto.Count,
                productDto.Price,
                productDto.OldPrice,
                productDto.Weight,
                productDto.Volume,
                productDto.TotalPrice,
                productDto.PageTitle!,
                productDto.Uid,
                productDto.IsReadOnly,
                productDto.Type is null ? ProductType.Item : Enum.Parse<ProductType>(productDto.Type, ignoreCase: true),
                productDto.Toppings?.Select(topping => topping.Map()).ToArray() ?? Array.Empty<CartTopping>(),
                productDto.ImageInfo?.Map());
        }
    }
}