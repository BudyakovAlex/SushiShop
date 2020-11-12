using SushiShop.Core.Data.Dtos.Products;
using SushiShop.Core.Data.Dtos.Toppings;
using SushiShop.Core.Data.Models.Toppings;
using System.Collections.Generic;
using System.Linq;

namespace SushiShop.Core.Mappers
{
    public static class ToppingMapper
    {
        public static Topping Map(this ToppingDto toppingDto)
        {
            return new Topping(
                toppingDto.Id,
                toppingDto.PageTitle,
                toppingDto.Price,
                toppingDto.CountInBasket,
                toppingDto.ToppingCategory);
        }

        public static UpdateToppingDto Map(this Topping topping)
        {
            return new UpdateToppingDto
            {
                Count = topping.CountInBasket,
                Id = topping.Id
            };
        }

        public static Topping[] Map(this Dictionary<string, ToppingDto> toppings)
        {
            return toppings.Select(kv => kv.Value.Map()).ToArray();
        }

        public static UpdateToppingDto[] Map(this Topping[] toppingsDtos)
        {
            return toppingsDtos.Select(dto => dto.Map()).ToArray();
        }
    }
}