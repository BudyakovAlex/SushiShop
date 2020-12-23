using SushiShop.Core.Data.Dtos.Profile;
using SushiShop.Core.Data.Models.Profile;

namespace SushiShop.Core.Mappers
{
    public static class ProfileMapper
    {
        public static Profile Map(this ProfileDto profileDto)
        {
            return new Profile(
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

        public static ProfileDto Map(this Profile profile)
        {
            var profileDto = new ProfileDto
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
