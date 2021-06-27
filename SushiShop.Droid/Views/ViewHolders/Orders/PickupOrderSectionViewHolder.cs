using Android.Graphics;
using Android.Text;
using Android.Text.Method;
using Android.Text.Style;
using Android.Views;
using Android.Widget;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Extensions;
using Google.Android.Material.TextField;
using MvvmCross.Platforms.Android.Binding;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using SushiShop.Core.Converters;
using SushiShop.Core.Data.Enums;
using SushiShop.Core.Resources;
using SushiShop.Core.ViewModels.Orders.Sections;
using SushiShop.Droid.Views.Controls;
using SushiShop.Droid.Views.Spans;
using SushiShop.Droid.Views.ViewHolders.Abstract;

namespace SushiShop.Droid.Views.ViewHolders.Orders
{
    public class PickupOrderSectionViewHolder : CardViewHolder<PickupOrderSectionViewModel>
    {
        private View selectLocationContainerView;
        private View locationContainerView;
        private View selectTimeContainerView;
        private TextView addressTextView;
        private TextView phoneTextView;
        private TextView workingHoursTextView;
        private TextView timeTextView;
        private TextInputEditText nameEditText;
        private TextInputEditText phoneEditText;
        private StepperView devicesStepperView;
        private TextInputEditText commentsEditText;
        private View cashContainerView;
        private View onlineContainerView;
        private ImageView cashImageView;
        private ImageView onlineImageView;
        private TextView productsTextView;
        private TextView discountByPromocodeTextView;
        private TextView discountByCardTextView;
        private TextView appliedScoresTextView;
        private TextView totalTextView;
        private Button confirmButton;
        private TextView privacyTextView;
        private TextView discountByPromocodeTitleTextView;
        private TextView discountByCardTitleTextView;
        private TextView appliedScoresTitleTextView;

        public PickupOrderSectionViewHolder(View view, IMvxAndroidBindingContext context) : base(view, context)
        {
        }

        protected override void DoInit(View view)
        {
            base.DoInit(view);

            selectLocationContainerView = view.FindViewById<View>(Resource.Id.select_location_container_view);
            locationContainerView = view.FindViewById<View>(Resource.Id.location_container_view);
            selectTimeContainerView = view.FindViewById<View>(Resource.Id.select_time_container_view);
            addressTextView = view.FindViewById<TextView>(Resource.Id.address_text_view);
            phoneTextView = view.FindViewById<TextView>(Resource.Id.phone_text_view);
            workingHoursTextView = view.FindViewById<TextView>(Resource.Id.working_hours_text_view);
            timeTextView = view.FindViewById<TextView>(Resource.Id.time_text_view);

            nameEditText = view.FindViewById<TextInputEditText>(Resource.Id.name_edit_text);
            phoneEditText = view.FindViewById<TextInputEditText>(Resource.Id.phone_edit_text);
            devicesStepperView = view.FindViewById<StepperView>(Resource.Id.devices_stepper_view);
            commentsEditText = view.FindViewById<TextInputEditText>(Resource.Id.comments_edit_text);
            cashContainerView = view.FindViewById<View>(Resource.Id.cash_container_view);
            onlineContainerView = view.FindViewById<View>(Resource.Id.online_container_view);
            cashImageView = view.FindViewById<ImageView>(Resource.Id.cash_image_view);
            onlineImageView = view.FindViewById<ImageView>(Resource.Id.online_image_view);

            productsTextView = view.FindViewById<TextView>(Resource.Id.products_text_view);
            discountByPromocodeTextView = view.FindViewById<TextView>(Resource.Id.discount_by_promocode_text_view);
            discountByCardTextView = view.FindViewById<TextView>(Resource.Id.discount_by_card_text_view);
            appliedScoresTextView = view.FindViewById<TextView>(Resource.Id.applied_scores_text_view);
            totalTextView = view.FindViewById<TextView>(Resource.Id.total_text_view);
            confirmButton = view.FindViewById<Button>(Resource.Id.confirm_button);
            confirmButton.SetRoundedCorners(view.Context.DpToPx(25));
            confirmButton.Text = AppStrings.CheckoutOrder;

            view.FindViewById<TextView>(Resource.Id.pickup_text_view).Text = AppStrings.Free;
            view.FindViewById<TextInputLayout>(Resource.Id.name_text_input_layout).Hint = $"{AppStrings.Name}*";
            view.FindViewById<TextInputLayout>(Resource.Id.phone_text_input_layout).Hint = $"{AppStrings.Phone}*";
            view.FindViewById<TextInputLayout>(Resource.Id.comments_text_input_layout).Hint = AppStrings.Comment;

            view.FindViewById<TextView>(Resource.Id.address_title_text_view).Text = AppStrings.ShopAddress;
            view.FindViewById<TextView>(Resource.Id.pick_shop_text_view).Text = AppStrings.PickShop;
            view.FindViewById<TextView>(Resource.Id.time_title_text_view).Text = AppStrings.OrderTime;
            view.FindViewById<TextView>(Resource.Id.pick_time_title_text_view).Text = AppStrings.PickAt;
            view.FindViewById<TextView>(Resource.Id.personal_data_title_text_view).Text = AppStrings.PersonalData;
            view.FindViewById<TextView>(Resource.Id.preferred_info_title_text_view).Text = AppStrings.Preferences;
            view.FindViewById<TextView>(Resource.Id.devices_title_text_view).Text = AppStrings.DevicesCount;
            view.FindViewById<TextView>(Resource.Id.payment_method_title_text_view).Text = AppStrings.PaymentMethod;
            view.FindViewById<TextView>(Resource.Id.cash_text_view).Text = AppStrings.OnPickup;
            view.FindViewById<TextView>(Resource.Id.cash_description_text_view).Text = AppStrings.CashPaymentDescription;
            view.FindViewById<TextView>(Resource.Id.online_text_view).Text = AppStrings.Online;
            view.FindViewById<TextView>(Resource.Id.online_description_text_view).Text = AppStrings.OnlinePaymentDescription;
            view.FindViewById<TextView>(Resource.Id.total_title_text_view).Text = AppStrings.Total;

            view.FindViewById<TextView>(Resource.Id.products_title_text_view).Text = $"{AppStrings.Products}:";
            view.FindViewById<TextView>(Resource.Id.pickup_title_text_view).Text = $"{AppStrings.Pickup}:";
            discountByPromocodeTitleTextView = view.FindViewById<TextView>(Resource.Id.discount_by_promocode_title_text_view);
            discountByPromocodeTitleTextView.Text = AppStrings.DiscountByPromocode;
            discountByCardTitleTextView = view.FindViewById<TextView>(Resource.Id.discount_by_card_title_text_view);
            discountByCardTitleTextView.Text = AppStrings.DiscountByCard;
            appliedScoresTitleTextView = view.FindViewById<TextView>(Resource.Id.applied_scores_title_text_view);
            appliedScoresTitleTextView.Text = AppStrings.AppliedScores;

            view.FindViewById<TextView>(Resource.Id.total_title_text_view).Text = $"{AppStrings.Total}:";
            appliedScoresTitleTextView.Visibility = ViewStates.Gone;
            appliedScoresTextView.Visibility = ViewStates.Gone;

            SetupPrivacyTextView(view);
        }

        public override void BindData()
        {
            base.BindData();

            using var bindingSet = CreateBindingSet();

            bindingSet.Bind(selectLocationContainerView).For(v => v.BindClick()).To(vm => vm.SelectAddressCommand);
            bindingSet.Bind(locationContainerView).For(v => v.BindVisible()).To(vm => vm.ShopAddress)
                      .WithConversion<StringToBoolConverter>();
            bindingSet.Bind(selectTimeContainerView).For(v => v.BindVisible()).To(vm => vm.ShopAddress)
                      .WithConversion<StringToBoolConverter>();
            bindingSet.Bind(addressTextView).For(v => v.Text).To(vm => vm.ShopAddress);
            bindingSet.Bind(phoneTextView).For(v => v.Text).To(vm => vm.ShopPhone);
            bindingSet.Bind(workingHoursTextView).For(v => v.Text).To(vm => vm.ShopTimeWorking);
            bindingSet.Bind(selectTimeContainerView).For(v => v.BindClick()).To(vm => vm.SelectReceiveDateTimeCommand);
            bindingSet.Bind(timeTextView).For(v => v.Text).To(vm => vm.ReceiveDateTimePresentation);
            bindingSet.Bind(nameEditText).For(v => v.Text).To(vm => vm.Name).TwoWay();
            bindingSet.Bind(phoneEditText).For(v => v.Text).To(vm => vm.Phone).TwoWay();
            bindingSet.Bind(devicesStepperView).For(v => v.DataContext).To(vm => vm.СutleryStepperViewModel);
            bindingSet.Bind(commentsEditText).For(v => v.Text).To(vm => vm.Comments).TwoWay();
            bindingSet.Bind(cashImageView).For(v => v.BindVisible()).To(vm => vm.PaymentMethod)
                      .WithConversion<PaymentMethodToVisibleConverter>(PaymentMethod.OnPoint);
            bindingSet.Bind(onlineImageView).For(v => v.BindVisible()).To(vm => vm.PaymentMethod)
                      .WithConversion<PaymentMethodToVisibleConverter>(PaymentMethod.Online);
            bindingSet.Bind(cashContainerView).For(v => v.BindClick()).To(vm => vm.ChangePaymentMethodCommand)
                      .CommandParameter(PaymentMethod.OnPoint);
            bindingSet.Bind(onlineContainerView).For(v => v.BindClick()).To(vm => vm.ChangePaymentMethodCommand)
                      .CommandParameter(PaymentMethod.Online);

            bindingSet.Bind(productsTextView).For(v => v.Text).To(vm => vm.ProductsPrice);
            bindingSet.Bind(discountByPromocodeTextView).For(v => v.Text).To(vm => vm.DiscountByPromocode);
            bindingSet.Bind(discountByCardTextView).For(v => v.Text).To(vm => vm.DiscountByCardPresentation);
            bindingSet.Bind(appliedScoresTextView).For(v => v.Text).To(vm => vm.ScoresToApply);

            bindingSet.Bind(totalTextView).For(v => v.Text).To(vm => vm.PriceToPay);
            bindingSet.Bind(confirmButton).For(v => v.BindClick()).To(vm => vm.ConfirmOrderCommand);
        }

        private void SetupPrivacyTextView(View view)
        {
            var spannableString = new SpannableString(AppStrings.PrivacyPolicyConfirmOrder);
            var policyLinkSpan = new LinkSpan(_ => ViewModel?.ShowPrivacyPolicyCommand?.Execute(null), isUnderlineText: true);
            var termsOfThePublicOfferLinkSpan = new LinkSpan(_ => ViewModel?.ShowPublicOfferCommand?.Execute(null), isUnderlineText: true);
            var userAgreementLinkSpan = new LinkSpan(_ => ViewModel?.ShowUserAgreementCommand?.Execute(null), isUnderlineText: true);

            spannableString.SetSpan(policyLinkSpan, 108, 134, SpanTypes.ExclusiveExclusive);
            spannableString.SetSpan(termsOfThePublicOfferLinkSpan, 136, 164, SpanTypes.ExclusiveExclusive);
            spannableString.SetSpan(userAgreementLinkSpan, 167, 194, SpanTypes.ExclusiveExclusive);

            var foregroundColorArgb = view.Context.GetColor(Resource.Color.gray4);
            var foregroundSpan = new ForegroundColorSpan(new Color(foregroundColorArgb));
            spannableString.SetSpan(new ForegroundColorSpan(new Color(foregroundColorArgb)), 108, 134, SpanTypes.ExclusiveExclusive);
            spannableString.SetSpan(new ForegroundColorSpan(new Color(foregroundColorArgb)), 136, 164, SpanTypes.ExclusiveExclusive);
            spannableString.SetSpan(new ForegroundColorSpan(new Color(foregroundColorArgb)), 167, 194, SpanTypes.ExclusiveExclusive);

            privacyTextView = view.FindViewById<TextView>(Resource.Id.privacy_text_view);
            privacyTextView.SetText(spannableString, TextView.BufferType.Spannable);
            privacyTextView.MovementMethod = LinkMovementMethod.Instance;
            privacyTextView.SetHighlightColor(Color.Transparent);
        }
    }
}