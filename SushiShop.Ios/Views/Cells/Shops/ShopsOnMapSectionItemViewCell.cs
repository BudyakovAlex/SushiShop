using BuildApps.Core.Mobile.Common.Extensions;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Views.Cells;
using CoreLocation;
using Foundation;
using Google.Maps;
using MvvmCross.Binding.BindingContext;
using MvvmCross.ViewModels;
using SushiShop.Core.ViewModels.Shops.Items;
using SushiShop.Core.ViewModels.Shops.Sections;
using SushiShop.Ios.Common;
using SushiShop.Ios.Views.Controls;
using SushiShop.Ios.Views.ViewControllers;
using System;
using System.Collections.Specialized;
using UIKit;

namespace SushiShop.Ios.Views.Cells.Shops
{
    public partial class ShopsOnMapSectionItemViewCell : BaseCollectionViewCell, IUIAppearance
    {
        public static readonly NSString Key = new NSString(nameof(ShopsOnMapSectionItemViewCell));
        public static readonly UINib Nib = UINib.FromName(Key, NSBundle.MainBundle);

        private MainViewController rootViewController = UIApplication.SharedApplication.KeyWindow.RootViewController as MainViewController;
        private MapView mapView;
        private IDisposable subscription;
        private ShopDetailsBottomView shopDetailsBottomView;

        protected ShopsOnMapSectionItemViewCell(IntPtr handle)
            : base(handle)
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
            set => OpenOrHideExpandContentView(value?.IsSelected ?? false);
        }

        private void OnItemsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            UpdateMarkers();
        }

        protected ShopsOnMapSectionViewModel ViewModel => DataContext as ShopsOnMapSectionViewModel;

        protected override void Initialize()
        {
            base.Initialize();

            shopDetailsBottomView = ShopDetailsBottomView.Create();
            
            var target = new CLLocationCoordinate2D(Core.Common.Constants.Map.MapStartPointLatitude, Core.Common.Constants.Map.MapStartPointLongitude);
            var camera = CameraPosition.FromCamera(target, 5);
            mapView = new MapView
            {
                Camera = camera,
                TranslatesAutoresizingMaskIntoConstraints = false,
                MyLocationEnabled = false
            };

            mapView.TappedMarker += OnMarkerTapped;
            mapView.CoordinateTapped += OnCoordinateTapped;

            AddSubview(mapView);
            NSLayoutConstraint.ActivateConstraints(new[]
            {
                mapView.TopAnchor.ConstraintEqualTo(TopAnchor),
                mapView.BottomAnchor.ConstraintEqualTo(BottomAnchor),
                mapView.TrailingAnchor.ConstraintEqualTo(TrailingAnchor),
                mapView.LeadingAnchor.ConstraintEqualTo(LeadingAnchor)
            });
        }

        protected override void Bind()
        {
            base.Bind();

            var bindingSet = this.CreateBindingSet<ShopsOnMapSectionItemViewCell, ShopsOnMapSectionViewModel>();

            bindingSet.Bind(this).For(v => v.Items).To(vm => vm.Items);
            bindingSet.Bind(shopDetailsBottomView).For(v => v.DataContext).To(vm => vm.SelectedItem);
            bindingSet.Bind(this).For(nameof(SelectedItem)).To(vm => vm.SelectedItem);

            bindingSet.Apply();
        }

        private bool OnMarkerTapped(MapView map, Marker marker)
        {
            if (marker.UserData.IsNotNull() && marker.UserData is WrappedObject<ShopItemViewModel> wrappedObject)
            {
                SetMarker(wrappedObject.Data);
                return true;
            }

            return false;
        }

        private void OnCoordinateTapped(object sender, GMSCoordEventArgs e)
        {
            SetMarker(ViewModel.SelectedItem);
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

        private void UpdateMarkers()
        {
            BeginInvokeOnMainThread(() =>
            {
                mapView.Clear();

                foreach (var item in items)
                {
                    var position = new CLLocationCoordinate2D(item.Latitude, item.Longitude);
                    var marker = Marker.FromPosition(position);
                    var imageName = item.IsSelected ? ImageNames.SelectedMarker : ImageNames.DefaultMarker;
                    marker.Map = mapView;
                    marker.Icon = UIImage.FromBundle(imageName);
                    marker.UserData = new WrappedObject<ShopItemViewModel>(item);
                }
            });
        }

        private void OpenOrHideExpandContentView(bool isOpen)
        {
            if (isOpen)
            {
                shopDetailsBottomView.Show();
            }
            else
            {
                shopDetailsBottomView.Hide();
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                subscription?.Dispose();
                mapView.TappedMarker -= OnMarkerTapped;
                mapView.CoordinateTapped -= OnCoordinateTapped;
            }
        }

        private class WrappedObject<TData> : NSObject
        {
            public WrappedObject(TData data)
            {
                Data = data;
            }

            public TData Data { get; }
        }
    }
}