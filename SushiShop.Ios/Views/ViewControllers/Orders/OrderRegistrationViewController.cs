using BuildApps.Core.Mobile.MvvmCross.UIKit.Views.ViewControllers;
using MvvmCross.Platforms.Ios.Binding;
using SushiShop.Core.ViewModels.Orders;

namespace SushiShop.Ios.Views.ViewControllers.Orders
{
    public partial class OrderRegistrationViewController : BaseViewController<OrderRegistrationViewModel>
    {
        protected override void InitStylesAndContent()
        {
            base.InitStylesAndContent();

            scrollableTabsView.IsFixedTabs = true;
            DeliveryView.Hidden = true;
        }

        protected override void Bind()
        {
            base.Bind();

            var bindingSet = CreateBindingSet();

            bindingSet.Bind(scrollableTabsView).For(v => v.Items).To(vm => vm.TabsTitles);
            bindingSet.Bind(scrollableTabsView).For(v => v.SelectedIndex).To(vm => vm.SelectedIndex);
            // bindingSet.Bind(DeliveryView).For(v => v.BindVisible()).To(vm => vm.IsDelivery && vm.DeliveryOrderSectionViewModel.)

            bindingSet.Apply();
        }
    }
}

