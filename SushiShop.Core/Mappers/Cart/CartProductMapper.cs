using SushiShop.Core.Data.Dtos.Cart;
using SushiShop.Core.Data.Enums;
using SushiShop.Core.Data.Models.Cart;
using SushiShop.Core.Mappers.Common;
using System;
using System.Linq;

namespace SushiShop.Core.Mappers.Cart
{
    public static class CartProductMapper
    {
        public static CartProduct Map(this CartProductDto productDto)
        {
            return new CartProduct(productDto.Id,
                                   productDto.Count,
                                   productDto.Price,
                                   productDto.OldPrice,
                                   productDto.Weight,
                                   productDto.Volume,
                                   productDto.TotalPrice,
                                   productDto.PageTitle!,
                                   productDto.Uid,
                                   productDto.IsReadOnly,
                                   Enum.Parse<ProductType>(productDto.Type, ignoreCase: true),
                                   productDto.Toppings!.Select(topping => topping.Map()).ToArray(),
                                   productDto.ImageInfo?.Map());
        }
    }
}