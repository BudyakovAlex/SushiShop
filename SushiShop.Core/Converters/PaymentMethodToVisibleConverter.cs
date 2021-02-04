using System;
using System.Globalization;
using MvvmCross.Converters;
using SushiShop.Core.Data.Enums;

namespace SushiShop.Core.Converters
{
    public class PaymentMethodToVisibleConverter : MvxValueConverter<PaymentMethod, bool>
    {
        protected override bool Convert(PaymentMethod value, Type targetType, object parameter, CultureInfo culture)
        {
            return value == (PaymentMethod)parameter;
        }
    }
}
