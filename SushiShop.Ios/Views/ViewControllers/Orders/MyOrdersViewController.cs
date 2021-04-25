using BuildApps.Core.Mobile.MvvmCross.UIKit.Views.ViewControllers;
using MvvmCross.Platforms.Ios.Binding;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using MvvmCross.Platforms.Ios.Views;
using MvvmCross.ViewModels;
using SushiShop.Core.ViewModels.Orders;
using SushiShop.Core.ViewModels.Orders.Items;
using SushiShop.Ios.Common;
using SushiShop.Ios.Sources;
using SushiShop.Ios.Views.Cells.Orders;
using System.Linq;
using UIKit;

namespace SushiShop.Ios.Views.ViewControllers.Orders
{
    [MvxChildPresentation]
    public partial class MyOrdersViewController : BaseViewController<MyOrdersViewModel>
    {
        private TableViewSource _source;
        private MvxUIRefreshControl refreshControl;

        private MvxInteraction _goToCartInteraction;
        public MvxInteraction GoToCartInteraction
        {
            get => _goToCartInteraction;
            set
            {
                if (_goToCartInteraction != null)
                {
                    _goToCartInteraction.Requested -= OnGoToCartInteractionRequested;
                }

                _goToCartInteraction = value;
                _goToCartInteraction.Requested += OnGoToCartInteractionRequested;
            }
        }

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
            bindingSet.Bind(_source).For(v => v.LoadMoreCommand).To(vm => vm.Pagination.LoadMoreItemsCommand);
            bindingSet.Bind(LoadingIndicator).For(v => v.BindVisible()).To(vm => vm.IsLoading);
            bindingSet.Bind(this).For(v => v.GoToCartInteraction).To(vm => vm.GoToCartInteraction);
            bindingSet.Bind(refreshControl).For(v => v.RefreshCommand).To(vm => vm.RefreshDataCommand);
            bindingSet.Bind(refreshControl).For(v => v.IsRefreshing).To(vm => vm.IsRefreshing);

            bindingSet.Apply();
        }

        private void InitializeTableView()
        {
            _source = new TableViewSource(TableView)
                .Register<OrderItemViewModel>(OrderItemViewCell.Nib, OrderItemViewCell.Key);

            TableView.Source = _source;
            TableView.RowHeight = OrderItemViewCell.Height;
            TableView.ContentInset = new UIEdgeInsets(6f, 0f, 0f, 0f);

            refreshControl = new MvxUIRefreshControl();
            TableView.RefreshControl = refreshControl;
        }

        private void OnGoToCartInteractionRequested(object sender, System.EventArgs e)
        {
            if (UIApplication.SharedApplication.Windows.FirstOrDefault()?.RootViewController is MainViewController mainViewController)
            {
                mainViewController.TabIndex = Constants.UI.CartViewTabIndex;
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (_goToCartInteraction != null && disposing)
            {
                _goToCartInteraction.Requested -= OnGoToCartInteractionRequested;
            }
        }
    }
}