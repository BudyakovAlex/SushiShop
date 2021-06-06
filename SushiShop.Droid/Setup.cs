using Android.Widget;
using MvvmCross.Binding.Bindings.Target.Construction;
using MvvmCross.Platforms.Android.Core;
using MvvmCross.Platforms.Android.Presenters;
using SushiShop.Core;
using SushiShop.Core.IoC;
using SushiShop.Core.Plugins;
using SushiShop.Droid.Plugins;
using SushiShop.Droid.Presenter;
using SushiShop.Droid.TargetBindings;
using SushiShop.Droid.Views.Controllers;

namespace SushiShop.Droid
{
    public class Setup : MvxAndroidSetup<App>
    {
        protected override void InitializeLastChance()
        {
            base.InitializeLastChance();

            var container = CompositionRoot.Container;

            container.RegisterSingleton<IDialog, Dialog>();
            container.RegisterSingleton<ITabLayoutController, TabLayoutController>();
        }

        protected override void FillTargetFactories(IMvxTargetBindingFactoryRegistry registry)
        {
            base.FillTargetFactories(registry);

            registry.RegisterCustomBindingFactory<ImageView>(ImageViewUrlTargetBinding.DefaultImageViewUrlTargetBinding, view => new ImageViewUrlTargetBinding(view, false));
            registry.RegisterCustomBindingFactory<ImageView>(ImageViewUrlTargetBinding.AdaptedImageViewUrlTargetBinding, view => new ImageViewUrlTargetBinding(view, true));
            registry.RegisterCustomBindingFactory<AndroidX.AppCompat.Widget.Toolbar>(BackNavigaitonItemTargetBinding.DefaultBackNavigaitonItemTargetBinding, view => new BackNavigaitonItemTargetBinding(view));
        }

        protected override IMvxAndroidViewPresenter CreateViewPresenter()
        {
            return new CustomAndroidViewPresenter(AndroidViewAssemblies);
        }
    }
}