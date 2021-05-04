#nullable enable

using Android.Content;
using Android.Runtime;
using Android.Util;
using Android.Widget;
using AndroidX.RecyclerView.Widget;
using BuildApps.Core.Mobile.Common.Extensions;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Extensions;
using MvvmCross.Binding.Extensions;
using SushiShop.Droid.Platform.Listeners;
using System;
using System.Collections;
using System.Collections.Specialized;

namespace SushiShop.Droid.Views.Controls
{
    [Register(nameof(SushiShop) + "." + nameof(PagesIndicatorView))]
    public class PagesIndicatorView : LinearLayout
    {
        private LinearLayoutManager? layoutManager;
        private int currentActiveIndex;

        private IDisposable? collectionChangedSubscription;

        public PagesIndicatorView(Context context) : base(context)
        {
            Initialize();
        }

        public PagesIndicatorView(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            Initialize();
        }

        public PagesIndicatorView(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
            Initialize();
        }

        public PagesIndicatorView(Context context, IAttributeSet attrs, int defStyleAttr, int defStyleRes) : base(context, attrs, defStyleAttr, defStyleRes)
        {
            Initialize();
        }

        protected PagesIndicatorView(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
            Initialize();
        }

        private IEnumerable? items;
        public IEnumerable? Items
        {
            get => items;
            set
            {
                if (value is INotifyCollectionChanged notifyCollectionChanged)
                {
                    collectionChangedSubscription?.Dispose();
                    collectionChangedSubscription = notifyCollectionChanged.SubscribeToCollectionChanged(OnItemsCollectionChanged);
                }

                items = value;
                RefreshControlState();
            }
        }

        private void RefreshControlState()
        {
            Post(() =>
            {
                RemoveAllViews();
                if (Items.IsNullOrEmpty())
                {
                    return;
                }

                var itemsCount = Items.Count();
                if (itemsCount - 1 < currentActiveIndex)
                {
                    currentActiveIndex = 0;
                }

                for (int i = 0; i < itemsCount; i++)
                {
                    var isActive = currentActiveIndex == i;
                    var indicatorView = new IndicatorItemView(Context!, isActive);
                    AddView(indicatorView);
                }
            });
        }

        private void OnItemsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e) =>
            RefreshControlState();

        private void Initialize()
        {
            Orientation = Orientation.Horizontal;
        }

        public void AttachToRecyclerView(RecyclerView recyclerView)
        {
            if (recyclerView != null)
            {
                recyclerView.AddOnScrollListener(new RecyclerViewOnScrollListener(OnRecyclerViewScrolled));
                layoutManager = recyclerView.GetLayoutManager() as LinearLayoutManager;
                return;
            }

            layoutManager = null;
        }

        private void OnRecyclerViewScrolled(RecyclerView recyclerView, int dx, int dy)
        {
            if (layoutManager is null)
            {
                return;
            }

            var newSelctedIndex = dx < 0
                ? layoutManager.FindFirstVisibleItemPosition()
                : layoutManager.FindLastVisibleItemPosition();

            RefreshSelectedItem(currentActiveIndex, newSelctedIndex);
        }

        private void RefreshSelectedItem(int previousSelectedIndex, int currentSelectedIndex)
        {
            currentActiveIndex = currentSelectedIndex;

            var previousItemView = GetChildAt(previousSelectedIndex);
            var currentItemView = GetChildAt(currentActiveIndex);

            if (previousItemView != null)
            {
                previousItemView.SetBackgroundResource(Resource.Drawable.ic_page_indicator_inactive);
            }
            
            if (currentItemView != null)
            {
                currentItemView.SetBackgroundResource(Resource.Drawable.ic_page_indicator_active);
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (!disposing)
            {
                return;
            }

            collectionChangedSubscription?.Dispose();
            collectionChangedSubscription = null;
        }

        private class IndicatorItemView : FrameLayout
        {
            public IndicatorItemView(Context context, bool isActive = false)
                : base(context)
            {
                Initialize(context, isActive);
            }

            protected IndicatorItemView(IntPtr javaReference, JniHandleOwnership transfer)
                : base(javaReference, transfer)
            {
            }

            private void Initialize(Context context, bool isActive)
            {
                var size = (int)context.DpToPx(10);
                var margin = (int)context.DpToPx(2);

                LayoutParameters = new MarginLayoutParams(size, size)
                {
                    MarginStart = margin,
                    MarginEnd = margin,
                };

                var backgroundResource = isActive
                    ? Resource.Drawable.ic_page_indicator_active
                    : Resource.Drawable.ic_page_indicator_inactive;

                SetBackgroundResource(backgroundResource);
            }
        }
    }
}