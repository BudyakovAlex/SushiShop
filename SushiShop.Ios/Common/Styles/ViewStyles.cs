using CoreGraphics;
using UIKit;

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
    }
}
