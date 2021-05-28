using Android.Runtime;
using Android.Views;
using AndroidX.Core.Widget;
using AndroidX.RecyclerView.Widget;
using System;
namespace SushiShop.Droid.Platform.Listeners
{
    public class EndlessScrollChangedListener : RecyclerView.OnScrollListener, NestedScrollView.IOnScrollChangeListener
    {
        private int scrolledDx;
        private int scrolledDy;

        public EndlessScrollChangedListener()
        {
        }

        public EndlessScrollChangedListener(IntPtr ptr, JniHandleOwnership owner)
            : base(ptr, owner)
        {
        }

        public Action LoadMoreAction { get; set; }

        public override void OnScrolled(RecyclerView recyclerView, int dx, int dy)
        {
            var oldDx = scrolledDx;
            var oldDy = scrolledDy;

            scrolledDx += dx;
            scrolledDy += dy;

            OnScrollChange(recyclerView, scrolledDx, scrolledDy, oldDx, oldDy);
        }

        public void OnScrollChange(NestedScrollView v, int scrollX, int scrollY, int oldScrollX, int oldScrollY)
        {
            OnScrollChange((View)v, scrollX, scrollY, oldScrollX, oldScrollY);
        }

        public void OnScrollChange(View v, int scrollX, int scrollY, int oldScrollX, int oldScrollY)
        {
            var absScrollY = Math.Abs(scrollY);
            if ((absScrollY >= v.MeasuredHeight / 2)
                && (absScrollY > oldScrollY))
            {
                LoadMoreAction?.Invoke();
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                LoadMoreAction = null;
            }

            base.Dispose(disposing);
        }
    }
}
