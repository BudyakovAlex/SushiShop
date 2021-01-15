using BuildApps.Core.Mobile.MvvmCross.UIKit.Views.ViewControllers;
using SushiShop.Core.ViewModels.Orders;

namespace SushiShop.Ios.Views.ViewControllers.Orders
{
    public partial class OrderRegistrationViewController : BaseViewController<OrderRegistrationViewModel>
    {
        protected override void InitStylesAndContent()
        {
            base.InitStylesAndContent();

            scrollableTabsView.IsFixedTabs = true;
        }

        protected override void Bind()
        {
            base.Bind();

            var bindingSet = CreateBindingSet();

            bindingSet.Bind(scrollableTabsView).For(v => v.Items).To(vm => vm.TabsTitles);

            bindingSet.Apply();
        }
    }
}

