using MvvmCross.Converters;
using System;
using System.Globalization;

namespace SushiShop.Core.Converters
{
    public class BoolToValueConverter<TValue> : MvxValueConverter<bool, TValue>
    {
        public TValue TrueValue { get; set; }
        public TValue FalseValue { get; set; }

        protected override TValue Convert(bool value, Type targetType, object parameter, CultureInfo culture)
        {
            return value ? TrueValue : FalseValue;
        }
    }
}