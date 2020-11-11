using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;
using MvvmCross.ViewModels;
using SushiShop.Core.ViewModels.Menu.Items;

namespace SushiShop.Core.ViewModels.Menu
{
    public class FilteredProductsViewModel : BaseViewModel
    {
        public FilteredProductsViewModel(ProductItemViewModel[] productItemViewModels)
        {
            Items = new MvxObservableCollection<ProductItemViewModel>(productItemViewModels);
        }

        public MvxObservableCollection<ProductItemViewModel> Items { get; }
    }
}