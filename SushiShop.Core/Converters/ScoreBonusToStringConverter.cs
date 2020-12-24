using System;
using System.Globalization;
using MvvmCross.Converters;
using SushiShop.Core.Resources;

namespace SushiShop.Core.Converters
{
    public class ScoreBonusToStringConverter : MvxValueConverter<int, string>
    {
        protected override string Convert(int value, Type targetType, object parameter, CultureInfo culture)
        {
            return $"{value} {AppStrings.Points}";
        }
    }
}
