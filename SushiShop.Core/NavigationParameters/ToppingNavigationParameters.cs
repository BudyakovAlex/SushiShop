using System.Collections.Generic;
using SushiShop.Core.Data.Models.Toppings;

namespace SushiShop.Core.NavigationParameters
{
    public class ToppingNavigationParameters
    {
        public ToppingNavigationParameters(List<Topping> toppings, string pageTitle)
        {
            Toppings = toppings;
            PageTitle = pageTitle;
        }

        public List<Topping> Toppings { get; }
        public string PageTitle { get; set; }
    }
}