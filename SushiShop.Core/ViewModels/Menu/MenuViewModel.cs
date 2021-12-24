using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Acr.UserDialogs;
using BuildApps.Core.Mobile.Common.Wrappers;
using BuildApps.Core.Mobile.MvvmCross.Commands;
using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using SushiShop.Core.Common;
using SushiShop.Core.Data.Enums;
using SushiShop.Core.Data.Models.Cities;
using SushiShop.Core.Managers.Cities;
using SushiShop.Core.Managers.CommonInfo;
using SushiShop.Core.Managers.Menu;
using SushiShop.Core.Managers.Promotions;
using SushiShop.Core.Messages;
using SushiShop.Core.NavigationParameters;
using SushiShop.Core.Plugins;
using SushiShop.Core.Providers;
using SushiShop.Core.Providers.UserOrderPreferences;
using SushiShop.Core.Resources;
using SushiShop.Core.ViewModels.Cities;
using SushiShop.Core.ViewModels.Cities.Items;
using SushiShop.Core.ViewModels.Menu.Items;
using Xamarin.Essentials;

namespace SushiShop.Core.ViewModels.Menu
{
    public class MenuViewModel : BasePageViewModel
    {
        private readonly IMenuManager menuManager;
        private readonly IPromotionsManager promotionsManager;
        private readonly ICitiesManager citiesManager;
        private readonly ICommonInfoManager commonInfoManager;
        private readonly IUserSession userSession;
        private readonly IUserOrderPreferencesProvider userOrderPreferencesProvider;
        private readonly IUserDialogs userDialogs;
        private readonly ILocation location;

        private City? city;

        public MenuViewModel(
            IMenuManager menuManager,
            IPromotionsManager promotionsManager,
            ICitiesManager citiesManager,
            ICommonInfoManager commonInfoManager,
            IUserSession userSession,
            IUserOrderPreferencesProvider userOrderPreferencesProvider,
            IUserDialogs userDialogs,
            ILocation location)
        {
            this.menuManager = menuManager;
            this.promotionsManager = promotionsManager;
            this.citiesManager = citiesManager;
            this.commonInfoManager = commonInfoManager;
            this.userSession = userSession;
            this.userOrderPreferencesProvider = userOrderPreferencesProvider;
            this.userDialogs = userDialogs;
            this.location = location;

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

        protected override Task RefreshDataAsync()
        {
            return Task.WhenAll(base.RefreshDataAsync(), ReloadDataAsync());
        }

        protected override async Task<bool> TryToReloadDataWithConnectionAsync()
        {
            var wasConnectionBeforeLoad = HasConnection;
            await ReloadDataAsync(true);
            return wasConnectionBeforeLoad && HasConnection;
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
                .Where(promotion => promotion.ShouldShowOnHome ?? true)
                .Select(promotion => new MenuPromotionItemViewModel(promotion) { ExecutionStateWrapper = ExecutionStateWrapper })
                .ToArray();

            Items.Clear();
            SimpleItems.Clear();

            var availableCategoriesForMainMenu = categoryItems.Where(item => item.ShouldShowOnMainMenu).ToArray();
            Items.Add(new MenuPromotionListItemViewModel(promotionItems) { ExecutionStateWrapper = ExecutionStateWrapper });
            Items.AddRange(availableCategoriesForMainMenu);

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

            await ShowApplicationUpdateConfirmationIfNeededAsync();

            _ = TryRefreshGelolocationAsync(citiesTask.Result.Data);
        }

        private async Task TryRefreshGelolocationAsync(City[] cities)
        {
            try
            {
                var lastKnownLocation = await Geolocation.GetLastKnownLocationAsync();
                if (lastKnownLocation is null)
                {
                    var request = new GeolocationRequest(GeolocationAccuracy.Best, TimeSpan.FromSeconds(3));
                    try
                    {
                        lastKnownLocation = await Geolocation.GetLocationAsync(request);
                    }
                    catch (FeatureNotEnabledException)
                    {
                        var result = await userDialogs.ConfirmAsync("Сервис геолокации выключен, хотите включить?", null, "Да", "Нет");
                        if (result)
                        {
                            await location.RequestEnableLocationServiceAsync();
                            try
                            {
                                lastKnownLocation = await Geolocation.GetLocationAsync(request);
                            }
                            catch (FeatureNotEnabledException)
                            {
                                await userDialogs.AlertAsync("Не удалось определить ваше местоположение", null, "Ок");
                            }
                        }
                    }
                }

                if (lastKnownLocation is null && city != null)
                {
                    return;
                }

                if (lastKnownLocation is null)
                {
                    await PreselectDefaultCityWithConfirmationAsync(cities);
                    return;
                }

                var placemarks = await Geocoding.GetPlacemarksAsync(lastKnownLocation);
                var firtPlacemark = placemarks.FirstOrDefault();
                if (firtPlacemark is null && city != null)
                {
                    return;
                }

                if (firtPlacemark is null)
                {
                    await PreselectDefaultCityWithConfirmationAsync(cities);
                    return;
                }

                var foundCity = cities.FirstOrDefault(city => city.Name.ToLowerInvariant()
                                                                       .Equals(firtPlacemark.SubAdminArea
                                                                       .ToLowerInvariant()));
                if ((foundCity is null && city != null) ||
                    (foundCity != null && foundCity.Id == city?.Id))
                {
                    return;
                }

                if (foundCity is null)
                {
                    await PreselectDefaultCityWithConfirmationAsync(cities);
                    return;
                }

                var message = string.Format(AppStrings.IsItYourCityQuestionTemplate, foundCity.Name);
                var isConfirmed = await UserDialogs.Instance.ConfirmAsync(message, okText: AppStrings.Yes, cancelText: AppStrings.No);
                if (!isConfirmed)
                {
                    PreselectDefaultCity(cities);
                    SelectCityCommand.Execute();
                    return;
                }

                city = foundCity;
                SetCityAndClearPreferencesIfNeeded(city);

                await RaisePropertyChanged(nameof(CityName));

                _ = ReloadDataAsync();
                Messenger.Publish(new RefreshCartMessage(this));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                PreselectDefaultCity(cities);
                SelectCityCommand.Execute(); 
            }
        }

        private void PreselectDefaultCity(City[] cities)
        {
            var foundCity = cities.FirstOrDefault(city => city.Name.ToLowerInvariant()
                                                                   .Equals(Constants.Menu.DefaultCityName
                                                                   .ToLowerInvariant()));
            if (foundCity != null)
            {
                SetCityAndClearPreferencesIfNeeded(foundCity);
                city = foundCity;

                _ = RaisePropertyChanged(nameof(CityName));
            }
        }

        private async Task PreselectDefaultCityWithConfirmationAsync(City[] cities)
        {
            PreselectDefaultCity(cities);

            var message = string.Format(AppStrings.IsItYourCityQuestionTemplate, CityName);
            var isConfirmed = await UserDialogs.Instance.ConfirmAsync(message, okText: AppStrings.Yes, cancelText: AppStrings.No);
            if (!isConfirmed)
            {
                PreselectDefaultCity(cities);
                SelectCityCommand.Execute();
                return;
            }
        }

        private async Task SelectCityAsync()
        {
            var selectedCityIds = city is null ? Array.Empty<long>() : new[] { city.Id };
            var navigationParams = new SelectCityNavigationParameters(selectedCityIds);
            var result = await NavigationManager.NavigateAsync<SelectCityViewModel, SelectCityNavigationParameters, List<CityItemViewModel>?>(navigationParams);
            if (result is null)
            {
                return;
            }

            city = result.First().City;
            SetCityAndClearPreferencesIfNeeded(city);
            await RaisePropertyChanged(nameof(CityName));
            await ReloadDataAsync();

            Messenger.Publish(new CityChangedMessage(this));
            Messenger.Publish(new RefreshCartMessage(this));
        }

        private async Task ShowApplicationUpdateConfirmationIfNeededAsync()
        {
            var platform = DeviceInfo.Platform.ToString();
            var version = VersionTracking.CurrentVersion;
            var applicationInformationResult = await commonInfoManager.GetApplicationInformationAsync(platform, version);
            if (!applicationInformationResult.IsSuccessful ||
                !applicationInformationResult.Data.ShouldUpdate)
            {
                return;
            }

            //HACK: to avoid design issue
            var shouldUpdate = await UserDialogs.Instance.ConfirmAsync(string.Empty, applicationInformationResult.Data.Message, cancelText: AppStrings.Yes, okText: AppStrings.No);
            if (shouldUpdate)
            {
                return;
            }

            var updateAppUrl = DeviceInfo.Platform == DevicePlatform.Android
                ? applicationInformationResult.Data.Platforms.Android.Url
                : applicationInformationResult.Data.Platforms.Ios.Url;

            await Browser.OpenAsync(updateAppUrl, BrowserLaunchMode.External);
        }

        private void SetCityAndClearPreferencesIfNeeded(City? city)
        {
            var previousCity = userSession.GetCity();
            userSession.SetCity(city);
            if (previousCity?.Id == city?.Id)
            {
                return;
            }

            userOrderPreferencesProvider.ClearAll();
        }
    }
}