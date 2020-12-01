using System;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Views.Cells;
using Foundation;
using MvvmCross.Binding.BindingContext;
using SushiShop.Core.ViewModels.Menu.Items;
using SushiShop.Ios.Common.Styles;
using UIKit;

namespace SushiShop.Ios.Views.Cells.Menu
{
    public partial class MenuPromotionItemViewCell : BaseCollectionViewCell
    {
        public static readonly NSString Key = new NSString(nameof(MenuPromotionItemViewCell));
        public static readonly UINib Nib = UINib.FromName(Key, NSBundle.MainBundle);

        protected MenuPromotionItemViewCell(IntPtr handle)
            : base(handle)
        {
        }

        protected override void Initialize()
        {
            base.Initialize();

            ImageView.SetPlaceholders();
        }

        protected override void Bind()
        {
            base.Bind();

            var bindingSet = this.CreateBindingSet<MenuPromotionItemViewCell, MenuPromotionItemViewModel>();
            bindingSet.Bind(ImageView).For(v => v.ImageUrl).To(vm => vm.ImageUrl);
            bindingSet.Apply();
        }
    }
}
