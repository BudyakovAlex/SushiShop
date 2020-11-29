using System;
using System.Globalization;
using Foundation;
using MvvmCross.Converters;
using UIKit;

namespace SushiShop.Ios.Converters
{
    public class StringToStrikethroughAttributedTextConverter : MvxValueConverter<string, NSMutableAttributedString>
    {
        protected override NSMutableAttributedString Convert(string value, Type targetType, object parameter, CultureInfo culture)
        {
            return new NSMutableAttributedString(value ?? string.Empty, new UIStringAttributes() { StrikethroughStyle = NSUnderlineStyle.Single });
        }
    }
}
