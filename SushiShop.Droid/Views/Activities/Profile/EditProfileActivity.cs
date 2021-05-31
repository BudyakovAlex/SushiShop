using Android.App;
using Android.Text;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.Widget;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Listeners;
using Google.Android.Material.TextField;
using MvvmCross.Commands;
using MvvmCross.Platforms.Android.Binding;
using SushiShop.Core.Converters;
using SushiShop.Core.Data.Models.Plugins;
using SushiShop.Core.Extensions;
using SushiShop.Core.IoC;
using SushiShop.Core.Plugins;
using SushiShop.Core.Resources;
using SushiShop.Core.ViewModels.Profile;
using SushiShop.Droid.Extensions;
using SushiShop.Droid.Platform.Watchers;
using SushiShop.Droid.Views.Activities.Abstract;
using System.Linq;
using System.Threading.Tasks;
using Toolbar = AndroidX.AppCompat.Widget.Toolbar;

namespace SushiShop.Droid.Views.Activities.Profile
{
    [Activity]
    public class EditProfileActivity : BaseActivity<EditProfileViewModel>
    {
        private Toolbar toolbar;
        private TextView saveTextView;
        private TextInputEditText nameEditText;
        private TextInputEditText genderEditText;
        private TextInputEditText dateOfBirthEditText;
        private TextInputEditText phoneEditText;
        private TextInputEditText emailEditText;
        private SwitchCompat phoneNotificationsSwitch;
        private SwitchCompat emailNotificationSwitch;

        public EditProfileActivity() : base(Resource.Layout.activity_edit_profile)
        {
        }

        protected override void InitializeViewPoroperties()
        {
            base.InitializeViewPoroperties();

            toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            saveTextView = FindViewById<TextView>(Resource.Id.save_text_view);
            nameEditText = FindViewById<TextInputEditText>(Resource.Id.name_edit_text);
            genderEditText = FindViewById<TextInputEditText>(Resource.Id.gender_edit_text);
            dateOfBirthEditText = FindViewById<TextInputEditText>(Resource.Id.date_of_birth_edit_text);
            phoneEditText = FindViewById<TextInputEditText>(Resource.Id.phone_edit_text);
            emailEditText = FindViewById<TextInputEditText>(Resource.Id.email_edit_text);
            phoneNotificationsSwitch = FindViewById<SwitchCompat>(Resource.Id.sms_notifications_switch);
            emailNotificationSwitch = FindViewById<SwitchCompat>(Resource.Id.email_notifications_switch);

            phoneEditText.AddTextChangedListener(new PhoneTextWatcher(phoneEditText));
            genderEditText.InputType = InputTypes.Null;

            saveTextView.Text = AppStrings.Save;
            FindViewById<TextView>(Resource.Id.toolbar_title_text_view).Text = AppStrings.PersonalData;
            FindViewById<TextInputLayout>(Resource.Id.name_text_input_layout).Hint = AppStrings.Name;
            FindViewById<TextInputLayout>(Resource.Id.gender_text_input_layout).Hint = AppStrings.Gender;
            FindViewById<TextInputLayout>(Resource.Id.date_of_birth_text_input_layout).Hint = AppStrings.DateOfBirth;
            FindViewById<TextInputLayout>(Resource.Id.phone_text_input_layout).Hint = AppStrings.Phone;
            FindViewById<TextInputLayout>(Resource.Id.email_text_input_layout).Hint = AppStrings.Email;
            FindViewById<TextView>(Resource.Id.email_notifications_text_view).Text = AppStrings.ReceiveNotificationsAboutNewsAndPromotions;
            FindViewById<TextView>(Resource.Id.sms_notifications_text_view).Text = AppStrings.ReceiveNotificationsAboutNewsAndPromotions;

            genderEditText.SetOnClickListener(new ViewOnClickListener(OnGenderEditTextClickedAsync));
        }

        protected override void Bind()
        {
            base.Bind();

            using var bindingSet = CreateBindingSet();

            bindingSet.Bind(toolbar).For(v => v.BindBackNavigationItemCommand()).To(vm => vm.CloseCommand);
            bindingSet.Bind(nameEditText).For(v => v.Text).To(vm => vm.FullName);
            bindingSet.Bind(genderEditText).For(v => v.Text).To(vm => vm.Gender)
                .WithConversion<GenderTypeToStringConverter>();
            bindingSet.Bind(dateOfBirthEditText).For(v => v.Text).To(vm => vm.DateOfBirth)
                .WithConversion<DateTimeToStringConverter>();
            bindingSet.Bind(phoneEditText).For(v => v.Text).To(vm => vm.Phone);
            bindingSet.Bind(emailEditText).For(v => v.Text).To(vm => vm.Email);
            bindingSet.Bind(phoneNotificationsSwitch).For(v => v.BindChecked()).To(vm => vm.IsAllowNotifications);
            bindingSet.Bind(emailNotificationSwitch).For(v => v.BindChecked()).To(vm => vm.IsAllowSubscribe);
            bindingSet.Bind(saveTextView).For(v => v.BindClick()).To(vm => vm.SaveCommand);
        }

        private Task OnGenderEditTextClickedAsync(View view)
        {
            if (CompositionRoot.Container.TryResolve(out IDialog dialog))
            {
                var dialogActions = ViewModel.GenderTypes.Select(gender => new DialogAction(gender.ToLocalizedString(), new MvxCommand(() => ViewModel.Gender = gender)));
                return dialog.ShowActionSheetAsync(
                    AppStrings.Gender,
                    null,
                    AppStrings.Cancel,
                    dialogActions.ToArray());
            }

            return Task.CompletedTask;
        }
    }
}
