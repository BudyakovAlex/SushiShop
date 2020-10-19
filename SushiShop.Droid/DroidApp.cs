using Android.App;
using Android.Runtime;
using MvvmCross.Platforms.Android.Views;
using SushiShop.Core;
using System;
using Acr.UserDialogs;

namespace SushiShop.Droid
{
    [Application(Label = "@string/app_name", Theme = "@style/AppTheme")]
    public class DroidApp : MvxAndroidApplication<Setup, App>
    {
        protected DroidApp(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {
        }

        public override void OnCreate()
        {
            base.OnCreate();
            UserDialogs.Init(this);

            Xamarin.Essentials.Platform.Init(this);
        }
    }
}