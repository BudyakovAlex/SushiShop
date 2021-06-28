using Android.App;
using Android.Graphics;
using Android.Text;
using Android.Text.Method;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.Widget;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Extensions;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Listeners;
using Google.Android.Material.TextField;
using MvvmCross.Platforms.Android.Binding;
using SushiShop.Core.Converters;
using SushiShop.Core.Resources;
using SushiShop.Core.ViewModels.Profile;
using SushiShop.Droid.Extensions;
using SushiShop.Droid.Platform.Watchers;
using SushiShop.Droid.Views.Activities.Abstract;
using SushiShop.Droid.Views.Listeners;
using SushiShop.Droid.Views.Spans;
using System;
using System.Threading.Tasks;
using Toolbar = AndroidX.AppCompat.Widget.Toolbar;

namespace SushiShop.Droid.Views.Activities.Profile
{
    [Activity]
    public class RegistrationActivity : BaseActivity<RegistrationViewModel>
    {
        private const int StartClickablePrivacyPolicy = 61;
        private const int EndClickablePrivacyPolicy = 88;

        private Toolbar toolbar;
        private TextInputEditText nameTextEditText;
        private TextInputEditText dateOfBirthTextEditText;
        private TextInputEditText phoneTextEditText;
        private TextInputEditText emailTextEditText;
        private SwitchCompat pushNotificationsSwitch;
        private SwitchCompat emailNotificationsSwitch;
        private SwitchCompat smsNotificationsSwitch;
        private AppCompatButton registerButton;
        private View loadingOverlayView;

        public RegistrationActivity() : base(Resource.Layout.activity_registration)
        {
        }

        protected override void InitializeViewPoroperties()
        {
            base.InitializeViewPoroperties();

            toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            nameTextEditText = FindViewById<TextInputEditText>(Resource.Id.name_edit_text);
            dateOfBirthTextEditText = FindViewById<TextInputEditText>(Resource.Id.date_of_birth_edit_text);
            phoneTextEditText = FindViewById<TextInputEditText>(Resource.Id.phone_edit_text);
            emailTextEditText = FindViewById<TextInputEditText>(Resource.Id.email_edit_text);
            pushNotificationsSwitch = FindViewById<SwitchCompat>(Resource.Id.push_notifications_switch);
            emailNotificationsSwitch = FindViewById<SwitchCompat>(Resource.Id.email_notifications_switch);
            smsNotificationsSwitch = FindViewById<SwitchCompat>(Resource.Id.sms_notifications_switch);
            registerButton = FindViewById<AppCompatButton>(Resource.Id.register_button);
            loadingOverlayView = FindViewById<View>(Resource.Id.loading_overlay_view);

            phoneTextEditText.AddTextChangedListener(new PhoneTextWatcher(phoneTextEditText));

            nameTextEditText.SetOnKeyListener(new ViewOnKeyListener(OnNameEditTextKeyListener));
            dateOfBirthTextEditText.InputType = InputTypes.Null;
            dateOfBirthTextEditText.SetOnClickListener(new ViewOnClickListener(OnBirthdayTextEditTextClickedAsync));
            registerButton.Text = AppStrings.Register;
            toolbar.Title = AppStrings.Registration;
            FindViewById<TextInputLayout>(Resource.Id.name_text_input_layout).Hint = AppStrings.NameAndSurnameRequired;
            FindViewById<TextInputLayout>(Resource.Id.date_of_birth_text_input_layout).Hint = AppStrings.DateOfBirthRequired;
            FindViewById<TextInputLayout>(Resource.Id.phone_text_input_layout).Hint = AppStrings.PhoneRequired;
            FindViewById<TextInputLayout>(Resource.Id.email_text_input_layout).Hint = AppStrings.EmailRequired;
            FindViewById<TextView>(Resource.Id.push_notifications_text_view).Text = AppStrings.PushNotifications;
            FindViewById<TextView>(Resource.Id.email_notifications_text_view).Text = AppStrings.EmailNotifications;
            FindViewById<TextView>(Resource.Id.sms_notifications_text_view).Text = AppStrings.SmsNotifications;

            registerButton.SetRoundedCorners(this.DpToPx(25));

            var spannableString = new SpannableString(AppStrings.PrivacyPolicyConfirmation);
            var clickableSpan = new LinkSpan((_) => OnLinkSpanClick());
            spannableString.SetSpan(clickableSpan, StartClickablePrivacyPolicy, EndClickablePrivacyPolicy, SpanTypes.ExclusiveExclusive);
            var textView = FindViewById<TextView>(Resource.Id.terms_text_view);
            textView.SetText(spannableString, TextView.BufferType.Spannable);
            textView.MovementMethod = LinkMovementMethod.Instance;
            textView.SetHighlightColor(Color.Transparent);
        }

        protected override void Bind()
        {
            base.Bind();

            using var bindingSet = CreateBindingSet();

            bindingSet.Bind(toolbar).For(v => v.BindBackNavigationItemCommand()).To(vm => vm.CloseCommand);
            bindingSet.Bind(nameTextEditText).For(v => v.Text).To(vm => vm.FullName);
            bindingSet.Bind(dateOfBirthTextEditText).For(v => v.Text).To(vm => vm.DateOfBirth)
                .WithConversion<DateTimeToStringConverter>();
            bindingSet.Bind(phoneTextEditText).For(v => v.Text).To(vm => vm.Phone);
            bindingSet.Bind(emailTextEditText).For(v => v.Text).To(vm => vm.Email);
            bindingSet.Bind(pushNotificationsSwitch).For(v => v.BindChecked()).To(vm => vm.IsAcceptPushNotifications);
            bindingSet.Bind(emailNotificationsSwitch).For(v => v.BindChecked()).To(vm => vm.IsAcceptEmailNotifications);
            bindingSet.Bind(smsNotificationsSwitch).For(v => v.BindChecked()).To(vm => vm.IsAcceptSmsNotifications);
            bindingSet.Bind(registerButton).For(v => v.BindClick()).To(vm => vm.RegisterCommand);
            bindingSet.Bind(loadingOverlayView).For(v => v.BindVisible()).To(vm => vm.IsBusy);
        }

        private Task OnBirthdayTextEditTextClickedAsync(View view)
        {
            var date = ViewModel?.DateOfBirth ?? DateTime.Now;
            var datePickerDialog = new DatePickerDialog(this, OnDatePickerDialogSelectDate, date.Year, date.Month, date.Day);
            datePickerDialog.DatePicker.MaxDate = DateTime.Now.ToDialogPickerDate();
            datePickerDialog.Show();
            return Task.CompletedTask;
        }

        private void OnDatePickerDialogSelectDate(object sender, DatePickerDialog.DateSetEventArgs e)
        {
            ViewModel.DateOfBirth = e.Date;
            phoneTextEditText.RequestFocus();
        }

        private void OnLinkSpanClick()
        {
            ViewModel?.ShowPrivacyPolicyCommand?.Execute();
        }

        private bool OnNameEditTextKeyListener(View view, Keycode keyCode, KeyEvent e)
        {
            if (e.Action == KeyEventActions.Down)
            {
                switch (keyCode)
                {
                    case Keycode.DpadCenter:
                    case Keycode.Enter:
                        return dateOfBirthTextEditText.CallOnClick();
                    default:
                        break;
                }
            }

            return false;
        }
    }
}
