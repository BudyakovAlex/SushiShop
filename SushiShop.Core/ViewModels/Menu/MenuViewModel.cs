using Acr.UserDialogs;
using BuildApps.Core.Mobile.MvvmCross.Commands;
using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using SushiShop.Core.Common;
using SushiShop.Core.Data.Enums;
using SushiShop.Core.Data.Models.Cities;
using SushiShop.Core.Managers.Cities;
using SushiShop.Core.Managers.Menu;
using SushiShop.Core.Managers.Promotions;
using SushiShop.Core.NavigationParameters;
using SushiShop.Core.Providers;
using SushiShop.Core.Resources;
using SushiShop.Core.ViewModels.Cities;
using SushiShop.Core.ViewModels.Cities.Items;
using SushiShop.Core.ViewModels.Menu.Items;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace SushiShop.Core.ViewModels.Menu
{
    public class MenuViewModel : BasePageViewModel
    {
        private readonly IMenuManager menuManager;
        private readonly IPromotionsManager promotionsManager;
        private readonly ICitiesManager citiesManager;
        private readonly IUserSession userSession;

        private City? city;

        public MenuViewModel(
            IMenuManager menuManager,
            IPromotionsManager promotionsManager,
            ICitiesManager citiesManager,
            IUserSession userSession)
        {
            this.menuManager = menuManager;
            this.promotionsManager = promotionsManager;
            this.citiesManager = citiesManager;
            this.userSession = userSession;

            Items = new MvxObservableCollection<BaseViewModel>();
            SimpleItems = new MvxObservableCollection<BaseViewModel>();

            SelectCityCommand = new SafeAsyncCommand(ExecutionStateWrapper, SelectCityAsync);
            SwitchPresentationCommand = new MvxCommand(() => IsListMenuPresentation = !IsListMenuPresentation);
        }

        public MvxObservableCollection<BaseViewModel> Items { get; }
        public MvxObservableCollection<BaseViewModel> SimpleItems { get; }

        public IMvxCommand SelectCityCommand { get; }
        public IMvxCommand SwitchPresentationCommand { get; }

        private bool isListMenuPresentation;
        public bool IsListMenuPresentation
        {
            get => isListMenuPresentation;
            set => SetProperty(ref isListMenuPresentation, value);
        }

        public string CityName => city?.Name ?? Constants.Menu.DefaultCityName;

        public override async Task InitializeAsync()
        {
            await base.InitializeAsync();
            city = userSession.GetCity();

            await RaisePropertyChanged(nameof(CityName));

            _ = ExecutionStateWrapper.WrapAsync(() => ReloadDataAsync(true), awaitWhenBusy: true);
        }

        private async Task ReloadDataAsync(bool shouldReloadUserLocation = false)
        {
            var citiesTask = citiesManager.GetCitiesAsync();
            var promotionsTask = promotionsManager.GetPromotionsAsync(CityName);
            var menuTask = menuManager.GetMenuAsync(CityName);

            await Task.WhenAll(promotionsTask, menuTask, citiesTask);

            var categoryItems = menuTask.Result.Data.Categories
                .Select(category => new CategoryMenuItemViewModel(category) { ExecutionStateWrapper = ExecutionStateWrapper })
                .ToArray();

            var promotionItems = promotionsTask.Result.Data
                .Select(promotion => new MenuPromotionItemViewModel(promotion))
                .ToArray();

            Items.Clear();
            SimpleItems.Clear();

            Items.Add(new MenuPromotionListItemViewModel(promotionItems));
            Items.AddRange(categoryItems);

            var groupMenuItemViewModels = menuTask.Result.Data.Stickers
                .Select(sticker => new GroupMenuItemViewModel(sticker) { ExecutionStateWrapper = ExecutionStateWrapper })
                .ToArray();
            var groupsMenuItemViewModel = new GroupsMenuItemViewModel(groupMenuItemViewModels) { ExecutionStateWrapper = ExecutionStateWrapper };

            SimpleItems.AddRange(categoryItems);
            SimpleItems.Add(groupsMenuItemViewModel);
            SimpleItems.AddRange(new List<MenuActionItemViewModel>
            {
                new MenuActionItemViewModel(ActionType.Franchise, city) { ExecutionStateWrapper = ExecutionStateWrapper },
                new MenuActionItemViewModel(ActionType.Vacancies, city) { ExecutionStateWrapper = ExecutionStateWrapper }
            });

            if (!shouldReloadUserLocation)
            {
                return;
            }

            _ = TryRefreshGelolocationAsync(citiesTask.Result.Data);
        }

        private async Task TryRefreshGelolocationAsync(City[] cities)
        {
            try
            {
                if (city != null)
                {
                    return;
                }

                var lastKnownLocation = await Geolocation.GetLastKnownLocationAsync();
                if (lastKnownLocation is null)
                {
                    var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(3));
                    lastKnownLocation = await Geolocation.GetLocationAsync(request);
                }

                if (lastKnownLocation is null)
                {
                    return;
                }

                var placemarks = await Geocoding.GetPlacemarksAsync(lastKnownLocation);
                var firtPlacemark = placemarks.FirstOrDefault();
                if (firtPlacemark is null)
                {
                    return;
                }

                var foundCity = cities.FirstOrDefault(city => city.Name.ToLowerInvariant()
                                                                       .Equals(firtPlacemark.SubAdminArea
                                                                       .ToLowerInvariant()));
                if (foundCity is null)
                {
                    return;
                }

                var message = string.Format(AppStrings.IsItYourCityQuestionTemplate, foundCity.Name);
                var isConfirmed = await UserDialogs.Instance.ConfirmAsync(message, okText: AppStrings.Yes, cancelText: AppStrings.No);
                if (!isConfirmed)
                {
                    return;
                }

                userSession.SetCity(foundCity);
                city = foundCity;

                await RaisePropertyChanged(nameof(CityName));

                _ = ReloadDataAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        private async Task SelectCityAsync()
        {
            var selectedCityIds = city is null ? Array.Empty<int>() : new[] { city.Id };
            var navigationParams = new SelectCityNavigationParameters(selectedCityIds);
            var result = await NavigationManager.NavigateAsync<SelectCityViewModel, SelectCityNavigationParameters, List<CityItemViewModel>?>(navigationParams);
            if (result is null)
            {
                return;
            }

            city = result.First().City;
            userSession.SetCity(city);
            await RaisePropertyChanged(nameof(CityName));
            await ReloadDataAsync();
        }
    }
}