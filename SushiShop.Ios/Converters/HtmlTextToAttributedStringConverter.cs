using System;
using System.Globalization;
using Foundation;
using MvvmCross.Converters;

namespace SushiShop.Ios.Converters
{
    public class HtmlTextToAttributedStringConverter : MvxValueConverter<string, NSAttributedString>
    {
        protected override NSAttributedString Convert(string value, Type targetType, object parameter, CultureInfo culture)
        {
            var data = NSData.FromString(value ?? string.Empty, NSStringEncoding.UTF8);
            var documentAttributes = new NSAttributedStringDocumentAttributes()
            {
                DocumentType = NSDocumentType.HTML,
                StringEncoding = NSStringEncoding.UTF8,
            };

            NSError error = null;
            var attributedString = new NSAttributedString(data, documentAttributes, ref error);
            return attributedString;
        }
    }
}