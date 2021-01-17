using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;
using SushiShop.Core.Data.Models.Orders;

namespace SushiShop.Core.ViewModels.Orders.Items
{
    public class OrderDeliverySuggestionItemViewModel : BaseViewModel
    {
        public OrderDeliverySuggestionItemViewModel(OrderDelivery orderDelivery)
        {
            OrderDelivery = orderDelivery;
        }

        public OrderDelivery OrderDelivery { get; }

        public string? Address => OrderDelivery?.Address;
    }
}