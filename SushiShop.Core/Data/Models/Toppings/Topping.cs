namespace SushiShop.Core.Data.Models.Toppings
{
    public class Topping
    {
        public Topping(
            long id,
            string? pageTitle,
            long price,
            long countInBasket)
        {
            Id = id;
            PageTitle = pageTitle;
            Price = price;
            CountInBasket = countInBasket;
        }

        public long Id { get; }
        public string? PageTitle { get; }
        public long Price { get; }
        public long CountInBasket { get; }
    }
}