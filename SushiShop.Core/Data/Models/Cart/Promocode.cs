namespace SushiShop.Core.Data.Models.Cart
{
    public class Promocode
    {
        public Promocode(
            string? code,
            decimal discountFixed,
            int discountPercent,
            string? gift,
            string? description)
        {
            Code = code;
            DiscountFixed = discountFixed;
            DiscountPercent = discountPercent;
            Gift = gift;
            Description = description;
        }

        public string? Code { get; }
        public decimal DiscountFixed { get; }
        public int DiscountPercent { get; }
        public string? Gift { get; }
        public string? Description { get; }
    }
}