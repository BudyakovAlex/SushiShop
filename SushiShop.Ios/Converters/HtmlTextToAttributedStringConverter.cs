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
            var error = new NSError();
            var docuemntAttributes = new NSAttributedStringDocumentAttributes()
            {
                DocumentType = NSDocumentType.HTML,
                StringEncoding = NSStringEncoding.UTF8
            };

            return new NSAttributedString(value, docuemntAttributes, ref error);
        }
    }
}
