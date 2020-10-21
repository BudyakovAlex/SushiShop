using System;
using System.Globalization;
using MvvmCross.Converters;

namespace SushiShop.Core.Converters
{
    public class BoolToValueConverter<TValue> : MvxValueConverter<bool, TValue>
    {
        private readonly TValue trueValue;
        private readonly TValue falseValue;

        public BoolToValueConverter(TValue trueValue, TValue falseValue)
        {
            this.trueValue = trueValue;
            this.falseValue = falseValue;
        }

        protected override TValue Convert(bool value, Type targetType, object parameter, CultureInfo culture)
        {
            return value ? trueValue : falseValue;
        }
    }
}