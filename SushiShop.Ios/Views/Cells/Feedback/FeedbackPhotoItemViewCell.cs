using System;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Views.Cells;
using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Binding;
using SushiShop.Core.ViewModels.Common.Items;
using SushiShop.Ios.Common.Styles;
using UIKit;

namespace SushiShop.Ios.Views.Cells.Feedback
{
    public partial class FeedbackPhotoItemViewCell : BaseCollectionViewCell
    {
        public static readonly NSString Key = new NSString(nameof(FeedbackPhotoItemViewCell));
        public static readonly UINib Nib = UINib.FromName(Key, NSBundle.MainBundle);

        protected FeedbackPhotoItemViewCell(IntPtr handle)
            : base(handle)
        {
        }

        protected override void Initialize()
        {
            base.Initialize();

            ImageView.Layer.CornerRadius = 6f;
            ImageView.SetPlaceholders();
        }

        protected override void Bind()
        {
            base.Bind();

            var bindingSet = this.CreateBindingSet<FeedbackPhotoItemViewCell, PhotoItemViewModel>();

            bindingSet.Bind(this).For(v => v.BindTap()).To(vm => vm.ShowDetailsCommand);
            bindingSet.Bind(ImageView).For(v => v.ImagePath).To(vm => vm.ImagePath);
            bindingSet.Bind(CloseImageView).For(v => v.BindTap()).To(vm => vm.RemoveCommand);
            bindingSet.Bind(CloseImageView).For(v => v.BindVisible()).To(vm => vm.CanRemove);

            bindingSet.Apply();
        }
    }
}
