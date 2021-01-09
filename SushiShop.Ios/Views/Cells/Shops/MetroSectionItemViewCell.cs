using System;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Views.Cells;
using Foundation;
using MvvmCross.Binding.BindingContext;
using SushiShop.Core.ViewModels.Shops.Items;
using SushiShop.Core.ViewModels.Shops.Sections;
using SushiShop.Ios.Sources;
using UIKit;

namespace SushiShop.Ios.Views.Cells.Shops
{
    public partial class MetroSectionItemViewCell : BaseCollectionViewCell
    {
        public static readonly NSString Key = new NSString(nameof(MetroSectionItemViewCell));
        public static readonly UINib Nib = UINib.FromName(Key, NSBundle.MainBundle);

        private TableViewSource tableViewSource;

        protected MetroSectionItemViewCell(IntPtr handle)
            : base(handle)
        {
        }

        protected override void Initialize()
        {
            base.Initialize();

            tableViewSource = new TableViewSource(ContentTableView);
            tableViewSource.Register<MetroItemViewModel>(MetroItemViewCell.Nib, MetroItemViewCell.Key);
            ContentTableView.Source = tableViewSource;
        }

        protected override void Bind()
        {
            base.Bind();

            var bindingSet = this.CreateBindingSet<MetroSectionItemViewCell, MetroSectionViewModel>();

            bindingSet.Bind(tableViewSource).For(v => v.ItemsSource).To(vm => vm.Items);

            bindingSet.Apply();
        }
    }
}
