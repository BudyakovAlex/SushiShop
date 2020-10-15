using Android.App;
using Android.Runtime;
using MvvmCross.Platforms.Android.Views;
using SushiShop.Core;
using System;

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
            Xamarin.Essentials.Platform.Init(this);
        }
    }
}