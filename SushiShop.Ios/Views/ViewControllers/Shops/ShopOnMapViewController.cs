using BuildApps.Core.Mobile.MvvmCross.UIKit.Views.ViewControllers;
using CoreLocation;
using Google.Maps;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using SushiShop.Core.ViewModels.Shops;
using SushiShop.Core.ViewModels.Shops.Items;
using SushiShop.Ios.Common;
using SushiShop.Ios.Views.Controls;
using UIKit;

namespace SushiShop.Ios.Views.ViewControllers.Shops
{
    [MvxChildPresentation]
    public partial class ShopOnMapViewController : BaseViewController<ShopOnMapViewModel>
    {
        private MapView mapView;
        private UIButton backButton;
        private ShopDetailsBottomView shopDetailsBottomView;

        public ShopOnMapViewController() : base("ShopOnMapViewController", null)
        {
        }

        public ShopItemViewModel SelectedItemViewModel
        {
            set => UpdateMarker(value);
        }

        protected override void InitStylesAndContent()
        {
            base.InitStylesAndContent();

            shopDetailsBottomView = ShopDetailsBottomView.Create();

            var target = new CLLocationCoordinate2D(Core.Common.Constants.Map.MapStartPointLatitude, Core.Common.Constants.Map.MapStartPointLongitude);
            var camera = CameraPosition.FromCamera(target, 5);
            mapView = new MapView
            {
                Camera = camera,
                TranslatesAutoresizingMaskIntoConstraints = false,
                MyLocationEnabled = false
            };

            MapViewContainer.AddSubview(mapView);
            NSLayoutConstraint.ActivateConstraints(new[]
            {
                mapView.TopAnchor.ConstraintEqualTo(MapViewContainer.TopAnchor),
                mapView.BottomAnchor.ConstraintEqualTo(MapViewContainer.BottomAnchor),
                mapView.TrailingAnchor.ConstraintEqualTo(MapViewContainer.TrailingAnchor),
                mapView.LeadingAnchor.ConstraintEqualTo(MapViewContainer.LeadingAnchor),
            });
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            shopDetailsBottomView.Show();
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);

            shopDetailsBottomView.Hide();
        }

        protected override void InitNavigationItem(UINavigationItem navigationItem)
        {
            base.InitNavigationItem(navigationItem);

            backButton = Components.CreateDefaultBarButton(ImageNames.ArrowBack);
            navigationItem.LeftBarButtonItem = new UIBarButtonItem(backButton);
            backButton.AddGestureRecognizer(new UITapGestureRecognizer(() => ViewModel?.PlatformCloseCommand?.Execute(null)));
        }

        protected override void Bind()
        {
            base.Bind();

            var bindingSet = this.CreateBindingSet<ShopOnMapViewController, ShopOnMapViewModel>();

            bindingSet.Bind(this).For(v => v.Title).To(vm => vm.Title);
            bindingSet.Bind(this).For(nameof(SelectedItemViewModel)).To(vm => vm.ShopItemViewModel);
            bindingSet.Bind(shopDetailsBottomView).For(v => v.DataContext).To(vm => vm.ShopItemViewModel);

            bindingSet.Apply();
        }

        private void UpdateMarker(ShopItemViewModel viewModel)
        {
            BeginInvokeOnMainThread(() =>
            {
                mapView.Clear();
                var position = new CLLocationCoordinate2D(viewModel.Latitude, viewModel.Longitude);
                var marker = Marker.FromPosition(position);
                marker.Map = mapView;
                marker.Icon = UIImage.FromBundle(ImageNames.DefaultMarker);
                mapView.MoveCamera(CameraUpdate.SetTarget(position, 10));
            });
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (disposing)
            {
                shopDetailsBottomView?.Dispose();
            }
        }
    }
}