using BuildApps.Core.Mobile.MvvmCross.UIKit.Views.ViewControllers;
using MvvmCross.Platforms.Ios.Binding;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using SushiShop.Core.ViewModels.Orders;

namespace SushiShop.Ios.Views.ViewControllers.Orders
{
    [MvxChildPresentation]
    public partial class OrderDetailsViewController : BaseViewController<OrderDetailsViewModel>
    {
        protected override void Bind()
        {
            base.Bind();

            var bindingSet = CreateBindingSet();

            bindingSet.Bind(this).For(v => v.Title).To(vm => vm.Title);
            bindingSet.Bind(OrderCompositionLabel).For(v => v.Text).To(vm => vm.ShowOrderCompositionTitle);
            bindingSet.Bind(ShowOrderCompositionView).For(v => v.BindTap()).To(vm => vm.ShowOrderCompositionCommand);
            bindingSet.Bind(OrderNumberLabel).For(v => v.Text).To(vm => vm.OrderNumber);
            bindingSet.Bind(OrderStatusLabel).For(v => v.Text).To(vm => vm.Status);
            bindingSet.Bind(OrderDateLabel).For(v => v.Text).To(vm => vm.OrderDateTime);
            bindingSet.Bind(OrderReceiveMethodLabel).For(v => v.Text).To(vm => vm.ReceiveMethod);
            bindingSet.Bind(PreferredDeliveryTimeTitleLabel).For(v => v.Text).To(vm => vm.PreferredDeliveryTimeTitle);
            bindingSet.Bind(PreferredDeliveryTimeValueLabel).For(v => v.Text).To(vm => vm.PreferredDeliveryTimeValue);
            bindingSet.Bind(ReceiveMethodTitleLabel).For(v => v.Text).To(vm => vm.ReceiveMethodTitle);
            bindingSet.Bind(ReceiveMethodValueLabel).For(v => v.Text).To(vm => vm.ReceiveMethodValue);
            bindingSet.Bind(AddressTitleLabel).For(v => v.Text).To(vm => vm.DeliveryAddressTitle);
            bindingSet.Bind(AddressValueLabel).For(v => v.Text).To(vm => vm.DeliveryAddress);
            bindingSet.Bind(PhoneNumberLabel).For(v => v.Text).To(vm => vm.Phones);
            bindingSet.Bind(TimeLabel).For(v => v.Text).To(vm => vm.WorkingTime);
            bindingSet.Bind(PriceTitleLabel).For(v => v.Text).To(vm => vm.PriceTitle);
            bindingSet.Bind(PriceValueLabel).For(v => v.Text).To(vm => vm.PriceValue);
            bindingSet.Bind(ReceiveTitleLabel).For(v => v.Text).To(vm => vm.ReceiveTitle);
            bindingSet.Bind(ReceiveValueLabel).For(v => v.Text).To(vm => vm.ReceiveValue);
            bindingSet.Bind(TotalPriceTitleLabel).For(v => v.Text).To(vm => vm.TotalPriceTitle);
            bindingSet.Bind(TotalPriceValueLabel).For(v => v.Text).To(vm => vm.TotalPriceValue);
            bindingSet.Bind(RepeatOrderButton).For(v => v.BindVisible()).To(vm => vm.CanRepeat);
            bindingSet.Bind(RepeatOrderButton).For(v => v.BindTitle()).To(vm => vm.RepeatOrderTitle);
            bindingSet.Bind(RepeatOrderButton).For(v => v.BindTouchUpInside()).To(vm => vm.RepeatOrderCommand);

            bindingSet.Apply();
        }
    }
}
