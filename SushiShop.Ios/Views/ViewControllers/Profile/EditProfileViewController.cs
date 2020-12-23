using BuildApps.Core.Mobile.MvvmCross.UIKit.Views.ViewControllers;
using MvvmCross.Platforms.Ios.Binding;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using SushiShop.Core.Resources;
using SushiShop.Core.ViewModels.Profile;
using SushiShop.Ios.Common;
using UIKit;

namespace SushiShop.Ios.Views.ViewControllers.Profile
{
    [MvxChildPresentation(Animated = true)]
    public partial class EditProfileViewController : BaseViewController<EditProfileViewModel>
    {
        private UIButton saveButton;

        protected override void InitStylesAndContent()
        {
            base.InitStylesAndContent();

            Title = AppStrings.PersonalData;
            NametextField.Placeholder = AppStrings.Name;
            GenderTextField.Placeholder = AppStrings.Gender;
            DateOfBirdthtextField.Placeholder = AppStrings.DateOfBirth;
            PhoneTextField.Placeholder = AppStrings.Phone;
            EmailTextField.Placeholder = AppStrings.Email;

            DateOfBirdthtextField.UserInteractionEnabled = false;
        }

        protected override void InitNavigationItem(UINavigationItem navigationItem)
        {
            base.InitNavigationItem(navigationItem);

            saveButton = Components.CreateDefaultBarButtonText(AppStrings.Save);
            navigationItem.RightBarButtonItem = new UIBarButtonItem(saveButton);
            navigationItem.LeftBarButtonItem = Components.CreateBarButtonItem(ImageNames.ArrowBack);
        }

        protected override void Bind()
        {
            base.Bind();

            var bindingSet = CreateBindingSet();

            bindingSet.Bind(NavigationItem.LeftBarButtonItem).For(v => v.BindClicked()).To(vm => vm.PlatformCloseCommand);
            bindingSet.Bind(saveButton).For(v => v.BindTap()).To(vm => vm.SaveCommand);
            bindingSet.Bind(NametextField).For(v => v.Text).To(vm => vm.FullName);
            bindingSet.Bind(GenderTextField).For(v => v.Text).To(vm => vm.Gender);
            bindingSet.Bind(DateOfBirdthtextField).For(v => v.Text).To(vm => vm.DateOfBirth);
            bindingSet.Bind(PhoneTextField).For(v => v.Text).To(vm => vm.Phone);
            bindingSet.Bind(EmailTextField).For(v => v.Text).To(vm => vm.Email);
            bindingSet.Bind(IsAllowNotificationsSwitch).For(v => v.BindOn()).To(vm => vm.IsAllowNotifications).TwoWay();
            bindingSet.Bind(IsAllowPushSwitch).For(v => v.BindOn()).To(vm => vm.IsAllowPush).TwoWay();

            bindingSet.Apply();
        }
    }
}

