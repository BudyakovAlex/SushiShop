using SushiShop.Core.Data.Enums;
using SushiShop.Core.Data.Models.Menu;

namespace SushiShop.Core.NavigationParameters
{
    public class ProductNavigationParameters
    {
        public ProductNavigationParameters(Category category)
        {
            Category = category;
        }

        public ProductNavigationParameters(StickerType stickerType)
        {
            StickerType = stickerType;
        }

        public Category? Category { get; }
        public StickerType? StickerType { get; }
    }
}
