using BuildApps.Core.Mobile.MvvmCross.Commands;
using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;
using SushiShop.Core.Data.Models.Cities;
using SushiShop.Core.Resources;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SushiShop.Core.ViewModels.Orders.Items
{
    public class OrderDeliverySuggestionItemViewModel : BaseViewModel
    {
        private readonly Func<OrderDeliverySuggestionItemViewModel, Task> selectFunction;

        public OrderDeliverySuggestionItemViewModel(AddressSuggestion suggestion, Func<OrderDeliverySuggestionItemViewModel, Task> selectFunction)
        {
            Suggestion = suggestion;
            this.selectFunction = selectFunction;

            SelectCommand = new SafeAsyncCommand(ExecutionStateWrapper, SelectAsync);
        }

        public ICommand SelectCommand { get; }

        public AddressSuggestion Suggestion { get; }

        public string? Address => Suggestion?.Address;

        public string DeliveryPrice => $"{AppStrings.PriceOfDelivery}: {Suggestion?.DeliveryPrice} ";

        public bool IsDeliveryAvailable => Suggestion?.ShopId.HasValue ?? false;

        public double? Longitude => Suggestion?.Coordinates?.Longitude;

        public double? Latitude => Suggestion?.Coordinates?.Latitude;

        private Task SelectAsync()
        {
            return selectFunction?.Invoke(this) ?? Task.CompletedTask;
        }
    }
}