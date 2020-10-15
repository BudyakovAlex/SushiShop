using System;
using SushiShop.Core.Data.Enums;
using UIKit;

namespace SushiShop.Ios.Common
{
    public static class Font
    {
        private const string SFProDisplayRegular = "SF Pro Display Regular";
        private const string SFProDisplayRegularItalic = "SF Pro Display RegularItalic";
        private const string SFProDisplayMedium = "SF Pro Display Medium";
        private const string SFProDisplayMediumItalic = "SF Pro Display MediumItalic";
        private const string SFProDisplaySemibold = "SF Pro Display Semibold";
        private const string SFProDisplaySemiboldItalic = "SF Pro Display SemiboldItalic";
        private const string SFProDisplayBold = "SF Pro Display Bold";
        private const string SFProDisplayBoldItalic = "SF Pro Display BoldItalic";

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
