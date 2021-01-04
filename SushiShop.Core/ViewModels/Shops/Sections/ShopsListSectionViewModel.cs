using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;
using MvvmCross.ViewModels;
using SushiShop.Core.ViewModels.Shops.Items;

namespace SushiShop.Core.ViewModels.Shops.Sections
{
    public class ShopsListSectionViewModel : BaseViewModel
    {
        public ShopsListSectionViewModel()
        {
            Items = new MvxObservableCollection<ShopItemViewModel>();
        }

        public MvxObservableCollection<ShopItemViewModel> Items { get; }
    }
}