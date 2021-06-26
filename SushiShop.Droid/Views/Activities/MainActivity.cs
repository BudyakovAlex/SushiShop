using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using Android.Widget;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Extensions;
using Google.Android.Material.Tabs;
using SushiShop.Core.ViewModels;
using SushiShop.Droid.Views.Activities.Abstract;
using SushiShop.Droid.Views.Controls;
using SushiShop.Droid.Views.Fragments.Abstract;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SushiShop.Droid.Views.Activities
{
    [Activity(
        LaunchMode = LaunchMode.SingleTask,
        ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : BaseActivity<MainViewModel>, TabLayout.IOnTabSelectedListener
    {
        private const int CartTabIndex = 2;

        private static readonly int[] TabImageIds = new[]
        {
            Resource.Drawable.ic_menu,
            Resource.Drawable.ic_promotions,
            Resource.Drawable.ic_cart,
            Resource.Drawable.ic_profile,
            Resource.Drawable.ic_info,
        };

        private TabLayout tabLayout;
        private NonScrollableViewPager viewPager;

        public MainActivity()
            : base(Resource.Layout.activity_main)
        {
            Instance = this;
        }

        public static MainActivity Instance { get; private set; }

        public long BadgeCount
        {
            set => SetCartBadgeCount(value);
        }

        public void ShowCartTab()
        {
            viewPager.Post(() =>
            {
                viewPager.SetCurrentItem(CartTabIndex, false);
                tabLayout.GetTabAt(CartTabIndex)?.Select();
            });
        }

        public void OnTabReselected(TabLayout.Tab tab)
        {
        }

        public void OnTabSelected(TabLayout.Tab tab)
        {
            var tabFragments = SupportFragmentManager.Fragments.OfType<ITabFragment>().ToArray();
            for (var i = 0; i < tabFragments.Length; i++)
            {
                var tabFragment = tabFragments.ElementAtOrDefault(i);
                if (tabFragment is null)
                {
                    return;
                }

                tabFragment.IsActivated = i == tab.Position;
            }
        }

        public void OnTabUnselected(TabLayout.Tab tab)
        {
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            viewPager = FindViewById<NonScrollableViewPager>(Resource.Id.main_view_pager);
            viewPager.OffscreenPageLimit = 5;

            tabLayout = FindViewById<TabLayout>(Resource.Id.main_tab_layout);
            tabLayout.AddOnTabSelectedListener(this);

            _ = InitializeTabsAsync(bundle, tabLayout);
        }

        protected override void Bind()
        {
            base.Bind();

            using var bindingSet = CreateBindingSet();

            bindingSet.Bind(this).For(nameof(BadgeCount)).To(vm => vm.CartItemsTotalCount);
        }

        private async Task InitializeTabsAsync(Bundle bundle, TabLayout tabLayout)
        {
            if (bundle is null)
            {
                await ViewModel.LoadTabsCommand.ExecuteAsync();
            }

            var tabNames = ViewModel.TabNames;
            for (var i = 0; i < tabNames.Length; i++)
            {
                var view = LayoutInflater.Inflate(Resource.Layout.view_main_tab, null, false);
                var textView = view.FindViewById<TextView>(Resource.Id.text_view);
                var imageView = view.FindViewById<ImageView>(Resource.Id.image_view);

                textView.Text = tabNames[i];
                imageView.SetImageResource(TabImageIds[i]);

                var tab = tabLayout.GetTabAt(i);
                tab.SetCustomView(view);
            }

            _view.PostDelayed(() =>
            {
                var firstFragment = SupportFragmentManager.Fragments.OfType<ITabFragment>().FirstOrDefault();
                if (firstFragment is null)
                {
                    return;
                }

                firstFragment.IsActivated = true;
            }, 1000);
        }

        private void SetCartBadgeCount(long value)
        {
            var tab = tabLayout?.GetTabAt(CartTabIndex);
            if (tab == null)
            {
                return;
            }

            var badgeTextView = tab.CustomView.FindViewById<TextView>(Resource.Id.badge_text_view);
            var badgeView = tab.CustomView.FindViewById<View>(Resource.Id.badge_view);

            badgeTextView.SetText(value.ToString(), null);
            badgeTextView.Measure(0, 0);
            badgeTextView.Visibility = badgeView.Visibility = value <= 0 ? ViewStates.Gone : ViewStates.Visible;

            var size = Math.Max(badgeTextView.MeasuredHeight, badgeTextView.MeasuredWidth);
            badgeView.LayoutParameters.Width = size;
            badgeView.LayoutParameters.Height = size;
            badgeView.SetRoundedCorners(size / 2);
        }
    }
}