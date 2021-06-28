#nullable enable

using AndroidX.Fragment.App;
using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;
using MvvmCross.Platforms.Android.Presenters;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using MvvmCross.Platforms.Android.Views.Fragments;
using MvvmCross.Presenters;
using MvvmCross.ViewModels;
using SushiShop.Droid.Presenter.Attributes;
using SushiShop.Droid.Views.Fragments.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace SushiShop.Droid.Presenter
{
    public class CustomAndroidViewPresenter : MvxAndroidViewPresenter
    {
        private int countOpenedFragments = 0;

        public CustomAndroidViewPresenter(IEnumerable<Assembly> androidViewAssemblies) : base(androidViewAssemblies)
        {
        }

        public override void RegisterAttributeTypes()
        {
            base.RegisterAttributeTypes();
            AttributeTypesToActionsDictionary.Register<NestedFragmentPresentationAttribute>(ShowNestedFragmentAsync, CloseNestedFragmentAsync);
        }

        protected virtual Task<bool> CloseNestedFragmentAsync(IMvxViewModel viewModel, NestedFragmentPresentationAttribute attribute)
        {
            var currentFragment = FindVisibleFragment();
            if (currentFragment is null ||
                currentFragment.ChildFragmentManager.BackStackEntryCount == 0)
            {
                return CloseFragment(viewModel, attribute);
            }

            currentFragment.ChildFragmentManager.PopBackStack();
            return Task.FromResult(true);
        }

        protected virtual Task<bool> ShowNestedFragmentAsync(Type view, NestedFragmentPresentationAttribute attribute, MvxViewModelRequest request)
        {
            var fragmentHost = FindVisibleFragment();
            if (fragmentHost == null)
            {
                return ShowFragment(view, attribute, request);
            }

            PerformShowFragmentTransaction(fragmentHost.ChildFragmentManager, attribute, request);
            return Task.FromResult(true);
        }

        protected virtual Fragment? FindVisibleFragment(params Type[] types)
        {
            if (CurrentFragmentManager == null)
            {
                return null;
            }

            var firstTabFragment = CurrentFragmentManager.Fragments.OfType<ITabFragment>()
                .FirstOrDefault(fragment => fragment.IsActivated);
            if (firstTabFragment is null)
            {
                return CurrentFragmentManager.Fragments.FirstOrDefault(fragment => fragment.IsVisible);
            }

            return firstTabFragment as Fragment;
        }

        protected override async Task<bool> CloseActivity(IMvxViewModel viewModel, MvxActivityPresentationAttribute? attribute)
        {
            static bool closeLastFragment(FragmentManager fragmentManager)
            {
                var fragment = fragmentManager!.Fragments.LastOrDefault();
                if (fragment is MvxFragment mvxFragment &&
                    mvxFragment.ViewModel is BasePageViewModel basePageViewModel)
                {
                    basePageViewModel?.CloseCommand?.Execute(null);
                    return true;
                }

                return false;
            }

            if (countOpenedFragments > 0 && CurrentFragmentManager != null)
            {
                if (closeLastFragment(CurrentFragmentManager))
                {
                    return true;
                }
            }
            else
            {
                var fragmentManager = FindVisibleFragment()?.ChildFragmentManager;
                if (fragmentManager != null && fragmentManager.BackStackEntryCount != 0 && closeLastFragment(fragmentManager))
                {
                    return true;
                }
            }

            return await base.CloseActivity(viewModel, attribute);
        }

        protected override Task<bool> ShowFragment(Type view, MvxFragmentPresentationAttribute attribute, MvxViewModelRequest request)
        {
            countOpenedFragments++;
            return base.ShowFragment(view, attribute, request);
        }

        protected override Task<bool> CloseFragment(IMvxViewModel viewModel, MvxFragmentPresentationAttribute attribute)
        {
            countOpenedFragments--;
            return base.CloseFragment(viewModel, attribute);
        }
    }
}