using AndroidX.RecyclerView.Widget;
using SushiShop.Core.IoC;
using SushiShop.Droid.Views.Controllers;

namespace SushiShop.Droid.Views.Listeners
{
    public class HideTabLayoutRecyclerViewScrollListener : RecyclerView.OnScrollListener
    {
        private readonly ITabLayoutController tabLayoutController;

        public HideTabLayoutRecyclerViewScrollListener()
        {
            tabLayoutController = CompositionRoot.Container.Resolve<ITabLayoutController>();
        }

        public override void OnScrolled(RecyclerView recyclerView, int dx, int dy)
        {
            base.OnScrolled(recyclerView, dx, dy);

            tabLayoutController.SetPosition(dy);
        }

        public void Show()
        {
            tabLayoutController.Show();
        }
    }
}
