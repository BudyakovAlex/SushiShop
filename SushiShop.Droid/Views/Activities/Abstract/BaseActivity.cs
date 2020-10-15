using Android.Content.PM;
using MvvmCross.ViewModels;

namespace SushiShop.Droid.Views.Activities.Abstract
{
    public abstract class BaseActivity<TViewModel> : BuildApps.Core.Mobile.MvvmCross.UIKit.Views.Activities.BaseActivity<TViewModel>
        where TViewModel : class, IMvxViewModel
    {
        protected BaseActivity(int resourceId)
            : base(resourceId)
        {
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}