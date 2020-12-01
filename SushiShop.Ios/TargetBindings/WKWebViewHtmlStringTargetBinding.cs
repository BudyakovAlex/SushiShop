using Foundation;
using MvvmCross.Binding;
using MvvmCross.Binding.Bindings.Target;
using WebKit;

namespace SushiShop.Ios.TargetBindings
{
    public class WKWebViewHtmlStringTargetBinding : MvxTargetBinding<WKWebView, string>
    {
        public WKWebViewHtmlStringTargetBinding(WKWebView target)
            : base(target)
        {
        }

        public override MvxBindingMode DefaultMode => MvxBindingMode.OneWay;

        protected override void SetValue(string value)
        {
            var htmlString = value is null ? new NSString() : new NSString(value);
            Target.LoadHtmlString(htmlString, null);
        }
    }
}
