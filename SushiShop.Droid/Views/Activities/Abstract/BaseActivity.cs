using Android.Content.PM;
using Android.Views;
using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;

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
            base.OnBackPressed();
            ViewModel?.CloseCommand.Execute(null);
        }
    }
}