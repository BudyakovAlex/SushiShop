using Android.App;
using Android.Webkit;
using AndroidX.AppCompat.Widget;
using MvvmCross.Platforms.Android.Binding;
using SushiShop.Core.Resources;
using SushiShop.Core.ViewModels.Payment;
using SushiShop.Droid.Extensions;
using SushiShop.Droid.Views.Activities.Abstract;
using System;

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
            webView.Settings.LoadWithOverviewMode = true;
            webView.Settings.UseWideViewPort = true;

            webView.SetWebViewClient(new PaymentWebViewClient()
            {
                PaymentOkAction = () => ViewModel.ConfirmPaymentCommand?.Execute(null),
                PaymentErrorAction = () => ViewModel.CloseCommand?.Execute(null)
            });

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

        private class PaymentWebViewClient : WebViewClient
        {
            public Action PaymentOkAction { get; set; }

            public Action PaymentErrorAction { get; set; }

            public override void OnPageFinished(WebView view, string url)
            {
                base.OnPageFinished(view, url);

                var canConfirmPayment = url.Contains(Core.Common.Constants.Rest.PaymentOkResource);
                if (canConfirmPayment)
                {
                    PaymentOkAction?.Invoke();
                    return;
                }

                var isPaymentError = url.Contains(Core.Common.Constants.Rest.PaymentErrorResource);
                if (isPaymentError)
                {
                    PaymentErrorAction?.Invoke();
                }
            }
        }
    }
}
