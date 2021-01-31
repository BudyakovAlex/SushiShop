using System;
using System.Globalization;
using MvvmCross.Converters;

namespace SushiShop.Core.Converters
{
    public class DecimalToStringConverter : MvxValueConverter<decimal, string>
    {
        protected override string Convert(decimal value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString();
        }

        protected override decimal ConvertBack(string value, Type targetType, object parameter, CultureInfo culture)
        {
            return decimal.TryParse(value, out decimal result) ? result : 0;
        }
    }
}
