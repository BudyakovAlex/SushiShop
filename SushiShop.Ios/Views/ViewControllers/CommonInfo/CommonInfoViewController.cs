using BuildApps.Core.Mobile.MvvmCross.UIKit.Views.ViewControllers;
using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using SushiShop.Core.ViewModels.Common;

namespace SushiShop.Ios.Views.ViewControllers.CommonInfo
{
    [MvxChildPresentation]
    public partial class CommonInfoViewController : BaseViewController<CommonInfoViewModel>
    {
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

        protected override void Bind()
        {
            base.Bind();

            var bindingSet = this.CreateBindingSet<CommonInfoViewController, CommonInfoViewModel>();

            bindingSet.Bind(this).For(v => v.Title).To(vm => vm.Title);
            bindingSet.Bind(this).For(v => v.Html).To(vm => vm.Content);

            bindingSet.Apply();
        }
    }
}