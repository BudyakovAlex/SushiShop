using System.Linq;
using SushiShop.Core.Data.Dtos.Menu;
using SushiShop.Core.Data.Models.Menu;

namespace SushiShop.Core.Mappers
{
    public static class MenuMapper
    {
        public static Menu Map(this MenuDto dto) =>
            new Menu(
                dto.Categories.Select(x => x.Map()).ToArray(),
                dto.Stickers.Map());
    }
}
