using BuildApps.Core.Mobile.MvvmCross.UIKit.Views.ViewControllers;
using MvvmCross.Platforms.Ios.Binding;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using SushiShop.Core.Resources;
using SushiShop.Core.ViewModels.Profile;
using SushiShop.Ios.Common;
using UIKit;

namespace SushiShop.Ios.Views.ViewControllers.Profile
{
    [MvxTabPresentation(WrapInNavigationController = true)]
    public partial class ProfileViewController : BaseViewController<ProfileViewModel>
    {
        private UIButton logoutButton;

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

            logoutButton = Components.CreateDefaultBarButton(ImageNames.Logout);
            navigationItem.RightBarButtonItem = new UIBarButtonItem(logoutButton);
        }

        protected override void Bind()
        {
            base.Bind();

            var bindingSet = CreateBindingSet();

            bindingSet.Bind(LoginButton).For(v => v.BindTap()).To(vm => vm.LoginCommand);
            bindingSet.Bind(RegisterButton).For(v => v.BindTap()).To(vm => vm.RegistrationCommand);
            bindingSet.Bind(EmailLoginTextField).For(v => v.Text).To(vm => vm.Login);
            bindingSet.Bind(logoutButton).For(v => v.BindTap()).To(vm => vm.LogoutCommand);
            bindingSet.Bind(logoutButton).For(v => v.BindVisible()).To(vm => vm.IsLogged);
            bindingSet.Bind(ProfileView).For(v => v.BindVisible()).To(vm => vm.IsLogged);
            bindingSet.Bind(ScoreButton).For(v => v.BindTap()).To(vm => vm.ShowScoreViewCommand);
            bindingSet.Bind(PersonalDataView).For(v => v.BindTap()).To(vm => vm.ShowPersonalDataViewCommand);
            bindingSet.Bind(MyOrdersView).For(v => v.BindTap()).To(vm => vm.ShowMyOrdersViewCommand);
            bindingSet.Bind(FeedbackView).For(v => v.BindTap()).To(vm => vm.ShowFeedbackViewCommand);
            bindingSet.Bind(UserImage).For(v => v.BindTap()).To(vm => vm.ChooseNewImageCommand);
            bindingSet.Bind(UserNameLabel).For(v => v.Text).To(vm => vm.UserName);
            bindingSet.Bind(ScoreButton.TitleLabel).For(v => v.Text).To(vm => vm.Score);

            bindingSet.Apply();
        }
    }
}
