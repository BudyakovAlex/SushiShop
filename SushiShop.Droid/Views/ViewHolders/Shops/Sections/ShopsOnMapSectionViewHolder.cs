using System;
using System.Collections.Specialized;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.Views;
using BuildApps.Core.Mobile.Common.Extensions;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using MvvmCross.Platforms.Android.Views;
using MvvmCross.ViewModels;
using SushiShop.Core.Data.Models.Common;
using SushiShop.Core.ViewModels.Shops.Items;
using SushiShop.Core.ViewModels.Shops.Sections;
using SushiShop.Droid.Extensions;
using SushiShop.Droid.Views.Controls;
using SushiShop.Droid.Views.ViewHolders.Abstract;

namespace SushiShop.Droid.Views.ViewHolders.Shops.Sections
{
    public class ShopsOnMapSectionViewHolder : CardViewHolder<ShopsOnMapSectionViewModel>, IOnMapReadyCallback
    {
        private GoogleMap googleMap;
        private SupportMapFragment mapFragment;
        private IDisposable subscription;
        private ShopDetailsBottomView shopDetailsBottomView;

        public ShopsOnMapSectionViewHolder(View view, IMvxAndroidBindingContext context) : base(view, context)
        {
        }

        private MvxObservableCollection<ShopItemViewModel> items;
        public MvxObservableCollection<ShopItemViewModel> Items
        {
            get => items;
            set
            {
                items = value;

                subscription?.Dispose();
                subscription = null;

                subscription = items?.SubscribeToCollectionChanged(OnItemsCollectionChanged);
                UpdateMarkers();
            }
        }

        public ShopItemViewModel SelectedItem
        {
            set => OpenOrHideShopDetailsView();
        }

        private float zoom;
        public float Zoom
        {
            get => zoom;
            set
            {
                zoom = value;
                if (CenterCoordinates == null)
                {
                    return;
                }

                SetCamera();
            }
        }

        private Coordinates centerCoordinates;
        public Coordinates CenterCoordinates
        {
            get => centerCoordinates;
            set
            {
                centerCoordinates = value;
                if (value is null)
                {
                    return;
                }

                SetCamera();
            }
        }

        protected new ShopsOnMapSectionViewModel ViewModel => BindingContext.DataContext as ShopsOnMapSectionViewModel;

        protected override void DoInit(View view)
        {
            base.DoInit(view);

            shopDetailsBottomView = new ShopDetailsBottomView(ItemView.Context, BindingContext);
            shopDetailsBottomView.HideEvent += OnShopDetailsBottomViewHide;

            InitializeMap();
            OpenOrHideShopDetailsView();
        }

        public override void BindData()
        {
            base.BindData();

            using var bindingSet = CreateBindingSet();

            bindingSet.Bind(this).For(v => v.CenterCoordinates).To(vm => vm.CenterCoordinates);
            bindingSet.Bind(this).For(v => v.Zoom).To(vm => vm.ZoomFactor);
            bindingSet.Bind(this).For(v => v.Items).To(vm => vm.Items);
            bindingSet.Bind(this).For(nameof(SelectedItem)).To(vm => vm.SelectedItem);
        }

        public override void OnAttachedToWindow()
        {
            base.OnAttachedToWindow();

            mapFragment.OnAttach(ItemView.Context);
        }

        public override void OnDetachedFromWindow()
        {
            base.OnDetachedFromWindow();

            mapFragment.OnDetach();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                subscription?.Dispose();
                googleMap.MarkerClick -= OnMarkerClick;
                googleMap.MapClick -= OnMapClick;
                shopDetailsBottomView.HideEvent -= OnShopDetailsBottomViewHide;
            }
        }

        public void OnMapReady(GoogleMap googleMap)
        {
            this.googleMap = googleMap;
            this.googleMap.MapClick += OnMapClick;
            this.googleMap.MarkerClick += OnMarkerClick;

            SetCamera();
            UpdateMarkers();
        }

        private void InitializeMap()
        {
            mapFragment = SupportMapFragment.NewInstance();
            (ItemView.Context as MvxActivity).SupportFragmentManager
                .BeginTransaction()
                .Add(Resource.Id.map_container, mapFragment)
                .Commit();
            mapFragment.GetMapAsync(this);
        }

        private void OnMarkerClick(object sender, GoogleMap.MarkerClickEventArgs e)
        {
            if (e.Marker.Tag.IsNotNull() && e.Marker.Tag is WrappedObject<ShopItemViewModel> wrappedObject)
            {
                SetMarker(wrappedObject.Data);
            }
        }

        private void OnMapClick(object sender, GoogleMap.MapClickEventArgs e)
        {
            SetMarker(ViewModel?.SelectedItem);
        }

        private void OnShopDetailsBottomViewHide(object sender, EventArgs e)
        {
            SetMarker(ViewModel?.SelectedItem);
        }

        private void SetMarker(ShopItemViewModel data)
        {
            if (data == null)
            {
                return;
            }

            ViewModel?.SelectItemCommand?.Execute(data);
            UpdateMarkers();
        }

        private void OnItemsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            UpdateMarkers();
        }

        private void UpdateMarkers()
        {
            if (googleMap == null)
            {
                return;
            }

            googleMap.Clear();

            foreach (var item in items)
            {
                var markerOptions = new MarkerOptions();
                markerOptions.SetPosition(new LatLng(item.Latitude, item.Longitude));
                markerOptions.SetIcon(ItemView.Context.DrawableToBitmapDescriptor(item.IsSelected ? Resource.Drawable.ic_selected_marker : Resource.Drawable.ic_default_marker));
                var marker = googleMap.AddMarker(markerOptions);
                marker.Tag = new WrappedObject<ShopItemViewModel>(item);
            }
        }

        private void OpenOrHideShopDetailsView()
        {
            if (ViewModel?.SelectedItem == null)
            {
                shopDetailsBottomView.Hide();
                return;
            }

            shopDetailsBottomView.SetData(ViewModel.SelectedItem);
            shopDetailsBottomView.Show();
        }

        private void SetCamera()
        {
            var coordinates = new LatLng(centerCoordinates.Latitude ?? 0, centerCoordinates.Longitude ?? 0);
            var currentZoom = zoom == 0 ? Core.Common.Constants.Map.DefaultZoomFactor : zoom;
            var cameraPosition = CameraPosition.FromLatLngZoom(coordinates, currentZoom);
            googleMap?.MoveCamera(CameraUpdateFactory.NewCameraPosition(cameraPosition));
        }

        private class WrappedObject<TData> : Java.Lang.Object
        {
            public WrappedObject(TData data)
            {
                Data = data;
            }

            public TData Data { get; }
        }
    }
}
