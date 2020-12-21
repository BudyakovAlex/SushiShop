using SushiShop.Core.Data.Dtos.Profile;
using SushiShop.Core.Data.Models.Profile;

namespace SushiShop.Core.Mappers.Profile
{
    public static class AuthProfileMapper
    {
        public static AuthorizationData Map(this AuthorizationDataDto authProfileDto)
        {
            return new AuthorizationData(authProfileDto.IsValidUser,
                                   authProfileDto.Token,
                                   authProfileDto.UserId);
        }
    }
}