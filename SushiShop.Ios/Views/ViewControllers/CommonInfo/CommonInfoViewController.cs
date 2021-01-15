using BuildApps.Core.Mobile.MvvmCross.UIKit.Views.ViewControllers;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using SushiShop.Core.ViewModels.Common;
using SushiShop.Ios.Delegates;
using SushiShop.Ios.Extensions;

namespace SushiShop.Ios.Views.ViewControllers.CommonInfo
{
    [MvxChildPresentation]
    public partial class CommonInfoViewController : BaseViewController<CommonInfoViewModel>
    {
        protected override void InitStylesAndContent()
        {
            base.InitStylesAndContent();

            WebView.NavigationDelegate = new WKWebViewNavigationDelegate();
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