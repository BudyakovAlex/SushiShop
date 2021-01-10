using System;
using System.Threading.Tasks;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Views.Controls;
using CoreAnimation;
using CoreFoundation;
using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Binding;
using SushiShop.Core.ViewModels.Common.Items;
using SushiShop.Core.ViewModels.Shops.Items;
using SushiShop.Ios.Delegates;
using SushiShop.Ios.Sources;
using SushiShop.Ios.Views.Cells.Feedback;
using UIKit;

namespace SushiShop.Ios.Views.Controls
{
    [Register(nameof(ShopDetailsBottomView))]
    public partial class ShopDetailsBottomView : MvxGenericView<ShopDetailsBottomView>
    {
        private const float HeightPercentExpand = 0.3f;
        private const float HeightPercentCollapse = 0.8f;

        private readonly float maxHeight = (float)UIApplication.SharedApplication.KeyWindow.Frame.Height / 2.0f * 1.5f;
        private readonly float startHeight = (float)UIApplication.SharedApplication.KeyWindow.Frame.Height * 0.25f;

        private NSLayoutConstraint heightConstraint;
        private bool isExpanded;

        private CollectionViewSource photosCollectionViewSource;

        public ShopDetailsBottomView(IntPtr handle) : base(handle)
        {
        }

        protected override void Initialize()
        {
            base.Initialize();

            TranslatesAutoresizingMaskIntoConstraints = false;

            RootContentView.Layer.MaskedCorners = CACornerMask.MinXMinYCorner | CACornerMask.MaxXMinYCorner;
            RootContentView.Layer.CornerRadius = 16f;
            RootContentView.AddGestureRecognizer(new UIPanGestureRecognizer(OnPanGesture));

            photosCollectionViewSource = new CollectionViewSource(PhotosCollectionView)
                .Register<PhotoItemViewModel>(FeedbackPhotoItemViewCell.Nib, FeedbackPhotoItemViewCell.Key);
            PhotosCollectionView.Source = photosCollectionViewSource;
            PhotosCollectionView.Delegate = new FeedbackPhotosCollectionViewDelegateFlowLayout();

            heightConstraint = HeightAnchor.ConstraintEqualTo(0f);
            heightConstraint.Active = true;
        }

        protected override void Bind()
        {
            base.Bind();

            var bindingSet = this.CreateBindingSet<ShopDetailsBottomView, ShopItemViewModel>();

            bindingSet.Bind(PhoneLabel).For(v => v.Text).To(vm => vm.Phone);
            bindingSet.Bind(TimeWorkingLabel).For(v => v.Text).To(vm => vm.WorkingTime);
            bindingSet.Bind(DriveWayLabel).For(v => v.Text).To(vm => vm.DriveWay);
            bindingSet.Bind(DriveWayContainerView).For(v => v.BindVisible()).To(vm => vm.HasDriveWay);
            bindingSet.Bind(TitleLabel).For(v => v.Text).To(vm => vm.LongTitle);
            bindingSet.Bind(photosCollectionViewSource).For(v => v.ItemsSource).To(vm => vm.Photos);

            bindingSet.Apply();
        }

        public void Show()
        {
            UIApplication.SharedApplication.KeyWindow.AddSubview(this);
            NSLayoutConstraint.ActivateConstraints(new[]
            {
                LeadingAnchor.ConstraintEqualTo(UIApplication.SharedApplication.KeyWindow.LeadingAnchor),
                TrailingAnchor.ConstraintEqualTo(UIApplication.SharedApplication.KeyWindow.TrailingAnchor),
                BottomAnchor.ConstraintEqualTo(UIApplication.SharedApplication.KeyWindow.BottomAnchor)
            });

            Animate(0.000001f, () => UIApplication.SharedApplication.KeyWindow.LayoutIfNeeded());
            DispatchQueue.MainQueue.DispatchAsync(async () => await AnimateHeightAsync(isExpanded ? maxHeight : startHeight));
        }

        public void Hide()
        {
            DispatchQueue.MainQueue.DispatchAsync(async () =>
            {
                await AnimateHeightAsync(0f);
                RemoveFromSuperview();
            });
        }

        private void OnPanGesture(UIPanGestureRecognizer panGestureRecognizer)
        {
            var translation = panGestureRecognizer.TranslationInView(RootContentView);
            var height = 0f;
            switch (panGestureRecognizer.State)
            {
                case UIGestureRecognizerState.Began:
                case UIGestureRecognizerState.Changed:
                    height = isExpanded ? maxHeight : startHeight;
                    height -= (float)translation.Y;
                    if (height > maxHeight)
                    {
                        height = maxHeight;
                    }

                    if (height < startHeight)
                    {
                        height = startHeight;
                    }

                    heightConstraint.Constant = height;
                    break;

                case UIGestureRecognizerState.Ended:
                case UIGestureRecognizerState.Cancelled:
                    if (!isExpanded)
                    {
                        isExpanded = heightConstraint.Constant / maxHeight > HeightPercentExpand;
                        height = isExpanded ? maxHeight : startHeight;
                    }
                    else
                    {
                        isExpanded = heightConstraint.Constant / maxHeight >= HeightPercentCollapse;
                        height = isExpanded ? maxHeight : startHeight;
                    }

                    DispatchQueue.MainQueue.DispatchAsync(async () => await AnimateHeightAsync(height));
                    break;
            }
        }

        private Task AnimateHeightAsync(float height)
        {
            heightConstraint.Constant = height;
            return AnimateAsync(
                0.2,
                () =>
                {
                    UIApplication.SharedApplication.KeyWindow.LayoutIfNeeded();
                    ContentScrollView.ScrollEnabled = isExpanded;
                });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                RemoveFromSuperview();
            }

            base.Dispose(disposing);
        }
    }
}
