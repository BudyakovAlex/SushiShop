using SushiShop.Core.Data.Dtos.Cart;
using SushiShop.Core.Mappers.Common;
using System.Linq;
using Model = SushiShop.Core.Data.Models.Cart;

namespace SushiShop.Core.Mappers.Cart
{
    public static class CartMapper
    {
        public static Model.Cart Map(this CartDto cartDto)
        {
            return new Model.Cart(
                cartDto.BasketId,
                cartDto.City,
                cartDto.PriceGroup,
                cartDto.TotalCount,
                cartDto.TotalSum,
                cartDto.Discount,
                cartDto.Currency!.Map(),
                cartDto.Products!.Select(product => product.Map()).ToArray(),
                cartDto.Promoсode!.Map());
        }
    }
}