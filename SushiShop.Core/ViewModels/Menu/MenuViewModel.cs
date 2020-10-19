using BuildApps.Core.Mobile.MvvmCross.Commands;
using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;
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
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

            SelectBannerItemCommand = new SafeAsyncCommand(ExecutionStateWrapper, SelectBannerItemAsync);
        }

        public MvxObservableCollection<BaseViewModel> Items { get; }
        public MvxObservableCollection<BaseViewModel> SimpleItems { get; }

        public IMvxCommand SelectCityCommand { get; }
        public IMvxCommand SwitchPresentationCommand { get; }
        public IMvxCommand SelectBannerItemCommand { get; }

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

            var groupMenuItemViewModels = response.Data.Stickers.Select(sticker => new GroupMenuItemViewModel(sticker)).ToList();
            var categoryMenuItemViewModels = response.Data.Categories.Select(categories => new CategoryMenuItemViewModel(categories)).ToList();

            Items.Clear();
            SimpleItems.Clear();

            Items.Add(new GroupsMenuItemViewModel(groupMenuItemViewModels));
            Items.AddRange(categoryMenuItemViewModels);

            SimpleItems.AddRange(categoryMenuItemViewModels);
            SimpleItems.Add(new GroupsMenuItemViewModel(groupMenuItemViewModels));
            SimpleItems.AddRange(new List<MenuActionItemViewModel>
                {
                    new MenuActionItemViewModel(ActionType.Franchise),
                    new MenuActionItemViewModel(ActionType.Vacancies)
                });
        }

        private async Task SelectCityAsync()
        {
            var navigationParams = new SelectCityNavigationParameters();
            var result = await NavigationManager.NavigateAsync<SelectCityViewModel, SelectCityNavigationParameters, List<CityItemViewModel>?>(navigationParams);
            if (result is null)
            {
                return;
            }

            city = result.First().City;
            await RaisePropertyChanged(nameof(CityName));
        }

        private Task SelectBannerItemAsync()
        {
            return Task.CompletedTask;
        }
    }
}