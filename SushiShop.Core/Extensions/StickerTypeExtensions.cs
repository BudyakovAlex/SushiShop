using SushiShop.Core.Data.Enums;
using SushiShop.Core.Resources;

namespace SushiShop.Core.Extensions
{
    public static class StickerTypeExtensions
    {
        public static string ToLocalizedString(this StickerType stickerType)
        {
            return stickerType switch
            {
                StickerType.Hit => AppStrings.Hit,
                StickerType.Hot => AppStrings.Hot,
                StickerType.New => AppStrings.Season,
                StickerType.Vegan => AppStrings.Vegan,
                _ => string.Empty
            };
        }
    }
}