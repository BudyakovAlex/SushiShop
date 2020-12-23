using SushiShop.Core.Data.Dtos.Profile;
using SushiShop.Core.Data.Models.Profile;

namespace SushiShop.Core.Mappers
{
    public static class BaseProfileMapper
    {
        public static BaseProfile Map(this BaseProfileDto profileDto)
        {
            return new BaseProfile(
                profileDto.Email,
                profileDto.Phone,
                profileDto.DateOfBirth,
                profileDto.FirstName,
                profileDto.LastName,
                profileDto.FullName,
                profileDto.Gender,
                profileDto.IsAllowSubscribe,
                profileDto.IsAllowNotifications,
                profileDto.IsAllowPush,
                profileDto.IsNeedRegistration);
        }

        public static BaseProfileDto Map(this BaseProfile profile)
        {
            var profileDto = new BaseProfileDto
            {
                Email = profile.Email,
                Phone = profile.Phone,
                DateOfBirth = profile.DateOfBirth,
                FirstName = profile.FirstName,
                LastName = profile.LastName,
                FullName = profile.FullName,
                Gender = profile.Gender,
                IsAllowSubscribe = profile.IsAllowSubscribe,
                IsAllowNotifications = profile.IsAllowNotifications,
                IsAllowPush = profile.IsAllowPush,
                IsNeedRegistration = profile.IsNeedRegistration
            };

            return profileDto;
        }
    }
}
