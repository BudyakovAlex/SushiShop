using CoreAnimation;
using System;
using CoreGraphics;
using UIKit;
using FFImageLoading.Cross;

namespace SushiShop.Ios.Common.Styles
{
    public static class ViewStyles
    {
        public static void ApplyShadow(this UIView view, bool shouldRasterize = true)
        {
            view.Layer.MasksToBounds = false;
            view.Layer.ShadowColor = Colors.RealBlack.CGColor;
            view.Layer.ShadowOffset = new CGSize(0f, 2f);
            view.Layer.ShadowOpacity = 0.2f;
            view.Layer.ShadowRadius = 18f;

            if (shouldRasterize)
            {
                view.Layer.ShouldRasterize = true;
                view.Layer.RasterizationScale = UIScreen.MainScreen.Scale;
            }
            else
            {
                view.Layer.ShouldRasterize = false;
            }
        }

        public static void SetCornerRadius(this UIView view, nfloat? cornerRadius = null)
        {
            view.Layer.CornerRadius = cornerRadius ?? view.Frame.Height / 2;
            view.Layer.MasksToBounds = true;
        }

        public static CAGradientLayer SetGradientBackground(this UIView view)
        {
            var gradientLayer = new CAGradientLayer()
            {
                Frame = view.Bounds,
                Colors = new[] { Colors.OrangeGradientStart.CGColor, Colors.OrangeGradientEnd.CGColor },
                StartPoint = new CGPoint(0, 0.5),
                EndPoint = new CGPoint(1, 0.5)
            };
            view.Layer.AddSublayer(gradientLayer);
            return gradientLayer;
        }

        public static void SetPlaceholders(this MvxCachedImageView imageView, string imageName = ImageNames.DefaultPlaceholder)
        {
            imageView.ErrorPlaceholderImagePath = imageName;
            imageView.LoadingPlaceholderImagePath = imageName;
        }
    }
}
