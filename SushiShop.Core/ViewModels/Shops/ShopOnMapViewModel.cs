using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;
using SushiShop.Core.Data.Models.Shops;
using SushiShop.Core.NavigationParameters;
using SushiShop.Core.Resources;
using SushiShop.Core.ViewModels.Shops.Items;
using System.Threading.Tasks;

namespace SushiShop.Core.ViewModels.Shops
{
    public class ShopOnMapViewModel : BasePageViewModel<ShopOnMapNavigationParameter, Shop>
    {
        private bool isSelectionMode;

        public override void Prepare(ShopOnMapNavigationParameter parameter)
        {
            isSelectionMode = parameter.IsSelectionMode;

            ShopItemViewModel = new ShopItemViewModel(
                parameter.Shop,
                GoToMapAsync,
                ConfirmSelectionAsync,
                isSelectionMode);
        }

        public ShopItemViewModel? ShopItemViewModel { get; private set; }

        public string Title => AppStrings.OnMap;

        private Task ConfirmSelectionAsync(Shop shop)
        {
            return NavigationManager.CloseAsync(this, shop);
        }

        private async Task GoToMapAsync(Shop shop)
        {
            var navigationParameter = new ShopOnMapNavigationParameter(shop, isSelectionMode);
            var selectedShop = await NavigationManager.NavigateAsync<ShopOnMapViewModel, ShopOnMapNavigationParameter, Shop>(navigationParameter);
            if (!isSelectionMode)
            {
                return;
            }

            await NavigationManager.CloseAsync(this, selectedShop);
        }
    }
}