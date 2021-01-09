using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using SushiShop.Core.Data.Models.Shops;
using SushiShop.Core.ViewModels.Shops.Items;
using System.Linq;

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

        public void SetShops(Shop[] shops)
        {
            var viewModels = shops.Select(item => new ShopItemViewModel(item)).ToArray();
            Items.ReplaceWith(viewModels);
        }

        private void ItemSelected(ShopItemViewModel shopItemViewModel)
        {
            foreach (var item in Items)
            {
                item.IsSelected = item == shopItemViewModel && !item.IsSelected;
                if (item.IsSelected)
                {
                    SelectedItem = item;
                }
            }

            if (Items.Any(item => item.IsSelected))
            {
                return;
            }

            SelectedItem = null;

        }
    }
}