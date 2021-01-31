using BuildApps.Core.Mobile.MvvmCross.UIKit.Views.ViewControllers;
using Foundation;
using MvvmCross.Platforms.Ios.Binding;
using SushiShop.Core.Common;
using SushiShop.Core.Resources;
using SushiShop.Core.ViewModels.Payment;
using SushiShop.Ios.Common;
using SushiShop.Ios.Extensions;
using UIKit;
using WebKit;

namespace SushiShop.Ios.Views.ViewControllers.Payment
{
    public partial class PaymentViewController : BaseViewController<PaymentViewModel>, IWKNavigationDelegate
    {
        private UIButton backButton;

        public PaymentViewController() : base("PaymentViewController", null)
        {
        }

        protected override void InitStylesAndContent()
        {
            base.InitStylesAndContent();

            WebView.NavigationDelegate = this;
        }

        protected override void InitNavigationItem(UINavigationItem navigationItem)
        {
            base.InitNavigationItem(navigationItem);

            backButton = Components.CreateDefaultBarButton(ImageNames.ArrowBack);
            navigationItem.LeftBarButtonItem = new UIBarButtonItem(backButton);
            Title = AppStrings.OrderPayment;
        }

        protected override void Bind()
        {
            base.Bind();

            var bindingSet = CreateBindingSet();

            bindingSet.Bind(WebView).For(v => v.BindUrl()).To(vm => vm.PaymentUrl);
            bindingSet.Bind(backButton).For(v => v.BindTouchUpInside()).To(vm => vm.CloseCommand);

            bindingSet.Apply();
        }

        [Export("webView:didFinishNavigation:")]
        public void DidFinishNavigation(WKWebView webView, WKNavigation navigation)
        {
            webView.Url.AbsoluteString.EndsWith(SushiShop.Core.Common.Constants.Rest.PaymentOkResource);
        }
    }
}