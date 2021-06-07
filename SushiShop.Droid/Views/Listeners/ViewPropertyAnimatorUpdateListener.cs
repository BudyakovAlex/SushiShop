using System;
using Android.Views;
using AndroidX.Core.View;

namespace SushiShop.Droid.Views.Listeners
{
    public class ViewPropertyAnimatorUpdateListener : Java.Lang.Object, IViewPropertyAnimatorUpdateListener
    {
        public Action<View> updateAction;

        public ViewPropertyAnimatorUpdateListener(Action<View> updateAction)
        {
            this.updateAction = updateAction;
        }

        public void OnAnimationUpdate(View view)
        {
            updateAction?.Invoke(view);
        }
    }
}
