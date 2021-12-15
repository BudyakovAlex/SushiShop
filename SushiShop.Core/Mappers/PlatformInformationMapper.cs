using SushiShop.Core.Data.Dtos.Common;
using SushiShop.Core.Data.Models.Common;

namespace SushiShop.Core.Mappers
{
    public static class PlatformInformationMapper
    {
        public static PlatformInformation Map(this PlatformInformationDto dto) =>
            new PlatformInformation(
                dto.Version!,
                dto.Build,
                dto.Url!);
    }
}