using System.Linq;
using SushiShop.Core.Data.Dtos.Menu;
using SushiShop.Core.Data.Models.Menu;

namespace SushiShop.Core.Mappers
{
    public static class CategoryChildrenMapper
    {
        public static CategoryChildren Map(this CategoryChildrenDto dto) =>
            new CategoryChildren(
                dto.Ids!,
                dto.SubCategories!.Select(x => x.Map()).ToArray());
    }
}
