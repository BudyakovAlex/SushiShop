using Android.OS;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.Widget;
using AndroidX.ConstraintLayout.Widget;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Extensions;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Views.Fragments;
using Google.Android.Material.TextField;
using MvvmCross.Platforms.Android.Binding;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using SushiShop.Core.Resources;
using SushiShop.Core.ViewModels;
using SushiShop.Core.ViewModels.Profile;
using SushiShop.Droid.Extensions;
using SushiShop.Droid.Views.Fragments.Abstract;
using Toolbar = AndroidX.AppCompat.Widget.Toolbar;

namespace SushiShop.Droid.Views.Fragments.Profile
{
    [MvxTabLayoutPresentation(
        TabLayoutResourceId = Resource.Id.main_tab_layout,
        ViewPagerResourceId = Resource.Id.main_view_pager,
        ActivityHostViewModelType = typeof(MainViewModel))]
    public class ProfileFragment : BaseFragment<ProfileViewModel>, ITabFragment
    {
        private Toolbar toolbar;
        private AppCompatButton loginButton;
        private AppCompatButton registerButton;
        private TextInputLayout phoneEmailTextInputLayout;
        private TextInputEditText phoneEmailEditText;
        private ImageView exitImageView;
        private ImageView userImageView;
        private TextView profileNameTextView;
        private AppCompatButton discountButton;
        private ConstraintLayout editProfileConstraintLayout;
        private ConstraintLayout myOrdersConstraintLayout;
        private ConstraintLayout feedbackConstraintLayout;

        public ProfileFragment()
            : base(Resource.Layout.fragment_profile)
        {
        }

        public bool IsActivated { get; set; }

        protected override void InitializeViewPoroperties(View view, Bundle savedInstanceState)
        {
            base.InitializeViewPoroperties(view, savedInstanceState);

            toolbar = view.FindViewById<Toolbar>(Resource.Id.toolbar);
            phoneEmailTextInputLayout = view.FindViewById<TextInputLayout>(Resource.Id.phone_email_text_input_layout);
            phoneEmailEditText = view.FindViewById<TextInputEditText>(Resource.Id.phone_email_edit_text);
            loginButton = view.FindViewById<AppCompatButton>(Resource.Id.login_button);
            registerButton = view.FindViewById<AppCompatButton>(Resource.Id.registration_button);
            exitImageView = view.FindViewById<ImageView>(Resource.Id.exit_image_view);
            userImageView = view.FindViewById<ImageView>(Resource.Id.user_image_view);
            profileNameTextView = view.FindViewById<TextView>(Resource.Id.profile_name_text_view);
            discountButton = view.FindViewById<AppCompatButton>(Resource.Id.discount_button);
            editProfileConstraintLayout = view.FindViewById<ConstraintLayout>(Resource.Id.edit_profile_constraint_layout);
            myOrdersConstraintLayout = view.FindViewById<ConstraintLayout>(Resource.Id.my_orders_constraint_layout);
            feedbackConstraintLayout = view.FindViewById<ConstraintLayout>(Resource.Id.feedback_constraint_layout);

            phoneEmailTextInputLayout.Hint = AppStrings.ProfileLoginPlaceholder;
            loginButton.Text = AppStrings.Login;
            registerButton.Text = AppStrings.Register;
            toolbar.Title = AppStrings.Profile;
            editProfileConstraintLayout.FindViewById<TextView>(Resource.Id.edit_profile_text_view).Text = AppStrings.PersonalData;
            myOrdersConstraintLayout.FindViewById<TextView>(Resource.Id.my_orders_text_view).Text = AppStrings.MyOrders;
            feedbackConstraintLayout.FindViewById<TextView>(Resource.Id.feedback_text_view).Text = AppStrings.Feedback;

            loginButton.SetRoundedCorners(Context.DpToPx(25));
            discountButton.SetRoundedCorners(Context.DpToPx(18));
            userImageView.SetRoundedCorners(Context.DpToPx(35));
        }

        protected override void Bind()
        {
            base.Bind();

            using var bindingSet = CreateBindingSet();

            bindingSet.Bind(loginButton).For(v => v.BindClick()).To(vm => vm.LoginCommand);
            bindingSet.Bind(registerButton).For(v => v.BindClick()).To(vm => vm.RegistrationCommand);
            bindingSet.Bind(phoneEmailEditText).For(v => v.Text).To(vm => vm.PhoneOrEmail);
            bindingSet.Bind(exitImageView).For(v => v.BindClick()).To(vm => vm.LogoutCommand);
            bindingSet.Bind(exitImageView).For(v => v.BindVisible()).To(vm => vm.IsAuthorized);
            bindingSet.Bind(loginButton).For(v => v.BindHidden()).To(vm => vm.IsAuthorized);
            bindingSet.Bind(registerButton).For(v => v.BindHidden()).To(vm => vm.IsAuthorized);
            bindingSet.Bind(phoneEmailTextInputLayout).For(v => v.BindHidden()).To(vm => vm.IsAuthorized);
            bindingSet.Bind(userImageView).For(v => v.BindAdaptedUrl()).To(vm => vm.Avatar);
            bindingSet.Bind(userImageView).For(v => v.BindClick()).To(vm => vm.ChooseNewImageCommand);
            bindingSet.Bind(userImageView).For(v => v.BindVisible()).To(vm => vm.IsAuthorized);
            bindingSet.Bind(profileNameTextView).For(v => v.Text).To(vm => vm.Username);
            bindingSet.Bind(profileNameTextView).For(v => v.BindVisible()).To(vm => vm.IsAuthorized);
            bindingSet.Bind(discountButton).For(v => v.BindClick()).To(vm => vm.ShowBonusProgramCommand);
            bindingSet.Bind(discountButton).For(v => v.BindVisible()).To(vm => vm.IsAuthorized);
            bindingSet.Bind(discountButton).For(v => v.Text).To(vm => vm.Score);
            bindingSet.Bind(editProfileConstraintLayout).For(v => v.BindClick()).To(vm => vm.ShowEditProfileCommand);
            bindingSet.Bind(editProfileConstraintLayout).For(v => v.BindVisible()).To(vm => vm.IsAuthorized);
            bindingSet.Bind(myOrdersConstraintLayout).For(v => v.BindClick()).To(vm => vm.ShowMyOrdersCommand);
            bindingSet.Bind(myOrdersConstraintLayout).For(v => v.BindVisible()).To(vm => vm.IsAuthorized);
            bindingSet.Bind(feedbackConstraintLayout).For(v => v.BindClick()).To(vm => vm.ShowFeedbackCommand);
            bindingSet.Bind(feedbackConstraintLayout).For(v => v.BindVisible()).To(vm => vm.IsAuthorized);
        }
    }
}