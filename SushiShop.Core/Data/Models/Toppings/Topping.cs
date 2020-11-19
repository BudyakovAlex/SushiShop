using SushiShop.Core.Data.Enums;

namespace SushiShop.Core.Data.Models.Toppings
{
    public class Topping
    {
        public Topping(
            long id,
            string? pageTitle,
            decimal price,
            int countInBasket,
            ToppingCategory toppingCategory)
        {
            Id = id;
            PageTitle = pageTitle;
            Price = price;
            CountInBasket = countInBasket;
            ToppingCategory = toppingCategory;
        }

        public long Id { get; }
        public string? PageTitle { get; }
        public decimal Price { get; }
        public ToppingCategory ToppingCategory { get; set; }
        public int CountInBasket { get; set; }
    }
}