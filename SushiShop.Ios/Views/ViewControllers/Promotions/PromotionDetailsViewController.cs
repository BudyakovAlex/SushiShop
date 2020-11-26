using BuildApps.Core.Mobile.MvvmCross.UIKit.Views.ViewControllers;
using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Binding;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using SushiShop.Core.ViewModels.Promotions;
using SushiShop.Ios.Common.Styles;

namespace SushiShop.Ios.Views.ViewControllers.Promotions
{
    [MvxChildPresentation]
    public partial class PromotionDetailsViewController : BaseViewController<PromotionDetailsViewModel>
    {
        private string htmlContent;
        public string HtmlContent
        {
            get => htmlContent;
            set
            {
                htmlContent = value;
                HtmlContentChanged(htmlContent);
            }
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            NavigationController.SetNavigationBarHidden(hidden: true, animated: true);
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);
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

            bindingSet.Bind(BackButton).For(v => v.BindTouchUpInside()).To(vm => vm.PlatformCloseCommand);
            bindingSet.Bind(LoadingView).For(v => v.BindVisible()).To(vm => vm.IsBusy);
            bindingSet.Bind(ImageView).For(v => v.ImageUrl).To(vm => vm.ImageUrl);
            bindingSet.Bind(DateLabel).For(v => v.Text).To(vm => vm.PublicationDateRangeTitle);
            bindingSet.Bind(IntroLabel).For(v => v.Text).To(vm => vm.IntroTitle);
            bindingSet.Bind(this).For(v => v.HtmlContent).To(vm => vm.HtmlContent);
            bindingSet.Bind(StepperView).For(v => v.ViewModel).To(vm => vm.StepperViewModel);
            bindingSet.Bind(StepperView).For(v => v.BindVisible()).To(vm => vm.CanAddToCart);

            bindingSet.Apply();
        }

        private void HtmlContentChanged(string html)
        {
            if (!string.IsNullOrEmpty(html))
            {
                NSError error = null;
                var attributedString = new NSAttributedString(
                    NSData.FromString(html, NSStringEncoding.UTF8),
                    new NSAttributedStringDocumentAttributes
                    {
                        DocumentType = NSDocumentType.HTML,
                        StringEncoding = NSStringEncoding.UTF8
                    },
                    ref error);

                if (error is null)
                {
                    ContentTextView.AttributedText = attributedString;
                    ContentTextViewHeightConstraint.Constant = attributedString.Size.Height;

                    return;
                }
            }

            ContentTextView.Text = string.Empty;
            ContentTextViewHeightConstraint.Constant = 0f;
        }
    }
}
