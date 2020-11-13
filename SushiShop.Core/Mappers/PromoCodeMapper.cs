using SushiShop.Core.Data.Dtos.Cart;
using SushiShop.Core.Data.Models.Cart;

namespace SushiShop.Core.Mappers
{
    public static class PromoCodeMapper
    {
        public static PromoCode Map(this PromoCodeDto promoCodeDto)
        {
            return new PromoCode(promoCodeDto.PromoCode,
                promoCodeDto.DiscountFixed,
                promoCodeDto.DiscountPercent,
                promoCodeDto.ItemGift,
                promoCodeDto.Description);
        }
    }
}
