using System;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Views.ViewControllers;
using CoreFoundation;
using CoreGraphics;
using FFImageLoading.Args;
using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Binding;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using SushiShop.Core.ViewModels.Promotions;
using SushiShop.Ios.Common.Styles;
using SushiShop.Ios.Converters;

namespace SushiShop.Ios.Views.ViewControllers.Promotions
{
    [MvxChildPresentation]
    public partial class PromotionDetailsViewController : BaseViewController<PromotionDetailsViewModel>
    {
        private bool hasPublicationDate;
        public bool HasPublicationDate
        {
            get => hasPublicationDate;
            set
            {
                hasPublicationDate = value;

                DateLabel.Hidden = !hasPublicationDate;
                DateLabelBottomConstraint.Constant = hasPublicationDate ? 8f : 0f;
            }
        }

        private bool canAddToCart;
        public bool CanAddToCart
        {
            get => canAddToCart;
            set
            {
                canAddToCart = value;

                StepperView.Hidden = !canAddToCart;
                ContentTextViewBottomConstraint.Constant = canAddToCart ? 80f : 16f;
            }
        }

        public NSAttributedString AttributedHtmlContent
        {
            set
            {
                ContentTextView.AttributedText = value;

                var size = ContentTextView.SizeThatFits(new CGSize(ContentTextView.Bounds.Width, nfloat.MaxValue));
                ContentTextViewHeightConstraint.Constant = size.Height;
            }
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            ImageView.OnSuccess += OnImageViewSuccess;
            NavigationController.SetNavigationBarHidden(hidden: true, animated: true);
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);

            ImageView.OnSuccess -= OnImageViewSuccess;
            NavigationController.SetNavigationBarHidden(hidden: false, animated: true);
        }

        protected override void InitStylesAndContent()
        {
            base.InitStylesAndContent();

            BackButton.SetCornerRadius();
            ImageView.SetPlaceholders();
        }

        protected override void Bind()
        {
            base.Bind();

            var bindingSet = this.CreateBindingSet<PromotionDetailsViewController, PromotionDetailsViewModel>();

            bindingSet.Bind(this).For(nameof(AttributedHtmlContent)).To(vm => vm.HtmlContent)
               .WithConversion<HtmlTextToAttributedStringConverter>();
            bindingSet.Bind(this).For(v => v.HasPublicationDate).To(vm => vm.HasPublicationDate);
            bindingSet.Bind(this).For(v => v.CanAddToCart).To(vm => vm.CanAddToCart);
            bindingSet.Bind(BackButton).For(v => v.BindTouchUpInside()).To(vm => vm.PlatformCloseCommand);
            bindingSet.Bind(LoadingView).For(v => v.BindVisible()).To(vm => vm.IsBusy);
            bindingSet.Bind(ImageView).For(v => v.ImagePath).To(vm => vm.ImageUrl);
            bindingSet.Bind(DateLabel).For(v => v.Text).To(vm => vm.PublicationDateTitle);
            bindingSet.Bind(PageTitleLabel).For(v => v.Text).To(vm => vm.PageTitle);
            bindingSet.Bind(StepperView).For(v => v.ViewModel).To(vm => vm.StepperViewModel);

            bindingSet.Apply();
        }

        private void OnImageViewSuccess(object _, SuccessEventArgs args)
        {
            DispatchQueue.MainQueue.DispatchAsync(() =>
            {
                var ratio = args.ImageInformation.OriginalHeight / args.ImageInformation.OriginalWidth;
                ImageViewAspectRationConstraint.Constant = ratio;
            });
        }
    }
}
