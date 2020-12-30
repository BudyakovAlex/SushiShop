using SushiShop.Core.Data.Dtos.Common;
using SushiShop.Core.Data.Models.Common;

namespace SushiShop.Core.Mappers
{
    public static class SocialNetworkMapper
    {
        public static SocialNetwork Map(this SocialNetworkDto dto)
        {
            return new SocialNetwork(dto.Url!, dto.ImageUrl!);
        }
    }
}