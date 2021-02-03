using System;
using System.Globalization;
using MvvmCross.Converters;

namespace SushiShop.Core.Converters
{
    public class StringToBoolConverter : MvxValueConverter<string, bool>
    {
        protected override bool Convert(string value, Type targetType, object parameter, CultureInfo culture)
        {
            return !string.IsNullOrWhiteSpace(value);
        }
    }
}
