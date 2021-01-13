using SushiShop.Core.Data.Models.Shops;

namespace SushiShop.Core.NavigationParameters
{
    public class ShopsNearMetroNavigationParameters
    {
        public ShopsNearMetroNavigationParameters(
            MetroShop[] shops,
            string? title,
            bool isSelectionMode)
        {
            Shops = shops;
            Title = title;
            IsSelectionMode = isSelectionMode;
        }

        public MetroShop[] Shops { get; }

        public string? Title { get; }

        public bool IsSelectionMode { get; }
    }
}
