using BuildApps.Core.Mobile.MvvmCross.UIKit.Views.Cells;
using CoreGraphics;
using Foundation;
using MvvmCross.Binding.BindingContext;
using SushiShop.Core.ViewModels.Common.Items;
using System;
using UIKit;

namespace SushiShop.Ios.Views.Cells.Common
{
    public partial class PhotoDetailsItemViewCell : BaseCollectionViewCell
    {
        public static readonly NSString Key = new NSString(nameof(PhotoDetailsItemViewCell));
        public static readonly UINib Nib = UINib.FromName(Key, NSBundle.MainBundle);

        protected PhotoDetailsItemViewCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        protected override void Initialize()
        {
            base.Initialize();

            ContentView.AddGestureRecognizer(new UIPinchGestureRecognizer(OnPinch));
        }

        protected override void Bind()
        {
            base.Bind();

            using var bindingSet = this.CreateBindingSet<PhotoDetailsItemViewCell, PhotoDetailsItemViewModel>();

            bindingSet.Bind(ImageView).For(v => v.ImagePath).To(vm => vm.ImagePath);
        }

        private void OnPinch(UIPinchGestureRecognizer recognizer)
        {
            switch (recognizer.State)
            {
                case UIGestureRecognizerState.Began:
                case UIGestureRecognizerState.Changed:
                    ImageView.Transform = CGAffineTransform.MakeScale(recognizer.Scale, recognizer.Scale);
                    break;

                case UIGestureRecognizerState.Possible:
                case UIGestureRecognizerState.Ended:
                case UIGestureRecognizerState.Cancelled:
                case UIGestureRecognizerState.Failed:
                default:
                    Animate(0.5d, () => ImageView.Transform = CGAffineTransform.MakeIdentity());
                    break;
            }
        }
    }
}
