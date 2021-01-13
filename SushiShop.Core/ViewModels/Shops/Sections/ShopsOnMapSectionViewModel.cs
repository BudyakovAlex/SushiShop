using BuildApps.Core.Mobile.MvvmCross.Extensions;
using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using SushiShop.Core.Data.Models.Common;
using SushiShop.Core.Data.Models.Shops;
using SushiShop.Core.ViewModels.Shops.Items;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SushiShop.Core.ViewModels.Shops.Sections
{
    public class ShopsOnMapSectionViewModel : BaseViewModel
    {
        private readonly Func<Shop, Task> goToMapFunc;
        private readonly Func<Shop, Task> confirmSelectionFunc;
        private readonly bool isSelectionMode;

        public ShopsOnMapSectionViewModel(
            Coordinates centerCoordinates,
            float zoomFactor,
            Func<Shop, Task> goToMapFunc,
            Func<Shop, Task> confirmSelectionFunc,
            bool isSelectionMode)
        {
            Items = new MvxObservableCollection<ShopItemViewModel>();

            this.goToMapFunc = goToMapFunc;
            this.confirmSelectionFunc = confirmSelectionFunc;
            this.isSelectionMode = isSelectionMode;

            CenterCoordinates = centerCoordinates;
            ZoomFactor = zoomFactor;

            SelectItemCommand = new MvxCommand<ShopItemViewModel>(ItemSelected);
            ConfirmSelectionCommand = new MvxAsyncCommand(ConfirmSelectionAsync, () => isSelectionMode && SelectedItem != null);
        }

        public IMvxCommand<ShopItemViewModel> SelectItemCommand { get; }

        public ICommand ConfirmSelectionCommand { get; }

        public MvxObservableCollection<ShopItemViewModel> Items { get; }

        private ShopItemViewModel? selectedItem;
        public ShopItemViewModel? SelectedItem
        {
            get => selectedItem;
            set => SetProperty(ref selectedItem, value, ConfirmSelectionCommand.RaiseCanExecuteChanged);
        }

        public Coordinates CenterCoordinates { get; }

        public float ZoomFactor { get; }

        public void SetShops(Shop[] shops)
        {
            var viewModels = shops.Select(item => new ShopItemViewModel(
                item,
                goToMapFunc,
                confirmSelectionFunc,
                isSelectionMode)).ToArray();

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

        private Task ConfirmSelectionAsync()
        {
            if (SelectedItem is null)
            {
                return Task.CompletedTask;
            }

            return confirmSelectionFunc?.Invoke(SelectedItem.Key) ?? Task.CompletedTask;
        }
    }
}