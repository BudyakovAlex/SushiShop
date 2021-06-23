using Android.App;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using AndroidX.AppCompat.Widget;
using AndroidX.Core.Content;
using BuildApps.Core.Mobile.Common.Extensions;
using Google.Android.Material.TextField;
using MvvmCross.DroidX.RecyclerView;
using MvvmCross.ViewModels;
using SushiShop.Core.Data.Models.Common;
using SushiShop.Core.ViewModels.Orders;
using SushiShop.Core.ViewModels.Orders.Items;
using SushiShop.Droid.Extensions;
using SushiShop.Droid.Views.Activities.Abstract;
using System;
using System.Collections.Specialized;
using Xamarin.Essentials;

namespace SushiShop.Droid.Views.Activities.Orders
{
    [Activity]
    public class SelectOrderDeliveryAddressActivity : BaseActivity<SelectOrderDeliveryAddressViewModel>, IOnMapReadyCallback, GoogleMap.IOnMapClickListener
    {
        private IDisposable subscription;
        private bool isZoomedOnStart;
        private GoogleMap googleMap;
        private SupportMapFragment mapFragment;
        private MvxRecyclerView suggestionsRecyclerView;
        private Toolbar toolbar;
        private TextInputEditText searchEditText;

        public SelectOrderDeliveryAddressActivity() : base(Resource.Layout.activity_common_info)
        {
        }

        public bool HasSelectedLocation
        {
            set
            {
                AboutPointView.Hidden = !value;
                UpdateZones();
            }
        }

        private MvxObservableCollection<DeliveryZoneItemViewModel> items;
        public MvxObservableCollection<DeliveryZoneItemViewModel> Items
        {
            get => items;
            set
            {
                items = value;

                subscription?.Dispose();
                subscription = null;

                subscription = items?.SubscribeToCollectionChanged(OnItemsCollectionChanged);
                UpdateZones();
            }
        }

        private MvxInteraction removeFocusInteraction;
        public MvxInteraction RemoveFocusInteraction
        {
            get => removeFocusInteraction;
            set
            {
                if (removeFocusInteraction != null)
                {
                    removeFocusInteraction.Requested -= OnRemoveFocusInteractionRequested;
                }

                removeFocusInteraction = value;
                if (removeFocusInteraction != null)
                {
                    removeFocusInteraction.Requested += OnRemoveFocusInteractionRequested;
                }
            }
        }

        public void OnMapClick(LatLng point)
        {
            ViewModel?.TryLoadPlacemarkCommand?.Execute(new Coordinates(point.Longitude, point.Latitude));
        }

        public void OnMapReady(GoogleMap googleMap)
        {
            this.googleMap = googleMap;
            this.googleMap.SetOnMapClickListener(this);

            var target = new LatLng(Core.Common.Constants.Map.MapStartPointLatitude, Core.Common.Constants.Map.MapStartPointLongitude);
            var cameraPosition = CameraPosition.FromLatLngZoom(target, 5);
            googleMap?.MoveCamera(CameraUpdateFactory.NewCameraPosition(cameraPosition));
        }

        protected override void InitializeViewPoroperties()
        {
            base.InitializeViewPoroperties();

            suggestionsRecyclerView = FindViewById<MvxRecyclerView>(Resource.Id.recycler_view);
            toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            searchEditText = FindViewById<TextInputEditText>(Resource.Id.search_edit_text);
        }

        protected override void Bind()
        {
            base.Bind();

            var bindingSet = CreateBindingSet();

            bindingSet.Bind(toolbar).For(v => v.BindBackNavigationItemCommand()).To(vm => vm.CloseCommand);
            bindingSet.Bind(searchEditText).For(v => v.Text).To(vm => vm.AddressQuery);
            bindingSet.Bind(searchSuggestionsTableViewSource).For(v => v.ItemsSource).To(vm => vm.Suggestions);
            bindingSet.Bind(this).For(nameof(HasSelectedLocation)).To(vm => vm.HasSelectedLocation);
            bindingSet.Bind(this).For(nameof(Items)).To(vm => vm.Items);
            bindingSet.Bind(AddressSelectedPointLabel).For(v => v.Text).To(vm => vm.SelectedLocation.Address);
            bindingSet.Bind(DeliveryPriceLabel).For(v => v.Text).To(vm => vm.SelectedLocation.DeliveryPrice);
            bindingSet.Bind(BadLocationLabel).For(v => v.BindHidden()).To(vm => vm.SelectedLocation.IsDeliveryAvailable);
            bindingSet.Bind(DeliveryThereButton).For(v => v.BindVisible()).To(vm => vm.SelectedLocation.IsDeliveryAvailable);
            bindingSet.Bind(DeliveryPriceLabel).For(v => v.BindVisible()).To(vm => vm.SelectedLocation.IsDeliveryAvailable);
            bindingSet.Bind(DeliveryThereButton).For(v => v.BindTouchUpInside()).To(vm => vm.ConfirmAddress);
            bindingSet.Bind(this).For(v => v.RemoveFocusInteraction).To(vm => vm.RemoveFocusInteraction);

            bindingSet.Apply();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                subscription?.Dispose();
                subscription = null;
                RemoveFocusInteraction = null;
            }
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

        private void OnItemsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            UpdateZones();
        }

        private void OnRemoveFocusInteractionRequested(object _, EventArgs __)
        {
            searchEditText.ClearFocus();
        }

        private void UpdateZones()
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                googleMap.Clear();

                foreach (var item in items)
                {
                    var polygonOptions = new PolygonOptions();
                    foreach (var zone in item.DeliveryZone.Polygon)
                    {
                        polygonOptions.Add(new LatLng(zone.Latitude.Value, zone.Longitude.Value));
                    }

                    polygonOptions.InvokeFillColor(ContextCompat.GetColor(this, Resource.Color.orange2QuarterTransparent));
                    polygonOptions.InvokeStrokeColor(ContextCompat.GetColor(this, Resource.Color.orange2));
                    polygonOptions.InvokeStrokeWidth(2);

                    googleMap.AddPolygon(polygonOptions);
                }

                if (ViewModel.HasSelectedLocation)
                {
                    var position = new LatLng(ViewModel.SelectedLocation.Latitude ?? 0, ViewModel.SelectedLocation.Longitude ?? 0);
                    var markerOptions = new MarkerOptions();
                    markerOptions.SetPosition(position);
                    markerOptions.SetIcon(this.DrawableToBitmapDescriptor(Resource.Drawable.ic_selected_marker));
                    googleMap.AddMarker(markerOptions);
                    if (!isZoomedOnStart)
                    { 
                        var cameraPosition = CameraPosition.FromLatLngZoom(position, ViewModel.ZoomFactor);
                        googleMap.MoveCamera(CameraUpdateFactory.NewCameraPosition(cameraPosition));
                        isZoomedOnStart = true;
                    }
                }
                else
                {
                    var cameraPosition = CameraPosition.FromLatLngZoom(new LatLng(ViewModel.Latitude, ViewModel.Longitude), ViewModel.ZoomFactor);
                    googleMap.MoveCamera(CameraUpdateFactory.NewCameraPosition(cameraPosition));
                    isZoomedOnStart = true;
                }
            });
        }
    }
}
