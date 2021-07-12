using BuildApps.Core.Mobile.Common.Extensions;
using BuildApps.Core.Mobile.MvvmCross.Commands;
using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract.Items;
using MvvmCross.ViewModels;
using SushiShop.Core.Common;
using SushiShop.Core.Data.Models.Cities;
using SushiShop.Core.Data.Models.Common;
using SushiShop.Core.Managers.Cities;
using SushiShop.Core.Managers.Shops;
using SushiShop.Core.Plugins;
using SushiShop.Core.Providers;
using SushiShop.Core.ViewModels.Orders.Items;
using System;
using System.Collections.Specialized;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SushiShop.Core.ViewModels.Orders
{
    public class SelectOrderDeliveryAddressViewModel : BaseItemsPageViewModel<DeliveryZoneItemViewModel, AddressSuggestion?, AddressSuggestion>
    {
        private const int SearchMillisecondsDelay = 1500;

        private readonly IShopsManager shopsManager;
        private readonly ICitiesManager citiesManager;
        private readonly IUserSession userSession;
        private readonly IDialog dialog;

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
            Suggestions.SubscribeToCollectionChanged(OnSuggestionsCollectionChanged).DisposeWith(Disposables);

            RemoveFocusInteraction = new MvxInteraction();
            RestoreCursorInteraction = new MvxInteraction();
            ConfirmAddressCommand = new SafeAsyncCommand(ExecutionStateWrapper, ConfirmAddressAsync);
            TryLoadPlacemarkCommand = new SafeAsyncCommand<Coordinates>(ExecutionStateWrapper, TryLoadPlacemarkAsync);
        }

        public bool IsSuggestionsEmpty => Suggestions.IsEmpty();

        private string? addressQuery;
        public string? AddressQuery
        {
            get => addressQuery;
            set => SetProperty(ref addressQuery, value, () => _ = OnAddressQueryChangedAsync(value));
        }

        public ICommand ConfirmAddressCommand { get; }

        public ICommand TryLoadPlacemarkCommand { get; }

        public bool HasSelectedLocation => SelectedLocation != null;

        public MvxObservableCollection<OrderDeliverySuggestionItemViewModel> Suggestions { get; }

        public float ZoomFactor => _city?.ZoomFactor ?? Constants.Map.DefaultZoomFactor;

        public double Latitude => _city?.Latitude ?? Constants.Map.MapStartPointLatitude;

        public double Longitude => _city?.Longitude ?? Constants.Map.MapStartPointLongitude;

        public MvxInteraction RemoveFocusInteraction { get; }
        public MvxInteraction RestoreCursorInteraction { get; }

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
            var response = await shopsManager.GetShopsAsync(city?.Name, true);
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

        private void OnSuggestionsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            _ = RaisePropertyChanged(nameof(IsSuggestionsEmpty));
        }

        private Task SelectAsync(OrderDeliverySuggestionItemViewModel viewModel)
        {
            if (viewModel.Suggestion.IsHouseAddress)
            {
                SelectedLocation = viewModel;
                Suggestions.Clear();
                AddressQuery = null;
                RemoveFocusInteraction.Raise();
                return Task.CompletedTask;
            }

            Suggestions.Clear();
            AddressQuery = viewModel.Address;
            RestoreCursorInteraction.Raise();
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

            SelectedLocation = new OrderDeliverySuggestionItemViewModel(suggestion, SelectAsync);
            cancellationTokenSource = null;
        }

        private async Task OnAddressQueryChangedAsync(string? query)
        {
            await Task.Delay(SearchMillisecondsDelay);

            if (query != AddressQuery)
            {
                return;
            }

            if (AddressQuery.IsNullOrEmpty())
            {
                Suggestions.Clear();
                return;
            }

            cancellationTokenSource?.Cancel();
            cancellationTokenSource = new CancellationTokenSource();

            var response = await citiesManager.SearchAddressAsync(_city?.Name, query!, cancellationTokenSource.Token);
            var viewModels = response?.Data?.Select(suggestion => new OrderDeliverySuggestionItemViewModel(suggestion, SelectAsync)).ToArray() ?? Array.Empty<OrderDeliverySuggestionItemViewModel>();

            Suggestions.ReplaceWith(viewModels);
            cancellationTokenSource = null;
        }

        public override void Prepare(AddressSuggestion? parameter)
        {
            if (parameter is null)
            {
                return;
            }

            SelectedLocation = new OrderDeliverySuggestionItemViewModel(parameter, SelectAsync);
        }
    }
}