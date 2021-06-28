using System;
using Android.Content.Res;
using Android.Graphics;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.Core.Content;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Extensions;
using MvvmCross.DroidX.RecyclerView;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using static AndroidX.RecyclerView.Widget.RecyclerView;

namespace SushiShop.Droid.Views.Adapters
{
    public class TabsAdapter : MvxRecyclerAdapter
    {
        private readonly long resourceLayoutId;
        private bool isClick;

        public TabsAdapter()
        {
        }

        public TabsAdapter(IMvxAndroidBindingContext bindingContext, long resourceLayoutId) : base(bindingContext)
        {
            this.resourceLayoutId = resourceLayoutId;
        }

        protected TabsAdapter(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        public event EventHandler SelectedIndexChanged;

        private int selectedIndex = -1;
        public int SelectedIndex
        {
            get => selectedIndex;
            set
            {
                var previousSelectedIndex = selectedIndex;
                selectedIndex = value;
                NotifyItemChanged(previousSelectedIndex);
                NotifyItemChanged(selectedIndex);
                if (isClick)
                {
                    ItemClick?.Execute(selectedIndex);
                }

                SelectedIndexChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        protected override void OnItemViewClick(object sender, EventArgs e)
        {
            isClick = true;
            SelectedIndex = (sender as MvxRecyclerViewHolder).LayoutPosition;
            isClick = false;
        }

        public override void OnBindViewHolder(ViewHolder holder, int position)
        {
            base.OnBindViewHolder(holder, position);
            ChangeBackgroundSelectedItem(holder);
        }

        public override long GetItemId(int position)
        {
            return resourceLayoutId;
        }

        private void ChangeBackgroundSelectedItem(ViewHolder viewHolder)
        {
            var indicatorView = viewHolder.ItemView.FindViewById<View>(Resource.Id.indicator_tab);
            var textView = viewHolder.ItemView.FindViewById<TextView>(Resource.Id.tab_text_view);

            var selectedTextColorArgb = ContextCompat.GetColor(viewHolder.ItemView.Context, Resource.Color.black);
            var unselectedTextColorArgb = ContextCompat.GetColor(viewHolder.ItemView.Context, Resource.Color.gr);

            var textColorArgb = viewHolder.LayoutPosition == selectedIndex
                ? selectedTextColorArgb
                : unselectedTextColorArgb;

            textView.SetTextColor(ColorStateList.ValueOf(new Color(textColorArgb)));
            textView.Text = (viewHolder as MvxRecyclerViewHolder)?.DataContext?.ToString();

            indicatorView.Visibility = viewHolder.LayoutPosition == selectedIndex ? ViewStates.Visible : ViewStates.Gone;
            indicatorView.SetRoundedCorners(viewHolder.ItemView.Context.DpToPx(2));
        }
    }
}
