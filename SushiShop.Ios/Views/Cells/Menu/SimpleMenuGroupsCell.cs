using BuildApps.Core.Mobile.MvvmCross.UIKit.Views.Cells;
using Foundation;
using MvvmCross.Binding.BindingContext;
using SushiShop.Core.ViewModels.Menu.Items;
using SushiShop.Ios.Views.Controls;
using System;
using UIKit;

namespace SushiShop.Ios.Views.Cells.Menu
{
    public partial class SimpleMenuGroupsCell : BaseCollectionViewCell
    {
        public static readonly NSString Key = new NSString("SimpleMenuGroupsCell");
        public static readonly UINib Nib;

        static SimpleMenuGroupsCell()
        {
            Nib = UINib.FromName("SimpleMenuGroupsCell", NSBundle.MainBundle);
        }

        protected SimpleMenuGroupsCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        protected override void Stylize()
        {
            base.Stylize();
            GroupStackView.RegisterView<SimpleMenuGroupView, GroupMenuItemViewModel>();
        }

        protected override void DoBind()
        {
            base.DoBind();

            var bindingSet = this.CreateBindingSet<SimpleMenuGroupsCell, GroupsMenuItemViewModel>();

            bindingSet.Bind(GroupStackView).For(v => v.ItemsSource).To(vm => vm.Items);

            bindingSet.Apply();
        }
    }
}
