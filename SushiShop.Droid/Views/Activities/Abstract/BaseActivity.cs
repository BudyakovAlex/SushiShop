using Android.Content.PM;
using MvvmCross.Platforms.Android.Views;
using MvvmCross.ViewModels;

namespace SushiShop.Droid.Views.Activities.Abstract
{
    public abstract class BaseActivity : MvxActivity
    {
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }

    public abstract class BaseActivity<TViewModel> : BaseActivity
        where TViewModel : class, IMvxViewModel
    {
        public new TViewModel ViewModel
        {
            get => (TViewModel) base.ViewModel;
            set => base.ViewModel = value;
        }
    }
}
