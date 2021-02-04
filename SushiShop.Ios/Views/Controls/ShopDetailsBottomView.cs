﻿using BuildApps.Core.Mobile.MvvmCross.UIKit.Views.Controls;
using CoreAnimation;
using CoreFoundation;
using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Binding;
using SushiShop.Core.Resources;
using SushiShop.Core.ViewModels.Common.Items;
using SushiShop.Core.ViewModels.Shops.Items;
using SushiShop.Ios.Delegates;
using SushiShop.Ios.Extensions;
using SushiShop.Ios.Sources;
using SushiShop.Ios.Views.Cells.Feedback;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        private readonly Dictionary<NSRange, string> _phonesRangesMappings = new Dictionary<NSRange, string>();

        private NSLayoutConstraint heightConstraint;

        private CollectionViewSource photosCollectionViewSource;

        public ShopDetailsBottomView(IntPtr handle) : base(handle)
        {
        }

        public bool IsExpanded { get; set; }

        protected ShopItemViewModel ViewModel => DataContext as ShopItemViewModel;

        public string[] Phones
        {
            set => SetPhonesAttributedString(value);
        }

        protected override void Initialize()
        {
            base.Initialize();

            TranslatesAutoresizingMaskIntoConstraints = false;

            DriveTitleLabel.Text = AppStrings.DriveWay;
            GalleryTitleLabel.Text = AppStrings.Gallery;

            RootContentView.Layer.MaskedCorners = CACornerMask.MinXMinYCorner | CACornerMask.MaxXMinYCorner;
            RootContentView.Layer.CornerRadius = 16f;
            RootContentView.AddGestureRecognizer(new UIPanGestureRecognizer(OnPanGesture));

            photosCollectionViewSource = new CollectionViewSource(PhotosCollectionView)
                .Register<PhotoItemViewModel>(FeedbackPhotoItemViewCell.Nib, FeedbackPhotoItemViewCell.Key);
            PhotosCollectionView.Source = photosCollectionViewSource;
            PhotosCollectionView.Delegate = new FeedbackPhotosCollectionViewDelegateFlowLayout();

            heightConstraint = HeightAnchor.ConstraintEqualTo(0f);
            heightConstraint.Active = true;

            PickupThereButton.WithoutRadius = true;
        }

        protected override void Bind()
        {
            base.Bind();

            var bindingSet = this.CreateBindingSet<ShopDetailsBottomView, ShopItemViewModel>();

            bindingSet.Bind(this).For(nameof(Phones)).To(vm => vm.Phones);
            bindingSet.Bind(TimeWorkingLabel).For(v => v.Text).To(vm => vm.WorkingTime);
            bindingSet.Bind(DriveWayLabel).For(v => v.Text).To(vm => vm.DriveWay);
            bindingSet.Bind(DriveWayContainerView).For(v => v.BindVisible()).To(vm => vm.HasDriveWay);
            bindingSet.Bind(TitleLabel).For(v => v.Text).To(vm => vm.LongTitle);
            bindingSet.Bind(photosCollectionViewSource).For(v => v.ItemsSource).To(vm => vm.Photos);
            bindingSet.Bind(GalleryTitleLabel).For(v => v.BindVisible()).To(vm => vm.HasPhotos);
            bindingSet.Bind(PhotosCollectionView).For(v => v.BindVisible()).To(vm => vm.HasPhotos);
            bindingSet.Bind(PickupThereButton).For(v => v.BindVisible()).To(vm => vm.IsSelectionMode);
            bindingSet.Bind(PickupThereButton).For(v => v.BindTouchUpInside()).To(vm => vm.ConfirmSelectionCommand);

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
            DispatchQueue.MainQueue.DispatchAsync(async () => await AnimateHeightAsync(IsExpanded ? maxHeight : startHeight));
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
                    height = IsExpanded ? maxHeight : startHeight;
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
                    if (!IsExpanded)
                    {
                        IsExpanded = heightConstraint.Constant / maxHeight > HeightPercentExpand;
                        height = IsExpanded ? maxHeight : startHeight;
                    }
                    else
                    {
                        IsExpanded = heightConstraint.Constant / maxHeight >= HeightPercentCollapse;
                        height = IsExpanded ? maxHeight : startHeight;
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
                    ContentScrollView.ScrollEnabled = IsExpanded;
                });
        }

        private void SetPhonesAttributedString(string[] phones)
        {
            if (phones is null)
            {
                return;
            }

            var joinedText = string.Join(", ", phones);
            var attributedText = new NSMutableAttributedString(joinedText);
            foreach (var item in phones)
            {
                var startPosition = joinedText.IndexOf(item);
                var endPosition = startPosition + item.Length;
                _phonesRangesMappings[new NSRange(startPosition, endPosition)] = item;
            }

            PhoneLabel.AttributedText = attributedText;
            PhoneLabel.UserInteractionEnabled = true;
            PhoneLabel.AddGestureRecognizer(new UITapGestureRecognizer(OnPhoneLabelTapped));
        }

        private void OnPhoneLabelTapped(UITapGestureRecognizer gesture)
        {
            foreach (var item in _phonesRangesMappings)
            {
                if (gesture.DidTapAttributedTextInLabel(PhoneLabel, item.Key))
                {
                    ViewModel?.CallCommand?.Execute(item.Value);
                    break;
                }
            }
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
