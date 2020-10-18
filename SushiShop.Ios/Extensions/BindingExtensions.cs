using SushiShop.Ios.Common;
using UIKit;

namespace SushiShop.Ios.Extensions
{
    public static class BindingExtensions
    {
        public static string BindImage(this UIButton _) => BindingConstants.Image;
    }
}