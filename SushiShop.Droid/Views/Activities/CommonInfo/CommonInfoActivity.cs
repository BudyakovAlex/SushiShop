using Android.App;
using Android.Webkit;
using AndroidX.AppCompat.Widget;
using MvvmCross.Platforms.Android.Binding;
using SushiShop.Core.ViewModels.Common;
using SushiShop.Droid.Views.Activities.Abstract;
using System;

namespace SushiShop.Droid.Views.Activities.CommonInfo
{
    [Activity]
    public class CommonInfoActivity : BaseActivity<CommonInfoViewModel>
    {
        private WebView webView;
        private Toolbar toolbar;

        public CommonInfoActivity() : base(Resource.Layout.activity_common_info)
        {
        }

        protected override void InitializeViewPoroperties()
        {
            base.InitializeViewPoroperties();

            webView = FindViewById<WebView>(Resource.Id.web_view);
            toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
        }

        protected override void Bind()
        {
            base.Bind();

            using var bindingSet = CreateBindingSet();

            bindingSet.Bind(this).For(v => v.Title).To(v => v.Title);
            bindingSet.Bind(webView).For(v => v.BindWebViewHtml()).To(v => v.Content);
        }
    }
}
