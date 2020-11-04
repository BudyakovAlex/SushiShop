using BuildApps.Core.Mobile.MvvmCross.UIKit.Views.ViewControllers;
using MvvmCross.Platforms.Ios.Binding;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using SushiShop.Core.Resources;
using SushiShop.Core.ViewModels.CardProduct.Items;
using SushiShop.Core.ViewModels.ProductDetails;
using SushiShop.Ios.Common;
using SushiShop.Ios.Common.Styles;
using SushiShop.Ios.Sources;
using SushiShop.Ios.Views.Cells.Products;
using UIKit;

namespace SushiShop.Ios.Views.ViewControllers.ProductDetails
{
    [MvxModalPresentation(
        WrapInNavigationController = true,
        Animated = true,
        ModalPresentationStyle = UIModalPresentationStyle.OverFullScreen)]
    public partial class ToppingsViewController : BaseViewController<ToppingsViewModel>
    {
        private TableViewSource tableViewSource;

        protected override void InitStylesAndContent()
        {
            base.InitStylesAndContent();

            InitializeTableView();

            AddButton.SetCornerRadius();
            DataTableView.ContentInset = new UIEdgeInsets(0, 0, 82, 0);
        }

        protected override void Bind()
        {
            base.Bind();

            var bindingSet = CreateBindingSet();

            bindingSet.Bind(tableViewSource).For(v => v.ItemsSource).To(vm => vm.Items);
            bindingSet.Bind(NavigationItem.LeftBarButtonItem).For(v => v.BindClicked()).To(vm => vm.PlatformCloseCommand);
            bindingSet.Bind(AddButton).For(v => v.BindTap()).To(vm => vm.AddToCartCommand);
            bindingSet.Bind(NavigationItem.RightBarButtonItem).For(v => v.BindClicked()).To(vm => vm.ResetCommand);
            bindingSet.Bind(this).For(v => v.Title).To(vm => vm.PageTitle);

            bindingSet.Apply();
        }

        protected override void InitNavigationItem(UINavigationItem navigationItem)
        {
            base.InitNavigationItem(navigationItem);

            navigationItem.LeftBarButtonItem = Components.CreateBarButtonItem(ImageNames.ArrowBack);
            navigationItem.RightBarButtonItem = Components.CreateBarButtonItemText(AppStrings.Discard);
        }

        private void InitializeTableView()
        {
            tableViewSource = new TableViewSource(DataTableView)
                .Register<ToppingItemViewModel>(ToppingItemCell.Nib, ToppingItemCell.Key);

            DataTableView.Source = tableViewSource;
        }
    }
}