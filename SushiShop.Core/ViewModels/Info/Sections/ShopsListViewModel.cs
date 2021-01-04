using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;
using MvvmCross.ViewModels;
using SushiShop.Core.ViewModels.Info.Items;

namespace SushiShop.Core.ViewModels.Info.Sections
{
    public class ShopsListViewModel : BaseViewModel
    {
        public ShopsListViewModel()
        {
            Items = new MvxObservableCollection<ShopItemViewModel>();
        }

        public MvxObservableCollection<ShopItemViewModel> Items { get; }
    }
}