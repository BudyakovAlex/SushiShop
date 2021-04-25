using BuildApps.Core.Mobile.MvvmCross.Commands;
using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;
using MvvmCross.ViewModels;
using SushiShop.Core.ViewModels.Menu.Items;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SushiShop.Core.ViewModels.Menu
{
    public class FilteredProductsViewModel : BaseViewModel
    {
        public FilteredProductsViewModel(ProductItemViewModel[] productItemViewModels, Func<Task> refreshDataFunc)
        {
            Items = new MvxObservableCollection<ProductItemViewModel>(productItemViewModels);

            RefreshDataCommand = new SafeAsyncCommand(ExecutionStateWrapper, refreshDataFunc);
        }

        public MvxObservableCollection<ProductItemViewModel> Items { get; }

        public ICommand RefreshDataCommand { get; }

        public bool IsRefreshing => ExecutionStateWrapper.IsBusy;

        protected override void OnIsBusyWrapperChanged()
        {
            RaisePropertyChanged(nameof(IsRefreshing));
        }
    }
}