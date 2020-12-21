using SushiShop.Core.Data.Dtos.Profile;
using SushiShop.Core.Data.Models.Profile;
using SushiShop.Core.Mappers.Common;

namespace SushiShop.Core.Mappers.Profile
{
    public static class PersonalDataMapper
    {
        public static PersonalData Map(this PersonalDataDto personalDataDto)
        {
            return new PersonalData(
                personalDataDto.UserId,
                personalDataDto.Email,
                personalDataDto.Phone,
                personalDataDto.DateOfBirth,
                personalDataDto.FirstName,
                personalDataDto.LastName,
                personalDataDto.FullName,
                personalDataDto.Gender,
                personalDataDto.IsAllowSubscribe,
                personalDataDto.IsAllowNotifications,
                personalDataDto.IsAllowPush,
                personalDataDto.IsNeedRegistration,
                personalDataDto.DateOfBirthFormated,
                personalDataDto.CanChangeDateOfBirth,
                personalDataDto.SubscribeSales,
                personalDataDto.Photo?.Map());
        }
    }
}