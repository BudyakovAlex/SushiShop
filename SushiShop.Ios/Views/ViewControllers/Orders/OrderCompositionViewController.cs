using BuildApps.Core.Mobile.MvvmCross.UIKit.Views.ViewControllers;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using SushiShop.Core.ViewModels.Orders;
using SushiShop.Core.ViewModels.Orders.Items;
using SushiShop.Ios.Sources;
using SushiShop.Ios.Views.Cells.Orders;

namespace SushiShop.Ios.Views.ViewControllers.Orders
{
    [MvxChildPresentation]
    public partial class OrderCompositionViewController : BaseViewController<OrderCompositionViewModel>
    {
        private TableViewSource _source;

        protected override void InitStylesAndContent()
        {
            base.InitStylesAndContent();
            InitializeTableView();
        }

        protected override void Bind()
        {
            base.Bind();

            var bindingSet = CreateBindingSet();

            bindingSet.Bind(this).For(v => v.Title).To(vm => vm.Title);
            bindingSet.Bind(_source).For(v => v.ItemsSource).To(vm => vm.Items);
            
            bindingSet.Apply();
        }

        private void InitializeTableView()
        {
            _source = new TableViewSource(TableView)
                .Register<OrderProductItemViewModel>(OrderProductItemViewCell.Nib, OrderProductItemViewCell.Key);

            TableView.Source = _source;
            TableView.RowHeight = OrderProductItemViewCell.Height;
        }
    }
}
