using SushiShop.Core.Data.Dtos.Products;
using SushiShop.Core.Data.Enums;
using SushiShop.Core.Data.Models.Products;
using SushiShop.Core.Data.Models.Stickers;
using SushiShop.Core.Mappers.Common;
using SushiShop.Core.Mappers.Topping;
using System;
using System.Linq;
using Xamarin.Essentials;

namespace SushiShop.Core.Mappers.Products
{
    public static class ProductParametersMapper
    {
        public static ProductParameters Map(this ProductParametersDto parametersDto)
        {
            var stickers = parametersDto.StickerParams?
                .Select(kv => new StickerParams(
                    Enum.Parse<StickerType>(kv.Key, ignoreCase: true),
                    kv.Value.StickerImage!,
                    ColorConverters.FromHex(kv.Value.StickerBg)))
                .ToArray();

            return new ProductParameters(
                stickers,
                parametersDto.AvailableToppings?.Map(),
                parametersDto.Proteins,
                parametersDto.Fats,
                parametersDto.Carbons,
                parametersDto.CalorificValue,
                parametersDto.Weight,
                parametersDto.Volume);
        }
    }
}