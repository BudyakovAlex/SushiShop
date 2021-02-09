using BuildApps.Core.Mobile.MvvmCross.UIKit.Views.ViewControllers;
using Foundation;
using MvvmCross.Platforms.Ios.Binding;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using SushiShop.Core.Resources;
using SushiShop.Core.ViewModels.Info;
using SushiShop.Core.ViewModels.Shops.Sections;
using SushiShop.Ios.Common;
using SushiShop.Ios.Delegates;
using SushiShop.Ios.Extensions;
using SushiShop.Ios.Sources;
using SushiShop.Ios.Views.Cells.Shops;
using UIKit;

namespace SushiShop.Ios.Views.ViewControllers.Shops
{
    [MvxChildPresentation]
    public partial class ShopsViewController : BaseViewController<ShopsViewModel>
    {
        private CollectionViewSource shopsContentCollectionViewSource;

        private UIButton backButton;

        protected override void InitStylesAndContent()
        {
            base.InitStylesAndContent();

            tabsScrollableTabView.IsFixedTabs = true;
            tabsScrollableTabView.OnTabChangedAfterTapAction = OnTabChangedAfterTap;
            InitializeContentCollectionView();
        }

        protected override void InitNavigationItem(UINavigationItem navigationItem)
        {
            base.InitNavigationItem(navigationItem);

            backButton = Components.CreateDefaultBarButton(ImageNames.ArrowBack);
            navigationItem.LeftBarButtonItem = new UIBarButtonItem(backButton);
        }

        protected override void Bind()
        {
            base.Bind();

            var bindingSet = CreateBindingSet();

            bindingSet.Bind(tabsScrollableTabView).For(v => v.Items).To(vm => vm.TabsTitles);
            bindingSet.Bind(tabsScrollableTabView).For(v => v.SelectedIndex).To(vm => vm.SelectedIndex).TwoWay();
            bindingSet.Bind(shopsContentCollectionViewSource).For(v => v.ItemsSource).To(vm => vm.Items);
            bindingSet.Bind(backButton).For(v => v.BindTouchUpInside()).To(vm => vm.CloseCommand);
            bindingSet.Bind(this).For(v => v.Title).To(vm => vm.Title);

            bindingSet.Apply();
        }

        private void InitializeContentCollectionView()
        {
            shopsContentCollectionViewSource = new CollectionViewSource(contentCollectionView);
            shopsContentCollectionViewSource.Register<ShopsOnMapSectionViewModel>(ShopsOnMapSectionItemViewCell.Nib, ShopsOnMapSectionItemViewCell.Key);
            shopsContentCollectionViewSource.Register<ShopsListSectionViewModel>(ShopsListSectionItemViewCell.Nib, ShopsListSectionItemViewCell.Key);
            shopsContentCollectionViewSource.Register<MetroSectionViewModel>(MetroSectionItemViewCell.Nib, MetroSectionItemViewCell.Key);
            contentCollectionView.Source = shopsContentCollectionViewSource;
            contentCollectionView.Delegate = new ShopsContentCollectionViewDelegateFlowLayout(OnDecelerated);
        }

        private void OnTabChangedAfterTap()
        {
            var indexPath = NSIndexPath.FromRowSection(tabsScrollableTabView.SelectedIndex, 0);
            contentCollectionView.ScrollToItem(indexPath, UICollectionViewScrollPosition.CenteredHorizontally, true);
        }

        private void OnDecelerated()
        {
            var indexPath = contentCollectionView.GetCenterIndexPathOrDefault();
            if (indexPath != null && tabsScrollableTabView.SelectedIndex != indexPath.Row)
            {
                tabsScrollableTabView.SelectedIndex = indexPath.Row;
            }
        }
    }
}
