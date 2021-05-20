using Android.Views;
using System;

namespace SushiShop.Droid.Views.Listeners
{
    public class ScaleListener : ScaleGestureDetector.SimpleOnScaleGestureListener
    {
        private readonly Action<ScaleGestureDetector> scaleFunc;

        public ScaleListener(Action<ScaleGestureDetector> scaleFunc)
        {
            this.scaleFunc = scaleFunc;
        }

        public override bool OnScale(ScaleGestureDetector detector)
        {
            scaleFunc?.Invoke(detector);

            return base.OnScale(detector);
        }
    }
}