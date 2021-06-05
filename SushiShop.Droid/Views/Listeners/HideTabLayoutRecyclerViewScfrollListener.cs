using AndroidX.RecyclerView.Widget;
using SushiShop.Droid.Helpers;

namespace SushiShop.Droid.Views.Listeners
{
    public class HideTabLayoutRecyclerViewScrollListener : RecyclerView.OnScrollListener
    {
        public HideTabLayoutRecyclerViewScrollListener()
        {
        }

        public override void OnScrolled(RecyclerView recyclerView, int dx, int dy)
        {
            base.OnScrolled(recyclerView, dx, dy);

            HideTabLayoutHelper.Instance.SetPosition(dy);
        }

        public void Show()
        {
            HideTabLayoutHelper.Instance.Show();
        }
    }
}
