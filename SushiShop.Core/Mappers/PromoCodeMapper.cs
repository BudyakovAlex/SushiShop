using SushiShop.Core.Data.Dtos.Cart;
using SushiShop.Core.Data.Models.Cart;

namespace SushiShop.Core.Mappers
{
    public static class PromoCodeMapper
    {
        public static Promocode Map(this PromocodeDto promoCodeDto)
        {
            return new Promocode(promoCodeDto.PromoCode,
                promoCodeDto.DiscountFixed,
                promoCodeDto.DiscountPercent,
                promoCodeDto.ItemGift,
                promoCodeDto.Description);
        }
    }
}
