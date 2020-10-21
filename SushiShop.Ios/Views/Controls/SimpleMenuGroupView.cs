using BuildApps.Core.Mobile.MvvmCross.UIKit.Views.Controls;
using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Binding;
using SushiShop.Core.ViewModels.Menu.Items;
using System;

namespace SushiShop.Ios.Views.Controls
{
    [Register("SimpleMenuGroupView")]
    public partial class SimpleMenuGroupView : MvxGenericView<SimpleMenuGroupView>
    {
        protected SimpleMenuGroupView(IntPtr handle) : base(handle)
        {
        }

        protected override void Initialize()
        {
            base.Initialize();
            ContainerView.Layer.CornerRadius = 6;
        }

        protected override void Bind()
        {
            base.Bind();

            var bindingSet = this.CreateBindingSet<SimpleMenuGroupView, GroupMenuItemViewModel>();

            bindingSet.Bind(ContainerView).For(v => v.BindTap()).To(vm => vm.ShowDetailsCommand);
            bindingSet.Bind(GroupNameLabel).For(v => v.Text).To(vm => vm.Title);
            bindingSet.Bind(GroupImageView).For(v => v.ImagePath).To(vm => vm.ImageUrl);

            bindingSet.Apply();
        }
    }
}