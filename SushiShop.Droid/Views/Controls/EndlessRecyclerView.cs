using Android.Content;
using Android.Runtime;
using Android.Util;
using AndroidX.RecyclerView.Widget;
using MvvmCross.DroidX.RecyclerView;
using SushiShop.Droid.Platform.Listeners;
using System;
using System.Windows.Input;

namespace SushiShop.Droid.Views.Controls
{
    [Register(nameof(SushiShop) + "." + nameof(EndlessRecyclerView))]
    public class EndlessRecyclerView : MvxRecyclerView
    {
        public const int DefaultStyle = -1;

        private EndlessScrollChangedListener scrollChangedListener;
        private LinearLayoutManager linearLayoutManager;

        protected EndlessRecyclerView(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {
        }

        public EndlessRecyclerView(Context context) : this(context, null)
        {
        }

        public EndlessRecyclerView(Context context, IAttributeSet attrs)
            : this(context, attrs, DefaultStyle)
        {
        }

        public EndlessRecyclerView(Context context, IAttributeSet attrs, int defStyle)
            : base(context, attrs, defStyle)
        {
            Initialize(context);
        }

        public ICommand LoadMoreItemsCommand { get; set; }

        private bool hasNextPage;
        public bool HasNextPage
        {
            get => hasNextPage;
            set
            {
                if (hasNextPage == value)
                {
                    return;
                }

                hasNextPage = value;
                SetOnScrollListenerState();
            }
        }

        private void SetOnScrollListenerState()
        {
            if (hasNextPage)
            {
                AddOnScrollListener(scrollChangedListener);
                return;
            }

            RemoveOnScrollListener(scrollChangedListener);
        }

        private void Initialize(Context context)
        {
            scrollChangedListener = new EndlessScrollChangedListener
            {
                LoadMoreAction = OnLoadMore
            };

            linearLayoutManager = new LinearLayoutManager(context, LinearLayoutManager.Vertical, false);
            SetLayoutManager(linearLayoutManager);
        }

        private void OnLoadMore()
        {
            LoadMoreItemsCommand?.Execute(null);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                SetOnScrollChangeListener(null);
                RemoveOnScrollListener(scrollChangedListener);

                if (scrollChangedListener != null)
                {
                    scrollChangedListener.LoadMoreAction = null;
                }

                scrollChangedListener = null;
            }

            base.Dispose(disposing);
        }
    }
}
