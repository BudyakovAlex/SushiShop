using System;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Views.Cells;
using Foundation;
using MvvmCross.Binding.BindingContext;
using SushiShop.Core.ViewModels.Menu;
using SushiShop.Core.ViewModels.Menu.Items;
using SushiShop.Ios.Common;
using SushiShop.Ios.Delegates;
using SushiShop.Ios.Sources;
using SushiShop.Ios.Views.ViewControllers;
using UIKit;

namespace SushiShop.Ios.Views.Cells.Menu
{
    public partial class FilteredProductsItemViewCell : BaseCollectionViewCell
    {
        public static readonly NSString Key = new NSString(nameof(FilteredProductsItemViewCell));
        public static readonly UINib Nib = UINib.FromName(Key, NSBundle.MainBundle);

        private MainViewController rootViewController = (MainViewController) UIApplication.SharedApplication.KeyWindow.RootViewController;
        private CollectionViewSource source;
        private int scrollY;

        protected FilteredProductsItemViewCell(IntPtr handle)
            : base(handle)
        {
        }

        public Func<bool> ListenScrollChanges { get; set; }

        private bool ShouldHandleScrolling => ListenScrollChanges?.Invoke() ?? true;

        public override void PrepareForReuse()
        {
            base.PrepareForReuse();
            scrollY = (int) ProductsCollectionView.ContentOffset.Y;
        }

        protected override void Initialize()
        {
            base.Initialize();

            BackgroundColor = Colors.Background;
            ContentView.BackgroundColor = Colors.Background;
            ProductsCollectionView.BackgroundColor = Colors.Background;

            InitializeCollectionView();
        }

        protected override void Bind()
        {
            base.Bind();

            var bindingSet = this.CreateBindingSet<FilteredProductsItemViewCell, FilteredProductsViewModel>();
            bindingSet.Bind(source).For(v => v.ItemsSource).To(vm => vm.Items);
            bindingSet.Apply();
        }

        private void InitializeCollectionView()
        {
            source = new CollectionViewSource(ProductsCollectionView)
                .Register<ProductItemViewModel>(ProductItemViewCell.Nib, ProductItemViewCell.Key);

            ProductsCollectionView.Source = source;
            ProductsCollectionView.Delegate = new FilteredProductsCollectionViewDelegateFlowLayout(OnScrolled);
            ProductsCollectionView.ContentInset = new UIEdgeInsets(0f, 0f, Constants.UI.TabHeight, 0f);
        }

        private void OnScrolled(UIScrollView scrollView)
        {
            if (!ShouldHandleScrolling)
            {
                return;
            }

            var newScrollY = (int) scrollView.ContentOffset.Y;
            if (Math.Abs(newScrollY - scrollY) < (scrollView.Bounds.Height * 0.1f))
            {
                return;
            }

            if (newScrollY > 0
                && (newScrollY > scrollY || (newScrollY + scrollView.Bounds.Height) > scrollView.ContentSize.Height))
            {
                rootViewController.HideTabView();
            }
            else
            {
                rootViewController.ShowTabView();
            }

            scrollY = newScrollY;
        }
    }
}