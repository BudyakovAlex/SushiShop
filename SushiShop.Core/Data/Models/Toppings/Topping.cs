namespace SushiShop.Core.Data.Models.Toppings
{
    public class Topping
    {
        public Topping(
            int id,
            string? pageTitle,
            long price,
            int countInBasket,
            string? toppingCategory)
        {
            Id = id;
            PageTitle = pageTitle;
            Price = price;
            CountInBasket = countInBasket;
            ToppingCategory = toppingCategory;
        }

        public int Id { get; }
        public string? PageTitle { get; }
        public long Price { get; }
        public string? ToppingCategory { get; set; }
        public int CountInBasket { get; set; }
    }
}