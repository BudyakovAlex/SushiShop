using AndroidX.RecyclerView.Widget;
using System;

namespace SushiShop.Droid.Platform.Listeners
{
    public class RecyclerViewOnScrollListener : RecyclerView.OnScrollListener
    {
        private readonly Action<RecyclerView, int, int> scrolledAction;

        public RecyclerViewOnScrollListener(Action<RecyclerView, int, int> scrolledAction)
        {
            this.scrolledAction = scrolledAction;
        }

        public override void OnScrolled(RecyclerView recyclerView, int dx, int dy)
        {
            base.OnScrolled(recyclerView, dx, dy);
            scrolledAction?.Invoke(recyclerView, dx, dy);
        }
    }
}