using SushiShop.Core.Data.Models.Shops;

namespace SushiShop.Core.NavigationParameters
{
    public class ShopOnMapNavigationParameter
    {
        public ShopOnMapNavigationParameter(Shop shop)
        {
            Shop = shop;
        }

        public Shop Shop { get; }
    }
}