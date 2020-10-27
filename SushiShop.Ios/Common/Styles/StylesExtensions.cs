using CoreAnimation;
using CoreGraphics;
using SushiShop.Core.Data.Enums;
using UIKit;

namespace SushiShop.Ios.Common.Styles
{
    public static class StylesExtensions
    {
        public static void SetBackButtonStyle(this UIButton button)
        {
            button.BackgroundColor = Colors.TransparentBlack;
            button.SetImage(UIImage.FromBundle(ImageNames.ImageBackWhite), UIControlState.Normal);
            button.Layer.CornerRadius = button.Frame.Height / 2;
            button.Layer.MasksToBounds = true;
        }

        public static void SetGradientStyle(this UIButton button, string text = null)
        {
            button.SetGradientBackground();
            button.Layer.CornerRadius = button.Frame.Height / 2;
            button.Layer.MasksToBounds = true;
            button.SetTitle(text, UIControlState.Normal);
            button.SetTitleColor(Colors.White, UIControlState.Normal);
            button.Font = Font.Create(FontStyle.Regular, 18);
        }

        public static void SetGradientBackground(this UIView view)
        {
            var gradientLayer = new CAGradientLayer()
            {
                Frame = view.Bounds,
                Colors = new[] { Colors.OrangeStartGradient.CGColor, Colors.OrangeEndGradient.CGColor },
                StartPoint = new CGPoint(0, 0.5),
                EndPoint = new CGPoint(1, 0.5)
            };
            view.Layer.AddSublayer(gradientLayer);
        }

        public static void SetRegularStyle(this UILabel label, string text = null, int fontSize = 16)
        {
            label.Text = text;
            label.Font = Font.Create(FontStyle.Regular, fontSize);
        }

        public static void SetRegularItalicStyle(this UILabel label, string text = null, int fontSize = 16)
        {
            label.Text = text;
            label.Font = Font.Create(FontStyle.RegularItalic, fontSize);
        }

        public static void SetMediumStyle(this UILabel label, string text = null, int fontSize = 16)
        {
            label.Text = text;
            label.Font = Font.Create(FontStyle.Medium, fontSize);
        }

        public static void SetMediumItalicStyle(this UILabel label, string text = null, int fontSize = 16)
        {
            label.Text = text;
            label.Font = Font.Create(FontStyle.MediumItalic, fontSize);
        }

        public static void SetBoldStyle(this UILabel label, string text = null, int fontSize = 16)
        {
            label.Text = text;
            label.Font = Font.Create(FontStyle.Bold, fontSize);
        }

        public static void SetBoldItalicStyle(this UILabel label, string text = null, int fontSize = 16)
        {
            label.Text = text;
            label.Font = Font.Create(FontStyle.BoldItalic, fontSize);
        }

        public static void SetSemiboldStyle(this UILabel label, string text = null, int fontSize = 16)
        {
            label.Text = text;
            label.Font = Font.Create(FontStyle.Semibold, fontSize);
        }

        public static void SetSemiboldItalicStyle(this UILabel label, string text = null, int fontSize = 16)
        {
            label.Text = text;
            label.Font = Font.Create(FontStyle.SemiboldItalic, fontSize);
        }

        public static void SetPriceStyle(this UILabel label, string text = null)
        {
            label.SetBoldStyle(text, 24);
        }

        public static void SetOldPriceStyle(this UILabel label, string text = null)
        {
            label.SetBoldStyle(text, 24);
            label.TextColor = Colors.Gray2;
        }
    }
}
