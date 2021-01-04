using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using SushiShop.Core.ViewModels.Shops.Items;

namespace SushiShop.Core.ViewModels.Shops.Sections
{
    public class ShopsOnMapSectionViewModel : BaseViewModel
    {
        public ShopsOnMapSectionViewModel()
        {
            Items = new MvxObservableCollection<ShopItemViewModel>();

            SelectItemCommand = new MvxCommand<ShopItemViewModel>(ItemSelected);
        }

        public IMvxCommand<ShopItemViewModel> SelectItemCommand { get; }

        public MvxObservableCollection<ShopItemViewModel> Items { get; }

        private ShopItemViewModel? selectedItem;
        public ShopItemViewModel? SelectedItem
        {
            get => selectedItem;
            set => SetProperty(ref selectedItem, value);
        }

        private void ItemSelected(ShopItemViewModel shopItemViewModel)
        {
            foreach (var item in Items)
            {
                item.IsSelected = item == shopItemViewModel && !item.IsSelected;
            }
        }
    }
}