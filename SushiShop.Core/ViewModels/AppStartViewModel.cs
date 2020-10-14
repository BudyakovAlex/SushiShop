using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;

namespace SushiShop.Core.ViewModels
{
    public class AppStartViewModel : BasePageViewModel
    {
        public override void ViewAppeared()
        {
            base.ViewAppeared();
            _ = NavigationManager.NavigateAsync<MainViewModel>();
        }
    }
}