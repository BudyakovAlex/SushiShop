using SushiShop.Core.Data.Models.Shops;

namespace SushiShop.Core.NavigationParameters
{
    public class ShopOnMapNavigationParameter
    {
        public ShopOnMapNavigationParameter(Shop shop, bool isSelectionMode)
        {
            Shop = shop;
            IsSelectionMode = isSelectionMode;
        }

        public Shop Shop { get; }

        public bool IsSelectionMode { get; }
    }
}