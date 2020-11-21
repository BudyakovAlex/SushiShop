using System.Collections.Generic;
using SushiShop.Core.Data.Models.Toppings;

namespace SushiShop.Core.NavigationParameters
{
    public class ToppingNavigationParameters
    {
        public ToppingNavigationParameters(
            List<Topping> toppings,
            string? title,
            string? currency)
        {
            Toppings = toppings;
            Title = title;
            Currency = currency;
        }

        public List<Topping> Toppings { get; }

        public string? Title { get; set; }

        public string? Currency { get; }
    }
}