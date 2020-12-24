using BuildApps.Core.Mobile.MvvmCross.UIKit.Views.ViewControllers;
using MvvmCross.Platforms.Ios.Binding;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using SushiShop.Core.ViewModels.Orders;
using SushiShop.Core.ViewModels.Orders.Items;
using SushiShop.Ios.Sources;
using SushiShop.Ios.Views.Cells.Orders;
using UIKit;

namespace SushiShop.Ios.Views.ViewControllers.Orders
{
    [MvxChildPresentation]
    public partial class MyOrdersViewController : BaseViewController<MyOrdersViewModel>
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
            bindingSet.Bind(LoadingIndicator).For(v => v.BindVisible()).To(vm => vm.IsLoading);

            bindingSet.Apply();
        }

        private void InitializeTableView()
        {
            _source = new TableViewSource(TableView)
                .Register<OrderItemViewModel>(OrderItemViewCell.Nib, OrderItemViewCell.Key);

            TableView.Source = _source;
            TableView.RowHeight = OrderItemViewCell.Height;
            TableView.ContentInset = new UIEdgeInsets(6f, 0f, 0f, 0f);
        }
    }
}

