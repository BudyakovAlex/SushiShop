using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;
using MvvmCross.Platforms.Android.Presenters;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using MvvmCross.Platforms.Android.Views.Fragments;
using MvvmCross.ViewModels;

namespace SushiShop.Droid.Presenter
{
    public class CustomAndroidViewPresenter : MvxAndroidViewPresenter
    {
        public CustomAndroidViewPresenter(IEnumerable<Assembly> androidViewAssemblies) : base(androidViewAssemblies)
        {
        }

        protected override Task<bool> CloseActivity(IMvxViewModel viewModel, MvxActivityPresentationAttribute attribute)
        {
            if (CurrentActivity.SupportFragmentManager.BackStackEntryCount != 0)
            {
                if (CurrentFragmentManager.Fragments.LastOrDefault() is MvxFragment mvxFragment &&
                    mvxFragment.ViewModel is BasePageViewModel basePageViewModel)
                {
                    basePageViewModel?.CloseCommand?.Execute(null);
                    return Task.FromResult(true);
                }
            }

            return base.CloseActivity(viewModel, attribute);
        }
    }
}
