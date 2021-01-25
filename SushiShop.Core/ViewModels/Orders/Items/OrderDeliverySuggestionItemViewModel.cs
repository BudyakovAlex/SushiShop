using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;
using SushiShop.Core.Data.Models.Orders;

namespace SushiShop.Core.ViewModels.Orders.Items
{
    public class OrderDeliverySuggestionItemViewModel : BaseViewModel
    {
        public OrderDeliverySuggestionItemViewModel(OrderDeliveryRequest orderDelivery)
        {
            OrderDelivery = orderDelivery;
        }

        public OrderDeliveryRequest OrderDelivery { get; }

        public string? Address => OrderDelivery?.Address;
    }
}