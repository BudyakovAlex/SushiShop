using SushiShop.Core.Data.Dtos.Products;
using SushiShop.Core.Data.Enums;
using SushiShop.Core.Data.Models.Products;
using System;
using System.Linq;

namespace SushiShop.Core.Mappers
{
    public static class ProductParametersMapper
    {
        public static ProductParameters Map(this ProductParametersDto parametersDto)
        {
            var stickers = parametersDto.Stickers?.Select(sticker => Enum.Parse<StickerType>(sticker, ignoreCase: true)).ToArray() ?? Array.Empty<StickerType>();
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
