using MvvmCross.Converters;
using SushiShop.Core.Data.Enums;
using SushiShop.Ios.Common;
using System;
using System.Globalization;
using UIKit;

namespace SushiShop.Ios.Converters
{
    public class StickerTypeToUIImageConverter : MvxValueConverter<StickerType, UIImage>
    {
        protected override UIImage Convert(StickerType value, Type targetType, object parameter, CultureInfo culture)
        {
            var imageName = value switch
            {
                StickerType.Hit => ImageNames.HitCategoryIcon,
                StickerType.Hot => ImageNames.HotCategoryIcon,
                StickerType.New => ImageNames.SeazonCategoryIcon,
                StickerType.Vegan => ImageNames.VeganCategoryIcon,
                _ => string.Empty
            };

            return UIImage.FromBundle(imageName);
        }
    }
}
