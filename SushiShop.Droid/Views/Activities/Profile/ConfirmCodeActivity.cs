using Android.App;
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
        private AppCompatButton confirmButton;
        private View loadingOverlayView;

        public ConfirmCodeActivity() : base(Resource.Layout.activity_confirm_code)
        {
        }

        protected override void InitializeViewPoroperties()
        {
            base.InitializeViewPoroperties();

            toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            confirmationMessageTextView = FindViewById<TextView>(Resource.Id.confirmation_message_text_view);
            codeTextInputLayout = FindViewById<TextInputLayout>(Resource.Id.confirm_code_text_input_layout);
            codeEditText = FindViewById<TextInputEditText>(Resource.Id.confirm_code_edit_text);
            confirmButton = FindViewById<AppCompatButton>(Resource.Id.confirm_code_button);
            loadingOverlayView = FindViewById<View>(Resource.Id.loading_overlay_view);

            confirmButton.SetRoundedCorners(this.DpToPx(25));

            confirmButton.Text = AppStrings.Continue;
            toolbar.Title = AppStrings.AcceptPhoneTitle;
        }

        protected override void Bind()
        {
            base.Bind();

            using var bindingSet = CreateBindingSet();

            bindingSet.Bind(confirmationMessageTextView).For(v => v.Text).To(vm => vm.Message);
            bindingSet.Bind(codeTextInputLayout).For(v => v.Hint).To(vm => vm.Placeholder);
            bindingSet.Bind(codeEditText).For(v => v.Text).To(vm => vm.Code);
            bindingSet.Bind(confirmButton).For(v => v.BindClick()).To(vm => vm.ContinueCommand);
            bindingSet.Bind(loadingOverlayView).For(v => v.BindVisible()).To(vm => vm.IsBusy);
            bindingSet.Bind(toolbar).For(v => v.BindBackNavigationItemCommand()).To(vm => vm.CloseCommand);
        }
    }
}
