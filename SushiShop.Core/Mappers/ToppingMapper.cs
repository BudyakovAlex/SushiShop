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
                toppingDto.CountInBasket);
        }

        public static Topping[] Map(this Dictionary<string, ToppingDto> toppings)
        {
            return toppings.Select(kv => kv.Value.Map()).ToArray();
        }
    }
}