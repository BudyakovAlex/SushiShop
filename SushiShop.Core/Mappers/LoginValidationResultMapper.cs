using SushiShop.Core.Data.Dtos.Profile;
using SushiShop.Core.Data.Models.Profile;

namespace SushiShop.Core.Mappers
{
    public static class LoginValidationResultMapper
    {
        public static LoginValidationResult Map(this LoginValidationResultDto dto)
        {
            return new LoginValidationResult(
                dto.IsAuthByPhone,
                dto.IsAuthByEmail,
                dto.IsRegistrationNeeded,
                dto.Message,
                dto.Placeholder,
                dto.IsPhone,
                dto.IsEmail,
                dto.RepeatCodeTimeout);
        }
    }
}