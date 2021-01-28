using SushiShop.Core.Data.Dtos.Common;
using SushiShop.Core.Data.Models.Common;

namespace SushiShop.Core.Mappers
{
    public static class SocialNetworkMapper
    {
        public static LinkedImage Map(this LinkedImageDto dto)
        {
            return new LinkedImage(dto.Url!, dto.ImageUrl!);
        }
    }
}