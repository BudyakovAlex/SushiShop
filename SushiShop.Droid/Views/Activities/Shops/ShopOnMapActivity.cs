using Android.App;
using Android.Views;
using AndroidX.AppCompat.Widget;
using MvvmCross.Binding.Combiners;
using MvvmCross.Platforms.Android.Binding;
using SushiShop.Core.ViewModels.Shops;
using SushiShop.Core.ViewModels.Shops.Items;
using SushiShop.Droid.Extensions;
using SushiShop.Droid.Views.Activities.Abstract;

namespace SushiShop.Droid.Views.Activities.Shops
{
    [Activity]
    public class ShopOnMapActivity : BaseActivity<ShopOnMapViewModel>
    {
        private Toolbar toolbar;
        private View loadingOverlayView;

        public ShopOnMapActivity() : base(Resource.Layout.activity_shop_on_map)
        {
        }

        public ShopItemViewModel SelectedItemViewModel
        {
            set => UpdateMarker(value);
        }

        protected override void InitializeViewPoroperties()
        {
            base.InitializeViewPoroperties();

            toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            loadingOverlayView = FindViewById<View>(Resource.Id.loading_overlay_view);
        }

        protected override void Bind()
        {
            base.Bind();

            using var bindingSet = CreateBindingSet();

            bindingSet.Bind(toolbar).For(v => v.BindBackNavigationItemCommand()).To(vm => vm.CloseCommand);
            bindingSet.Bind(toolbar).For(v => v.Title).To(vm => vm.Title);
            bindingSet.Bind(this).For(nameof(SelectedItemViewModel)).To(vm => vm.ShopItemViewModel);
            bindingSet.Bind(loadingOverlayView).For(v => v.BindVisible()).ByCombining(
                new MvxAndValueCombiner(),
                vm => vm.IsBusy,
                vm => vm.IsNotRefreshing);
        }

        private void UpdateMarker(ShopItemViewModel viewModel)
        {
            Xamarin.Essentials.MainThread.BeginInvokeOnMainThread(() =>
            {
                //TODO: to android impl
                //mapView.Clear();
                //var position = new CLLocationCoordinate2D(viewModel.Latitude, viewModel.Longitude);
                //var marker = Marker.FromPosition(position);
                //marker.Map = mapView;
                //marker.Icon = UIImage.FromBundle(ImageNames.DefaultMarker);
                //mapView.MoveCamera(CameraUpdate.SetTarget(position, 10));

                if (!viewModel.IsSelectionMode)
                {
                    SetShopDetailsBottomViewDataContext();
                }
            });
        }

        private void SetShopDetailsBottomViewDataContext()
        {
            //shopDetailsBottomView.DataContext = ViewModel.ShopItemViewModel;
            //shopDetailsBottomView.Show();
        }

        //private bool OnMarkerTapped(MapView map, Marker marker)
        //{
        //    SetShopDetailsBottomViewDataContext();
        //    return true;
        //}

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (disposing)
            {
                //mapView.TappedMarker -= OnMarkerTapped;
            }
        }
    }
}
