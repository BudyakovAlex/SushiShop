using System;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Views.Cells;
using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Binding;
using SushiShop.Core.ViewModels.Shops.Items;
using UIKit;

namespace SushiShop.Ios.Views.Cells.Shops
{
    public partial class ShopItemViewCell : BaseTableViewCell
    {
        public static readonly NSString Key = new NSString(nameof(ShopItemViewCell));
        public static readonly UINib Nib = UINib.FromName(Key, NSBundle.MainBundle);

        protected ShopItemViewCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void Bind()
        {
            base.Bind();

            var bindingSet = this.CreateBindingSet<ShopItemViewCell, ShopItemViewModel>();

            bindingSet.Bind(AddressLabel).For(v => v.Text).To(vm => vm.LongTitle);
            bindingSet.Bind(PhoneLabel).For(v => v.Text).To(vm => vm.Phone);
            bindingSet.Bind(WorkingTimeLabel).For(v => v.Text).To(vm => vm.WorkingTime);
            bindingSet.Bind(MetroButton).For(v => v.BindTouchUpInside()).To(vm => vm.ShowNearestMetroCommand);
            bindingSet.Bind(MetroButton).For(v => v.BindVisible()).To(vm => vm.HasNearestMetro);
            bindingSet.Bind(MapButton).For(v => v.BindTouchUpInside()).To(vm => vm.GoToMapCommand);

            bindingSet.Apply();
        }
    }
}
