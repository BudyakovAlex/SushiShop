using SushiShop.Core.Data.Dtos.Profile;
using SushiShop.Core.Data.Models.Profile;

namespace SushiShop.Core.Mappers.Profile
{
    public static class RegistrationDataMapper
    {
        public static RegistrationData Map(this RegistrationDataDto registrationDataDto)
        {
            return new RegistrationData(registrationDataDto.Email,
                                        registrationDataDto.Phone,
                                        registrationDataDto.DateOfBirth,
                                        registrationDataDto.FirstName,
                                        registrationDataDto.LastName,
                                        registrationDataDto.FullName,
                                        registrationDataDto.Gender,
                                        registrationDataDto.IsAllowSubscribe,
                                        registrationDataDto.IsAllowNotificarions,
                                        registrationDataDto.IsAllowPush);
        }
    }
}