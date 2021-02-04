using System;
using System.Collections.Specialized;
using BuildApps.Core.Mobile.Common.Extensions;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Views.ViewControllers;
using CoreAnimation;
using CoreLocation;
using Google.Maps;
using MvvmCross.Platforms.Ios.Binding;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using MvvmCross.ViewModels;
using SushiShop.Core.Data.Models.Common;
using SushiShop.Core.Resources;
using SushiShop.Core.ViewModels.Orders;
using SushiShop.Core.ViewModels.Orders.Items;
using SushiShop.Ios.Common;
using SushiShop.Ios.Sources;
using SushiShop.Ios.Views.Cells.Orders;
using UIKit;

namespace SushiShop.Ios.Views.ViewControllers.Orders
{
    [MvxModalPresentation(WrapInNavigationController = true)]
    public partial class SelectOrderDeliveryAddressViewController : BaseViewController<SelectOrderDeliveryAddressViewModel>
    {
        private UIButton backButton;
        private SelectOrderDeliverySearchController searchSuggestionsController;
        private TableViewSource searchSuggestionsTableViewSource;
        private MapView mapView;
        private IDisposable subscription;

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

        protected override void InitStylesAndContent()
        {
            base.InitStylesAndContent();

            Title = AppStrings.SelectOrderDeliveryTitle;

            DeliveryThereButton.WithoutRadius = true;
            DefinesPresentationContext = true;
            ExtendedLayoutIncludesOpaqueBars = true;
            AboutPointView.Layer.MaskedCorners = CACornerMask.MinXMinYCorner | CACornerMask.MaxXMinYCorner;
            AboutPointView.Layer.CornerRadius = 16f;

            searchSuggestionsController = new SelectOrderDeliverySearchController();
            searchSuggestionsController.TableView.Source = searchSuggestionsTableViewSource = new TableViewSource(searchSuggestionsController.TableView)
                .Register<OrderDeliverySuggestionItemViewModel>(OrderDeliverySuggestionItemViewCell.Nib, OrderDeliverySuggestionItemViewCell.Key);
            searchSuggestionsController.SearchBar.SearchTextField.Placeholder = AppStrings.EnterYourAddress;
            searchSuggestionsController.SearchBar.TintColor = Colors.Orange2;
            searchSuggestionsController.CancelButtonText = AppStrings.Cancel;

            var target = new CLLocationCoordinate2D(Core.Common.Constants.Map.MapStartPointLatitude, Core.Common.Constants.Map.MapStartPointLongitude);
            var camera = CameraPosition.FromCamera(target, 5);
            mapView = new MapView
            {
                Camera = camera,
                TranslatesAutoresizingMaskIntoConstraints = false,
                MyLocationEnabled = true
            };
            mapView.CoordinateTapped += MapViewCoordinateTapped;

            MapViewContainer.AddSubview(mapView);
            NSLayoutConstraint.ActivateConstraints(new[]
            {
                mapView.TopAnchor.ConstraintEqualTo(MapViewContainer.TopAnchor),
                mapView.BottomAnchor.ConstraintEqualTo(MapViewContainer.BottomAnchor),
                mapView.TrailingAnchor.ConstraintEqualTo(MapViewContainer.TrailingAnchor),
                mapView.LeadingAnchor.ConstraintEqualTo(MapViewContainer.LeadingAnchor)
            });
        } 

        protected override void InitNavigationItem(UINavigationItem navigationItem)
        {
            base.InitNavigationItem(navigationItem);
            
            backButton = Components.CreateDefaultBarButton(ImageNames.ArrowBack);
            navigationItem.LeftBarButtonItem = new UIBarButtonItem(backButton);
            navigationItem.SearchController = searchSuggestionsController;
        }

        protected override void Bind()
        {
            base.Bind();

            var bindingSet = CreateBindingSet();

            bindingSet.Bind(backButton).For(v => v.BindTouchUpInside()).To(vm => vm.CloseCommand);
            bindingSet.Bind(searchSuggestionsController.SearchBar).For(v => v.Text).To(vm => vm.AddressQuery);
            bindingSet.Bind(searchSuggestionsTableViewSource).For(v => v.ItemsSource).To(vm => vm.Suggestions);
            bindingSet.Bind(this).For(nameof(HasSelectedLocation)).To(vm => vm.HasSelectedLocation);
            bindingSet.Bind(this).For(nameof(Items)).To(vm => vm.Items);
            bindingSet.Bind(AddressSelectedPointLabel).For(v => v.Text).To(vm => vm.SelectedLocation.Address);
            bindingSet.Bind(DeliveryPriceLabel).For(v => v.Text).To(vm => vm.SelectedLocation.DeliveryPrice);
            bindingSet.Bind(BadLocationLabel).For(v => v.BindHidden()).To(vm => vm.SelectedLocation.IsDeliveryAvailable);
            bindingSet.Bind(DeliveryThereButton).For(v => v.BindVisible()).To(vm => vm.SelectedLocation.IsDeliveryAvailable);
            bindingSet.Bind(DeliveryPriceLabel).For(v => v.BindVisible()).To(vm => vm.SelectedLocation.IsDeliveryAvailable);
            bindingSet.Bind(DeliveryThereButton).For(v => v.BindTouchUpInside()).To(vm => vm.ConfirmAddress);

            bindingSet.Apply();
        }

        private void OnItemsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            UpdateZones();
        }

        private void UpdateZones()
        {
            BeginInvokeOnMainThread(() =>
            {
                mapView.Clear();

                foreach (var item in items)
                {
                    var path = new MutablePath();
                    foreach (var zone in item.DeliveryZone.Polygon)
                    {
                        path.AddLatLon((double)zone.Latitude, (double)zone.Longitude);
                    }

                    var polygon = Polygon.FromPath(path);
                    polygon.FillColor = Colors.Orange2.ColorWithAlpha(0.3f);
                    polygon.StrokeColor = Colors.Orange2;
                    polygon.StrokeWidth = 2;
                    polygon.Map = mapView;
                }

                if (ViewModel.HasSelectedLocation)
                {
                    var position = new CLLocationCoordinate2D(ViewModel.SelectedLocation.Latitude ?? 0, ViewModel.SelectedLocation.Longitude ?? 0);
                    var marker = Marker.FromPosition(position);
                    marker.Map = mapView;
                    marker.Icon = UIImage.FromBundle(ImageNames.DefaultMarker);
                }
                else
                {
                    mapView.MoveCamera(CameraUpdate.SetTarget(new CLLocationCoordinate2D(ViewModel.Latitude, ViewModel.Longitude), ViewModel.ZoomFactor));
                }
            });
        }

        private void MapViewCoordinateTapped(object sender, GMSCoordEventArgs e)
        {
            ViewModel?.TryLoadPlacemarkCommand?.Execute(new Coordinates(e.Coordinate.Longitude, e.Coordinate.Latitude));
        }
    }
}

