using BuildApps.Core.Mobile.Common.Extensions;
using BuildApps.Core.Mobile.MvvmCross.Commands;
using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract.Items;
using MvvmCross.ViewModels;
using SushiShop.Core.Data.Models.Cities;
using SushiShop.Core.Data.Models.Common;
using SushiShop.Core.Managers.Cities;
using SushiShop.Core.Managers.Shops;
using SushiShop.Core.Plugins;
using SushiShop.Core.Providers;
using SushiShop.Core.ViewModels.Orders.Items;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SushiShop.Core.ViewModels.Orders
{
    public class SelectOrderDeliveryAddressViewModel : BaseItemsPageViewModelResult<DeliveryZoneItemViewModel, AddressSuggestion>
    {
        private const int SearchMillisecondsDelay = 1500;

        private readonly IShopsManager shopsManager;
        private readonly ICitiesManager citiesManager;
        private readonly IUserSession userSession;
        private readonly IDialog dialog;
        private readonly ICommand selectCommand;

        private CancellationTokenSource? cancellationTokenSource;
        private City? _city;

        public SelectOrderDeliveryAddressViewModel(
            IShopsManager shopsManager,
            ICitiesManager citiesManager,
            IUserSession userSession,
            IDialog dialog)
        {
            this.shopsManager = shopsManager;
            this.citiesManager = citiesManager;
            this.userSession = userSession;
            this.dialog = dialog;
            Suggestions = new MvxObservableCollection<OrderDeliverySuggestionItemViewModel>();

            ConfirmAddress = new SafeAsyncCommand(ExecutionStateWrapper, ConfirmAddressAsync);
            TryLoadPlacemarkCommand = new SafeAsyncCommand<Coordinates>(ExecutionStateWrapper, TryLoadPlacemarkAsync);
            selectCommand = new SafeAsyncCommand<OrderDeliverySuggestionItemViewModel>(ExecutionStateWrapper, SelectAsync);
        }

        private string? addressQuery;
        public string? AddressQuery
        {
            get => addressQuery;
            set => SetProperty(ref addressQuery, value, () => _ = OnAddressQueryChangedAsync(value));
        }

        public ICommand ConfirmAddress { get; }

        public ICommand TryLoadPlacemarkCommand { get; }

        public bool HasSelectedLocation => SelectedLocation != null;

        public MvxObservableCollection<OrderDeliverySuggestionItemViewModel> Suggestions { get; }

        public float ZoomFactor => _city?.ZoomFactor ?? 0;

        public double Latitude => _city?.Latitude ?? 0;

        public double Longitude => _city?.Longitude ?? 0;

        private OrderDeliverySuggestionItemViewModel? selectedLocation;
        public OrderDeliverySuggestionItemViewModel? SelectedLocation
        {
            get => selectedLocation;
            protected set => SetProperty(ref selectedLocation, value, () => RaisePropertyChanged(nameof(HasSelectedLocation)));
        }

        public override Task InitializeAsync()
        {
            _city = userSession.GetCity();
            return Task.WhenAll(base.InitializeAsync(), RefreshDataAsync());
        }

        protected override async Task RefreshDataAsync()
        {
            var city = userSession.GetCity();
            var response = await shopsManager.GetShopsAsync(city?.Name);
            if (!response.IsSuccessful)
            {
                var error = response.Errors.FirstOrDefault();
                if (error.IsNullOrEmpty())
                {
                    return;
                }

                await dialog.ShowToastAsync(error);
                return;
            }

            var deliveryZonesViewModels = response.Data.SelectMany(item => item.DeliveryZones)
                .Select(deliveryZone => new DeliveryZoneItemViewModel(deliveryZone))
                .ToArray();

            Items.ReplaceWith(deliveryZonesViewModels);
        }

        private Task SelectAsync(OrderDeliverySuggestionItemViewModel viewModel)
        {
            SelectedLocation = viewModel;
            Suggestions.Clear();
            AddressQuery = null;

            return Task.CompletedTask;
        }

        private Task ConfirmAddressAsync()
        {
            if (SelectedLocation is null ||
                !SelectedLocation.IsDeliveryAvailable)
            {
                return Task.CompletedTask;
            }

            return NavigationManager.CloseAsync(this, SelectedLocation.Suggestion);
        }

        private async Task TryLoadPlacemarkAsync(Coordinates coordinates)
        {
            cancellationTokenSource?.Cancel();
            cancellationTokenSource = new CancellationTokenSource();

            var response = await citiesManager.SearchByLocationAsync(coordinates, cancellationTokenSource.Token);
            if (!response.IsSuccessful)
            {
                SelectedLocation = null;
                return;
            }

            var suggestion = response.Data.FirstOrDefault();
            if (suggestion is null)
            {
                return;
            }

            SelectedLocation = new OrderDeliverySuggestionItemViewModel(suggestion, selectCommand);
            cancellationTokenSource = null;
        }

        private async Task OnAddressQueryChangedAsync(string? query)
        {
            await Task.Delay(SearchMillisecondsDelay);

            if (query != AddressQuery)
            {
                return;
            }

            cancellationTokenSource?.Cancel();
            cancellationTokenSource = new CancellationTokenSource();

            var response = await citiesManager.SearchAddressAsync(_city?.Name, query!, cancellationTokenSource.Token);
            var viewModels = response?.Data?.Select(suggestion => new OrderDeliverySuggestionItemViewModel(suggestion, selectCommand)).ToArray() ?? Array.Empty<OrderDeliverySuggestionItemViewModel>();

            Suggestions.ReplaceWith(viewModels);
            cancellationTokenSource = null;
        }
    }
}