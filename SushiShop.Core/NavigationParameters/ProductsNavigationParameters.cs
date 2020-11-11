using SushiShop.Core.Data.Models.Menu;
using SushiShop.Core.Data.Models.Stickers;

namespace SushiShop.Core.NavigationParameters
{
    public class ProductsNavigationParameters
    {
        public ProductsNavigationParameters(Category category)
        {
            Category = category;
        }

        public ProductsNavigationParameters(Sticker sticker)
        {
            Sticker = sticker;
        }

        public Category? Category { get; }
        public Sticker? Sticker { get; }
    }
}
