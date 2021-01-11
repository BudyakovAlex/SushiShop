﻿using BuildApps.Core.Mobile.MvvmCross.UIKit.Views.ViewControllers;
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
    public partial class ConfirmCodeViewController : BaseViewController<ConfirmCodeViewModel>
    {
        private UIButton backButton;

        protected override void InitStylesAndContent()
        {
            base.InitStylesAndContent();

            Title = AppStrings.AcceptPhoneTitle;
            CodeTextField.Placeholder = AppStrings.LastFourDigits;
            CodeTextField.InputAccessoryView = new DoneAccessoryView(View, () => ViewModel?.ContinueCommand?.Execute());
        }

        public override void ViewWillDisappear(bool animated)
        {
            UIApplication.SharedApplication.KeyWindow.EndEditing(true);
            base.ViewWillDisappear(animated);
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

            bindingSet.Bind(backButton).For(v => v.BindTouchUpInside()).To(vm => vm.CloseCommand);
            bindingSet.Bind(ContinueButton).For(v => v.BindTouchUpInside()).To(vm => vm.ContinueCommand);
            bindingSet.Bind(CodeTextField).For(v => v.Text).To(vm => vm.Code);
            bindingSet.Bind(ConfirmationMessageLabel).For(v => v.Text).To(vm => vm.Message);
            bindingSet.Bind(LoadingActivityIndicator).For(v => v.BindVisible()).To(vm => vm.IsBusy);

            bindingSet.Apply();
        }
    }
}

