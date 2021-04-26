using SushiShop.Core.Data.Dtos.Cart;
using SushiShop.Core.Data.Models.Cart;
using System.Linq;

namespace SushiShop.Core.Mappers
{
    public static class CartMapper
    {
        public static Cart Map(this CartDto cartDto)
        {
            return new Cart(
                cartDto.BasketId,
                cartDto.City,
                cartDto.PriceGroup,
                cartDto.WarningMessage,
                cartDto.TotalCount,
                cartDto.TotalSum,
                cartDto.Discount,
                cartDto.Currency!.Map(),
                cartDto.Products!.Select(product => product.Map()).ToArray(),
                cartDto.Promoсode!.Map());
        }
    }
}