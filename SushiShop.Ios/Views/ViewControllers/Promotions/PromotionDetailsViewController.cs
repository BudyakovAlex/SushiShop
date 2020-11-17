using BuildApps.Core.Mobile.MvvmCross.UIKit.Views.ViewControllers;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Binding;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using SushiShop.Core.ViewModels.Promotions;
using SushiShop.Ios.Common;
using SushiShop.Ios.Common.Styles;
using UIKit;

namespace SushiShop.Ios.Views.ViewControllers.Promotions
{
    [MvxChildPresentation]
    public partial class PromotionDetailsViewController : BaseViewController<PromotionDetailsViewModel>
    {
        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            NavigationController.SetNavigationBarHidden(hidden: true, animated: true);
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);
            NavigationController.SetNavigationBarHidden(hidden: false, animated: true);
        }

        protected override void InitStylesAndContent()
        {
            base.InitStylesAndContent();

            ImageView.SetPlaceholders();
        }

        protected override void Bind()
        {
            base.Bind();

            var bindingSet = this.CreateBindingSet<PromotionDetailsViewController, PromotionDetailsViewModel>();

            bindingSet.Bind(BackButton).For(v => v.BindTouchUpInside()).To(vm => vm.PlatformCloseCommand);
            bindingSet.Bind(LoadingView).For(v => v.BindVisible()).To(vm => vm.IsBusy);
            bindingSet.Bind(ImageView).For(v => v.ImagePath).To(vm => vm.ImageUrl);

            bindingSet.Apply();
        }
    }
}
