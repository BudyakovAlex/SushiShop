namespace SushiShop.Core.Data.Models.Cart
{
    public class PromoCode
    {
        public PromoCode(string? promocode, long discountFixed, int? discountPercent, string? itemGift, string? description)
        {
            Promocode = promocode;
            DiscountFixed = discountFixed;
            DiscountPercent = discountPercent;
            ItemGift = itemGift;
            Description = description;
        }

        public string? Promocode { get; set; }
        public long DiscountFixed { get; set; }
        public int? DiscountPercent { get; set; }
        public string? ItemGift { get; set; }
        public string? Description { get; set; }
    }
}