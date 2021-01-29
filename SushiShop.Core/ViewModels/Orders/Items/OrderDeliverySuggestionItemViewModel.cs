using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;
using SushiShop.Core.Data.Models.Cities;
using System.Windows.Input;

namespace SushiShop.Core.ViewModels.Orders.Items
{
    public class OrderDeliverySuggestionItemViewModel : BaseViewModel
    {
        public OrderDeliverySuggestionItemViewModel(AddressSuggestion suggestion, ICommand selectCommand)
        {
            Suggestion = suggestion;
            SelectCommand = selectCommand;
        }

        public ICommand SelectCommand { get; }

        public AddressSuggestion Suggestion { get; }

        public string? Address => Suggestion?.Address;

        public bool IsDeliveryAvailable => Suggestion?.ShopId.HasValue ?? false;
    }
}