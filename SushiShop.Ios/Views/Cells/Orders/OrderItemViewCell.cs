using System;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Views.Cells;
using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Binding;
using SushiShop.Core.Resources;
using SushiShop.Core.ViewModels.Orders.Items;
using UIKit;

namespace SushiShop.Ios.Views.Cells.Orders
{
    public partial class OrderItemViewCell : BaseTableViewCell
    {
        public const float Height = 127f;

        public static readonly NSString Key = new NSString(nameof(OrderItemViewCell));
        public static readonly UINib Nib = UINib.FromName(Key, NSBundle.MainBundle);

        protected OrderItemViewCell(IntPtr handle)
            : base(handle)
        {
        }

        protected override void Initialize()
        {
            base.Initialize();

            RepeatButton.SetTitle(AppStrings.Repeat, UIControlState.Normal);
        }

        protected override void Bind()
        {
            base.Bind();

            var bindingSet = this.CreateBindingSet<OrderItemViewCell, OrderItemViewModel>();

            bindingSet.Bind(this).For(v => v.BindTap()).To(vm => vm.ShowDetailsCommand);
            bindingSet.Bind(NumberLabel).For(v => v.Text).To(vm => vm.OrderNumber);
            bindingSet.Bind(DateLabel).For(v => v.Text).To(vm => vm.OrderDateTime);
            bindingSet.Bind(StatusLabel).For(v => v.Text).To(vm => vm.Status);
            bindingSet.Bind(ReceiveMethodLabel).For(v => v.Text).To(vm => vm.ReceiveMethod);
            bindingSet.Bind(TotalPriceLabel).For(v => v.Text).To(vm => vm.TotalPrice);
            bindingSet.Bind(RepeatButton).For(v => v.BindVisible()).To(vm => vm.CanRepeat);
            bindingSet.Bind(RepeatButton).For(v => v.BindTouchUpInside()).To(vm => vm.RepeatOrderCommand);

            bindingSet.Apply();
        }
    }
}
