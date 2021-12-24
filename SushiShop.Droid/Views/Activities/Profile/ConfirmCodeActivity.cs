using Android.App;
using Android.Graphics;
using Android.Text;
using Android.Text.Style;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.Widget;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Extensions;
using Google.Android.Material.TextField;
using MvvmCross.Platforms.Android.Binding;
using SushiShop.Core.Resources;
using SushiShop.Core.ViewModels.Profile;
using SushiShop.Droid.Extensions;
using SushiShop.Droid.Views.Activities.Abstract;
using Toolbar = AndroidX.AppCompat.Widget.Toolbar;

namespace SushiShop.Droid.Views.Activities.Profile
{
    [Activity]
    public class ConfirmCodeActivity : BaseActivity<ConfirmCodeViewModel>
    {
        private Toolbar toolbar;
        private TextView confirmationMessageTextView;
        private TextInputLayout codeTextInputLayout;
        private TextInputEditText codeEditText;
        private View loadingOverlayView;
        private TextView messageToReceiveNewMessageTextView;
        private AppCompatButton sendNewCodeButton;

        public ConfirmCodeActivity()
            : base(Resource.Layout.activity_confirm_code)
        {
        }

        public int SecondsToSendNewMessage
        {
            set => UpdateTextMessageAndVisibilityControls(value);
        }

        protected override void InitializeViewPoroperties()
        {
            base.InitializeViewPoroperties();

            toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            confirmationMessageTextView = FindViewById<TextView>(Resource.Id.confirmation_message_text_view);
            codeTextInputLayout = FindViewById<TextInputLayout>(Resource.Id.confirm_code_text_input_layout);
            codeEditText = FindViewById<TextInputEditText>(Resource.Id.confirm_code_edit_text);
            loadingOverlayView = FindViewById<View>(Resource.Id.loading_overlay_view);
            messageToReceiveNewMessageTextView = FindViewById<TextView>(Resource.Id.message_to_receive_new_code_text_view);
            sendNewCodeButton = FindViewById<AppCompatButton>(Resource.Id.send_new_code_button);

            sendNewCodeButton.SetRoundedCorners(this.DpToPx(25));

            toolbar.Title = AppStrings.AcceptPhoneTitle;
            sendNewCodeButton.Text = AppStrings.ReceiveCode;
        }

        protected override void Bind()
        {
            base.Bind();

            using var bindingSet = CreateBindingSet();

            bindingSet.Bind(confirmationMessageTextView).For(v => v.Text).To(vm => vm.Message);
            bindingSet.Bind(codeTextInputLayout).For(v => v.Hint).To(vm => vm.Placeholder);
            bindingSet.Bind(codeEditText).For(v => v.Text).To(vm => vm.Code);
            bindingSet.Bind(loadingOverlayView).For(v => v.BindVisible()).To(vm => vm.IsBusy);
            bindingSet.Bind(toolbar).For(v => v.BindBackNavigationItemCommand()).To(vm => vm.CloseCommand);
            bindingSet.Bind(sendNewCodeButton).For(v => v.BindClick()).To(vm => vm.SendCodeCommnad);
            bindingSet.Bind(this).For(nameof(SecondsToSendNewMessage)).To(vm => vm.SecondsToSendNewMessage);
        }

        private void UpdateTextMessageAndVisibilityControls(int seconds)
        {
            var formattedString = string.Format(AppStrings.ReceiveNewCallAfterSecondsTemplate, seconds);
            var spannableString = new SpannableString(formattedString);
            spannableString.SetSpan(new ForegroundColorSpan(Color.Black), 0, 27, SpanTypes.ExclusiveExclusive);
            spannableString.SetSpan(new ForegroundColorSpan(Color.Red), 27, spannableString.Length(), SpanTypes.ExclusiveExclusive);
            messageToReceiveNewMessageTextView.SetText(spannableString, TextView.BufferType.Spannable);
            sendNewCodeButton.Visibility = seconds != 0 ? ViewStates.Gone : ViewStates.Visible;
            messageToReceiveNewMessageTextView.Visibility = seconds == 0 ? ViewStates.Gone : ViewStates.Visible;
        }
    }
}
