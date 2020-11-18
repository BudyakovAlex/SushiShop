using SushiShop.Core.Data.Dtos.Cart;
using SushiShop.Core.Data.Models.Cart;

namespace SushiShop.Core.Mappers
{
    public static class CartProductMapper
    {
        public static CartProduct Map(this CartProductDto productDto)
        {
            return new CartProduct(
                productDto.Id,
                productDto.Count,
                productDto.Price,
                productDto.Amount,
                productDto.PageTitle!,
                productDto.Uid,
                productDto.IsReadonly,
                null); //TODO: clarify toppings structure
        }
    }
}
