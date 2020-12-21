using SushiShop.Core.Data.Dtos.Profile;
using SushiShop.Core.Data.Models.Profile;

namespace SushiShop.Core.Mappers.Profile
{
    public static class LoginProfileMapper
    {
        public static LoginProfile Map(this LoginProfileDto loginProfileDto)
        {
            return new LoginProfile(loginProfileDto.IsAuthByEmail,
                                    loginProfileDto.IsAuthByPhone,
                                    loginProfileDto.IsEmail,
                                    loginProfileDto.IsPhone,
                                    loginProfileDto.Message,
                                    loginProfileDto.IsNeedRegistration,
                                    loginProfileDto.Phone);
        }
    }
}