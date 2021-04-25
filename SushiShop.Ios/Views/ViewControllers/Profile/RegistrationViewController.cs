using BuildApps.Core.Mobile.Common.Extensions;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Views.ViewControllers;
using Foundation;
using MvvmCross.Platforms.Ios.Binding;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using SushiShop.Core.Converters;
using SushiShop.Core.Resources;
using SushiShop.Core.ViewModels.Profile;
using SushiShop.Ios.Common;
using SushiShop.Ios.Extensions;
using SushiShop.Ios.Formatters;
using SushiShop.Ios.Views.Controls;
using UIKit;

namespace SushiShop.Ios.Views.ViewControllers.Profile
{
    [MvxModalPresentation(
        WrapInNavigationController = true,
        Animated = true,
        ModalPresentationStyle = UIModalPresentationStyle.OverFullScreen)]
    public partial class RegistrationViewController : BaseViewControllerWithKeyboard<RegistrationViewModel>
    {
        private readonly NSRange privacyPolicyRange = new NSRange(61, 27);

        private UIButton backButton;
        private UIDatePicker dateOfBirthDatePicker;

        protected override bool HandlesKeyboardNotifications => true;

        protected override void InitStylesAndContent()
        {
            base.InitStylesAndContent();

            Title = AppStrings.Registration;

            NameTextField.Placeholder = AppStrings.NameAndSurnameRequired;
            DateOfBirthTextField.Placeholder = AppStrings.DateOfBirthRequired;
            PhoneTextField.Placeholder = AppStrings.PhoneRequired;
            EmailTextField.Placeholder = AppStrings.EmailRequired;
            _ = new PhoneNumberFormatter(PhoneTextField);

            DateOfBirthTextField.InputView = dateOfBirthDatePicker = new UIDatePicker()
            {
                Mode = UIDatePickerMode.Date,
                PreferredDatePickerStyle = UIDatePickerStyle.Wheels,
                MaximumDate = NSDate.Now
            };

            DateOfBirthTextField.InputAccessoryView = new DoneAccessoryView(View, () => { });

            var attributedText = new NSMutableAttributedString(AppStrings.PrivacyPolicyConfirmation);
            attributedText.AddAttribute(UIStringAttributeKey.UnderlineStyle, NSNumber.FromInt32((int)NSUnderlineStyle.Single), privacyPolicyRange);
            AcceptDescriptionLabel.AttributedText = attributedText;
            AcceptDescriptionLabel.AddGestureRecognizer(new UITapGestureRecognizer(TapOnLabel).DisposeWith(ViewModel?.Disposables));
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

            bindingSet.Bind(backButton).For(v => v.BindTap()).To(vm => vm.CloseCommand);
            bindingSet.Bind(RegisterButton).For(v => v.BindTap()).To(vm => vm.RegisterCommand);
            bindingSet.Bind(NameTextField).For(v => v.Text).To(vm => vm.FullName);
            bindingSet.Bind(dateOfBirthDatePicker).For(v => v.BindDate()).To(vm => vm.DateOfBirth).TwoWay();
            bindingSet.Bind(DateOfBirthTextField).For(v => v.Text).To(vm => vm.DateOfBirth)
                .WithConversion<DateTimeToStringConverter>();
            bindingSet.Bind(PhoneTextField).For(v => v.Text).To(vm => vm.Phone);
            bindingSet.Bind(EmailTextField).For(v => v.Text).To(vm => vm.Email);
            bindingSet.Bind(PushNotificationsSwitch).For(v => v.BindOn()).To(vm => vm.IsAcceptPushNotifications).TwoWay();
            bindingSet.Bind(EmailNotificationsSwitch).For(v => v.BindOn()).To(vm => vm.IsAcceptEmailNotifications).TwoWay();
            bindingSet.Bind(SmsNotificationsSwitch).For(v => v.BindOn()).To(vm => vm.IsAcceptSmsNotifications).TwoWay();
            bindingSet.Bind(LoadingActivityIndicator).For(v => v.BindVisible()).To(vm => vm.IsBusy);

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

