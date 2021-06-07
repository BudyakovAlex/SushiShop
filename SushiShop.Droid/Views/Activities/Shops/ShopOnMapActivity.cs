using Android.App;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.Views;
using MvvmCross.Binding.Combiners;
using MvvmCross.Platforms.Android.Binding;
using SushiShop.Core.ViewModels.Shops;
using SushiShop.Core.ViewModels.Shops.Items;
using SushiShop.Droid.Extensions;
using SushiShop.Droid.Views.Activities.Abstract;
using SushiShop.Droid.Views.Controls;
using Toolbar = AndroidX.AppCompat.Widget.Toolbar;

namespace SushiShop.Droid.Views.Activities.Shops
{
    [Activity]
    public class ShopOnMapActivity : BaseActivity<ShopOnMapViewModel>, IOnMapReadyCallback
    {
        private Toolbar toolbar;
        private View loadingOverlayView;
        private GoogleMap googleMap;
        private SupportMapFragment mapFragment;
        private ShopDetailsBottomView shopDetailsBottomView;

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
            shopDetailsBottomView = new ShopDetailsBottomView(this, BindingContext)
            {
                CanClose = false
            };

            InitializeMap();
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

        public void OnMapReady(GoogleMap googleMap)
        {
            this.googleMap = googleMap;
            UpdateMarker(ViewModel.ShopItemViewModel);
        }

        private void InitializeMap()
        {
            mapFragment = SupportMapFragment.NewInstance();
            SupportFragmentManager
                .BeginTransaction()
                .Add(Resource.Id.map_container, mapFragment)
                .Commit();
            mapFragment.GetMapAsync(this);
        }

        private void UpdateMarker(ShopItemViewModel viewModel)
        {
            if (googleMap == null)
            {
                return;
            }

            var markerOptions = new MarkerOptions();
            var coordinates = new LatLng(viewModel.Latitude, viewModel.Longitude);
            markerOptions.SetPosition(coordinates);
            markerOptions.SetIcon(this.DrawableToBitmapDescriptor(Resource.Drawable.ic_default_marker));
            googleMap.AddMarker(markerOptions);

            var cameraPosition = CameraPosition.FromLatLngZoom(coordinates, Core.Common.Constants.Map.DefaultZoomFactor);
            googleMap?.MoveCamera(CameraUpdateFactory.NewCameraPosition(cameraPosition));

            shopDetailsBottomView.SetData(viewModel);

            _view.PostDelayed(() =>
            {
                shopDetailsBottomView.Show();
            }, 200);
        }
    }
}
