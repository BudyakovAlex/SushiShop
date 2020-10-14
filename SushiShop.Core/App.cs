using MvvmCross.ViewModels;
using SushiShop.Core.ViewModels;

namespace SushiShop.Core
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            base.Initialize();
            RegisterAppStart<AppStartViewModel>();
        }
    }
}
