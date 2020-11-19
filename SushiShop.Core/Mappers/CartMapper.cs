using SushiShop.Core.Data.Dtos.Cart;
using SushiShop.Core.Data.Models.Cart;
using System.Linq;

namespace SushiShop.Core.Mappers
{
    public static class CartMapper
    {
        public static Cart Map(this CartDto cartDto)
        {
            return new Cart(cartDto.BasketId,
                cartDto.City,
                cartDto.PriceGroup,
                cartDto.TotalCount,
                cartDto.TotalSum,
                cartDto.Discount,
                cartDto.Currency?.Select(currency => currency.Map()).ToArray(),
                cartDto.Products?.Select(product => product.Map()).ToArray(),
                cartDto.Promoсodes?.Select(promocode => promocode.Map()).ToArray());
        }
    }
}