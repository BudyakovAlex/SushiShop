using MvvmCross.Platforms.Android.Core;
using SushiShop.Core;
using SushiShop.Core.IoC;
using SushiShop.Core.Plugins;
using SushiShop.Droid.Plugins;

namespace SushiShop.Droid
{
    public class Setup : MvxAndroidSetup<App>
    {
        protected override void InitializeLastChance()
        {
            base.InitializeLastChance();

            var container = CompositionRoot.Container;

            container.RegisterSingleton<IDialog, Dialog>();
        }
    }
}