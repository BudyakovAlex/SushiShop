using BuildApps.Core.Mobile.MvvmCross.UIKit.Views.Controls;
using Foundation;
using MvvmCross.Binding.BindingContext;
using System;

namespace SushiShop.Ios.Views.Controls
{
    [Register("SimpleMenuGroupView")]
    public partial class SimpleMenuGroupView : MvxGenericView<SimpleMenuGroupView>
    {
        protected SimpleMenuGroupView(IntPtr handle) : base(handle)
        {
        }

        protected override void DoAwakeFromNib()
        {
            base.DoAwakeFromNib();
            ContainerView.Layer.CornerRadius = 6;
        }

        protected override void DoBind()
        {
            base.DoBind();

            var bindingSet = this.CreateBindingSet<SimpleMenuGroupView, MenuItemViewModel>();

            bindingSet.Bind(GroupNameLabel).For(v => v.Text).To(vm => vm.Title);

            bindingSet.Apply();
        }
    }
}
