using SushiShop.Core.Data.Dtos.Menu;
using SushiShop.Core.Mappers.Stickers;
using System.Linq;
using Model = SushiShop.Core.Data.Models.Menu;

namespace SushiShop.Core.Mappers.Menu
{
    public static class MenuMapper
    {
        public static Model.Menu Map(this MenuDto dto) =>
            new Model.Menu(
                dto.Categories!.Select(category => category.Map()).ToArray(),
                dto.Stickers!.Map());
    }
}