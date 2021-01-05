using SushiShop.Core.Data.Models.Shops;

namespace SushiShop.Core.NavigationParameters
{
    public class ShopsNearMetroNavigationParameters
    {
        public ShopsNearMetroNavigationParameters(MetroShop[] shops)
        {
            Shops = shops;
        }

        public MetroShop[] Shops { get; }
    }
}
