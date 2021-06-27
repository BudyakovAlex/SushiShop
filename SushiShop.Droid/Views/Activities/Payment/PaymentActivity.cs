using Android.App;
using Android.Webkit;
using AndroidX.AppCompat.Widget;
using MvvmCross.Platforms.Android.Binding;
using SushiShop.Core.Resources;
using SushiShop.Core.ViewModels.Payment;
using SushiShop.Droid.Extensions;
using SushiShop.Droid.Views.Activities.Abstract;

namespace SushiShop.Droid.Views.Activities.Payment
{
    [Activity]
    public class PaymentActivity : BaseActivity<PaymentViewModel>
    {
        private WebView webView;
        private Toolbar toolbar;

        public PaymentActivity() : base(Resource.Layout.activity_payment)
        {
        }

        protected override void InitializeViewPoroperties()
        {
            base.InitializeViewPoroperties();

            webView = FindViewById<WebView>(Resource.Id.web_view);
            webView.Settings.JavaScriptEnabled = true;

            toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            toolbar.Title = AppStrings.OrderPayment;
            SetSupportActionBar(toolbar);
        }

        protected override void Bind()
        {
            base.Bind();

            using var bindingSet = CreateBindingSet();

            bindingSet.Bind(webView).For(v => v.BindWebViewUri()).To(v => v.PaymentUrl);
            bindingSet.Bind(toolbar).For(v => v.BindBackNavigationItemCommand()).To(v => v.CloseCommand);
        }
    }
}
