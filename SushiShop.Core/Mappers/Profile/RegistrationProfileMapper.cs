using SushiShop.Core.Data.Dtos.Profile;
using SushiShop.Core.Data.Models.Profile;

namespace SushiShop.Core.Mappers.Profile
{
    public static class RegistrationProfileMapper
    {
        public static RegistrationProfile Map(this ProfileRegistrationDto registrationProfileDto)
        {
            return new RegistrationProfile(
                registrationProfileDto.Message,
                registrationProfileDto.Phone);
        }
    }
}