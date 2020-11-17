namespace SushiShop.Core.Data.Models.Cart
{
    public class Promocode
    {
        public Promocode(string? code,
            decimal? discountFixed,
            int? discountPercent,
            string[] itemGift,
            string? description)
        {
            Code = code;
            DiscountFixed = discountFixed;
            DiscountPercent = discountPercent;
            ItemGift = itemGift;
            Description = description;
        }

        public string? Code { get; set; }
        public decimal? DiscountFixed { get; set; }
        public int? DiscountPercent { get; set; }
        public string[] ItemGift { get; set; }
        public string? Description { get; set; }
    }
}