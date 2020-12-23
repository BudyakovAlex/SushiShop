using SushiShop.Core.Data.Dtos.Cart;
using SushiShop.Core.Data.Enums;
using SushiShop.Core.Data.Models.Cart;
using SushiShop.Core.Data.Models.Toppings;

namespace SushiShop.Core.Mappers
{
    public static class CartToppingMapper
    {
        public static CartTopping Map(this CartToppingDto cartToppingDto)
        {
            return new CartTopping(
                cartToppingDto.Id,
                cartToppingDto.Count,
                cartToppingDto.Price,
                cartToppingDto.Amount,
                cartToppingDto.PageTitle);
        }

        public static Topping ToProductTopping(this CartTopping cartTopping)
        {
            return new Topping(
                cartTopping.Id,
                cartTopping.PageTitle,
                cartTopping.Price,
                cartTopping.Count,
                ToppingCategory.Topping);
        }
    }
}