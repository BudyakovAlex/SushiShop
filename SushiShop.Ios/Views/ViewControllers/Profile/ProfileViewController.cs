using BuildApps.Core.Mobile.MvvmCross.UIKit.Views.ViewControllers;
using MvvmCross.Platforms.Ios.Binding;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using SushiShop.Core.Converters;
using SushiShop.Core.Resources;
using SushiShop.Core.ViewModels.Profile;
using SushiShop.Ios.Common;
using SushiShop.Ios.Common.Styles;
using UIKit;

namespace SushiShop.Ios.Views.ViewControllers.Profile
{
    [MvxTabPresentation(WrapInNavigationController = true)]
    public partial class ProfileViewController : BaseViewControllerWithKeyboard<ProfileViewModel>
    {
        private UIButton logoutButton;

        protected override bool HandlesKeyboardNotifications => true;

        protected override void InitStylesAndContent()
        {
            base.InitStylesAndContent();

            EmailLoginTextField.Placeholder = AppStrings.ProfileLoginPlaceholder;

            RegisterButton.Layer.BorderWidth = 2;
            RegisterButton.Layer.BorderColor = Colors.OrangeGradientEnd.CGColor;
            RegisterButton.Layer.CornerRadius = RegisterButton.Frame.Height / 2;
        }

        protected override void InitNavigationItem(UINavigationItem navigationItem)
        {
            base.InitNavigationItem(navigationItem);

            navigationItem.Title = AppStrings.Profile;
            UserImage.SetCornerRadius(35);
            UserImage.SetPlaceholders(ImageNames.UserProfilePlaceholder);

            logoutButton = Components.CreateDefaultBarButton(ImageNames.Logout);
            navigationItem.RightBarButtonItem = new UIBarButtonItem(logoutButton);
        }

        protected override void Bind()
        {
            base.Bind();

            var bindingSet = CreateBindingSet();

            bindingSet.Bind(LoginButton).For(v => v.BindTouchUpInside()).To(vm => vm.LoginCommand);
            bindingSet.Bind(RegisterButton).For(v => v.BindTouchUpInside()).To(vm => vm.RegistrationCommand);
            bindingSet.Bind(EmailLoginTextField).For(v => v.Text).To(vm => vm.PhoneOrEmail);
            bindingSet.Bind(logoutButton).For(v => v.BindTouchUpInside()).To(vm => vm.LogoutCommand);
            bindingSet.Bind(logoutButton).For(v => v.BindVisible()).To(vm => vm.IsAuthorized);
            bindingSet.Bind(ProfileView).For(v => v.BindVisible()).To(vm => vm.IsAuthorized);
            bindingSet.Bind(UserImage).For(v => v.ImagePath).To(vm => vm.Avatar);
            bindingSet.Bind(ScoreButton).For(v => v.BindTouchUpInside()).To(vm => vm.ShowBonusProgramCommand);
            bindingSet.Bind(PersonalDataView).For(v => v.BindTap()).To(vm => vm.ShowEditProfileCommand);
            bindingSet.Bind(MyOrdersView).For(v => v.BindTap()).To(vm => vm.ShowMyOrdersCommand);
            bindingSet.Bind(FeedbackView).For(v => v.BindTap()).To(vm => vm.ShowFeedbackCommand);
            bindingSet.Bind(UserImage).For(v => v.BindTap()).To(vm => vm.ChooseNewImageCommand);
            bindingSet.Bind(UserNameLabel).For(v => v.Text).To(vm => vm.Username);
            bindingSet.Bind(ScoreButton).For(v => v.BindTitle()).To(vm => vm.Score);
            bindingSet.Bind(LoadingActivityIndicator).For(v => v.BindVisible()).To(vm => vm.IsBusy);

            bindingSet.Apply();
        }
    }
}