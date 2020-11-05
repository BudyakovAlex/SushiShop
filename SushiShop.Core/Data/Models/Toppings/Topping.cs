namespace SushiShop.Core.Data.Models.Toppings
{
    public class Topping
    {
        public Topping(
            long id,
            string? pageTitle,
            long price,
            int countInBasket)
        {
            Id = id;
            PageTitle = pageTitle;
            Price = price;
            CountInBasket = countInBasket;
        }

        public long Id { get; }
        public string? PageTitle { get; }
        public long Price { get; }
        public int CountInBasket { get; set; }
    }
}