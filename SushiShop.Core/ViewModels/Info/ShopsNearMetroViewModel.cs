using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract.Items;
using SushiShop.Core.ViewModels.Info.Items;
using SushiShop.Core.ViewModels.Info.Sections;

namespace SushiShop.Core.ViewModels.Info
{
    public class ShopsNearMetroViewModel : BaseItemsPageViewModel<ShopItemViewModel>
    {
        public ShopsNearMetroViewModel()
        {
            MetroSectionViewModel = new MetroSectionViewModel();
        }

        public MetroSectionViewModel MetroSectionViewModel { get; }
    }
}