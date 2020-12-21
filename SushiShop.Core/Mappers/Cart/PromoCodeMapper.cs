using SushiShop.Core.Data.Dtos.Cart;
using SushiShop.Core.Data.Models.Cart;

namespace SushiShop.Core.Mappers.Cart
{
    public static class PromoCodeMapper
    {
        public static Promocode Map(this PromocodeDto promoсodeDto)
        {
            return new Promocode(promoсodeDto.Promoсode,
                                 promoсodeDto.DiscountFixed,
                                 promoсodeDto.DiscountPercent,
                                 promoсodeDto.Gift,
                                 promoсodeDto.Description);
        }
    }
}