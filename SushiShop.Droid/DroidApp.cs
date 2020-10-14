﻿using System;
using Android.App;
using Android.Runtime;
using MvvmCross.Platforms.Android.Views;
using SushiShop.Core;

namespace SushiShop.Droid
{
    [Application(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar")]
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
