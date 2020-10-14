using System.Threading.Tasks;
using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;
using MvvmCross.Commands;
using SushiShop.Core.Resources;
using SushiShop.Core.ViewModels.Cart;
using SushiShop.Core.ViewModels.Info;
using SushiShop.Core.ViewModels.Menu;
using SushiShop.Core.ViewModels.Profile;
using SushiShop.Core.ViewModels.Promotions;

namespace SushiShop.Core.ViewModels
{
    public class MainViewModel : BasePageViewModel
    {
        public MainViewModel()
        {
            LoadTabsCommand = new MvxAsyncCommand(LoadTabsAsync);
        }

        public IMvxAsyncCommand LoadTabsCommand { get; }

        public string[] TabNames { get; } = new string[]
        {
            AppStrings.Menu,
            AppStrings.Promotions,
            AppStrings.Cart,
            AppStrings.Profile,
            AppStrings.Info
        };

        private async Task LoadTabsAsync()
        {
            await Task.WhenAll(
                NavigationManager.NavigateAsync<MenuViewModel>(),
                NavigationManager.NavigateAsync<PromotionsViewModel>(),
                NavigationManager.NavigateAsync<CartViewModel>(),
                NavigationManager.NavigateAsync<ProfileViewModel>(),
                NavigationManager.NavigateAsync<InfoViewModel>());
        }
    }
}
