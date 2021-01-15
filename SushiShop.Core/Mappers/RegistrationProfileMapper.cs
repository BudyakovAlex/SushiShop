using SushiShop.Core.Data.Dtos.Profile;
using SushiShop.Core.Data.Models.Profile;

namespace SushiShop.Core.Mappers
{
    public static class RegistrationProfileMapper
    {
        public static ConfirmationResult Map(this ConfirmationResultDto registrationProfileDto)
        {
            return new ConfirmationResult(
                registrationProfileDto.Message!,
                registrationProfileDto.Phone!,
                registrationProfileDto.Placeholder!);
        }
    }
}