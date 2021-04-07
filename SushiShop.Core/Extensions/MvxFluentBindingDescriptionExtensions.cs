using MvvmCross.Binding.BindingContext;
using SushiShop.Core.Converters;
using System;

namespace SushiShop.Core.Extensions
{
    public static class MvxFluentBindingDescriptionExtensions
    {
        public static MvxFluentBindingDescription<TTarget, TSource> WithConversion<TTarget, TSource, TFrom, TTo>(
            this MvxFluentBindingDescription<TTarget, TSource> bindingDescription,
            Func<TFrom, TTo> convertFunc,
            Func<TTo, TFrom>? convertBackFunc = null)
            where TTarget : class
        {
            return bindingDescription.WithConversion(new DelegateConverter<TFrom, TTo>(convertFunc, convertBackFunc));
        }

        public static MvxFluentBindingDescription<TTarget, TSource> WithBoolConversion<TTarget, TSource, TTo>(
            this MvxFluentBindingDescription<TTarget, TSource> bindingDescription,
            TTo trueValue,
            TTo falseValue)
            where TTarget : class
        {
            return bindingDescription.WithConversion(new BoolToValueConverter<TTo>(trueValue, falseValue));
        }
    }
}
