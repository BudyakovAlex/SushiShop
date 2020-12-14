using BuildApps.Core.Mobile.Common.Extensions;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Views.ViewControllers;
using Foundation;
using MvvmCross.Platforms.Ios.Binding;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using SushiShop.Core.Resources;
using SushiShop.Core.ViewModels.Profile;
using SushiShop.Ios.Common;
using SushiShop.Ios.Extensions;
using UIKit;

namespace SushiShop.Ios.Views.ViewControllers.Profile
{
    [MvxModalPresentation(
        WrapInNavigationController = true,
        Animated = true,
        ModalPresentationStyle = UIModalPresentationStyle.OverFullScreen)]
    public partial class RegistrationViewController : BaseViewController<RegistrationViewModel>
    {
        private readonly NSRange privacyPolicyRange = new NSRange(61, 27);

        private UIButton backButton;

        protected override void InitStylesAndContent()
        {
            base.InitStylesAndContent();

            Title = AppStrings.Registration;

            NameTextField.Placeholder = AppStrings.NameAndSurnameRequired;
            DateOfBirthTextField.Placeholder = AppStrings.DateOfBirthRequired;
            PhoneTextField.Placeholder = AppStrings.PhoneRequired;
            EmailTextField.Placeholder = AppStrings.EmailRequired;
            
            var attributedText = new NSMutableAttributedString(AppStrings.PrivacyPolicy);
            attributedText.AddAttribute(UIStringAttributeKey.UnderlineStyle, NSNumber.FromInt32((int)NSUnderlineStyle.Single), privacyPolicyRange);
            AcceptDescriptionLabel.AttributedText = attributedText;
            AcceptDescriptionLabel.AddGestureRecognizer(new UITapGestureRecognizer(TapOnLabel).DisposeWith(ViewModel?.Disposables));
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

            bindingSet.Bind(backButton).For(v => v.BindTap()).To(vm => vm.CloseCommand);
            bindingSet.Bind(RegisterButton).For(v => v.BindTap()).To(vm => vm.RegisterCommand);
            bindingSet.Bind(NameTextField).For(v => v.Text).To(vm => vm.Name);
            bindingSet.Bind(DateOfBirthTextField).For(v => v.Text).To(vm => vm.DateOfBirth);
            bindingSet.Bind(PhoneTextField).For(v => v.Text).To(vm => vm.Phone);
            bindingSet.Bind(EmailTextField).For(v => v.Text).To(vm => vm.Email);
            bindingSet.Bind(PushNotificationsSwitch).For(v => v.BindOn()).To(vm => vm.AcceptPushNotifications).TwoWay();
            bindingSet.Bind(EmailNotificationsSwitch).For(v => v.BindOn()).To(vm => vm.AcceptEmailNotifications).TwoWay();
            bindingSet.Bind(SmsNotificationsSwitch).For(v => v.BindOn()).To(vm => vm.AcceptSmsNotifications).TwoWay();

            bindingSet.Apply();
        }

        private void TapOnLabel(UITapGestureRecognizer gesture)
        {
            if (gesture.DidTapAttributedTextInLabel(AcceptDescriptionLabel, privacyPolicyRange))
            {
                ViewModel?.ShowPrivacyPolicyCommand?.Execute();
            }
        }
    }
}

