using SushiShop.Core.Data.Dtos.Profile;
using SushiShop.Core.Data.Models.Profile;
using SushiShop.Core.Mappers.Common;

namespace SushiShop.Core.Mappers.Profile
{
    public static class PersonalDataMapper
    {
        public static PersonalData Map(this PersonalDataDto personalDataDto)
        {
            return new PersonalData(personalDataDto.DateOfBirth,
                                    personalDataDto.DateOfBirthFormated,
                                    personalDataDto.DetaOfBirthReadonly,
                                    personalDataDto.Email,
                                    personalDataDto.SubscribeSales,
                                    personalDataDto.FirstName,
                                    personalDataDto.LastName,
                                    personalDataDto.FullName,
                                    personalDataDto.Gender,
                                    personalDataDto.Phone,
                                    personalDataDto.AllowNotification,
                                    personalDataDto.AllowPush,
                                    personalDataDto.Photo?.Map(),
                                    personalDataDto.UserId);
        }
    }
}