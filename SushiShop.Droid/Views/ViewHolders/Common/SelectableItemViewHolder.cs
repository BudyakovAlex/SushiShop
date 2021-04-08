using Android.Views;
using Android.Widget;
using BuildApps.Core.Mobile.MvvmCross.ViewModels.Simple;
using MvvmCross.Platforms.Android.Binding;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using SushiShop.Droid.Views.ViewHolders.Abstract;

namespace SushiShop.Droid.Views.ViewHolders.Common
{
    public class SelectableItemViewHolder : CardViewHolder<SelectableItemViewModel>
    {
        private TextView titleTextView;
        private ImageView checkmarkImageView;

        public SelectableItemViewHolder(View view, IMvxAndroidBindingContext context) : base(view, context)
        {
        }

        protected override void DoInit(View view)
        {
            base.DoInit(view);

            titleTextView = view.FindViewById<TextView>(Resource.Id.title_text_view);
            checkmarkImageView = view.FindViewById<ImageView>(Resource.Id.checkmark_image_view);
            checkmarkImageView.Visibility = ViewStates.Invisible;
        }

        public override void BindData()
        {
            base.BindData();

            using var bindingSet = CreateBindingSet();

            bindingSet.Bind(titleTextView).For(v => v.Text).To(vm => vm.Text);
            bindingSet.Bind(checkmarkImageView).For(v => v.BindVisible()).To(vm => vm.IsSelected);
        }
    }
}
