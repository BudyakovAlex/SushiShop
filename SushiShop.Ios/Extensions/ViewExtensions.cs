using System;
using CoreGraphics;
using UIKit;

namespace SushiShop.Ios.Extensions
{
    public static class ViewExtensions
    {
        public static void UpdateFrame(
            this UIView view,
            nfloat? x = null,
            nfloat? y = null,
            nfloat? width = null,
            nfloat? height = null)
        {
            view.Frame = new CGRect(
                x ?? view.Frame.X,
                y ?? view.Frame.Y,
                width ?? view.Frame.Width,
                height ?? view.Frame.Height);
        }
    }
}
