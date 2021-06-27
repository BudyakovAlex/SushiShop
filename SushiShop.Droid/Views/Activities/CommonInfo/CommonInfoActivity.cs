using Android.App;
using Android.Graphics;
using Android.Webkit;
using AndroidX.AppCompat.Widget;
using MvvmCross.Platforms.Android.Binding;
using SushiShop.Core.ViewModels.Common;
using SushiShop.Droid.Extensions;
using SushiShop.Droid.Views.Activities.Abstract;

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

        public string Content
        {
            set
            {
                if (value is null)
                {
                    return;
                }

                webView.LoadDataWithBaseURL(null, value, "text/html", "utf-8", null);
            }
        }

        protected override void InitializeViewPoroperties()
        {
            base.InitializeViewPoroperties();

            webView = FindViewById<WebView>(Resource.Id.web_view);
            webView.Settings.JavaScriptEnabled = true;

            toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
        }

        protected override void Bind()
        {
            base.Bind();

            using var bindingSet = CreateBindingSet();

            bindingSet.Bind(toolbar).For(v => v.Title).To(vm => vm.Title);
            bindingSet.Bind(toolbar).For(v => v.BindBackNavigationItemCommand()).To(vm => vm.CloseCommand);
            bindingSet.Bind(this).For(nameof(Content)).To(v => v.Content);
            
        }
    }
}
