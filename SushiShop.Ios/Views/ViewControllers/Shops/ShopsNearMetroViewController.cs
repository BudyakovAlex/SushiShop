using BuildApps.Core.Mobile.MvvmCross.UIKit.Views.ViewControllers;
using MvvmCross.Platforms.Ios.Binding;
using SushiShop.Core.ViewModels.Shops;
using SushiShop.Core.ViewModels.Shops.Items;
using SushiShop.Ios.Common;
using SushiShop.Ios.Sources;
using SushiShop.Ios.Views.Cells.Shops;
using SushiShop.Ios.Views.Controls;
using UIKit;

namespace SushiShop.Ios.Views.ViewControllers.Shops
{
    public partial class ShopsNearMetroViewController : BaseViewController<ShopsNearMetroViewModel>
    {
        private UIButton backButton;
        private TableViewSource tableViewSource;
        private NearestMetroView nearestMetroView;

        public ShopsNearMetroViewController() : base("ShopsNearMetroViewController", null)
        {
        }

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

        protected override void InitStylesAndContent()
        {
            base.InitStylesAndContent();

            tableViewSource = new TableViewSource(ContentTableView);
            tableViewSource.Register<ShopItemViewModel>(ShopItemViewCell.Nib, ShopItemViewCell.Key);
            ContentTableView.Source = tableViewSource;

            nearestMetroView = NearestMetroView.Create();
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

            bindingSet.Bind(this).For(v => v.Title).To(vm => vm.Title);
            bindingSet.Bind(tableViewSource).For(v => v.ItemsSource).To(vm => vm.Items);
            bindingSet.Bind(nearestMetroView).For(v => v.MetrosCollection).To(vm => vm.NearestMetro);
            bindingSet.Bind(this).For(v => v.IsNearestMetroNotEmpty).To(vm => vm.IsNearestMetroNotEmpty);
            bindingSet.Bind(nearestMetroView).For(v => v.CloseCommand).To(vm => vm.ClearNearestMetroCommand);
            bindingSet.Bind(backButton).For(v => v.BindTouchUpInside()).To(vm => vm.PlatformCloseCommand);

            bindingSet.Apply();
        }
    }
}
