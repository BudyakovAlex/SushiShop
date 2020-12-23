using SushiShop.Core.Data.Dtos.Profile;
using SushiShop.Core.Data.Models.Profile;

namespace SushiShop.Core.Mappers
{
    public static class DetailedProfileMapper
    {
        public static DetailedProfile Map(this DetailedProfileDto profileDto)
        {
            return new DetailedProfile(
                profileDto.UserId,
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
                profileDto.IsNeedRegistration,
                profileDto.DateOfBirthFormated,
                profileDto.CanChangeDateOfBirth,
                profileDto.SubscribeSales,
                profileDto.Photo?.Map());
        }
    }
}