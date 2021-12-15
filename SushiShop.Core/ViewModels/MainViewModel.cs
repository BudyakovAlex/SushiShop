using Acr.UserDialogs;
using BuildApps.Core.Mobile.Common.Extensions;
using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;
using MvvmCross.Commands;
using SushiShop.Core.Managers.Cart;
using SushiShop.Core.Managers.CommonInfo;
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
using Xamarin.Essentials;

namespace SushiShop.Core.ViewModels
{
    public class MainViewModel : BasePageViewModel
    {
        private const int ViewModelLoadingDelayMs = 2500;

        private readonly ICartManager cartManager;
        private readonly ICommonInfoManager commonInfoManager;
        private readonly IUserSession userSession;
        private readonly IDialog dialog;
        private readonly IUserDialogs userDialogs;

        public MainViewModel(
            ICartManager cartManager,
            ICommonInfoManager commonInfoManager,
            IUserSession userSession,
            IDialog dialog,
            IUserDialogs userDialogs)
        {
            this.cartManager = cartManager;
            this.commonInfoManager = commonInfoManager;
            this.userSession = userSession;
            this.dialog = dialog;
            this.userDialogs = userDialogs;
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

            _ = ShowApplocationUpdateConfirmationIfNeededAsync();

            if (HasConnection)
            {
                return;
            }

            ShowNoInternetConeectionToast();
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
                dialog.DismissToast();
                return;
            }

            ShowNoInternetConeectionToast();
        }

        private void ShowNoInternetConeectionToast()
        {
            _ = dialog.ShowToastAsync(AppStrings.NoInternetConnection, true);
        }

        private async Task ShowApplocationUpdateConfirmationIfNeededAsync()
        {
            await Task.Delay(ViewModelLoadingDelayMs);

            var applicationInformationResult = await commonInfoManager.GetApplicationInformationAsync();
            if (!applicationInformationResult.IsSuccessful ||
                !applicationInformationResult.Data.ShouldUpdate)
            {
                return;
            }

            //HACK: to avoid design issue
            var shouldUpdate = await userDialogs.ConfirmAsync(string.Empty, applicationInformationResult.Data.Message, cancelText: AppStrings.Yes, okText: AppStrings.No);
            if (shouldUpdate)
            {
                return;
            }

            var updateAppUrl = DeviceInfo.Platform == DevicePlatform.Android
                ? applicationInformationResult.Data.Platforms.Android.Url
                : applicationInformationResult.Data.Platforms.Ios.Url;

            await Browser.OpenAsync(updateAppUrl, BrowserLaunchMode.External);
        }
    }
}