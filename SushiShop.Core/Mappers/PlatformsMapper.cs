using SushiShop.Core.Data.Dtos.Common;
using SushiShop.Core.Data.Models.Common;

namespace SushiShop.Core.Mappers
{
    public static class PlatformsMapper
    {
        public static Platforms Map(this PlatformsDto dto) =>
            new Platforms(dto.Ios!.Map(), dto.Android!.Map());
    }
}
