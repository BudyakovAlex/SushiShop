using BuildApps.Core.Mobile.MvvmCross.UIKit.Views.ViewControllers;
using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Binding;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using SushiShop.Core.ViewModels.Common;
using SushiShop.Ios.Common;
using UIKit;

namespace SushiShop.Ios.Views.ViewControllers.CommonInfo
{
    [MvxChildPresentation]
    public partial class CommonInfoViewController : BaseViewController<CommonInfoViewModel>
    {
        private UIButton backButton;

        private string html;
        public string Html
        {
            get => html;
            set
            {
                html = value;
                if (html != null)
                {
                    WebView.LoadHtmlString(new NSString(html), null);
                }
            }
        }

        protected override void InitNavigationItem(UINavigationItem navigationItem)
        {
            base.InitNavigationItem(navigationItem);

            backButton = Components.CreateDefaultBarButton(ImageNames.ImageBack);
            navigationItem.LeftBarButtonItem = new UIBarButtonItem(backButton);
        }

        protected override void Bind()
        {
            base.Bind();

            var bindingSet = this.CreateBindingSet<CommonInfoViewController, CommonInfoViewModel>();

            bindingSet.Bind(this).For(v => v.Title).To(vm => vm.Title);
            bindingSet.Bind(this).For(v => v.Html).To(vm => vm.Content);
            bindingSet.Bind(backButton).For(v => v.BindTap()).To(vm => vm.PlatformCloseCommand);

            bindingSet.Apply();
        }
    }
}