using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.Widget;
using AndroidX.ConstraintLayout.Widget;
using AndroidX.Core.View;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Adapter.TemplateSelectors;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Adapters;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Extensions;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Listeners;
using Java.Lang;
using MvvmCross.Binding.BindingContext;
using MvvmCross.DroidX.RecyclerView;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using MvvmCross.Platforms.Android.Views;
using SushiShop.Core.Resources;
using SushiShop.Core.ViewModels.Common.Items;
using SushiShop.Core.ViewModels.Shops.Items;
using SushiShop.Droid.Extensions;
using SushiShop.Droid.Views.ViewHolders.Feedback;
using System;
using System.Threading.Tasks;

namespace SushiShop.Droid.Views.Controls
{
    public class ShopDetailsBottomView : LinearLayout, IMvxBindingContextOwner
    {
        private const int AnimationDuration = 250;
        private const float ExpandedViewPercent = 0.3f;
        private const float CollapsedViewPercent = 0.7f;

        private IMvxBindingContext bindingContext;
        private ConstraintLayout infoShopConstraintLayout;
        private TextView titleShopTextView;
        private TextView phoneShopTextView;
        private TextView timeWorkingShopTextView;
        private TextView driveWayShopTitleTextView;
        private TextView driveWayShopTextView;
        private TextView galleryShopTitleTextView;
        private MvxRecyclerView galleryShopRecyclerView;
        private ScrollView contentShopScrollView;
        private AppCompatButton pickupThereButton;
        private FrameLayout containerFrameLayout;
        private bool isMoved;
        private float startRawY;
        private float startViewY;
        private bool isExpandedInfoShop;
        private bool isShowedView;
        private bool isFirstOpen = true;

        public event EventHandler HideEvent;

        public ShopDetailsBottomView(Context context, IMvxBindingContext bindingContext) : base(context)
        {
            this.bindingContext = bindingContext;
            Initialize();
        }

        protected ShopDetailsBottomView(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        public bool CanClose { get; set; } = true;

        public IMvxBindingContext BindingContext
        {
            get => bindingContext;
            set => throw new NotImplementedException("BindingContext is readonly in the list item");
        }

        public void Show()
        {
            if (isShowedView)
            {
                return;
            }

            if (isFirstOpen)
            {
                infoShopConstraintLayout.TranslationY = containerFrameLayout.Height;
            }

            isShowedView = true;
            ViewCompat.Animate(infoShopConstraintLayout)
                    .SetDuration(250)
                    .WithStartAction(new Runnable(() =>
                    {
                        containerFrameLayout.AddView(this, new FrameLayout.LayoutParams(FrameLayout.LayoutParams.MatchParent, (int)Context.DpToPx(1200)));
                        infoShopConstraintLayout.Visibility = ViewStates.Visible;
                    }))
                    .TranslationY(containerFrameLayout.Height * CollapsedViewPercent)
                    .Start();
        }

        public void Hide()
        {
            isShowedView = false;
            ViewCompat.Animate(infoShopConstraintLayout)
                    .SetDuration(250)
                    .TranslationY(containerFrameLayout.Height)
                    .WithEndAction(new Runnable(() =>
                    {
                        infoShopConstraintLayout.Visibility = ViewStates.Gone;
                        HideEvent?.Invoke(this, EventArgs.Empty);
                        containerFrameLayout.RemoveView(this);
                    }))
                    .Start();
        }

        public void SetData(ShopItemViewModel viewModel)
        {
            titleShopTextView.Text = viewModel.LongTitle;
            phoneShopTextView.Text = viewModel.Phone;
            timeWorkingShopTextView.Text = viewModel.WorkingTime;
            driveWayShopTextView.Text = viewModel.DriveWay;
            galleryShopRecyclerView.ItemsSource = viewModel.Photos;
            pickupThereButton.Visibility = viewModel.IsSelectionMode ? ViewStates.Visible : ViewStates.Gone;
            pickupThereButton.SetOnClickListener(new ViewOnClickListener((view) => OnPickupButtonClickedAsync(viewModel)));

            driveWayShopTextView.Visibility = driveWayShopTitleTextView.Visibility = viewModel.HasDriveWay ? ViewStates.Visible : ViewStates.Gone;
            galleryShopTitleTextView.Visibility = galleryShopRecyclerView.Visibility = viewModel.HasPhotos ? ViewStates.Visible : ViewStates.Gone;
        }

        private Task OnPickupButtonClickedAsync(ShopItemViewModel viewModel)
        {
            viewModel?.ConfirmSelectionCommand.Execute(null);
            return Task.CompletedTask;
        }

        private void Initialize()
        {
            var inflater = LayoutInflater.From(Context);
            var view = inflater.Inflate(Resource.Layout.layout_shop_details, null, false);

            containerFrameLayout = (Context as MvxActivity).FindViewById<FrameLayout>(Resource.Id.container_view);

            infoShopConstraintLayout = view.FindViewById<ConstraintLayout>(Resource.Id.info_shop_constraint_layout);
            titleShopTextView = infoShopConstraintLayout.FindViewById<TextView>(Resource.Id.title_shop_text_view);
            phoneShopTextView = infoShopConstraintLayout.FindViewById<TextView>(Resource.Id.phone_shop_text_view);
            timeWorkingShopTextView = infoShopConstraintLayout.FindViewById<TextView>(Resource.Id.time_working_shop_text_view);
            driveWayShopTitleTextView = infoShopConstraintLayout.FindViewById<TextView>(Resource.Id.drive_way_shop_title_text_view);
            driveWayShopTextView = infoShopConstraintLayout.FindViewById<TextView>(Resource.Id.drive_way_shop_text_view);
            galleryShopTitleTextView = infoShopConstraintLayout.FindViewById<TextView>(Resource.Id.gallery_title_text_view);
            galleryShopRecyclerView = infoShopConstraintLayout.FindViewById<MvxRecyclerView>(Resource.Id.gallery_recycler_view);
            contentShopScrollView = infoShopConstraintLayout.FindViewById<ScrollView>(Resource.Id.content_shop_scroll_view);
            pickupThereButton = infoShopConstraintLayout.FindViewById<AppCompatButton>(Resource.Id.pickup_there_button);

            pickupThereButton.Text = AppStrings.TakeThere;
            driveWayShopTitleTextView.Text = AppStrings.DriveWay;
            galleryShopTitleTextView.Text = AppStrings.Gallery;

            infoShopConstraintLayout.SetTopRoundedCorners(view.Context.DpToPx(25));
            infoShopConstraintLayout.Visibility = ViewStates.Gone;
            infoShopConstraintLayout.SetOnTouchListener(new ViewOnTouchListener(OnInfoShopLinearLayoutTouch));
            contentShopScrollView.SetOnTouchListener(new ViewOnTouchListener(OnInfoShopScrollViewTouch));

            InitializeGalleryRecyclerView();

            AddView(view, new LinearLayout.LayoutParams(LinearLayout.LayoutParams.MatchParent, LinearLayout.LayoutParams.MatchParent));
        }

        private void InitializeGalleryRecyclerView()
        {
            galleryShopRecyclerView.Adapter = new RecycleViewBindableAdapter((IMvxAndroidBindingContext)BindingContext);
            galleryShopRecyclerView.ItemTemplateSelector = new TemplateSelector()
                .AddElement<PhotoItemViewModel, FeedbackPhotoItemViewHolder>(Resource.Layout.item_feedback_photo);

            var layoutManager = new MvxGuardedLinearLayoutManager(Context) { Orientation = MvxGuardedLinearLayoutManager.Horizontal };
            galleryShopRecyclerView.SetLayoutManager(layoutManager);
            galleryShopRecyclerView.OffsetLeftAndRight((int)Context.DpToPx(16));
        }

        private bool OnInfoShopLinearLayoutTouch(View view, MotionEvent e)
        {
            ActionTouch(e);
            return true;
        }

        private bool OnInfoShopScrollViewTouch(View view, MotionEvent e)
        {
            ActionTouch(e);
            return false;
        }

        private void ActionTouch(MotionEvent e)
        {
            switch (e.Action)
            {
                case MotionEventActions.Down:
                    startRawY = e.RawY;
                    startViewY = e.GetY();
                    isMoved = false;
                    return;

                case MotionEventActions.Move:
                    isMoved = true;
                    infoShopConstraintLayout.TranslationY = e.RawY - startViewY;
                    pickupThereButton.TranslationY = -(infoShopConstraintLayout.TranslationY + pickupThereButton.Height);
                    return;

                case MotionEventActions.Cancel:
                case MotionEventActions.Outside:
                case MotionEventActions.Up:
                    if (!isMoved)
                    {
                        return;
                    }

                    if (e.RawY > startRawY)
                    {
                        if (isExpandedInfoShop || !CanClose)
                        {
                            infoShopConstraintLayout.Animate()
                                .TranslationY(containerFrameLayout.Height * CollapsedViewPercent)
                                .SetDuration(AnimationDuration)
                                .Start();
                            isExpandedInfoShop = false;
                        }
                        else
                        {
                            Hide();
                        }
                    }
                    else
                    {
                        infoShopConstraintLayout.Animate()
                                .TranslationY(containerFrameLayout.Height * ExpandedViewPercent)
                                .SetDuration(AnimationDuration)
                                .Start();
                        isExpandedInfoShop = true;
                    }
                    break;
            }
        }
    }
}
