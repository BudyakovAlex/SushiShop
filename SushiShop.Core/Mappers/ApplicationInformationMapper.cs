using SushiShop.Core.Data.Dtos.Common;
using SushiShop.Core.Data.Models.Common;

namespace SushiShop.Core.Mappers
{
    public static class ApplicationInformationMapper
    {
        public static ApplicationInformation Map(this ApplicationInformationDto dto) =>
            new ApplicationInformation(
                dto.ShouldUpdate,
                dto.Message!,
                dto.Platforms!.Map());
    }
}