using SushiShop.Core.Data.Dtos.Sticker;

namespace SushiShop.Core.Data.Dtos.Menu
{
    public class MenuDto
    {
        public MenuDto(CategoryDto[] categories, StickersDto stickers)
        {
            Categories = categories;
            Stickers = stickers;
        }

        public CategoryDto[] Categories { get; }

        public StickersDto Stickers { get; }
    }
}
