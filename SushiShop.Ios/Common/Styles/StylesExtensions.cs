using System;
using CoreAnimation;
using CoreGraphics;
using UIKit;

namespace SushiShop.Ios.Common.Styles
{
    public static class StylesExtensions
    {
        public static void SetCornerRadius(this UIView view, nfloat? cornerRadius = null)
        {
            view.Layer.CornerRadius = cornerRadius ?? view.Frame.Height / 2;
            view.Layer.MasksToBounds = true;
        }

        public static UIView SetGradientBackground(this UIView view)
        {
            var gradientLayer = new CAGradientLayer()
            {
                Frame = view.Bounds,
                Colors = new[] { Colors.OrangeStartGradient.CGColor, Colors.OrangeEndGradient.CGColor },
                StartPoint = new CGPoint(0, 0.5),
                EndPoint = new CGPoint(1, 0.5)
            };
            view.Layer.AddSublayer(gradientLayer);
            return view;
        }
    }
}
