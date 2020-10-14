using Android.App;
using MvvmCross.Platforms.Android.Views;
using SushiShop.Core;

namespace SushiShop.Droid.Views.Activities
{
    [Activity(MainLauncher = true, NoHistory = true)]
    public class SplashScreenActivity : MvxSplashScreenActivity<Setup, App>
    {
    }
}
