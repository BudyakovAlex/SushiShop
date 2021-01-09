using BuildApps.Core.Mobile.MvvmCross.UIKit.Views.Cells;
using Foundation;
using MvvmCross.Binding.BindingContext;
using SushiShop.Core.ViewModels.Shops.Items;
using SushiShop.Core.ViewModels.Shops.Sections;
using SushiShop.Ios.Sources;
using SushiShop.Ios.Views.Controls;
using System;
using UIKit;

namespace SushiShop.Ios.Views.Cells.Shops
{
    public partial class ShopsListSectionItemViewCell : BaseCollectionViewCell
    {
        public static readonly NSString Key = new NSString(nameof(ShopsListSectionItemViewCell));
        public static readonly UINib Nib = UINib.FromName(Key, NSBundle.MainBundle);

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
        }

        protected override void Initialize()
        {
            base.Initialize();

            tableViewSource = new TableViewSource(ContentTableView);
            tableViewSource.Register<ShopItemViewModel>(ShopItemViewCell.Nib, ShopItemViewCell.Key);
            ContentTableView.Source = tableViewSource;

            nearestMetroView = NearestMetroView.Create();
        }

        protected override void Bind()
        {
            base.Bind();

            var bindingSet = this.CreateBindingSet<ShopsListSectionItemViewCell, ShopsListSectionViewModel>();

            bindingSet.Bind(tableViewSource).For(v => v.ItemsSource).To(vm => vm.Items);
            bindingSet.Bind(nearestMetroView).For(v => v.MetrosCollection).To(vm => vm.NearestMetro);
            bindingSet.Bind(this).For(v => v.IsNearestMetroNotEmpty).To(vm => vm.IsNearestMetroNotEmpty);
            bindingSet.Bind(nearestMetroView).For(v => v.CloseCommand).To(vm => vm.ClearNearestMetroCommand);

            bindingSet.Apply();
        }
    }
}
