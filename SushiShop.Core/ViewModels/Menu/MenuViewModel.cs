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
        }

        private bool isListMenuPresentation;

        public bool IsListMenuPresentation
        {
            get => isListMenuPresentation;
            set => SetProperty(ref isListMenuPresentation, value);
        }

        public IMvxCommand SelectCityCommand { get; }
        public IMvxCommand SwitchPresentationCommand { get; }

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
    }
}