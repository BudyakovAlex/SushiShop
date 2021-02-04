using BuildApps.Core.Mobile.MvvmCross.UIKit.Views.ViewControllers;
using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using SushiShop.Core.ViewModels.Common;
using SushiShop.Ios.Extensions;
using System;
using UIKit;
using WebKit;

namespace SushiShop.Ios.Views.ViewControllers.CommonInfo
{
    [MvxChildPresentation]
    public partial class CommonInfoViewController : BaseViewController<CommonInfoViewModel>, IWKNavigationDelegate
    {
        [Export("webView:decidePolicyForNavigationAction:decisionHandler:")]
        public void DecidePolicy(
            WKWebView webView,
            WKNavigationAction navigationAction,
            Action<WKNavigationActionPolicy> decisionHandler)
        {
            UIApplication.SharedApplication.OpenUrl(navigationAction.Request.Url);
            decisionHandler.Invoke(WKNavigationActionPolicy.Allow);
        }

        protected override void InitStylesAndContent()
        {
            base.InitStylesAndContent();

            WebView.NavigationDelegate = this;
        }

        protected override void Bind()
        {
            base.Bind();

            var bindingSet = this.CreateBindingSet<CommonInfoViewController, CommonInfoViewModel>();

            bindingSet.Bind(this).For(v => v.Title).To(vm => vm.Title);
            bindingSet.Bind(WebView).For(v => v.BindHtmlString()).To(vm => vm.Content);

            bindingSet.Apply();
        }
    }
}