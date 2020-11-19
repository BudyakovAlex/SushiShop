namespace SushiShop.Core.Data.Models.Cart
{
    public class CartTopping
    {
        public CartTopping(long id, int count, int price, string? pageTitle)
        {
            Id = id;
            Count = count;
            Price = price;
            PageTitle = pageTitle;
        }

        public long Id { get; }
        public int Count { get; }
        public int Price { get; }
        public string? PageTitle { get; }
    }
}