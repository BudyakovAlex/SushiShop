using System.Collections.Generic;
using SushiShop.Core.Data.Models.Toppings;

namespace SushiShop.Core.NavigationParameters
{
    public class ToppingNavigationParameters
    {
        public ToppingNavigationParameters(List<Topping> toppings, string title)
        {
            Toppings = toppings;
            Title = title;
        }

        public List<Topping> Toppings { get; }
        public string Title { get; set; }
    }
}