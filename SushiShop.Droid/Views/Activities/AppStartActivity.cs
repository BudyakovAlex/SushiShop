using Android.App;
using Android.OS;
using SushiShop.Core.ViewModels;
using SushiShop.Droid.Views.Activities.Abstract;

namespace SushiShop.Droid.Views.Activities
{
    [Activity(NoHistory = true)]
    public class AppStartActivity : BaseActivity<AppStartViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.activity_app_start);
        }
    }
}
