using SushiShop.Core.Data.Dtos.Profile;
using Model = SushiShop.Core.Data.Models.Profile;

namespace SushiShop.Core.Mappers.Profile
{
    public static class ProfileMapper
    {
        public static Model.Profile Map(this ProfileDto profileDto)
        {
            return new Model.Profile(
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
    }
}