using System;
using System.Globalization;
using MvvmCross.Converters;

namespace SushiShop.Core.Converters
{
    public class AmountToBoolInvertVisibilityConverter : MvxValueConverter<int, bool>
    {
        protected override bool Convert(int value, Type targetType, object parameter, CultureInfo culture)
        {
            return value > 0;
        }
    }
}
