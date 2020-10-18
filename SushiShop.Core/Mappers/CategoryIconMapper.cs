using SushiShop.Core.Data.Dtos.Menu;
using SushiShop.Core.Data.Models.Menu;

namespace SushiShop.Core.Mappers
{
    public static class CategoryIconMapper
    {
        public static CategoryIcon Map(this CategoryIconDto dto) =>
            new CategoryIcon(dto.Original!, dto.Jpg!, dto.Webp!);
    }
}
