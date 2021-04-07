using System;
using System.Globalization;
using MvvmCross.Converters;

namespace SushiShop.Core.Converters
{
    public class DelegateConverter<TFrom, TTo> : MvxValueConverter<TFrom, TTo>
	{
        private readonly Func<TFrom, TTo> convertFunc;
        private readonly Func<TTo, TFrom>? convertBackFunc;

		public DelegateConverter(Func<TFrom, TTo> convertFunc, Func<TTo, TFrom>? convertBackFunc = null)
		{
			this.convertFunc = convertFunc;
            this.convertBackFunc = convertBackFunc;
		}

        protected override TTo Convert(TFrom value, Type targetType, object parameter, CultureInfo culture)
        {
            return convertFunc.Invoke(value);
        }

        protected override TFrom ConvertBack(TTo value, Type targetType, object parameter, CultureInfo culture)
        {
            var from = convertBackFunc is null ? default : convertBackFunc.Invoke(value);
            return from;
        }
    }
}