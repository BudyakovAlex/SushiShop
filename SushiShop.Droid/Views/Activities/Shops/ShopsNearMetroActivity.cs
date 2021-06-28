using Android.App;
using Android.Views;
using AndroidX.AppCompat.Widget;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Adapter.TemplateSelectors;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Adapters;
using MvvmCross.Binding.Combiners;
using MvvmCross.DroidX.RecyclerView;
using MvvmCross.Platforms.Android.Binding;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using SushiShop.Core.ViewModels.Shops;
using SushiShop.Core.ViewModels.Shops.Items;
using SushiShop.Droid.Extensions;
using SushiShop.Droid.Views.Activities.Abstract;
using SushiShop.Droid.Views.Controls;
using SushiShop.Droid.Views.ViewHolders.Shops;

namespace SushiShop.Droid.Views.Activities.Shops
{
    [Activity]
    public class ShopsNearMetroActivity : BaseActivity<ShopsNearMetroViewModel>
    {
        private Toolbar toolbar;
        private NearestMetroView nearestMetroView;
        private View loadingOverlayView;
        private MvxRecyclerView recyclerView;

        public ShopsNearMetroActivity() : base(Resource.Layout.activity_shops_near_metro)
        {
        }

        private bool isNearestMetroNotEmpty;
        public bool IsNearestMetroNotEmpty
        {
            get => isNearestMetroNotEmpty;
            set
            {
                isNearestMetroNotEmpty = value;
                if (value)
                {
                    nearestMetroView.Show();
                    return;
                }

                nearestMetroView.Hide();
            }
        }

        protected override void InitializeViewPoroperties()
        {
            base.InitializeViewPoroperties();

            InitializeRecyclerView();

            toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            nearestMetroView = FindViewById<NearestMetroView>(Resource.Id.nearest_metro_view);
            loadingOverlayView = FindViewById<View>(Resource.Id.loading_overlay_view);
        }

        protected override void Bind()
        {
            base.Bind();

            using var bindingSet = CreateBindingSet();

            bindingSet.Bind(toolbar).For(v => v.BindBackNavigationItemCommand()).To(vm => vm.CloseCommand);
            bindingSet.Bind(toolbar).For(v => v.Title).To(vm => vm.Title);
            bindingSet.Bind(recyclerView).For(v => v.ItemsSource).To(vm => vm.Items);
            bindingSet.Bind(loadingOverlayView).For(v => v.BindVisible()).ByCombining(
                new MvxAndValueCombiner(),
                vm => vm.IsBusy,
                vm => vm.IsNotRefreshing);

            bindingSet.Bind(nearestMetroView).For(v => v.MetrosCollection).To(vm => vm.NearestMetro);
            bindingSet.Bind(nearestMetroView).For(v => v.CloseCommand).To(vm => vm.ClearNearestMetroCommand);
            bindingSet.Bind(this).For(v => v.IsNearestMetroNotEmpty).To(vm => vm.IsNearestMetroNotEmpty);
        }

        private void InitializeRecyclerView()
        {
            recyclerView = FindViewById<MvxRecyclerView>(Resource.Id.recycler_view);
            recyclerView.Adapter = new RecycleViewBindableAdapter((IMvxAndroidBindingContext)BindingContext);
            recyclerView.ItemTemplateSelector = new TemplateSelector()
                .AddElement<ShopItemViewModel, ShopItemViewHolder>(Resource.Layout.item_shop);
        }
    }
}
