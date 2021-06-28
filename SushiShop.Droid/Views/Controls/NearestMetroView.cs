using Android.Content;
using Android.Graphics;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using AndroidX.Core.Content;
using AndroidX.Core.Content.Resources;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Adapter.TemplateSelectors;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Adapters;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Extensions;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Listeners;
using MvvmCross.DroidX.RecyclerView;
using SushiShop.Core.Resources;
using SushiShop.Core.ViewModels.Shops.Items;
using SushiShop.Droid.Extensions;
using SushiShop.Droid.Views.ViewHolders.Shops;
using System;
using System.Collections;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SushiShop.Droid.Views.Controls
{
    [Register(nameof(SushiShop) + "." + nameof(NearestMetroView))]
    public class NearestMetroView : FrameLayout, ViewTreeObserver.IOnGlobalLayoutListener
    {
        private MvxRecyclerView recyclerView;
        private TextView titleTextView;
        private LinearLayout containerLayout;
        private bool shouldTranslate = true;

        public NearestMetroView(Context context) : base(context)
        {
            Initialize();
        }

        public NearestMetroView(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            Initialize();
        }

        public NearestMetroView(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
            Initialize();
        }

        public NearestMetroView(Context context, IAttributeSet attrs, int defStyleAttr, int defStyleRes) : base(context, attrs, defStyleAttr, defStyleRes)
        {
            Initialize();
        }

        protected NearestMetroView(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
            Initialize();
        }

        public IEnumerable MetrosCollection
        {
            get => recyclerView.ItemsSource;
            set => recyclerView.ItemsSource = value;
        }

        public ICommand CloseCommand { get; set; }

        public void Hide(Action completionAction = null)
        {
            containerLayout.SlideDownAnimation(completionAction: () =>
            { 
                Visibility = ViewStates.Gone;
                completionAction?.Invoke();
            });
        }

        public void Show()
        {
            Visibility = ViewStates.Visible;
            containerLayout.SlideUpAnimation();
        }

        private void Initialize()
        {
            Visibility = ViewStates.Gone;

            InitializeContainerLinearLayout();
            InitializeTitleTextView();
            InitializeRecyclerView();

            SetOnClickListener(new ViewOnClickListener(OnContainerClickedAsync));
            ViewTreeObserver.AddOnGlobalLayoutListener(this);
        }

        public void OnGlobalLayout()
        {
            if (!shouldTranslate || Visibility == ViewStates.Visible)
            {
                return;
            }

            Hide();
        }

        private void InitializeContainerLinearLayout()
        {
            var frameLayoutParams = new FrameLayout.LayoutParams(
                    ViewGroup.LayoutParams.MatchParent,
                    ViewGroup.LayoutParams.WrapContent, GravityFlags.Bottom);

            containerLayout = new LinearLayout(Context)
            {
                LayoutParameters = frameLayoutParams,
                Orientation = Orientation.Vertical
            };

            containerLayout.SetTopRoundedCorners(Context.DpToPx(25));
            containerLayout.SetBackgroundResource(Resource.Color.white);
            AddView(containerLayout);
        }

        private Task OnContainerClickedAsync(View _)
        {
            Hide();
            CloseCommand?.Execute(null);

            return Task.CompletedTask;
        }

        private void InitializeTitleTextView()
        {
            var leftMargin = (int)Context.DpToPx(16);
            var topMargin = (int)Context.DpToPx(22);

            titleTextView = new TextView(Context)
            {
                LayoutParameters = new LayoutParams(
                   ViewGroup.LayoutParams.WrapContent,
                   ViewGroup.LayoutParams.WrapContent)
                {
                    MarginStart = leftMargin,
                    TopMargin = topMargin
                },
                TextSize = 18,
                Text = AppStrings.NearestMetroStation
            };

            var titleColorArgb = ContextCompat.GetColor(Context, Resource.Color.gray2);
            titleTextView.SetTextColor(new Color(titleColorArgb));

            var typeface = ResourcesCompat.GetFont(Context, Resource.Font.sf_pro_display_medium);
            titleTextView.SetTypeface(typeface, TypefaceStyle.Normal);
            containerLayout.AddView(titleTextView);
        }

        private void InitializeRecyclerView()
        {
            var topMargin = (int)Context.DpToPx(8);
            recyclerView = new MvxRecyclerView(Context, null)
            {
                LayoutParameters = new LayoutParams(
                   ViewGroup.LayoutParams.MatchParent,
                   ViewGroup.LayoutParams.WrapContent)
                {
                    TopMargin = topMargin,
                },

                NestedScrollingEnabled = false
            };

            containerLayout.AddView(recyclerView);

            recyclerView.Adapter = new RecycleViewBindableAdapter();
            recyclerView.ItemTemplateSelector = new TemplateSelector()
                .AddElement<MetroItemViewModel, NearestMetroItemViewHolder>(Resource.Layout.item_nearest_metro);
        }
    }
}