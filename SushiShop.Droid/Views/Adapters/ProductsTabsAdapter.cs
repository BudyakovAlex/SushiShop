using System;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Extensions;
using MvvmCross.DroidX.RecyclerView;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using static AndroidX.RecyclerView.Widget.RecyclerView;

namespace SushiShop.Droid.Views.Adapters
{
    public class ProductsTabsAdapter : MvxRecyclerAdapter
    {
        private bool isClick;

        public ProductsTabsAdapter()
        {
        }

        public ProductsTabsAdapter(IMvxAndroidBindingContext bindingContext) : base(bindingContext)
        {
        }

        protected ProductsTabsAdapter(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
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
            return Resource.Layout.item_product_tab;
        }

        private void ChangeBackgroundSelectedItem(ViewHolder viewHolder)
        {
            var indicatorView = viewHolder.ItemView.FindViewById<View>(Resource.Id.indicator_tab);
            var textView = viewHolder.ItemView.FindViewById<TextView>(Resource.Id.tab_text_view);
            textView.Text = (viewHolder as MvxRecyclerViewHolder)?.DataContext?.ToString(); ;
            indicatorView.Visibility = viewHolder.LayoutPosition == selectedIndex ? ViewStates.Visible : ViewStates.Gone;
            indicatorView.SetRoundedCorners(viewHolder.ItemView.Context.DpToPx(2));
        }
    }
}
