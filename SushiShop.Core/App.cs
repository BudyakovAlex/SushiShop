using MvvmCross.ViewModels;
using SushiShop.Core.IoC;
using SushiShop.Core.ViewModels;

namespace SushiShop.Core
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            var compositionRoot = new CompositionRoot();

            base.Initialize();

            compositionRoot.Initialize();
            RegisterAppStart<AppStartViewModel>();
        }
    }
}
