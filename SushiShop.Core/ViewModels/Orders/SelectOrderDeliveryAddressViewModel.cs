using Acr.UserDialogs;
using BuildApps.Core.Mobile.Common.Extensions;
using BuildApps.Core.Mobile.MvvmCross.Commands;
using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract.Items;
using MvvmCross.ViewModels;
using SushiShop.Core.Data.Models.Common;
using SushiShop.Core.Data.Models.Orders;
using SushiShop.Core.Managers.Shops;
using SushiShop.Core.Providers;
using SushiShop.Core.ViewModels.Orders.Items;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;

namespace SushiShop.Core.ViewModels.Orders
{
    public class SelectOrderDeliveryAddressViewModel : BaseItemsPageViewModelResult<DeliveryZoneItemViewModel, OrderDelivery>
    {
        private const int SearchMillisecondsDelay = 1500;

        private readonly IUserDialogs userDialogs;
        private readonly IShopsManager shopsManager;
        private readonly IUserSession userSession;

        public SelectOrderDeliveryAddressViewModel(IShopsManager shopsManager, IUserSession userSession)
        {
            userDialogs = UserDialogs.Instance;
            this.shopsManager = shopsManager;
            this.userSession = userSession;

            Suggestions = new MvxObservableCollection<OrderDeliverySuggestionItemViewModel>();

            ConfirmAddress = new SafeAsyncCommand(ExecutionStateWrapper, ConfirmAddressAsync);
            TryLoadPlacemarkCommand = new SafeAsyncCommand<Location>(ExecutionStateWrapper, TryLoadPlacemarkAsync);
        }

        private string? addressQuery;
        public string? AddressQuery
        {
            get => addressQuery;
            set => SetProperty(ref addressQuery, value, () => _ = OnAddressQueryChangedAsync(value));
        }

        public ICommand ConfirmAddress { get; }

        public ICommand TryLoadPlacemarkCommand { get; }

        public bool HasSelectedLocation => OrderDelivery != null;

        public MvxObservableCollection<OrderDeliverySuggestionItemViewModel> Suggestions { get; }

        private OrderDelivery? orderDelivery;
        protected OrderDelivery? OrderDelivery
        {
            get => orderDelivery;
            set => SetProperty(ref orderDelivery, value, () => RaisePropertyChanged(nameof(HasSelectedLocation)));
        }

        public override Task InitializeAsync()
        {
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

                await userDialogs.AlertAsync(error);
                return;
            }

            var deliveryZonesViewModels = response.Data.SelectMany(item => item.DeliveryZones)
                .Select(deliveryZone => new DeliveryZoneItemViewModel(deliveryZone))
                .ToArray();

            Items.ReplaceWith(deliveryZonesViewModels);
        }

        private Task ConfirmAddressAsync()
        {
            throw new System.NotImplementedException();
        }

        private Task TryLoadPlacemarkAsync(Location location)
        {
            throw new System.NotImplementedException();
        }

        private async Task OnAddressQueryChangedAsync(string? query)
        {
            await Task.Delay(SearchMillisecondsDelay);

            if (query != AddressQuery)
            {
                return;
            }

            var locations = await Geocoding.GetLocationsAsync(query);
            if (locations.Count() > 1)
            {
                var getPlacemarksTasks = locations.Select(location => Geocoding.GetPlacemarksAsync(location)).ToArray();
                await Task.WhenAll(getPlacemarksTasks);

                var suggestions = getPlacemarksTasks.SelectMany(task => task.Result)
                    .Select(placemark => new OrderDeliverySuggestionItemViewModel(new OrderDelivery(placemark.FeatureName, new Coordinates(placemark.Location.Longitude, placemark.Location.Latitude))))
                    .ToArray();

                Suggestions.ReplaceWith(suggestions);
                OrderDelivery = null;
                return;
            }

            Suggestions.Clear();
            var firstLocation = locations.FirstOrDefault();
            OrderDelivery = new OrderDelivery(AddressQuery, new Coordinates(firstLocation.Longitude, firstLocation.Latitude));
        }
    }
}
