using BuildApps.Core.Mobile.Common.Extensions;
using BuildApps.Core.Mobile.MvvmCross.Commands;
using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using SushiShop.Core.Data.Models;
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
        private City? city;

        public MenuViewModel()
        {
            Items = new MvxObservableCollection<MenuItemViewModel>();
            SimpleItems = new MvxObservableCollection<MenuItemViewModel>();

            SelectCityCommand = new SafeAsyncCommand(ExecutionStateWrapper, SelectCityAsync);
            SwitchPresentationCommand = new MvxCommand(() => IsListMenuPresentation = !IsListMenuPresentation);

            SelectBannerItemCommand = new SafeAsyncCommand(ExecutionStateWrapper, SelectBannerItemAsync);
            OpenFranchiseCommand = new SafeAsyncCommand(ExecutionStateWrapper, OpenFranchiseAsync);
            OpenVacanciesCommand = new SafeAsyncCommand(ExecutionStateWrapper, OpenVacanciesAsync);
        }

        public MvxObservableCollection<MenuItemViewModel> Items { get; }
        public MvxObservableCollection<MenuItemViewModel> SimpleItems { get; }

        public IMvxCommand SelectCityCommand { get; }
        public IMvxCommand SwitchPresentationCommand { get; }
        public IMvxCommand SelectBannerItemCommand { get; }
        public IMvxCommand OpenFranchiseCommand { get; }
        public IMvxCommand OpenVacanciesCommand { get; }

        private bool isListMenuPresentation;
        public bool IsListMenuPresentation
        {
            get => isListMenuPresentation;
            set => SetProperty(ref isListMenuPresentation, value);
        }

        public override async Task InitializeAsync()
        {
            await Task.WhenAll(base.InitializeAsync(), ReloadDataAsync());
        }

        private Task ReloadDataAsync()
        {
            Items.AddRange(
               new List<MenuItemViewModel>
               {
                    new MenuItemViewModel(new MenuItem("Бизнес ланчи", "https://gurmans.dp.ua/giuseppe/7980-large_default/sushi-set-kaliforniya.jpg")),
                    new MenuItemViewModel(new MenuItem("Роллы", "https://gurmans.dp.ua/giuseppe/7980-large_default/sushi-set-kaliforniya.jpg")),
                    new MenuItemViewModel(new MenuItem("Суши", "https://gurmans.dp.ua/giuseppe/7980-large_default/sushi-set-kaliforniya.jpg")),
                    new MenuItemViewModel(new MenuItem("Наборы", "https://gurmans.dp.ua/giuseppe/7980-large_default/sushi-set-kaliforniya.jpg")),
                    new MenuItemViewModel(new MenuItem("Супы", "https://gurmans.dp.ua/giuseppe/7980-large_default/sushi-set-kaliforniya.jpg")),
               });
            SimpleItems.AddRange(
                new List<MenuItemViewModel>
                {
                    new MenuItemViewModel(new MenuItem("Наборы", "https://gurmans.dp.ua/giuseppe/7980-large_default/sushi-set-kaliforniya.jpg")),
                    new MenuItemViewModel(new MenuItem("Роллы", "https://gurmans.dp.ua/giuseppe/7980-large_default/sushi-set-kaliforniya.jpg")),
                    new MenuItemViewModel(new MenuItem("Суши", "https://gurmans.dp.ua/giuseppe/7980-large_default/sushi-set-kaliforniya.jpg")),
                    new MenuItemViewModel(new MenuItem("Горячее", "https://gurmans.dp.ua/giuseppe/7980-large_default/sushi-set-kaliforniya.jpg")),
                    new MenuItemViewModel(new MenuItem("Бизнес ланчи", "https://gurmans.dp.ua/giuseppe/7980-large_default/sushi-set-kaliforniya.jpg")),
                });

            return Task.CompletedTask;
        }

        private async Task SelectCityAsync()
        {
            var navigationParams = new SelectCityNavigationParameters();
            var result = await NavigationManager.NavigateAsync<SelectCityViewModel, SelectCityNavigationParameters, List <CityItemViewModel>>(navigationParams);
            if (result.IsEmpty())
            {
                return;
            }

            city = result.First().City;
        }

        private Task SelectBannerItemAsync()
        {
            return Task.CompletedTask;
        }

        private Task OpenFranchiseAsync()
        {
            return Task.CompletedTask;
        }

        private Task OpenVacanciesAsync()
        {
            return Task.CompletedTask;
        }
    }
}