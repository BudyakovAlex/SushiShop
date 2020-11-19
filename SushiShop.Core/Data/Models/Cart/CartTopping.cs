namespace SushiShop.Core.Data.Models.Cart
{
    public class CartTopping
    {
        public CartTopping(
            long id,
            int count,
            decimal price,
            decimal amount,
            string? pageTitle)
        {
            Id = id;
            Count = count;
            Price = price;
            Amount = amount;
            PageTitle = pageTitle;
        }

        public long Id { get; }
        public int Count { get; }
        public decimal Price { get; }
        public decimal Amount { get; }
        public string? PageTitle { get; }
    }
}