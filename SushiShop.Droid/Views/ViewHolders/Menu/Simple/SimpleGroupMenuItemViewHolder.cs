using Android.Views;
using Android.Widget;
using AndroidX.ConstraintLayout.Widget;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Extensions;
using MvvmCross.Platforms.Android.Binding;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using SushiShop.Core.ViewModels.Menu.Items;
using SushiShop.Droid.Extensions;
using SushiShop.Droid.Views.ViewHolders.Abstract;

namespace SushiShop.Droid.Views.ViewHolders.Menu.Simple
{
    public class SimpleGroupMenuItemViewHolder : CardViewHolder<GroupMenuItemViewModel>
    {
        private TextView titleTextView;
        private ImageView imageView;
        private View view;

        public SimpleGroupMenuItemViewHolder(View view, IMvxAndroidBindingContext context) : base(view, context)
        {
        }

        protected override void DoInit(View view)
        {
            base.DoInit(view);

            this.view = view;

            var contentConstraintLayout = view.FindViewById<ConstraintLayout>(Resource.Id.content_constraint_layout);
            contentConstraintLayout.SetRoundedCorners(view.Context.DpToPx(4));

            titleTextView = view.FindViewById<TextView>(Resource.Id.group_title_text_view);
            imageView = view.FindViewById<ImageView>(Resource.Id.group_image_view);
        }

        public override void BindData()
        {
            base.BindData();

            using var bindingSet = CreateBindingSet();

            bindingSet.Bind(titleTextView).For(v => v.Text).To(v => v.Title);
            bindingSet.Bind(view).For(v => v.BindClick()).To(v => v.ShowDetailsCommand);
            bindingSet.Bind(imageView).For(v => v.BindUrl()).To(v => v.ImageUrl);
        }
    }
}
