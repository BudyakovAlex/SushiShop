using System;
using Android.Content;
using Android.Runtime;
using MvvmCross.DroidX.RecyclerView;

namespace SushiShop.Droid.Views.LayoutManagers
{
    public class ScrollableMvxGuardedLinearLayoutManager : MvxGuardedLinearLayoutManager
    {
        public enum ScrollDirection
        {
            Vertical,
            Horizontal
        }

        private Func<ScrollDirection, bool> canScrollFunc;

        public ScrollableMvxGuardedLinearLayoutManager(Context context, Func<ScrollDirection, bool> canScrollFunc) : base(context)
        {
            this.canScrollFunc = canScrollFunc;
        }

        protected ScrollableMvxGuardedLinearLayoutManager(IntPtr ptr, JniHandleOwnership transfer) : base(ptr, transfer)
        {
        }

        public override bool CanScrollHorizontally()
        {
            if (canScrollFunc != null)
            {
                return canScrollFunc.Invoke(ScrollDirection.Horizontal);
            }

            return base.CanScrollHorizontally();
        }

        public override bool CanScrollVertically()
        {
            if (canScrollFunc != null)
            {
                return canScrollFunc.Invoke(ScrollDirection.Vertical);
            }

            return base.CanScrollVertically();
        }
    }
}
