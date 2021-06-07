namespace SushiShop.Droid.Views.Controllers
{
    public interface ITabLayoutController
    {
        void Show();

        void Hide();

        void SetPosition(float margin, bool shouldAnimate = false);
    }
}
