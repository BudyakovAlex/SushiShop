using BuildApps.Core.Mobile.MvvmCross.UIKit.Views.ViewControllers;
using MvvmCross.Platforms.Ios.Binding;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using SushiShop.Core.Converters;
using SushiShop.Core.Resources;
using SushiShop.Core.ViewModels.Profile;
using SushiShop.Ios.Common;
using SushiShop.Ios.Sources;
using SushiShop.Ios.Views.Controls;
using UIKit;

namespace SushiShop.Ios.Views.ViewControllers.Profile
{
    [MvxChildPresentation(Animated = true)]
    public partial class EditProfileViewController : BaseViewControllerWithKeyboard<EditProfileViewModel>
    {
        private UIButton saveButton;
        private UIPickerView genderPickerView;
        private GenderPickerViewModel genderPickerViewModel;

        protected override bool HandlesKeyboardNotifications => true;

        protected override void InitStylesAndContent()
        {
            base.InitStylesAndContent();

            Title = AppStrings.PersonalData;
            NametextField.Placeholder = AppStrings.Name;
            GenderTextField.Placeholder = AppStrings.Gender;
            DateOfBirdthtextField.Placeholder = AppStrings.DateOfBirth;
            PhoneTextField.Placeholder = AppStrings.Phone;
            EmailTextField.Placeholder = AppStrings.Email;

            genderPickerView = new UIPickerView()
            {
                ShowSelectionIndicator = true
            };

            genderPickerViewModel = new GenderPickerViewModel(genderPickerView);
            genderPickerView.Model = genderPickerViewModel;
            GenderTextField.InputView = genderPickerView;
            GenderTextField.InputAccessoryView = new DoneAccessoryView(View, () => { });

            DateOfBirdthtextField.UserInteractionEnabled = false;
        }

        public override void ViewWillDisappear(bool animated)
        {
            UIApplication.SharedApplication.KeyWindow.EndEditing(true);
            base.ViewWillDisappear(animated);
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
            bindingSet.Bind(saveButton).For(v => v.BindTouchUpInside()).To(vm => vm.SaveCommand);
            bindingSet.Bind(NametextField).For(v => v.Text).To(vm => vm.FullName);
            bindingSet.Bind(GenderTextField).For(v => v.Text).To(vm => vm.Gender)
                .WithConversion<GenderTypeToStringConverter>();
            bindingSet.Bind(DateOfBirdthtextField).For(v => v.Text).To(vm => vm.DateOfBirth)
                .WithConversion<DateTimeToStringConverter>();
            bindingSet.Bind(PhoneTextField).For(v => v.Text).To(vm => vm.Phone);
            bindingSet.Bind(EmailTextField).For(v => v.Text).To(vm => vm.Email);
            bindingSet.Bind(IsAllowNotificationsSwitch).For(v => v.BindOn()).To(vm => vm.IsAllowNotifications).TwoWay();
            bindingSet.Bind(IsAllowPushSwitch).For(v => v.BindOn()).To(vm => vm.IsAllowPush).TwoWay();
            bindingSet.Bind(genderPickerViewModel).For(v => v.ItemsSource).To(vm => vm.GenderTypes);
            bindingSet.Bind(genderPickerViewModel).For(v => v.SelectedItem).To(vm => vm.Gender).TwoWay();
            bindingSet.Bind(LoadingActivityIndicator).For(v => v.BindVisible()).To(vm => vm.IsBusy);

            bindingSet.Apply();
        }
    }
}

