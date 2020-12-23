using SushiShop.Core.Data.Dtos.Profile;
using SushiShop.Core.Data.Models.Profile;

namespace SushiShop.Core.Mappers
{
    public static class AuthorizationMapper
    {
        public static AuthorizationData Map(this AuthorizationDataDto authProfileDto)
        {
            return new AuthorizationData(
                authProfileDto.UserId,
                authProfileDto.IsValidUser,
                authProfileDto.Token?.Map());
        }
    }
}