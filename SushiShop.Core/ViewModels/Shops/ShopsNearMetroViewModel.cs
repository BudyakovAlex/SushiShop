using Acr.UserDialogs;
using BuildApps.Core.Mobile.Common.Extensions;
using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract.Items;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using SushiShop.Core.Data.Models.Shops;
using SushiShop.Core.Managers.Shops;
using SushiShop.Core.NavigationParameters;
using SushiShop.Core.Plugins;
using SushiShop.Core.Providers;
using SushiShop.Core.ViewModels.Shops.Items;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SushiShop.Core.ViewModels.Shops
{
    public class ShopsNearMetroViewModel : BaseItemsPageViewModel<ShopItemViewModel, ShopsNearMetroNavigationParameters, Shop>
    {
        private readonly IShopsManager shopsManager;
        private readonly IUserSession userSession;
        private readonly IDialog dialog;

        private Dictionary<string, MetroShop[]>? metroShopsMappings;

        private bool isSelectionMode;

        public ShopsNearMetroViewModel(IShopsManager shopsManager, IUserSession userSession, IDialog dialog)
        {
            this.shopsManager = shopsManager;
            this.userSession = userSession;
            this.dialog = dialog;

            NearestMetro = new MvxObservableCollection<MetroItemViewModel>();
            NearestMetro.SubscribeToCollectionChanged(OnNearestMetroCollectionChanged).DisposeWith(Disposables);

            ClearNearestMetroCommand = new MvxCommand(() => NearestMetro.Clear());
        }

        public ICommand ClearNearestMetroCommand { get; }

        public bool IsNearestMetroNotEmpty => NearestMetro.Count > 0;

        public string? Title { get; private set; }

        public MvxObservableCollection<MetroItemViewModel> NearestMetro { get; }

        public override void Prepare(ShopsNearMetroNavigationParameters parameter)
        {
            isSelectionMode = parameter.IsSelectionMode;

            var viewModels = parameter.Shops.Select(item => new ShopItemViewModel(
                item.Shop!,
                GoToMapAsync,
                ConfirmSelectionAsync,
                isSelectionMode,
                showNearestMetroAction: ShowNearestMetro)).ToArray();

            Title = parameter.Title;
            Items.AddRange(viewModels);
        }

        public override async Task InitializeAsync()
        {
            await base.InitializeAsync();

            var city = userSession.GetCity();
            var response = await shopsManager.GetMetroShopsAsync(city?.Name);
            if (!response.IsSuccessful)
            {
                var error = response.Errors.FirstOrDefault();
                if (error.IsNotNullNorEmpty())
                {
                    await dialog.ShowToastAsync(error);
                }

                return;
            }

            metroShopsMappings = response.Data;
        }

        protected override Task CloseAsync(bool? isPlatform)
        {
            NearestMetro.Clear();
            return base.CloseAsync(isPlatform);
        }

        private async Task GoToMapAsync(Shop shop)
        {
            var navigationParameter = new ShopOnMapNavigationParameter(shop, isSelectionMode);
            var selectedShop = await NavigationManager.NavigateAsync<ShopOnMapViewModel, ShopOnMapNavigationParameter, Shop>(navigationParameter);
            if (!isSelectionMode ||
                selectedShop is null)
            {
                return;
            }

            NearestMetro.Clear();
            await NavigationManager.CloseAsync(this, selectedShop);
        }

        private async Task GoToShopAsync(MetroItemViewModel itemViewModel)
        {
            NearestMetro.Clear();

            var shops = metroShopsMappings?.GetValueOrDefault(itemViewModel.Text);
            if (shops is null || shops.Length == 0)
            {
                return;
            }

            var navigationParameter = new ShopsNearMetroNavigationParameters(shops, Title, isSelectionMode);
            var selectedShop = await NavigationManager.NavigateAsync<ShopsNearMetroViewModel, ShopsNearMetroNavigationParameters, Shop>(navigationParameter);
            if (!isSelectionMode ||
                selectedShop is null)
            {
                return;
            }

            NearestMetro.Clear();
            await NavigationManager.CloseAsync(this, selectedShop);
        }

        private Task ConfirmSelectionAsync(Shop shop)
        {
            NearestMetro.Clear();
            return NavigationManager.CloseAsync(this, shop);
        }

        private void ShowNearestMetro(Shop shop)
        {
            var metroViewModels = shop.Metro.Where(metro => metro.Name != Title)
                                            .Select(metro => new MetroItemViewModel(metro.Name!, GoToShopAsync))
                                            .ToArray();
            NearestMetro.ReplaceWith(metroViewModels);
        }

        private void OnNearestMetroCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            RaisePropertyChanged(nameof(IsNearestMetroNotEmpty));
        }
    }
}