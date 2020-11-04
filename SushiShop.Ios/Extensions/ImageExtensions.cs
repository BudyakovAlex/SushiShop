using System;
using CoreGraphics;
using FFImageLoading.Cross;
using SushiShop.Ios.Common;
using UIKit;

namespace SushiShop.Ios.Extensions
{
    public static class ImageExtensions
    {
        public static UIImage WithAlpha(this UIImage image, nfloat alpha) =>
            new UIGraphicsImageRenderer(image.Size, image.ImageRendererFormat)
                .CreateImage(_ => image.Draw(CGPoint.Empty, CGBlendMode.Normal, alpha));
    }
}
