using SushiShop.Core.Data.Dtos.Cart;
using SushiShop.Core.Data.Models.Cart;

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
            cartDto.Currency,
            cartDto.Products,
            cartDto.PromoCode,
            cartDto.ProductUid);
        }
    }
}