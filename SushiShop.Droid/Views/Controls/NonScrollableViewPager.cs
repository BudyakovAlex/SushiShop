#nullable enable

using System;
using Android.Content;
using Android.Runtime;
using Android.Util;
using Android.Views;
using AndroidX.ViewPager.Widget;

namespace SushiShop.Droid.Views.Controls
{
    [Register("SushiShop." + nameof(NonScrollableViewPager))]
    public class NonScrollableViewPager : ViewPager
    {
        public NonScrollableViewPager(Context context)
            : base(context)
        {
        }

        public NonScrollableViewPager(Context context, IAttributeSet attrs)
            : base(context, attrs)
        {
        }

        protected NonScrollableViewPager(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {
        }

        public override bool OnTouchEvent(MotionEvent? e) => false;

        public override bool OnInterceptTouchEvent(MotionEvent? ev) => false;
    }
}
