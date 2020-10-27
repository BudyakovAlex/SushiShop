using SushiShop.Core.Data.Models.Toppings;

namespace SushiShop.Core.NavigationParameters
{
    public class ToppingNavigationParameters
    {
        public ToppingNavigationParameters(Topping topping)
        {
            Topping = topping;
        }

        public Topping Topping { get; }
    }
}