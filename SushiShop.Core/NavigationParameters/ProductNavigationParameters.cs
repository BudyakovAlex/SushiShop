using SushiShop.Core.Data.Models.Menu;
using SushiShop.Core.Data.Models.Stickers;

namespace SushiShop.Core.NavigationParameters
{
    public class ProductNavigationParameters
    {
        public ProductNavigationParameters(Category category)
        {
            Category = category;
        }

        public ProductNavigationParameters(Sticker sticker)
        {
            Sticker = sticker;
        }

        public Category? Category { get; }
        public Sticker? Sticker { get; }
    }
}
