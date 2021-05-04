using MvvmCross.Platforms.Android.Presenters.Attributes;
using System;

namespace SushiShop.Droid.Presenter.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class NestedFragmentPresentationAttribute : MvxFragmentPresentationAttribute
    {
        public NestedFragmentPresentationAttribute()
        {
            AddToBackStack = true;
            AddFragment = true;
            IsCacheableFragment = false;
        }
    }
}