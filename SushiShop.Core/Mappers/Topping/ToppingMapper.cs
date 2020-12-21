using SushiShop.Core.Data.Dtos.Products;
using SushiShop.Core.Data.Dtos.Toppings;
using SushiShop.Core.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using Model = SushiShop.Core.Data.Models.Toppings;

namespace SushiShop.Core.Mappers.Topping
{
    public static class ToppingMapper
    {
        public static Model.Topping Map(this ToppingDto toppingDto)
        {
            return new Model.Topping(
                toppingDto.Id,
                toppingDto.PageTitle,
                toppingDto.Price,
                toppingDto.CountInBasket,
                Enum.Parse<ToppingCategory>(toppingDto.ToppingCategory, ignoreCase: true));
        }

        public static UpdateToppingDto Map(this Model.Topping topping)
        {
            return new UpdateToppingDto
            {
                Count = topping.CountInBasket,
                Id = topping.Id
            };
        }

        public static Model.Topping[] Map(this Dictionary<string, ToppingDto> toppings)
        {
            return toppings.Select(kv => kv.Value.Map()).ToArray();
        }

        public static UpdateToppingDto[] Map(this Model.Topping[] toppingsDtos)
        {
            return toppingsDtos.Select(dto => dto.Map()).ToArray();
        }
    }
}