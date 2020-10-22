using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BuildApps.Core.Mobile.MvvmCross.Commands;
using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;
using FFImageLoading;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using SushiShop.Core.Common;
using SushiShop.Core.Data.Enums;
using SushiShop.Core.Data.Models.Cities;
using SushiShop.Core.Managers.Menu;
using SushiShop.Core.Managers.Promotions;
using SushiShop.Core.NavigationParameters;
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

        private City? city;

        public MenuViewModel(IMenuManager menuManager, IPromotionsManager promotionsManager)
        {
            this.menuManager = menuManager;
            this.promotionsManager = promotionsManager;

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
            _ = ExecutionStateWrapper.WrapAsync(ReloadDataAsync, awaitWhenBusy: true);
        }

        private async Task ReloadDataAsync()
        {
            var cityName = city?.Name;

            var promotionsTask = promotionsManager.GetPromotionsAsync(cityName);
            var menuTask = menuManager.GetMenuAsync(cityName);

            await Task.WhenAll(promotionsTask, menuTask);

            var categoryItems = menuTask.Result.Data.Categories
                .Select(category => new CategoryMenuItemViewModel(category) { ExecutionStateWrapper = ExecutionStateWrapper })
                .ToArray();

            var promotionItems = promotionsTask.Result.Data
                .Select(promotion => new MenuPromotionItemViewModel(promotion))
                .ToArray();

            await PreloadImagesAsync(categoryItems, promotionItems);

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

            _ = Permissions.RequestAsync<Permissions.LocationWhenInUse>();
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
            await RaisePropertyChanged(nameof(CityName));
            await ReloadDataAsync();
        }

        private async Task PreloadImagesAsync(CategoryMenuItemViewModel[] categories, MenuPromotionItemViewModel[] promotions)
        {
            var urls = Enumerable.Concat(
                categories.Select(item => item.ImageUrl),
                promotions.Select(item => item.ImageUrl));

            await Task.WhenAll(urls.Select(PreloadImageAsync));
        }

        private async Task PreloadImageAsync(string url)
        {
            try
            {
                await ImageService.Instance.LoadUrl(url).PreloadAsync();
            }
            catch
            {
                // TODO: log
            }
        }
    }
}