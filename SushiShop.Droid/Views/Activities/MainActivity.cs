using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using Android.Widget;
using Google.Android.Material.Tabs;
using SushiShop.Core.ViewModels;
using SushiShop.Droid.Views.Activities.Abstract;
using System.Threading.Tasks;

namespace SushiShop.Droid.Views.Activities
{
    [Activity(
        LaunchMode = LaunchMode.SingleTop,
        ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : BaseActivity<MainViewModel>
    {
        private static readonly int[] TabImageIds = new[]
        {
            Resource.Drawable.ic_menu,
            Resource.Drawable.ic_promotions,
            Resource.Drawable.ic_cart,
            Resource.Drawable.ic_profile,
            Resource.Drawable.ic_info,
        };

        public MainActivity()
            : base(Resource.Layout.activity_main)
        {
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            var tabLayout = FindViewById<TabLayout>(Resource.Id.main_tab_layout);
            _ = InitializeTabsAsync(bundle, tabLayout);
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
        }
    }
}