using Android.OS;
using Android.Views;
using Android.Widget;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Extensions;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Views.Fragments;
using MvvmCross.Platforms.Android.Binding;
using SushiShop.Core.ViewModels.Orders;
using SushiShop.Droid.Presenter.Attributes;

namespace SushiShop.Droid.Views.Fragments.Orders
{

    [NestedFragmentPresentation(FragmentContentId = Resource.Id.container_view)]
    public class OrderDetailsFragment : BaseFragment<OrderDetailsViewModel>
    {
        private Toolbar toolbar;
        private View loadingOverlayView;
        private View addressContainerView;
        private View compositionContainerView;
        private TextView numberTextView;
        private TextView DateTextView;
        private TextView preferredDeliveryTimeTextView;
        private TextView deliveryTextView;
        private TextView cityTextView;
        private TextView contactsTextView;
        private TextView workingTimeTextView;
        private TextView productsPriceTextView;
        private TextView deliveryPriceTextView;
        private TextView totalPriceTextView;
        private TextView addressTitleTextView;
        private TextView deliveryTitleTextView;
        private TextView orderCompositionTitleTextView;
        private TextView orderPreferredDeliveryTimeTitleTextView;
        private TextView productsPriceTitleTextView;
        private TextView deliveryPriceTitleTextView;
        private TextView totalPriceTitleTextView;
        private Button repeatButton;
        private TextView statusTextView;
        private TextView deliveryTypeTextView;

        public OrderDetailsFragment() : base(Resource.Layout.fragment_order_details)
        {
        }

        protected override void InitializeViewPoroperties(View view, Bundle savedInstanceState)
        {
            base.InitializeViewPoroperties(view, savedInstanceState);

            toolbar = view.FindViewById<Toolbar>(Resource.Id.toolbar);
            loadingOverlayView = view.FindViewById<View>(Resource.Id.loading_overlay_view);

            addressContainerView = view.FindViewById<View>(Resource.Id.address_container_view);
            compositionContainerView = view.FindViewById<View>(Resource.Id.order_composition_container_view);
            numberTextView = view.FindViewById<TextView>(Resource.Id.order_number_text_view);
            DateTextView = view.FindViewById<TextView>(Resource.Id.date_text_view);
            statusTextView = view.FindViewById<TextView>(Resource.Id.status_text_view);
            deliveryTypeTextView = view.FindViewById<TextView>(Resource.Id.delivery_type_text_view);
            preferredDeliveryTimeTextView = view.FindViewById<TextView>(Resource.Id.preferred_delivery_time_text_view);
            deliveryTextView = view.FindViewById<TextView>(Resource.Id.delivery_text_view);
            cityTextView = view.FindViewById<TextView>(Resource.Id.city_text_view);
            contactsTextView = view.FindViewById<TextView>(Resource.Id.contacts_text_view);
            workingTimeTextView = view.FindViewById<TextView>(Resource.Id.working_time_text_view);

            productsPriceTextView = view.FindViewById<TextView>(Resource.Id.products_price_text_view);
            deliveryPriceTextView = view.FindViewById<TextView>(Resource.Id.delivery_price_text_view);
            totalPriceTextView = view.FindViewById<TextView>(Resource.Id.total_price_text_view);

            addressTitleTextView = view.FindViewById<TextView>(Resource.Id.address_title_text_view);
            deliveryTitleTextView = view.FindViewById<TextView>(Resource.Id.delivery_title_text_view);
            orderCompositionTitleTextView = view.FindViewById<TextView>(Resource.Id.order_composition_title_text_view);
            orderPreferredDeliveryTimeTitleTextView = view.FindViewById<TextView>(Resource.Id.preferred_delivery_time_title_text_view);

            productsPriceTitleTextView = view.FindViewById<TextView>(Resource.Id.products_price_title_text_view);
            deliveryPriceTitleTextView = view.FindViewById<TextView>(Resource.Id.delivery_price_title_text_view);
            totalPriceTitleTextView = view.FindViewById<TextView>(Resource.Id.total_price_title_text_view);

            repeatButton = view.FindViewById<Button>(Resource.Id.repeat_button);
            repeatButton.SetRoundedCorners(Context.DpToPx(25));
        }

        protected override void Bind()
        {
            base.Bind();

            using var bindingSet = CreateBindingSet();

            bindingSet.Bind(toolbar).For(v => v.Title).To(v => v.Title);

            bindingSet.Bind(orderCompositionTitleTextView).For(v => v.Text).To(vm => vm.ShowOrderCompositionTitle);
            bindingSet.Bind(compositionContainerView).For(v => v.BindClick()).To(vm => vm.ShowOrderCompositionCommand);
            bindingSet.Bind(numberTextView).For(v => v.Text).To(vm => vm.OrderNumber);
            bindingSet.Bind(statusTextView).For(v => v.Text).To(vm => vm.Status);
            bindingSet.Bind(DateTextView).For(v => v.Text).To(vm => vm.OrderDateTime);
            bindingSet.Bind(deliveryTypeTextView).For(v => v.Text).To(vm => vm.ReceiveMethod);
            bindingSet.Bind(orderPreferredDeliveryTimeTitleTextView).For(v => v.Text).To(vm => vm.PreferredDeliveryTimeTitle);
            bindingSet.Bind(preferredDeliveryTimeTextView).For(v => v.Text).To(vm => vm.PreferredDeliveryTimeValue);
            bindingSet.Bind(deliveryTitleTextView).For(v => v.Text).To(vm => vm.ReceiveMethodTitle);
            bindingSet.Bind(deliveryTextView).For(v => v.Text).To(vm => vm.ReceiveMethodValue);
            bindingSet.Bind(addressContainerView).For(v => v.BindVisible()).To(vm => vm.HasDeliveryAddress);
            bindingSet.Bind(addressTitleTextView).For(v => v.Text).To(vm => vm.DeliveryAddressTitle);
            bindingSet.Bind(cityTextView).For(v => v.Text).To(vm => vm.DeliveryAddress);
            bindingSet.Bind(contactsTextView).For(v => v.Text).To(vm => vm.Phones);
            bindingSet.Bind(workingTimeTextView).For(v => v.Text).To(vm => vm.WorkingTime);
            bindingSet.Bind(productsPriceTitleTextView).For(v => v.Text).To(vm => vm.PriceTitle);
            bindingSet.Bind(productsPriceTextView).For(v => v.Text).To(vm => vm.PriceValue);
            bindingSet.Bind(deliveryPriceTitleTextView).For(v => v.Text).To(vm => vm.ReceiveTitle);
            bindingSet.Bind(deliveryPriceTextView).For(v => v.Text).To(vm => vm.ReceiveValue);
            bindingSet.Bind(totalPriceTitleTextView).For(v => v.Text).To(vm => vm.TotalPriceTitle);
            bindingSet.Bind(totalPriceTextView).For(v => v.Text).To(vm => vm.TotalPriceValue);
            bindingSet.Bind(repeatButton).For(v => v.BindVisible()).To(vm => vm.CanRepeat);
            bindingSet.Bind(repeatButton).For(v => v.Text).To(vm => vm.RepeatOrderTitle);
            bindingSet.Bind(repeatButton).For(v => v.BindClick()).To(vm => vm.RepeatOrderCommand);
            bindingSet.Bind(loadingOverlayView).For(v => v.BindVisible()).To(vm => vm.IsLoading);
        }
    }
}