using System;
using System.Globalization;
using MvvmCross.Converters;
using static SushiShop.Core.Common.Constants;

namespace SushiShop.Core.Converters
{
    public class DateTimeToStringConverter : MvxValueConverter<DateTime?, string?>
    {
        protected override string? Convert(DateTime? value, Type targetType, object parameter, CultureInfo culture)
        {
            return value?.ToString(Format.DateTime.DateOfBirth);
        }
    }
}
