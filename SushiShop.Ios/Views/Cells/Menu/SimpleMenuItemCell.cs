﻿using BuildApps.Core.Mobile.MvvmCross.UIKit.Views.Cells;
using Foundation;
using MvvmCross.Binding.BindingContext;
using SushiShop.Core.ViewModels.Menu.Items;
using System;
using UIKit;

namespace SushiShop.Ios.Views.Cells.Menu
{
    public partial class SimpleMenuItemCell : BaseCollectionViewCell
    {
        public static readonly NSString Key = new NSString("SimpleMenuItemCell");
        public static readonly UINib Nib;

        static SimpleMenuItemCell()
        {
            Nib = UINib.FromName("SimpleMenuItemCell", NSBundle.MainBundle);
        }

        protected SimpleMenuItemCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        protected override void Stylize()
        {
            base.Stylize();
            ContainerView.Layer.CornerRadius = 6;
        }

        protected override void DoBind()
        {
            base.DoBind();

            var bindingSet = this.CreateBindingSet<SimpleMenuItemCell, MenuItemViewModel>();

            bindingSet.Bind(NameLabel).For(v => v.Text).To(vm => vm.Title);

            bindingSet.Apply();
        }
    }
}