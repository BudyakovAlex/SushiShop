using BuildApps.Core.Mobile.Common.Extensions;
using Foundation;
using MvvmCross.Binding;
using MvvmCross.Binding.Bindings.Target;
using WebKit;

namespace SushiShop.Ios.TargetBindings
{
    public class WKWebViewUrlTargetBinding : MvxTargetBinding<WKWebView, string>
    {
        public WKWebViewUrlTargetBinding(WKWebView target)
            : base(target)
        {
        }

        public override MvxBindingMode DefaultMode => MvxBindingMode.OneWay;

        protected override void SetValue(string value)
        {
            if (value.IsNullOrEmpty())
            {
                return;
            }

            Target.LoadRequest(new NSUrlRequest(new NSUrl(value)));
        }
    }
}