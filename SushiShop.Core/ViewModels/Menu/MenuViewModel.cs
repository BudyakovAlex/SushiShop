using BuildApps.Core.Mobile.MvvmCross.Commands;
using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;
using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract.Items;
using MvvmCross.Commands;
using SushiShop.Core.Data.Models;
using SushiShop.Core.ViewModels.Menu.Items;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SushiShop.Core.ViewModels.Menu
{
    public class MenuViewModel : BaseItemsPageViewModel<BaseViewModel>
    {
        public MenuViewModel()
        {
            SelectCityCommand = new SafeAsyncCommand(ExecutionStateWrapper, SelectCityAsync);
            SwitchPresentationCommand = new MvxCommand(() => IsListMenuPresentation = !IsListMenuPresentation);

            TapToBannerCommand = new SafeAsyncCommand(ExecutionStateWrapper, TapToBannerAsync);
            SelectFranchiseCommand = new SafeAsyncCommand(ExecutionStateWrapper, SelectFranchiseAsync);
            SelectVacanciesCommand = new SafeAsyncCommand(ExecutionStateWrapper, SelectVacanciesAsync);

            MenuItemCollection = new List<MenuItemViewModel>
            {
                new MenuItemViewModel(new MenuItem("Бизнес ланчи", "https://gurmans.dp.ua/giuseppe/7980-large_default/sushi-set-kaliforniya.jpg")),
                new MenuItemViewModel(new MenuItem("Роллы", "https://gurmans.dp.ua/giuseppe/7980-large_default/sushi-set-kaliforniya.jpg")),
                new MenuItemViewModel(new MenuItem("Суши", "https://gurmans.dp.ua/giuseppe/7980-large_default/sushi-set-kaliforniya.jpg")),
                new MenuItemViewModel(new MenuItem("Наборы", "https://gurmans.dp.ua/giuseppe/7980-large_default/sushi-set-kaliforniya.jpg")),
                new MenuItemViewModel(new MenuItem("Супы", "https://gurmans.dp.ua/giuseppe/7980-large_default/sushi-set-kaliforniya.jpg")),
            };
            MenuItemTiledCollection = new List<MenuItemViewModel>
            {
                new MenuItemViewModel(new MenuItem("Наборы", "https://gurmans.dp.ua/giuseppe/7980-large_default/sushi-set-kaliforniya.jpg")),
                new MenuItemViewModel(new MenuItem("Роллы", "https://gurmans.dp.ua/giuseppe/7980-large_default/sushi-set-kaliforniya.jpg")),
                new MenuItemViewModel(new MenuItem("Суши", "https://gurmans.dp.ua/giuseppe/7980-large_default/sushi-set-kaliforniya.jpg")),
                new MenuItemViewModel(new MenuItem("Горячее", "https://gurmans.dp.ua/giuseppe/7980-large_default/sushi-set-kaliforniya.jpg")),
                new MenuItemViewModel(new MenuItem("Бизнес ланчи", "https://gurmans.dp.ua/giuseppe/7980-large_default/sushi-set-kaliforniya.jpg")),
            };
        }

        private bool isListMenuPresentation;
        public bool IsListMenuPresentation
        {
            get => isListMenuPresentation;
            set => SetProperty(ref isListMenuPresentation, value);
        }

        private IEnumerable<MenuItemViewModel> menuItemCollection;
        public IEnumerable<MenuItemViewModel> MenuItemCollection
        {
            get => menuItemCollection;
            set => SetProperty(ref menuItemCollection, value);
        }

        private IEnumerable<MenuItemViewModel> menuItemTiledCollection;
        public IEnumerable<MenuItemViewModel> MenuItemTiledCollection
        {
            get => menuItemTiledCollection;
            set => SetProperty(ref menuItemTiledCollection, value);
        }

        public IMvxCommand SelectCityCommand { get; }
        public IMvxCommand SwitchPresentationCommand { get; }
        public IMvxCommand TapToBannerCommand { get; }
        public IMvxCommand SelectFranchiseCommand { get; }
        public IMvxCommand SelectVacanciesCommand { get; }

        public override Task InitializeAsync()
        {
            Items.AddRange(new List<BaseViewModel>()
            {
                new BannerMenuItemViewModel(new List<MenuItemViewModel>()
                {
                    new MenuItemViewModel(new MenuItem("", "https://gurmans.dp.ua/giuseppe/7980-large_default/sushi-set-kaliforniya.jpg")),
                    new MenuItemViewModel(new MenuItem("", "https://gurmans.dp.ua/giuseppe/7980-large_default/sushi-set-kaliforniya.jpg"))
                }),
                new MenuItemViewModel(new MenuItem("Бизнес ланчи", "https://gurmans.dp.ua/giuseppe/7980-large_default/sushi-set-kaliforniya.jpg")),
                new MenuItemViewModel(new MenuItem("Роллы", "https://gurmans.dp.ua/giuseppe/7980-large_default/sushi-set-kaliforniya.jpg")),
                new MenuItemViewModel(new MenuItem("Суши", "https://gurmans.dp.ua/giuseppe/7980-large_default/sushi-set-kaliforniya.jpg")),
                new MenuItemViewModel(new MenuItem("Наборы", "https://gurmans.dp.ua/giuseppe/7980-large_default/sushi-set-kaliforniya.jpg")),
                new MenuItemViewModel(new MenuItem("Супы", "https://gurmans.dp.ua/giuseppe/7980-large_default/sushi-set-kaliforniya.jpg")),
            });
            return base.InitializeAsync();
        }

        private Task SelectCityAsync()
        {
            return Task.CompletedTask;
        }

        private Task TapToBannerAsync()
        {
            return Task.CompletedTask;
        }

        private Task SelectFranchiseAsync()
        {
            return Task.CompletedTask;
        }

        private Task SelectVacanciesAsync()
        {
            return Task.CompletedTask;
        }
    }
}