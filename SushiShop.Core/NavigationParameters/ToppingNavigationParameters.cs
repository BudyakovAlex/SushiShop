using System.Collections.Generic;
using SushiShop.Core.Data.Models.Toppings;
using SushiShop.Core.NavigationParameters.Abstract;

namespace SushiShop.Core.NavigationParameters
{
    public class ToppingNavigationParameters
    {
        public ToppingNavigationParameters(List<Topping> toppings) 
        {
            Toppings = toppings;
        }

        public List<Topping> Toppings { get; }
    }
}