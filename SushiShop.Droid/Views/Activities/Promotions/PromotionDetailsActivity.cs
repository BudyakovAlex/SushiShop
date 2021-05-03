using Android.App;
using Android.Views;
using Android.Webkit;
using Android.Widget;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Extensions;
using MvvmCross.Platforms.Android.Binding;
using SushiShop.Core.ViewModels.Promotions;
using SushiShop.Droid.Extensions;
using SushiShop.Droid.Views.Activities.Abstract;
using SushiShop.Droid.Views.Controls;

namespace SushiShop.Droid.Views.Activities.Promotions
{
    [Activity]
    public class PromotionDetailsActivity : BaseActivity<PromotionDetailsViewModel>
    {
        private TextView dateTextView;
        private TextView titleTextView;
        private ImageView imageView;
        private WebView contentTextView;
        private ImageView backImageView;
        private BigStepperView stepperView;
        private View loadingOverlayView;

        public PromotionDetailsActivity() : base(Resource.Layout.activity_promotion_details)
        {
        }

        protected override void InitializeViewPoroperties()
        {
            base.InitializeViewPoroperties();

            dateTextView = FindViewById<TextView>(Resource.Id.date_text_view);
            titleTextView = FindViewById<TextView>(Resource.Id.title_text_view);
            imageView = FindViewById<ImageView>(Resource.Id.image_view);
            contentTextView = FindViewById<WebView>(Resource.Id.content_view);
            stepperView = FindViewById<BigStepperView>(Resource.Id.stepper_view);
            loadingOverlayView = FindViewById<View>(Resource.Id.loading_overlay_view);
            backImageView = FindViewById<ImageView>(Resource.Id.back_image_view);
            backImageView.SetRoundedCorners(this.DpToPx(20));
        }

        protected override void Bind()
        {
            base.Bind();

            using var bindingSet = CreateBindingSet();

            bindingSet.Bind(contentTextView).For(v => v.BindWebViewHtml()).To(vm => vm.HtmlContent);
            bindingSet.Bind(dateTextView).For(v => v.BindVisible()).To(vm => vm.HasPublicationDate);
            bindingSet.Bind(stepperView).For(v => v.BindVisible()).To(vm => vm.CanAddToCart);
            bindingSet.Bind(backImageView).For(v => v.BindClick()).To(vm => vm.CloseCommand);
            bindingSet.Bind(loadingOverlayView).For(v => v.BindVisible()).To(vm => vm.IsBusy);
            bindingSet.Bind(imageView).For(v => v.BindUrl()).To(vm => vm.ImageUrl);
            bindingSet.Bind(dateTextView).For(v => v.Text).To(vm => vm.PublicationDateTitle);
            bindingSet.Bind(titleTextView).For(v => v.Text).To(vm => vm.PageTitle);
            bindingSet.Bind(stepperView).For(v => v.DataContext).To(vm => vm.StepperViewModel);
        }
    }
}