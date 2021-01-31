using SushiShop.Ios.Common;
using SushiShop.Ios.TargetBindings;
using UIKit;
using WebKit;

namespace SushiShop.Ios.Extensions
{
    public static class BindingExtensions
    {
        public static string BindImage(this UIButton _) => BindingConstants.Image;

        public static string BindHtmlString(this WKWebView _) => nameof(WKWebViewHtmlStringTargetBinding);

        public static string BindUrl(this WKWebView _) => nameof(WKWebViewUrlTargetBinding);
    }
}