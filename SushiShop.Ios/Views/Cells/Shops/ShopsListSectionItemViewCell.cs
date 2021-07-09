using BuildApps.Core.Mobile.Common.Extensions;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Views.Cells;
using Foundation;
using MvvmCross.Binding.BindingContext;
using SushiShop.Core.Data.Enums;
using SushiShop.Core.Resources;
using SushiShop.Core.ViewModels.Shops.Items;
using SushiShop.Core.ViewModels.Shops.Sections;
using SushiShop.Ios.Common;
using SushiShop.Ios.Sources;
using SushiShop.Ios.Views.Controls;
using System;
using System.Reactive.Disposables;
using UIKit;

namespace SushiShop.Ios.Views.Cells.Shops
{
    public partial class ShopsListSectionItemViewCell : BaseCollectionViewCell
    {
        public static readonly NSString Key = new NSString(nameof(ShopsListSectionItemViewCell));
        public static readonly UINib Nib = UINib.FromName(Key, NSBundle.MainBundle);

        private readonly CompositeDisposable disposables;

        private TableViewSource tableViewSource;
        private NearestMetroView nearestMetroView;

        private bool isNearestMetroNotEmpty;
        public bool IsNearestMetroNotEmpty
        {
            get => isNearestMetroNotEmpty;
            set
            {
                isNearestMetroNotEmpty = value;
                if (value)
                {
                    nearestMetroView.Show();
                    return;
                }

                nearestMetroView.Hide();
            }
        }

        protected ShopsListSectionItemViewCell(IntPtr handle)
            : base(handle)
        {
            disposables = new CompositeDisposable();
        }

        protected override void Initialize()
        {
            base.Initialize();

            SearchBar.SearchTextField.AttributedPlaceholder = new NSAttributedString(
                AppStrings.EnterAddress,
                font: Font.Create(FontStyle.Regular, 15f),
                foregroundColor: Colors.GrayPlaceholder);
            SearchBar.SetImageforSearchBarIcon(
                UIImage.FromBundle(ImageNames.Search),
                UISearchBarIcon.Search,
                UIControlState.Normal);

            SearchBar.SearchTextPositionAdjustment = new UIOffset(5, 0);
            SearchBar.SetPositionAdjustmentforSearchBarIcon(new UIOffset(5, 0), UISearchBarIcon.Search);
            SearchBar.SubscribeToEvent(
                OnSearchButtonClicked,
                (searchBar, handler) => searchBar.SearchButtonClicked += handler,
                (searchBar, handler) => searchBar.SearchButtonClicked -= handler)
                .DisposeWith(disposables);

            tableViewSource = new TableViewSource(ContentTableView);
            tableViewSource.Register<ShopItemViewModel>(ShopItemViewCell.Nib, ShopItemViewCell.Key);
            ContentTableView.Source = tableViewSource;
            ContentTableView.KeyboardDismissMode = UIScrollViewKeyboardDismissMode.OnDrag;

            nearestMetroView = NearestMetroView.Create();
        }

        protected override void Bind()
        {
            base.Bind();

            var bindingSet = this.CreateBindingSet<ShopsListSectionItemViewCell, ShopsListSectionViewModel>();

            bindingSet.Bind(SearchBar).For(v => v.Text).To(vm => vm.Query);
            bindingSet.Bind(tableViewSource).For(v => v.ItemsSource).To(vm => vm.Items);
            bindingSet.Bind(nearestMetroView).For(v => v.MetrosCollection).To(vm => vm.NearestMetro);
            bindingSet.Bind(this).For(v => v.IsNearestMetroNotEmpty).To(vm => vm.IsNearestMetroNotEmpty);
            bindingSet.Bind(nearestMetroView).For(v => v.CloseCommand).To(vm => vm.ClearNearestMetroCommand);

            bindingSet.Apply();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (disposing)
            {
                disposables?.Dispose();
            }
        }

        private void OnSearchButtonClicked(object _, EventArgs __) =>
            SearchBar.EndEditing(true);
    }
}
