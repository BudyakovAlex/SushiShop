using SushiShop.Core.Data.Dtos.Profile;
using SushiShop.Core.Data.Models.Profile;

namespace SushiShop.Core.Mappers.Profile
{
    public static class RegistrationProfileMapper
    {
        public static ProfileRegistration Map(this ProfileRegistrationDto registrationProfileDto)
        {
            return new ProfileRegistration(
                registrationProfileDto.Message,
                registrationProfileDto.Phone);
        }
    }
}