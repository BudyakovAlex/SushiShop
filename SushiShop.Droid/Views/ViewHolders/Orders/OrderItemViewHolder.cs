using Android.Views;
using Android.Widget;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Extensions;
using MvvmCross.Platforms.Android.Binding;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using SushiShop.Core.Resources;
using SushiShop.Core.ViewModels.Orders.Items;
using SushiShop.Droid.Views.ViewHolders.Abstract;

namespace SushiShop.Droid.Views.ViewHolders.Orders
{
    public class OrderItemViewHolder : CardViewHolder<OrderItemViewModel>
    {
        private View view;
        private TextView orderNumberTextView;
        private TextView dateTextView;
        private TextView statusTextView;
        private TextView priceTextView;
        private TextView deliveryTypeTextView;
        private Button repeatButton;

        public OrderItemViewHolder(View view, IMvxAndroidBindingContext context) : base(view, context)
        {
        }

        protected override void DoInit(View view)
        {
            base.DoInit(view);

            this.view = view;
            orderNumberTextView = view.FindViewById<TextView>(Resource.Id.order_number_text_view);
            dateTextView = view.FindViewById<TextView>(Resource.Id.date_text_view);
            statusTextView = view.FindViewById<TextView>(Resource.Id.status_text_view);
            priceTextView = view.FindViewById<TextView>(Resource.Id.price_text_view);
            deliveryTypeTextView = view.FindViewById<TextView>(Resource.Id.delivery_type_text_view);

            repeatButton = view.FindViewById<Button>(Resource.Id.repeat_button);
            repeatButton.SetRoundedCorners(view.Context.DpToPx(18));
            repeatButton.Text = AppStrings.Repeat;
        }

        public override void BindData()
        {
            base.BindData();

            using var bindingSet = CreateBindingSet();

            bindingSet.Bind(repeatButton).For(v => v.BindClick()).To(vm => vm.RepeatOrderCommand);
            bindingSet.Bind(repeatButton).For(v => v.BindVisible()).To(vm => vm.CanRepeat);
            bindingSet.Bind(view).For(v => v.BindClick()).To(vm => vm.ShowDetailsCommand);
            bindingSet.Bind(orderNumberTextView).For(v => v.Text).To(vm => vm.OrderNumber);
            bindingSet.Bind(dateTextView).For(v => v.Text).To(vm => vm.OrderDateTime);
            bindingSet.Bind(statusTextView).For(v => v.Text).To(vm => vm.Status);
            bindingSet.Bind(priceTextView).For(v => v.Text).To(vm => vm.TotalPrice);
            bindingSet.Bind(deliveryTypeTextView).For(v => v.Text).To(vm => vm.ReceiveMethod);
        }
    }
}