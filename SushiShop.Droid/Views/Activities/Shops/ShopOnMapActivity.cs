using System;
using Android.App;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.Views;
using Android.Widget;
using AndroidX.ConstraintLayout.Widget;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Adapter.TemplateSelectors;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Adapters;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Extensions;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Listeners;
using MvvmCross.Binding.Combiners;
using MvvmCross.DroidX.RecyclerView;
using MvvmCross.Platforms.Android.Binding;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using SushiShop.Core.Resources;
using SushiShop.Core.ViewModels.Common.Items;
using SushiShop.Core.ViewModels.Shops;
using SushiShop.Core.ViewModels.Shops.Items;
using SushiShop.Droid.Extensions;
using SushiShop.Droid.Views.Activities.Abstract;
using SushiShop.Droid.Views.ViewHolders.Feedback;
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
        private LinearLayout infoShopLinearLayout;
        private TextView titleShopTextView;
        private TextView phoneShopTextView;
        private TextView timeWorkingShopTextView;
        private TextView driveWayShopTitleTextView;
        private TextView driveWayShopTextView;
        private TextView galleryShopTitleTextView;
        private MvxRecyclerView galleryShopRecyclerView;
        private ScrollView contentShopScrollView;
        private bool isMoved;
        private float startRawY;
        private float startViewY;
        private bool isExpandedInfoShop;

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
            infoShopLinearLayout = FindViewById<LinearLayout>(Resource.Id.info_shop_linear_layout);
            titleShopTextView = infoShopLinearLayout.FindViewById<TextView>(Resource.Id.title_shop_text_view);
            phoneShopTextView = infoShopLinearLayout.FindViewById<TextView>(Resource.Id.phone_shop_text_view);
            timeWorkingShopTextView = infoShopLinearLayout.FindViewById<TextView>(Resource.Id.time_working_shop_text_view);
            driveWayShopTitleTextView = infoShopLinearLayout.FindViewById<TextView>(Resource.Id.drive_way_shop_title_text_view);
            driveWayShopTextView = infoShopLinearLayout.FindViewById<TextView>(Resource.Id.drive_way_shop_text_view);
            galleryShopTitleTextView = infoShopLinearLayout.FindViewById<TextView>(Resource.Id.gallery_title_text_view);
            galleryShopRecyclerView = infoShopLinearLayout.FindViewById<MvxRecyclerView>(Resource.Id.gallery_recycler_view);
            contentShopScrollView = infoShopLinearLayout.FindViewById<ScrollView>(Resource.Id.content_shop_scroll_view);

            InitializeMap();
            InitializeGalleryRecyclerView();

            driveWayShopTitleTextView.Text = AppStrings.DriveWay;
            galleryShopTitleTextView.Text = AppStrings.Gallery;

            infoShopLinearLayout.SetTopRoundedCorners(this.DpToPx(25));

            infoShopLinearLayout.SetOnTouchListener(new ViewOnTouchListener(OnInfoShopLinearLayoutTouch));
            infoShopLinearLayout.Visibility = ViewStates.Gone;
            contentShopScrollView.SetOnTouchListener(new ViewOnTouchListener(OnInfoShopScrollViewTouch));
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

        private void InitializeGalleryRecyclerView()
        {
            galleryShopRecyclerView.Adapter = new RecycleViewBindableAdapter((IMvxAndroidBindingContext)BindingContext);
            galleryShopRecyclerView.ItemTemplateSelector = new TemplateSelector()
                .AddElement<PhotoItemViewModel, FeedbackPhotoItemViewHolder>(Resource.Layout.item_feedback_photo);

            var layoutManager = new MvxGuardedLinearLayoutManager(this) { Orientation = MvxGuardedLinearLayoutManager.Horizontal };
            galleryShopRecyclerView.SetLayoutManager(layoutManager);
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

            titleShopTextView.Text = viewModel.LongTitle;
            phoneShopTextView.Text = viewModel.Phone;
            timeWorkingShopTextView.Text = viewModel.WorkingTime;
            driveWayShopTextView.Text = viewModel.DriveWay;
            galleryShopRecyclerView.ItemsSource = viewModel.Photos;

            driveWayShopTextView.Visibility = driveWayShopTitleTextView.Visibility = viewModel.HasDriveWay ? ViewStates.Visible : ViewStates.Gone;
            galleryShopTitleTextView.Visibility = galleryShopRecyclerView.Visibility = viewModel.HasPhotos ? ViewStates.Visible : ViewStates.Gone;

            var height = mapFragment.View.Height / 3;
            infoShopLinearLayout.LayoutParameters = new ConstraintLayout.LayoutParams(ConstraintLayout.LayoutParams.MatchParent, height);
            infoShopLinearLayout.TranslationY = mapFragment.View.Height - height + toolbar.Height;
            infoShopLinearLayout.Visibility = ViewStates.Visible;
        }

        private bool OnInfoShopLinearLayoutTouch(View view, MotionEvent e)
        {
            ActionTouch(e);
            return false;
        }

        private bool OnInfoShopScrollViewTouch(View view, MotionEvent e)
        {
            ActionTouch(e);
            return false;
        }

        private void ActionTouch(MotionEvent e)
        {
            int height = 0;
            var location = new int[2];
            switch (e.ActionMasked)
            {
                case MotionEventActions.Down:
                    startRawY = e.RawY;
                    startViewY = e.GetY();
                    isMoved = false;
                    break;
                case MotionEventActions.Move:
                    isMoved = true;
                    mapFragment.View.GetLocationOnScreen(location);
                    height = mapFragment.View.Height - ((int)e.RawY - location[1] - (int)startViewY);

                    if (height < mapFragment.View.Height)
                    {
                        contentShopScrollView.ScrollTo(0, 0);
                    }

                    if (height > mapFragment.View.Height - location[1] - this.DpToPx(18))
                    {
                        return;
                    }

                    break;
                case MotionEventActions.Up:
                case MotionEventActions.Cancel:
                    if (!isMoved)
                    {
                        break;
                    }

                    if (e.RawY > startRawY)
                    {
                        height = (int)(mapFragment.View.Height * 0.3);
                        isExpandedInfoShop = !isExpandedInfoShop;
                    }
                    else
                    {
                        height = mapFragment.View.Height - location[1] - (int)this.DpToPx(18);
                        isExpandedInfoShop = !isExpandedInfoShop;
                    }

                    break;
            }

            if (!isMoved)
            {
                return;
            }

            infoShopLinearLayout.LayoutParameters = new ConstraintLayout.LayoutParams(ConstraintLayout.LayoutParams.MatchParent, height);
            infoShopLinearLayout.TranslationY = mapFragment.View.Height - height + toolbar.Height;
        }
    }
}
