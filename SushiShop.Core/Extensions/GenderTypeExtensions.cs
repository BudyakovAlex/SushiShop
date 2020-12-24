using SushiShop.Core.Data.Enums;
using SushiShop.Core.Resources;

namespace SushiShop.Core.Extensions
{
    public static class GenderTypeExtensions
    {
        public static string ToLocalizedString(this GenderType gender)
        {
            return gender switch
            {
                GenderType.Female => AppStrings.Female,
                GenderType.Male => AppStrings.Male,
                _ => string.Empty
            };
        }
    }
}
