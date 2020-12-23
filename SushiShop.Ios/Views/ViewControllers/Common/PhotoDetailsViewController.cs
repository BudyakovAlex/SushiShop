using BuildApps.Core.Mobile.MvvmCross.UIKit.Views.ViewControllers;
using CoreGraphics;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using SushiShop.Core.ViewModels.Common;
using UIKit;

namespace SushiShop.Ios.Views.ViewControllers.Common
{
    [MvxChildPresentation]
    public partial class PhotoDetailsViewController : BaseViewController<PhotoDetailsViewModel>
    {
        protected override void InitStylesAndContent()
        {
            base.InitStylesAndContent();

            View.AddGestureRecognizer(new UIPinchGestureRecognizer(OnPinch));
        }

        protected override void Bind()
        {
            base.Bind();

            var bindingSet = CreateBindingSet();

            bindingSet.Bind(this).For(v => v.Title).To(vm => vm.Title);
            bindingSet.Bind(ImageView).For(v => v.ImagePath).To(vm => vm.ImageUrl);

            bindingSet.Apply();
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
                    UIView.Animate(0.5d, () => ImageView.Transform = CGAffineTransform.MakeIdentity());
                    break;
            }
        }
    }
}
