namespace SushiShop.Core.Data.Models.Cart
{
    public class Promocode
    {
        public Promocode(string? promocode, long discountFixed,
            int? discountPercent, string[] itemGift, string? description)
        {
            PromoCode = promocode;
            DiscountFixed = discountFixed;
            DiscountPercent = discountPercent;
            ItemGift = itemGift;
            Description = description;
        }

        public string? PromoCode { get; set; }
        public long DiscountFixed { get; set; }
        public int? DiscountPercent { get; set; }
        public string[] ItemGift { get; set; }
        public string? Description { get; set; }
    }
}