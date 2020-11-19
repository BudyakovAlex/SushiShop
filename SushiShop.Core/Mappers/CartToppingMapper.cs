using SushiShop.Core.Data.Dtos.Cart;
using SushiShop.Core.Data.Models.Cart;

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
                cartToppingDto.PageTitle);
        }
    }
}
