using BuildApps.Core.Mobile.MvvmCross.UIKit.Views.ViewControllers;
using MvvmCross.Platforms.Ios.Binding;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using SushiShop.Core.Resources;
using SushiShop.Core.ViewModels.Info;
using SushiShop.Core.ViewModels.Info.Items;
using SushiShop.Ios.Views.Cells.Info;
using UIKit;

namespace SushiShop.Ios.Views.ViewControllers.Info
{
    [MvxTabPresentation(WrapInNavigationController = true)]
    public partial class InfoViewController : BaseViewController<InfoViewModel>
    {
        protected override void InitStylesAndContent()
        {
            base.InitStylesAndContent();

            PhoneTitleLabel.Text = AppStrings.Office;
            ShopsTitleLabel.Text = AppStrings.Shops;
            DevelopedAtTitleLabel.Text = AppStrings.DevelopedAt;

            MenuStackView.RegisterView<InfoMenuItemViewCell, InfoMenuItemViewModel>();
            SocialNetworksStackView.RegisterView<SocialNetworkItemViewCell, SocialNetworkItemViewModel>();
        }

        protected override void InitNavigationItem(UINavigationItem navigationItem)
        {
            base.InitNavigationItem(navigationItem);

            navigationItem.Title = AppStrings.Info;
        }

        protected override void Bind()
        {
            base.Bind();

            var bindingSet = CreateBindingSet();

            bindingSet.Bind(MenuStackView).For(v => v.ItemsSource).To(vm => vm.Items);
            bindingSet.Bind(SocialNetworksStackView).For(v => v.ItemsSource).To(vm => vm.SocialNetworks);
            bindingSet.Bind(ShopsContainerView).For(v => v.BindTap()).To(vm => vm.GoToShopsCommand);
            bindingSet.Bind(DevelopedAtView).For(v => v.BindTap()).To(vm => vm.OpenSiteInBrowserCommand);
            bindingSet.Bind(PhoneLabel).For(v => v.Text).To(vm => vm.OfficePhone);
            bindingSet.Bind(PhoneLabel).For(v => v.BindTap()).To(vm => vm.CallToOfficeCommand);
            bindingSet.Bind(PhoneContainerView).For(v => v.BindVisible()).To(vm => vm.HasOfficePhone);
            bindingSet.Bind(MenuStackView).For(v => v.ItemsSource).To(vm => vm.Items);

            bindingSet.Apply();
        }
    }
}