using Android.Views;
using Android.Widget;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Extensions;
using MvvmCross.Platforms.Android.Binding;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using SushiShop.Core.ViewModels.Menu.Items;
using SushiShop.Droid.Views.ViewHolders.Abstract;

namespace SushiShop.Droid.Views.ViewHolders.Menu.Simple
{
    public class SimpleMenuItemViewHolder : CardViewHolder<CategoryMenuItemViewModel>
    {
        private TextView titleTextView;
        private View view;

        public SimpleMenuItemViewHolder(View view, IMvxAndroidBindingContext context) : base(view, context)
        {
        }

        protected override void DoInit(View view)
        {
            base.DoInit(view);

            this.view = view;

            var contentFrameLayout = view.FindViewById<FrameLayout>(Resource.Id.content_frame_layout);
            contentFrameLayout.SetRoundedCorners(view.Context.DpToPx(4));

            titleTextView = view.FindViewById<TextView>(Resource.Id.title_text_view);
        }

        public override void BindData()
        {
            base.BindData();

            using var bindingSet = CreateBindingSet();

            bindingSet.Bind(titleTextView).For(v => v.Text).To(v => v.Title);
            bindingSet.Bind(titleTextView).For(v => v.BindClick()).To(v => v.ShowDetailsCommand);
            bindingSet.Bind(view).For(v => v.BindClick()).To(v => v.ShowDetailsCommand);
        }
    }
}