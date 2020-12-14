using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract.Items;
using SushiShop.Core.NavigationParameters;
using SushiShop.Core.ViewModels.Orders.Items;
using System;
using System.Linq;

namespace SushiShop.Core.ViewModels.Orders
{
    public class OrderCompositionViewModel : BaseItemsPageViewModel<OrderProductItemViewModel, OrderCompositionNavigationParameters>
    {
        public override void Prepare(OrderCompositionNavigationParameters parameter)
        {
            var viewModels = parameter.Products?.Select(product => new OrderProductItemViewModel(product, parameter.Currency)).ToArray()
                ?? Array.Empty<OrderProductItemViewModel>();

            Items.AddRange(viewModels);
        }
    }
}