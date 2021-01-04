using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;
using SushiShop.Core.Data.Models.Shops;
using SushiShop.Core.ViewModels.Shops.Items;

namespace SushiShop.Core.ViewModels.Shops
{
    public class ShopOnMapViewModel : BasePageViewModel<Shop>
    {
        public override void Prepare(Shop parameter)
        {
            ShopItemViewModel = new ShopItemViewModel(parameter);
        }

        public ShopItemViewModel? ShopItemViewModel { get; private set; }
    }
}