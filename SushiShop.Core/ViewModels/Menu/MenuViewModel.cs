using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildApps.Core.Mobile.MvvmCross.Commands;
using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;
using FFImageLoading;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using SushiShop.Core.Common;
using SushiShop.Core.Data.Enums;
using SushiShop.Core.Data.Models;
using SushiShop.Core.Managers.Menu;
using SushiShop.Core.NavigationParameters;
using SushiShop.Core.ViewModels.Cities;
using SushiShop.Core.ViewModels.Cities.Items;
using SushiShop.Core.ViewModels.Menu.Items;

namespace SushiShop.Core.ViewModels.Menu
{
    public class MenuViewModel : BasePageViewModel
    {
        private readonly IMenuManager menuManager;

        private City? city;

        public MenuViewModel(IMenuManager menuManager)
        {
            this.menuManager = menuManager;

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
            await Task.WhenAll(base.InitializeAsync(), ReloadDataAsync());
        }

        private async Task ReloadDataAsync()
        {
            var response = await menuManager.GetMenuAsync(city?.Name);

            var groupMenuItemViewModels = response.Data.Stickers.Select(sticker => new GroupMenuItemViewModel(sticker) { ExecutionStateWrapper = ExecutionStateWrapper }).ToList();
            var categoryMenuItemViewModels = response.Data.Categories.Select(categories => new CategoryMenuItemViewModel(categories) { ExecutionStateWrapper = ExecutionStateWrapper }).ToList();

            await Task.WhenAll(categoryMenuItemViewModels
                .Select(item => ImageService.Instance.LoadUrl(item.ImageUrl).PreloadAsync()));

            Items.Clear();
            SimpleItems.Clear();

            var groupsMenuItemViewModel = new GroupsMenuItemViewModel(groupMenuItemViewModels) { ExecutionStateWrapper = ExecutionStateWrapper };
            Items.Add(groupsMenuItemViewModel);
            Items.AddRange(categoryMenuItemViewModels);

            SimpleItems.AddRange(categoryMenuItemViewModels);
            SimpleItems.Add(groupsMenuItemViewModel);
            SimpleItems.AddRange(new List<MenuActionItemViewModel>
            {
                new MenuActionItemViewModel(ActionType.Franchise) { ExecutionStateWrapper = ExecutionStateWrapper },
                new MenuActionItemViewModel(ActionType.Vacancies) { ExecutionStateWrapper = ExecutionStateWrapper }
            });
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
    }
}