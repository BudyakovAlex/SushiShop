using Android.Runtime;
using AndroidX.RecyclerView.Widget;
using System;

namespace SushiShop.Droid.Views.LayoutManagers
{
    public class DelegateGridLayoutManagerSpanSizeLookup : GridLayoutManager.SpanSizeLookup
    {
        private readonly Func<int, int> getSpanSizeAtPosition;

        public DelegateGridLayoutManagerSpanSizeLookup(Func<int, int> getSpanSizeAtPosition)
        {
            this.getSpanSizeAtPosition = getSpanSizeAtPosition;
        }

        public DelegateGridLayoutManagerSpanSizeLookup(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        public override int GetSpanSize(int position)
        {
            return getSpanSizeAtPosition?.Invoke(position) ?? 1;
        }
    }
}