using Android.App;
using MvvmCross.Platforms.Android.Views;
using SushiShop.Core;

namespace SushiShop.Droid.Views.Activities
{
    [Activity(MainLauncher = true, NoHistory = true, Theme = "@style/Theme.Splash")]
    public class SplashScreenActivity : MvxSplashScreenActivity<Setup, App>
    {
    }
}