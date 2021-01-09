using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;
using SushiShop.Core.NavigationParameters;
using SushiShop.Core.Resources;
using SushiShop.Core.ViewModels.Shops.Items;

namespace SushiShop.Core.ViewModels.Shops
{
    public class ShopOnMapViewModel : BasePageViewModel<ShopOnMapNavigationParameter>
    {
        public override void Prepare(ShopOnMapNavigationParameter parameter)
        {
            ShopItemViewModel = new ShopItemViewModel(parameter.Shop);
        }

        public ShopItemViewModel? ShopItemViewModel { get; private set; }

        public string Title => AppStrings.OnMap;
    }
}