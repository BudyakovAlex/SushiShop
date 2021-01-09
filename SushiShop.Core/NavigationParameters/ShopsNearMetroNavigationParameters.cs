using SushiShop.Core.Data.Models.Shops;

namespace SushiShop.Core.NavigationParameters
{
    public class ShopsNearMetroNavigationParameters
    {
        public ShopsNearMetroNavigationParameters(MetroShop[] shops, string? title)
        {
            Shops = shops;
            Title = title;
        }

        public MetroShop[] Shops { get; }

        public string? Title { get; }
    }
}
