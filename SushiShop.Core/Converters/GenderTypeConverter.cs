using System;
using System.Globalization;
using MvvmCross.Converters;
using SushiShop.Core.Data.Enums;
using SushiShop.Core.Extensions;
using SushiShop.Core.Resources;

namespace SushiShop.Core.Converters
{
    public class GenderTypeToStringConverter : MvxValueConverter<GenderType, string>
    {
        protected override string Convert(GenderType value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToLocalizedString();
        }

        protected override GenderType ConvertBack(string value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == AppStrings.Female)
            {
                return GenderType.Female;
            }

            if (value == AppStrings.Male)
            {
                return GenderType.Male;
            }

            return GenderType.Unknown;
        }
    }
}
