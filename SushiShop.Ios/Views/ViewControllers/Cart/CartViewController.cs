using BuildApps.Core.Mobile.MvvmCross.UIKit.Views.ViewControllers;
using CoreFoundation;
using CoreGraphics;
using MvvmCross.Platforms.Ios.Binding;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using SushiShop.Core.Converters;
using SushiShop.Core.Resources;
using SushiShop.Core.ViewModels.Cart;
using SushiShop.Core.ViewModels.Cart.Items;
using SushiShop.Ios.Common;
using SushiShop.Ios.Sources;
using SushiShop.Ios.Views.Cells.Cart;
using SushiShop.Ios.Views.Controls;
using System.Linq;
using UIKit;

namespace SushiShop.Ios.Views.ViewControllers.Cart
{
    [MvxTabPresentation(WrapInNavigationController = true)]
    public partial class CartViewController : BaseViewControllerWithKeyboard<CartViewModel>
    {
        private const int MainViewTabIndex = 0;

        private TableViewSource productsTableViewSource;
        private TableViewSource toppingsTableViewSource;
        private TableViewSource packagesTableViewSource;
        private UIView footerView;

        protected override bool HandlesKeyboardNotifications => true;

        private bool isHideFooterView;
        public bool IsHideFooterView
        {
            get => isHideFooterView;
            set
            {
                isHideFooterView = value;
                ProductsTableView.TableFooterView = isHideFooterView ? null : footerView;
            }
        }

        protected override void InitNavigationItem(UINavigationItem navigationItem)
        {
            base.InitNavigationItem(navigationItem);

            navigationItem.Title = AppStrings.Cart;
        }

        protected override void InitStylesAndContent()
        {
            base.InitStylesAndContent();

            InitializeProductsTableView();
            InitializeSausesTableView();
            InitializePackagesTableView();
            SetFooterViewForTableViews();

            GoToMenuButton.AddGestureRecognizer(new UITapGestureRecognizer(OnGoToMenuButtonTapped));

            PromocodeTextField.Placeholder = AppStrings.Promocode;
            PromocodeTextField.InputAccessoryView = new DoneAccessoryView(View, () => ViewModel?.ApplyPromocodeCommand?.Execute());
        }

        protected override void Bind()
        {
            base.Bind();

            var bindingSet = CreateBindingSet();

            bindingSet.Bind(productsTableViewSource).For(v => v.ItemsSource).To(vm => vm.Products);
            bindingSet.Bind(toppingsTableViewSource).For(v => v.ItemsSource).To(vm => vm.Sauces);
            bindingSet.Bind(packagesTableViewSource).For(v => v.ItemsSource).To(vm => vm.Packages);
            bindingSet.Bind(AddSauseView).For(v => v.BindTap()).To(vm => vm.AddSaucesCommand);
            bindingSet.Bind(PriceLabel).For(v => v.Text).To(vm => vm.TotalPrice);
            bindingSet.Bind(PromocodeTextField).For(v => v.Text).To(vm => vm.Promocode);
            bindingSet.Bind(BottomCheckoutView).For(v => v.BindVisibility()).To(vm => vm.IsEmptyBasket);
            bindingSet.Bind(CheckoutButton).For(v => v.BindTap()).To(vm => vm.CheckoutCommand);
            bindingSet.Bind(ContentScrollView).For(v => v.BindVisibility()).To(vm => vm.IsEmptyBasket);
            bindingSet.Bind(this).For(v => v.IsHideFooterView).To(vm => vm.Sauces.Count)
               .WithConversion<AmountToBoolInvertVisibilityConverter>();

            bindingSet.Apply();
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);

            UIApplication.SharedApplication.KeyWindow.EndEditing(true);
        }

        private void InitializeProductsTableView()
        {
            productsTableViewSource = new TableViewSource(ProductsTableView)
                .Register<CartProductItemViewModel>(CartProductViewCell.Nib, CartProductViewCell.Key);
            ProductsTableView.Source = productsTableViewSource;
        }

        private void InitializeSausesTableView()
        {
            toppingsTableViewSource = new TableViewSource(ToppingsTableView)
                .Register<CartToppingItemViewModel>(CartToppingViewCell.Nib, CartToppingViewCell.Key);
            ToppingsTableView.Source = toppingsTableViewSource;
        }

        private void InitializePackagesTableView()
        {
            packagesTableViewSource = new TableViewSource(PackagesTableView)
                .Register<CartPackItemViewModel>(PackageViewCell.Nib, PackageViewCell.Key);
            PackagesTableView.Source = packagesTableViewSource;
        }

        private void SetFooterViewForTableViews()
        {
            footerView = new UIView(new CGRect(0, 0, ProductsTableView.Frame.Width, 1)) { BackgroundColor = UIColor.White };
            ToppingsTableView.TableFooterView = footerView;
            PackagesTableView.TableFooterView = new UIView(new CGRect(0, 0, ProductsTableView.Frame.Width, 1)) { BackgroundColor = Colors.Background };
        }

        private void OnGoToMenuButtonTapped()
        {
            if (UIApplication.SharedApplication.Windows.FirstOrDefault()?.RootViewController is MainViewController mainViewController)
            {
                mainViewController.TabIndex = MainViewTabIndex;
            }
        }
    }
}
