using Android.Graphics;
using Android.Text;
using Android.Text.Method;
using Android.Views;
using Android.Widget;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Extensions;
using Google.Android.Material.TextField;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using SushiShop.Core.Resources;
using SushiShop.Core.ViewModels.Orders.Sections;
using SushiShop.Droid.Views.Controls;
using SushiShop.Droid.Views.Spans;
using SushiShop.Droid.Views.ViewHolders.Abstract;

namespace SushiShop.Droid.Views.ViewHolders.Orders
{
    public class DeliveryOrderSectionViewHolder : CardViewHolder<DeliveryOrderSectionViewModel>
    {
        private View _selectLocationContainerView;
        private View _locationContainerView;
        private TextView _addressTextView;
        private TextView _deliveryPriceTextView;
        private TextView _timeTextView;
        private TextInputEditText _flatEditText;
        private TextInputEditText _entranceEditText;
        private TextInputEditText _intercomEditText;
        private TextInputEditText _floorEditText;
        private TextInputEditText _nameEditText;
        private TextInputEditText _phoneEditText;
        private StepperView _devicesStepperView;
        private TextInputEditText _commentsEditText;
        private View _cashContainerView;
        private View _onlineContainerView;
        private ImageView _cashImageView;
        private ImageView _onlineImageView;
        private TextView _productsTextView;
        private TextView _pickupTextView;
        private TextView _discountByPromocodeTextView;
        private TextView _discountByCardTextView;
        private TextView _appliedScoresTextView;
        private TextView _totalTextView;
        private Button _confirmButton;
        private TextView _privacyTextView;

        public DeliveryOrderSectionViewHolder(View view, IMvxAndroidBindingContext context) : base(view, context)
        {
        }

        protected override void DoInit(View view)
        {
            base.DoInit(view);

            _selectLocationContainerView = view.FindViewById<View>(Resource.Id.select_location_container_view);
            _locationContainerView = view.FindViewById<View>(Resource.Id.location_container_view);
            _addressTextView = view.FindViewById<TextView>(Resource.Id.address_text_view);
            _deliveryPriceTextView = view.FindViewById<TextView>(Resource.Id.delivery_price_text_view);
            _timeTextView = view.FindViewById<TextView>(Resource.Id.time_text_view);

            _flatEditText = view.FindViewById<TextInputEditText>(Resource.Id.flat_edit_text);
            _entranceEditText = view.FindViewById<TextInputEditText>(Resource.Id.entrance_edit_text);
            _intercomEditText = view.FindViewById<TextInputEditText>(Resource.Id.intercom_edit_text);
            _floorEditText = view.FindViewById<TextInputEditText>(Resource.Id.floor_edit_text);
            _nameEditText = view.FindViewById<TextInputEditText>(Resource.Id.name_edit_text);
            _phoneEditText = view.FindViewById<TextInputEditText>(Resource.Id.phone_edit_text);
            _devicesStepperView = view.FindViewById<StepperView>(Resource.Id.devices_stepper_view);
            _commentsEditText = view.FindViewById<TextInputEditText>(Resource.Id.comments_edit_text);
            _cashContainerView = view.FindViewById<View>(Resource.Id.cash_container_view);
            _onlineContainerView = view.FindViewById<View>(Resource.Id.online_container_view);
            _cashImageView = view.FindViewById<ImageView>(Resource.Id.cash_image_view);
            _onlineImageView = view.FindViewById<ImageView>(Resource.Id.online_image_view);

            _productsTextView = view.FindViewById<TextView>(Resource.Id.products_text_view);
            _pickupTextView = view.FindViewById<TextView>(Resource.Id.pickup_text_view);
            _discountByPromocodeTextView = view.FindViewById<TextView>(Resource.Id.discount_by_promocode_text_view);
            _discountByCardTextView = view.FindViewById<TextView>(Resource.Id.discount_by_card_text_view);
            _appliedScoresTextView = view.FindViewById<TextView>(Resource.Id.applied_scores_text_view);
            _totalTextView = view.FindViewById<TextView>(Resource.Id.total_title_text_view);
            _confirmButton = view.FindViewById<Button>(Resource.Id.confirm_button);
            _confirmButton.SetRoundedCorners(view.Context.DpToPx(25));
            _confirmButton.Text = AppStrings.CheckoutOrder;

            view.FindViewById<TextInputLayout>(Resource.Id.flat_text_input_layout).Hint = AppStrings.Apartment;
            view.FindViewById<TextInputLayout>(Resource.Id.entrance_text_input_layout).Hint = AppStrings.Entrance;
            view.FindViewById<TextInputLayout>(Resource.Id.intercom_text_input_layout).Hint = AppStrings.Intercom;
            view.FindViewById<TextInputLayout>(Resource.Id.floor_text_input_layout).Hint = AppStrings.Floor;
            view.FindViewById<TextInputLayout>(Resource.Id.name_text_input_layout).Hint = $"{AppStrings.Name}*";
            view.FindViewById<TextInputLayout>(Resource.Id.phone_text_input_layout).Hint = $"{AppStrings.Phone}*";
            view.FindViewById<TextInputLayout>(Resource.Id.comments_text_input_layout).Hint = AppStrings.Comment;

            view.FindViewById<TextView>(Resource.Id.address_title_text_view).Text = AppStrings.ShopAddress;
            view.FindViewById<TextView>(Resource.Id.pick_delivery_location_text_view).Text = AppStrings.PickDeliveryAddress;
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

            view.FindViewById<TextView>(Resource.Id.products_title_text_view).Text = $"{AppStrings.Products}:";
            view.FindViewById<TextView>(Resource.Id.pickup_title_text_view).Text = $"{AppStrings.Pickup};";
            view.FindViewById<TextView>(Resource.Id.discount_by_promocode_title_text_view).Text = AppStrings.DiscountByPromocode;
            view.FindViewById<TextView>(Resource.Id.discount_by_card_title_text_view).Text = AppStrings.DiscountByCard;
            view.FindViewById<TextView>(Resource.Id.applied_scores_title_text_view).Text = AppStrings.AppliedScores;
            view.FindViewById<TextView>(Resource.Id.total_title_text_view).Text = $"{AppStrings.Total}:";

            SetupPrivacyTextView(view);
        }

        public override void BindData()
        {
            base.BindData();
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

            _privacyTextView = view.FindViewById<TextView>(Resource.Id.privacy_text_view);
            _privacyTextView.SetText(spannableString, TextView.BufferType.Spannable);
            _privacyTextView.MovementMethod = LinkMovementMethod.Instance;
            _privacyTextView.SetHighlightColor(Color.Transparent);
        }
    }
}
