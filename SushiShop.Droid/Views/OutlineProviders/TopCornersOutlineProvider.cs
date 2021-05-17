using System;
using Android.Graphics;
using Android.Runtime;
using Android.Views;

namespace SushiShop.Droid.Views.OutlineProviders
{
    public class TopCornersOutlineProvider : ViewOutlineProvider
    {
        public float cornerRadius;

        public TopCornersOutlineProvider(float cornerRadius)
        {
            this.cornerRadius = cornerRadius;
        }

        public TopCornersOutlineProvider(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        public override void GetOutline(View view, Outline outline)
        {
            outline.SetRoundRect(0, 0, view.Width, view.Height + (int)cornerRadius, cornerRadius);
            view.ClipToOutline = true;
        }
    }
}
