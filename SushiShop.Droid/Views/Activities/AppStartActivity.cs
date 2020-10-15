using Android.App;
using SushiShop.Core.ViewModels;
using SushiShop.Droid.Views.Activities.Abstract;

namespace SushiShop.Droid.Views.Activities
{
    [Activity(NoHistory = true)]
    public class AppStartActivity : BaseActivity<AppStartViewModel>
    {
        public AppStartActivity()
            : base(Resource.Layout.activity_app_start)
        {
        }
    }
}