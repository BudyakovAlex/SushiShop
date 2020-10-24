using System;
using SushiShop.Core.Data.Enums;
using UIKit;

namespace SushiShop.Ios.Common
{
    public static class Font
    {
        private const string SFProDisplayRegular = "SFProDisplay-Regular";
        private const string SFProDisplayRegularItalic = "SFProDisplay-RegularItalic";
        private const string SFProDisplayMedium = "SFProDisplay-Medium";
        private const string SFProDisplayMediumItalic = "SFProDisplay-MediumItalic";
        private const string SFProDisplaySemibold = "SFProDisplay-Semibold";
        private const string SFProDisplaySemiboldItalic = "SFProDisplay-SemiboldItalic";
        private const string SFProDisplayBold = "SFProDisplay-Bold";
        private const string SFProDisplayBoldItalic = "SFProDisplay-BoldItalic";

        public static UIFont Create(FontStyle fontStyle, nfloat size)
        {
            var fontName = GetFontName(fontStyle);
            var font = UIFont.FromName(fontName, size);

            return font;

            static string GetFontName(FontStyle fontStyle) => fontStyle switch
            {
                FontStyle.Regular => SFProDisplayRegular,
                FontStyle.RegularItalic => SFProDisplayRegularItalic,
                FontStyle.Medium => SFProDisplayMedium,
                FontStyle.MediumItalic => SFProDisplayMediumItalic,
                FontStyle.Semibold => SFProDisplaySemibold,
                FontStyle.SemiboldItalic => SFProDisplaySemiboldItalic,
                FontStyle.Bold => SFProDisplayBold,
                FontStyle.BoldItalic => SFProDisplayBoldItalic,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}
