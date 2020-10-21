using SushiShop.Core.Data.Models.Stickers;

namespace SushiShop.Core.Data.Models.Menu
{
    public class Menu
    {
        public Menu(Category[] categories, Sticker[] stickers)
        {
            Categories = categories;
            Stickers = stickers;
        }

        public Category[] Categories { get; }

        public Sticker[] Stickers { get; }
    }
}
