using BuildApps.Core.Mobile.MvvmCross.UIKit.Views.ViewControllers;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using SushiShop.Core.ViewModels.Products;
using SushiShop.Core.ViewModels.Products.Items;
using SushiShop.Ios.Sources;
using SushiShop.Ios.Views.Cells.Products;

namespace SushiShop.Ios.Views.ViewControllers.Products
{
    [MvxChildPresentation(Animated = true)]
    public partial class ToppingsViewController : BaseViewController<ToppingsViewModel>
    {
        private TableViewSource tableViewSource;

        protected override void InitStylesAndContent()
        {
            base.InitStylesAndContent();
            InitializeTableView();
        }

        protected override void Bind()
        {
            base.Bind();

            var bindingSet = CreateBindingSet();

            bindingSet.Bind(tableViewSource).For(v => v.ItemsSource).To(vm => vm.Items);
            bindingSet.Bind(this).For(v => v.Title).To(vm => vm.Title);

            bindingSet.Apply();
        }

        private void InitializeTableView()
        {
            tableViewSource = new TableViewSource(DataTableView)
                .Register<ToppingItemViewModel>(ToppingItemCell.Nib, ToppingItemCell.Key);

            DataTableView.Source = tableViewSource;
        }
    }
}

