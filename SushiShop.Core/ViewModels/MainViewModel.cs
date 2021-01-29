using BuildApps.Core.Mobile.Common.Extensions;
using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;
using MvvmCross.Commands;
using SushiShop.Core.Managers.Cart;
using SushiShop.Core.Messages;
using SushiShop.Core.Plugins;
using SushiShop.Core.Providers;
using SushiShop.Core.Resources;
using SushiShop.Core.ViewModels.Cart;
using SushiShop.Core.ViewModels.Info;
using SushiShop.Core.ViewModels.Menu;
using SushiShop.Core.ViewModels.Profile;
using SushiShop.Core.ViewModels.Promotions;
using System.Threading.Tasks;

namespace SushiShop.Core.ViewModels
{
    public class MainViewModel : BasePageViewModel
    {
        private readonly ICartManager cartManager;
        private readonly IUserSession userSession;
        private readonly IDialog dialog;

        public MainViewModel(
            ICartManager cartManager,
            IUserSession userSession,
            IDialog dialog)
        {
            this.cartManager = cartManager;
            this.userSession = userSession;
            this.dialog = dialog;

            LoadTabsCommand = new MvxAsyncCommand(LoadTabsAsync);
            Messenger.Subscribe<RefreshCartMessage>((msg) => OnCartChanged()).DisposeWith(Disposables);
            Messenger.Subscribe<CartProductChangedMessage>((msg) => OnCartChanged()).DisposeWith(Disposables);
        }

        private long cartItemsTotalCount;
        public long CartItemsTotalCount
        {
            get => cartItemsTotalCount;
            set => SetProperty(ref cartItemsTotalCount, value);
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

        public async override Task InitializeAsync()
        {
            await base.InitializeAsync();
            _ = ExecutionStateWrapper.WrapAsync(RefreshDataAsync, awaitWhenBusy: true);
        }

        protected async override Task RefreshDataAsync()
        {
            var cart = await cartManager.GetCartAsync(userSession.GetCity()?.Name);
            if (!cart.IsSuccessful)
            {
                return;
            }

            CartItemsTotalCount = cart.Data?.TotalCount ?? 0;
        }

        private async Task LoadTabsAsync()
        {
            await Task.WhenAll(
                NavigationManager.NavigateAsync<MenuViewModel>(),
                NavigationManager.NavigateAsync<PromotionsViewModel>(),
                NavigationManager.NavigateAsync<CartViewModel>(),
                NavigationManager.NavigateAsync<ProfileViewModel>(),
                NavigationManager.NavigateAsync<InfoViewModel>());
        }

        private void OnCartChanged()
        {
            _ = SafeExecutionWrapper.WrapAsync(RefreshDataAsync);
        }

        protected override void OnHasConnectionChanged(bool hasConnection)
        {
            base.OnHasConnectionChanged(hasConnection);
            if (hasConnection)
            {
                return;
            }

            _ = dialog.ShowToastAsync("No internet connection");
        }
    }
}