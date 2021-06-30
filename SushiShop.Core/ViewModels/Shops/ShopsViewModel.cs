using BuildApps.Core.Mobile.Common.Extensions;
using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;
using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract.Items;
using SushiShop.Core.Common;
using SushiShop.Core.Data.Models.Cities;
using SushiShop.Core.Data.Models.Common;
using SushiShop.Core.Data.Models.Shops;
using SushiShop.Core.Extensions;
using SushiShop.Core.Managers.Shops;
using SushiShop.Core.Messages;
using SushiShop.Core.NavigationParameters;
using SushiShop.Core.Providers;
using SushiShop.Core.Resources;
using SushiShop.Core.ViewModels.Shops;
using SushiShop.Core.ViewModels.Shops.Items;
using SushiShop.Core.ViewModels.Shops.Sections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SushiShop.Core.ViewModels.Info
{
    public class ShopsViewModel : BaseItemsPageViewModel<BaseViewModel, bool, Shop>
    {
        private readonly IShopsManager shopsManager;
        private readonly IUserSession userSession;

        private ShopsOnMapSectionViewModel? shopsOnMapSectionViewModel;
        private ShopsListSectionViewModel? shopsListSectionViewModel;
        private MetroSectionViewModel? metroSectionViewModel;
        private ShopItemViewModel? savedSelectedItem;

        private int previousSelectedIndex;

        public ShopsViewModel(IShopsManager shopsManager, IUserSession userSession)
        {
            this.shopsManager = shopsManager;
            this.userSession = userSession;

            Messenger.Subscribe<CityChangedMessage>(OnCityChnaged).DisposeWith(Disposables);
        }

        public List<string> TabsTitles { get; } = new List<string>();

        private int selectedIndex;
        public int SelectedIndex
        {
            get => selectedIndex;
            set => SetProperty(ref selectedIndex, value, OnSelectedIndexChanged);
        }

        private string? title = AppStrings.Shops;
        public string? Title
        {
            get => title;
            set => SetProperty(ref title, value);
        }

        public bool IsSelectionMode { get; private set; }

        public override void Prepare(bool parameter)
        {
            IsSelectionMode = parameter;

            Title = IsSelectionMode ? AppStrings.SelectShop : AppStrings.Shops;

            RaisePropertyChanged(nameof(IsSelectionMode));
        }

        public override Task InitializeAsync()
        {
            return Task.WhenAll(base.InitializeAsync(), RefreshDataAsync());
        }

        protected override async Task RefreshDataAsync()
        {
            var city = userSession.GetCity();
            var isMetroAvailable = city?.IsMetroAvailable ?? true;

            Items.ReplaceWith(ProduceSectionsViewModels(city, isMetroAvailable));
            TabsTitles.ReplaceWith(ProduceSectionsTitles(isMetroAvailable));

            var getShopsTask = shopsManager.GetShopsAsync(city?.Name, IsSelectionMode);
            var getMetroShopsTask = shopsManager.GetMetroShopsAsync(city?.Name, IsSelectionMode);
            await Task.WhenAll(getShopsTask, getMetroShopsTask, RaisePropertyChanged(nameof(TabsTitles)));

            if (!getShopsTask.Result.IsSuccessful
                || !getMetroShopsTask.Result.IsSuccessful)
            {
                return;
            }

            shopsListSectionViewModel?.SetShops(getShopsTask.Result.Data);
            shopsOnMapSectionViewModel?.SetShops(getShopsTask.Result.Data);

            shopsListSectionViewModel?.SetMetroShops(getMetroShopsTask.Result.Data);
            metroSectionViewModel?.SetMetroShops(getMetroShopsTask.Result.Data);
        }

        public override void ViewAppearing()
        {
            base.ViewAppeared();

            if (!IsSelectionMode && selectedIndex == 0 ||
                IsSelectionMode && selectedIndex == 1)
            {
                ShowSelectedItem();
            }
        }

        public override void ViewDisappearing()
        {
            base.ViewDisappearing();

            if (!IsSelectionMode && previousSelectedIndex != 0 ||
                IsSelectionMode && previousSelectedIndex != 1)
            {
                return;
            }

            HideSelectedItem();
        }

        private void ShowSelectedItem()
        {
            if (shopsOnMapSectionViewModel is null)
            {
                return;
            }

            shopsOnMapSectionViewModel.SelectedItem = savedSelectedItem;
            savedSelectedItem = null;
        }

        private void HideSelectedItem()
        {
            if (shopsOnMapSectionViewModel is null)
            {
                return;
            }

            savedSelectedItem = shopsOnMapSectionViewModel.SelectedItem;
            shopsOnMapSectionViewModel.SelectedItem = null;
        }

        private IEnumerable<BaseViewModel> ProduceSectionsViewModels(City? city, bool isMetroAvailable)
        {
            var coordinates = city is null
                ? new Coordinates(Constants.Map.MapStartPointLongitude, Constants.Map.MapStartPointLatitude)
                : new Coordinates(city.Longitude, city.Latitude);

            if (IsSelectionMode)
            {
                yield return shopsListSectionViewModel = new ShopsListSectionViewModel(
                    GoToShopAsync,
                    GoToMapAsync,
                    ConfirmSelectionAsync,
                    IsSelectionMode).DisposeWith(Disposables);

                yield return shopsOnMapSectionViewModel = new ShopsOnMapSectionViewModel(
                    coordinates,
                    city?.ZoomFactor ?? Constants.Map.DefaultZoomFactor,
                    GoToMapAsync,
                    ConfirmSelectionAsync,
                    IsSelectionMode);
            }
            else
            {
                yield return shopsOnMapSectionViewModel = new ShopsOnMapSectionViewModel(
                   coordinates,
                   city?.ZoomFactor ?? Constants.Map.DefaultZoomFactor,
                   GoToMapAsync,
                   ConfirmSelectionAsync,
                   IsSelectionMode);

                yield return shopsListSectionViewModel = new ShopsListSectionViewModel(
                    GoToShopAsync,
                    GoToMapAsync,
                    ConfirmSelectionAsync,
                    IsSelectionMode).DisposeWith(Disposables);
            }

            if (isMetroAvailable)
            {
                yield return metroSectionViewModel = new MetroSectionViewModel(GoToShopAsync);
            }
        }

        private IEnumerable<string> ProduceSectionsTitles(bool isMetroAvailable)
        {
            if (IsSelectionMode)
            {
                yield return AppStrings.List;
                yield return AppStrings.OnMap;
            }
            else
            {
                yield return AppStrings.OnMap;
                yield return AppStrings.List;
            }

            if (isMetroAvailable)
            {
                yield return AppStrings.Metro;
            }
        }

        private void OnSelectedIndexChanged()
        {
            if (!IsSelectionMode && selectedIndex == 0 ||
                 IsSelectionMode && selectedIndex == 1)
            {
                ShowSelectedItem();
            }

            if (!IsSelectionMode && previousSelectedIndex == 0 ||
                IsSelectionMode && previousSelectedIndex == 1)
            {
                HideSelectedItem();
            }

            previousSelectedIndex = selectedIndex;
        }

        private void OnCityChnaged(CityChangedMessage message)
        {
            _ = SafeExecutionWrapper.WrapAsync(() => ExecutionStateWrapper.WrapAsync(RefreshDataAsync));
        }

        private async Task GoToMapAsync(Shop shop)
        {
            var navigationParameter = new ShopOnMapNavigationParameter(shop, IsSelectionMode);
            var selectedShop = await NavigationManager.NavigateAsync<ShopOnMapViewModel, ShopOnMapNavigationParameter, Shop>(navigationParameter);
            if (!IsSelectionMode ||
                selectedShop is null)
            {
                return;
            }

            await NavigationManager.CloseWithDelayAsync(this, selectedShop);
        }

        private async Task GoToShopAsync(MetroShop[] shops, string title)
        {
            var navigationParameter = new ShopsNearMetroNavigationParameters(shops, title, IsSelectionMode);
            var selectedShop = await NavigationManager.NavigateAsync<ShopsNearMetroViewModel, ShopsNearMetroNavigationParameters, Shop>(navigationParameter);
            if (!IsSelectionMode ||
                selectedShop is null)
            {
                return;
            }

            await NavigationManager.CloseWithDelayAsync(this, selectedShop);
        }

        private Task ConfirmSelectionAsync(Shop shop)
        {
            return NavigationManager.CloseWithDelayAsync(this, shop);
        }
    }
}