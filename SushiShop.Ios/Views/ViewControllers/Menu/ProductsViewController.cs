using BuildApps.Core.Mobile.MvvmCross.UIKit.Views.ViewControllers;
using CoreGraphics;
using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Binding;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using SushiShop.Core.ViewModels.Menu;
using SushiShop.Ios.Common;
using SushiShop.Ios.Delegates;
using SushiShop.Ios.Extensions;
using SushiShop.Ios.Sources;
using SushiShop.Ios.Views.Controls;
using UIKit;

namespace SushiShop.Ios.Views.ViewControllers.Menu
{
    [MvxChildPresentation]
    public partial class ProductsViewController : BaseViewController<ProductsViewModel>
    {
        private MainViewController rootViewController = (MainViewController) UIApplication.SharedApplication.KeyWindow.RootViewController;

        private UIStackView stackView;
        private ScrollableTabView filterTabView;
        private UICollectionView collectionView;
        private UIActivityIndicatorView loadingIndicator;
        private ProductsCollectionViewSource source;

        private bool isAppeared;

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            isAppeared = true;
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);

            isAppeared = false;
            rootViewController.ShowTabView();
        }

        protected override void InitStylesAndContent()
        {
            base.InitStylesAndContent();

            View.BackgroundColor = Colors.White;

            InitializeStackView();
            InitializeFilterTabView();
            InitializeCollectionView();
            InitializeLoadingIndicator();
        }

        protected override void Bind()
        {
            base.Bind();

            var bindingSet = this.CreateBindingSet<ProductsViewController, ProductsViewModel>();

            bindingSet.Bind(this).For(v => v.Title).To(vm => vm.Title);
            bindingSet.Bind(filterTabView).For(v => v.Items).To(vm => vm.Filters);
            bindingSet.Bind(filterTabView).For(v => v.SelectedIndex).To(vm => vm.SelectedFilterIndex);
            bindingSet.Bind(filterTabView).For(v => v.BindVisible()).To(vm => vm.IsFiltersVisible);
            bindingSet.Bind(source).For(v => v.ItemsSource).To(vm => vm.Items);
            bindingSet.Bind(loadingIndicator).For(v => v.BindVisible()).To(vm => vm.ExecutionStateWrapper.IsBusy);

            bindingSet.Apply();
        }

        private void InitializeStackView()
        {
            var safeAreaInsets = UIApplication.SharedApplication.KeyWindow.SafeAreaInsets;
            var navigationBarHeight = NavigationController.NavigationBar.Bounds.Height;
            var width = View.Bounds.Width;
            var height = View.Bounds.Height - safeAreaInsets.Top - navigationBarHeight;

            stackView = new UIStackView(new CGRect(0f, 0f, width, height))
            {
                Axis = UILayoutConstraintAxis.Vertical,
                Distribution = UIStackViewDistribution.Fill,
                Alignment = UIStackViewAlignment.Fill
            };

            View.AddSubview(stackView);
        }

        private void InitializeFilterTabView()
        {
            filterTabView = new ScrollableTabView();
            filterTabView.TranslatesAutoresizingMaskIntoConstraints = false;
            filterTabView.OnTabChangedAfterTapAction = OnTabChangedAfterTap;
            filterTabView.HeightAnchor.ConstraintEqualTo(44f).Active = true;

            stackView.AddArrangedSubview(filterTabView);
        }

        private void InitializeCollectionView()
        {
            var layout = new UICollectionViewFlowLayout { ScrollDirection = UICollectionViewScrollDirection.Horizontal };
            collectionView = new UICollectionView(CGRect.Empty, layout)
            {
                ShowsHorizontalScrollIndicator = false,
                PagingEnabled = true,
                BackgroundColor = Colors.Background
            };

            source = new ProductsCollectionViewSource(collectionView, () => isAppeared);
            collectionView.Source = source;
            collectionView.Delegate = new ProductsCollectionViewDelegateFlowLayout(OnDecelerated);

            stackView.AddArrangedSubview(collectionView);
        }

        private void InitializeLoadingIndicator()
        {
            loadingIndicator = new UIActivityIndicatorView();
            loadingIndicator.TranslatesAutoresizingMaskIntoConstraints = false;
            loadingIndicator.StartAnimating();

            View.AddSubview(loadingIndicator);

            NSLayoutConstraint.ActivateConstraints(new[]
            {
                loadingIndicator.CenterXAnchor.ConstraintEqualTo(View.CenterXAnchor),
                loadingIndicator.CenterYAnchor.ConstraintEqualTo(View.CenterYAnchor)
            });
        }

        private void OnTabChangedAfterTap()
        {
            var indexPath = NSIndexPath.FromRowSection(filterTabView.SelectedIndex, 0);
            collectionView.ScrollToItem(indexPath, UICollectionViewScrollPosition.CenteredHorizontally, true);

            rootViewController.ShowTabView();
        }

        private void OnDecelerated()
        {
            var indexPath = collectionView.GetCenterIndexPathOrDefault();
            if (indexPath != null && filterTabView.SelectedIndex != indexPath.Row)
            {
                filterTabView.SelectedIndex = indexPath.Row;
                rootViewController.ShowTabView();
            }
        }
    }
}
