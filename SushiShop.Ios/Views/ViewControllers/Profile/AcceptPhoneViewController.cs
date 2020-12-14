using BuildApps.Core.Mobile.MvvmCross.UIKit.Views.ViewControllers;
using MvvmCross.Platforms.Ios.Binding;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using SushiShop.Core.Resources;
using SushiShop.Core.ViewModels.Profile;
using SushiShop.Ios.Common;
using SushiShop.Ios.Views.Controls;
using UIKit;

namespace SushiShop.Ios.Views.ViewControllers.Profile
{
    [MvxModalPresentation(
        WrapInNavigationController = true,
        Animated = true,
        ModalPresentationStyle = UIModalPresentationStyle.OverFullScreen)]
    public partial class AcceptPhoneViewController : BaseViewController<AcceptPhoneViewModel>
    {
        private UIButton backButton;

        protected override void InitStylesAndContent()
        {
            base.InitStylesAndContent();

            Title = AppStrings.AcceptPhoneTitle;
            CodeTextField.Placeholder = AppStrings.LastFourDigits;
            CodeTextField.InputAccessoryView = new DoneAccessoryView(View, () => ViewModel?.ContinueCommand?.Execute());
        }

        protected override void InitNavigationItem(UINavigationItem navigationItem)
        {
            base.InitNavigationItem(navigationItem);

            backButton = Components.CreateDefaultBarButton(ImageNames.ArrowBack);
            navigationItem.LeftBarButtonItem = new UIBarButtonItem(backButton);
        }

        protected override void Bind()
        {
            base.Bind();

            var bindingSet = CreateBindingSet();

            bindingSet.Bind(backButton).For(v => v.BindTap()).To(vm => vm.PlatformCloseCommand);
            bindingSet.Bind(ContinueButton).For(v => v.BindTap()).To(vm => vm.ContinueCommand);
            bindingSet.Bind(CodeTextField).For(v => v.Text).To(vm => vm.Code);

            bindingSet.Apply();
        }
    }
}

