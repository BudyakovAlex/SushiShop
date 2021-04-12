using Bumptech.Glide.Load;
using Bumptech.Glide.Load.Engine;
using Bumptech.Glide.Request;
using Bumptech.Glide.Request.Target;
using System;

namespace SushiShop.Droid.Platform.Listeners
{
    public class GlideRequestListener : Java.Lang.Object, IRequestListener
    {
        private readonly Action failedAction;
        private readonly Action readyAction;

        public GlideRequestListener(Action failedAction, Action readyAction)
        {
            this.failedAction = failedAction;
            this.readyAction = readyAction;
        }

        public bool OnLoadFailed(GlideException p0, Java.Lang.Object p1, ITarget p2, bool p3)
        {
            failedAction?.Invoke();
            return true;
        }

        public bool OnResourceReady(Java.Lang.Object p0, Java.Lang.Object p1, ITarget p2, DataSource p3, bool p4)
        {
            readyAction?.Invoke();
            return true;
        }
    }
}
