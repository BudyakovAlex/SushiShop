using System;
using System.Collections.Specialized;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.Graphics;
using Android.Views;
using Android.Widget;
using AndroidX.ConstraintLayout.Widget;
using AndroidX.Core.View;
using BuildApps.Core.Mobile.Common.Extensions;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Adapter.TemplateSelectors;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Adapters;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Extensions;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Listeners;
using Java.Lang;
using MvvmCross.DroidX.RecyclerView;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using MvvmCross.Platforms.Android.Views;
using MvvmCross.ViewModels;
using SushiShop.Core.Data.Models.Common;
using SushiShop.Core.Resources;
using SushiShop.Core.ViewModels.Common.Items;
using SushiShop.Core.ViewModels.Shops.Items;
using SushiShop.Core.ViewModels.Shops.Sections;
using SushiShop.Droid.Extensions;
using SushiShop.Droid.Helpers;
using SushiShop.Droid.Views.ViewHolders.Abstract;
using SushiShop.Droid.Views.ViewHolders.Feedback;

namespace SushiShop.Droid.Views.ViewHolders.Shops.Sections
{
    public class ShopsOnMapSectionViewHolder : CardViewHolder<ShopsOnMapSectionViewModel>, IOnMapReadyCallback
    {
        private GoogleMap googleMap;
        private SupportMapFragment mapFragment;
        private IDisposable subscription;
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

            infoShopLinearLayout = view.FindViewById<LinearLayout>(Resource.Id.info_shop_linear_layout);
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

            infoShopLinearLayout.SetTopRoundedCorners(view.Context.DpToPx(25));

            infoShopLinearLayout.SetOnTouchListener(new ViewOnTouchListener(OnInfoShopLinearLayoutTouch));
            infoShopLinearLayout.Visibility = ViewStates.Gone;
            contentShopScrollView.SetOnTouchListener(new ViewOnTouchListener(OnInfoShopScrollViewTouch));
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

        private void InitializeGalleryRecyclerView()
        {
            galleryShopRecyclerView.Adapter = new RecycleViewBindableAdapter((IMvxAndroidBindingContext)BindingContext);
            galleryShopRecyclerView.ItemTemplateSelector = new TemplateSelector()
                .AddElement<PhotoItemViewModel, FeedbackPhotoItemViewHolder>(Resource.Layout.item_feedback_photo);

            var layoutManager = new MvxGuardedLinearLayoutManager(ItemView.Context) { Orientation = MvxGuardedLinearLayoutManager.Horizontal };
            galleryShopRecyclerView.SetLayoutManager(layoutManager);
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
                markerOptions.SetIcon(GetBitmapDescriptor(item.IsSelected ? Resource.Drawable.ic_selected_marker : Resource.Drawable.ic_default_marker));
                var marker = googleMap.AddMarker(markerOptions);
                marker.Tag = new WrappedObject<ShopItemViewModel>(item);
            }
        }

        private void OpenOrHideShopDetailsView()
        {
            if (ViewModel?.SelectedItem == null)
            {
                HideTabLayoutHelper.Instance.Show();
                ViewCompat.Animate(infoShopLinearLayout)
                    .SetDuration(250)
                    .TranslationY(ItemView.Height)
                    .WithEndAction(new Runnable(() =>
                    {
                        infoShopLinearLayout.Visibility = ViewStates.Gone;
                    }))
                    .Start();
                return;
            }

            HideTabLayoutHelper.Instance.Hide();

            titleShopTextView.Text = ViewModel.SelectedItem.LongTitle;
            phoneShopTextView.Text = ViewModel.SelectedItem.Phone;
            timeWorkingShopTextView.Text = ViewModel.SelectedItem.WorkingTime;
            driveWayShopTextView.Text = ViewModel.SelectedItem.DriveWay;
            galleryShopRecyclerView.ItemsSource = ViewModel.SelectedItem.Photos;

            driveWayShopTextView.Visibility = driveWayShopTitleTextView.Visibility = ViewModel.SelectedItem.HasDriveWay ? ViewStates.Visible : ViewStates.Gone;
            galleryShopTitleTextView.Visibility = galleryShopRecyclerView.Visibility = ViewModel.SelectedItem.HasPhotos ? ViewStates.Visible : ViewStates.Gone;

            if (infoShopLinearLayout.Visibility == ViewStates.Visible)
            {
                return;
            }

            var height = (ItemView.Height + (int)ItemView.Context.DpToPx(50)) / 3;
            infoShopLinearLayout.LayoutParameters = new ConstraintLayout.LayoutParams(ConstraintLayout.LayoutParams.MatchParent, height);
            infoShopLinearLayout.TranslationY = ItemView.Height + ItemView.Context.DpToPx(50) - height;
            infoShopLinearLayout.Visibility = ViewStates.Visible;
        }

        private BitmapDescriptor GetBitmapDescriptor(int id)
        {
            var vectorDrawable = ItemView.Context.GetDrawable(id);
            vectorDrawable.SetBounds(0, 0, vectorDrawable.IntrinsicWidth, vectorDrawable.IntrinsicHeight);
            var bm = Bitmap.CreateBitmap(vectorDrawable.IntrinsicWidth, vectorDrawable.IntrinsicHeight, Bitmap.Config.Argb8888);
            Canvas canvas = new Canvas(bm);
            vectorDrawable.Draw(canvas);
            return BitmapDescriptorFactory.FromBitmap(bm);
        }

        private void SetCamera()
        {
            var coordinates = new LatLng(centerCoordinates.Latitude ?? 0, centerCoordinates.Longitude ?? 0);
            var currentZoom = zoom == 0 ? Core.Common.Constants.Map.DefaultZoomFactor : zoom;
            var cameraPosition = CameraPosition.FromLatLngZoom(coordinates, currentZoom);
            googleMap?.MoveCamera(CameraUpdateFactory.NewCameraPosition(cameraPosition));
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
            switch (e.ActionMasked)
            {
                case MotionEventActions.Down:
                    startRawY = e.RawY;
                    startViewY = e.GetY();
                    isMoved = false;
                    break;
                case MotionEventActions.Move:
                    isMoved = true;
                    var location = new int[2];
                    ItemView.GetLocationOnScreen(location);
                    height = ItemView.Height - ((int)e.RawY - location[1] - (int)startViewY);

                    if (height < ItemView.Height)
                    {
                        contentShopScrollView.ScrollTo(0, 0);
                    }

                    if (height > ItemView.Height - ItemView.Context.DpToPx(18))
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
                        if (isExpandedInfoShop)
                        {
                            height = (int)(ItemView.Height * 0.3);
                            isExpandedInfoShop = !isExpandedInfoShop;
                        }
                        else
                        {
                            SetMarker(ViewModel?.SelectedItem);
                            return;
                        }
                    }
                    else
                    {
                        height = ItemView.Height - (int)ItemView.Context.DpToPx(18);
                        isExpandedInfoShop = !isExpandedInfoShop;
                    }

                    break;
            }

            infoShopLinearLayout.LayoutParameters = new ConstraintLayout.LayoutParams(ConstraintLayout.LayoutParams.MatchParent, height);
            infoShopLinearLayout.TranslationY = ItemView.Height - height;
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
