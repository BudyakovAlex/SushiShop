using Android.Views;
using Android.Widget;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Extensions;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using SushiShop.Core.ViewModels.Profile.Items;
using SushiShop.Droid.Extensions;
using SushiShop.Droid.Views.ViewHolders.Abstract;

namespace SushiShop.Droid.Views.ViewHolders.Profile
{
    public class ImageBonusProgramItemViewHolder : CardViewHolder<BonusProgramImageItemViewModel>
    {
        private ImageView imageView;

        public ImageBonusProgramItemViewHolder(View view, IMvxAndroidBindingContext context) : base(view, context)
        {
        }

        protected override void DoInit(View view)
        {
            base.DoInit(view);

            imageView = view.FindViewById<ImageView>(Resource.Id.image_view);

            imageView.SetRoundedCorners(view.Context.DpToPx(35));
        }

        public override void BindData()
        {
            base.BindData();

            using var bindingSet = CreateBindingSet();

            bindingSet.Bind(imageView).For(v => v.BindUrl()).To(vm => vm.ImageUrl);
        }
    }
}
