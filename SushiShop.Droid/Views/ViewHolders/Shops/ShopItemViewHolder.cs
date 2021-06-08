using Android.Graphics;
using Android.Text;
using Android.Text.Method;
using Android.Text.Style;
using Android.Views;
using Android.Widget;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Extensions;
using MvvmCross.Platforms.Android.Binding;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using SushiShop.Core.ViewModels.Shops.Items;
using SushiShop.Droid.Views.Spans;
using SushiShop.Droid.Views.ViewHolders.Abstract;

namespace SushiShop.Droid.Views.ViewHolders.Shops
{
    public class ShopItemViewHolder : CardViewHolder<ShopItemViewModel>
    {
        private TextView titleTextView;
        private TextView workingHoursTextView;
        private TextView phoneTextView;
        private ImageView showNearestMetroImageView;
        private ImageView showOnMapImageView;

        public ShopItemViewHolder(View view, IMvxAndroidBindingContext context) : base(view, context)
        {
        }

        public string[] Phones
        {
            set => SetPhonesSpannableString(value);
        }

        public int StartClickablePrivacyPolicy { get; private set; }

        protected override void DoInit(View view)
        {
            base.DoInit(view);

            titleTextView = view.FindViewById<TextView>(Resource.Id.title_text_view);
            workingHoursTextView = view.FindViewById<TextView>(Resource.Id.working_hours_text_view);
            phoneTextView = view.FindViewById<TextView>(Resource.Id.phone_text_view);

            showNearestMetroImageView = view.FindViewById<ImageView>(Resource.Id.show_nearest_metro_image_view);
            showOnMapImageView = view.FindViewById<ImageView>(Resource.Id.show_on_map_image_view);

            var cornerRadius = view.Context.DpToPx(24);
            showNearestMetroImageView.SetRoundedCorners(cornerRadius);
            showOnMapImageView.SetRoundedCorners(cornerRadius);
        }

        public override void BindData()
        {
            base.BindData();

            using var bindingSet = CreateBindingSet();

            bindingSet.Bind(titleTextView).For(v => v.Text).To(vm => vm.LongTitle);
            bindingSet.Bind(this).For(nameof(Phones)).To(vm => vm.Phones);
            bindingSet.Bind(workingHoursTextView).For(v => v.Text).To(vm => vm.WorkingTime);
            bindingSet.Bind(showNearestMetroImageView).For(v => v.BindClick()).To(vm => vm.ShowNearestMetroCommand);
            bindingSet.Bind(showNearestMetroImageView).For(v => v.BindVisible()).To(vm => vm.HasNearestMetro);
            bindingSet.Bind(showOnMapImageView).For(v => v.BindClick()).To(vm => vm.GoToMapCommand);

            //TODO: uncomment when will be ready on UI
            //bindingSet.Bind(PickupThereButton).For(v => v.BindClick()).To(vm => vm.ConfirmSelectionCommand);
            //bindingSet.Bind(PickupThereButton).For(v => v.BindVisible()).To(vm => vm.IsSelectionMode);
        }

        private void SetPhonesSpannableString(string[] phones)
        {
            if (phones is null)
            {
                return;
            }

            var joinedText = string.Join(", ", phones);
            var spannableString = new SpannableString(joinedText);

            foreach (var phone in phones)
            {
                var startIndex = joinedText.IndexOf(phone);
                var clickableSpan = new LinkSpan(OnLinkSpanClick, phone, false);
                spannableString.SetSpan(clickableSpan, startIndex, startIndex + phone.Length, SpanTypes.ExclusiveExclusive);

                var foregroundColorSpan = new ForegroundColorSpan(new Color(phoneTextView.CurrentTextColor));
                spannableString.SetSpan(foregroundColorSpan, startIndex, startIndex + phone.Length, SpanTypes.ExclusiveExclusive);
            }

            phoneTextView.MovementMethod = LinkMovementMethod.Instance;
            phoneTextView.SetHighlightColor(Color.Transparent);
            phoneTextView.SetText(spannableString, TextView.BufferType.Spannable);
        }

        private void OnLinkSpanClick(string phone)
        {
            ViewModel?.CallCommand.Execute(phone);
        }
    }
}