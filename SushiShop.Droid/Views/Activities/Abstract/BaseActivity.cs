using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Views;
using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;
using SushiShop.Droid.Plugins;

namespace SushiShop.Droid.Views.Activities.Abstract
{
    public abstract class BaseActivity<TViewModel> : BuildApps.Core.Mobile.MvvmCross.UIKit.Views.Activities.BaseActivity<TViewModel>
        where TViewModel : BasePageViewModel
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

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (requestCode == Location.LocationActivityResult)
            {
                Location.CompletionSource?.TrySetResult(true);
            }
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    ViewModel.CloseCommand.Execute(null);
                    break;
            }

            return base.OnOptionsItemSelected(item);
        }

        public override void OnBackPressed()
        {
            ViewModel?.CloseCommand.Execute(null);
        }
    }
}