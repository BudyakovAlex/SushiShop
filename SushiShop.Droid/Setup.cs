using Android.Widget;
using MvvmCross.Binding.Bindings.Target.Construction;
using MvvmCross.Platforms.Android.Core;
using SushiShop.Core;
using SushiShop.Core.IoC;
using SushiShop.Core.Plugins;
using SushiShop.Droid.Plugins;
using SushiShop.Droid.TargetBindings;

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

        protected override void FillTargetFactories(IMvxTargetBindingFactoryRegistry registry)
        {
            base.FillTargetFactories(registry);

            registry.RegisterCustomBindingFactory<ImageView>(ImageViewUrlTargetBinding.DefaultImageViewUrlTargetBinding, view => new ImageViewUrlTargetBinding(view));
            registry.RegisterCustomBindingFactory<ImageView>(ImageViewUrlTargetBinding.AdaptedImageViewUrlTargetBinding, view => new ImageViewUrlTargetBinding(view, true));
        }
    }
}