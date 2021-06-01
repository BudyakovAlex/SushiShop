using Android.Views;
using Android.Widget;
using MvvmCross.Platforms.Android.Binding;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using SushiShop.Core.ViewModels.Info.Items;
using SushiShop.Droid.Extensions;
using SushiShop.Droid.Views.ViewHolders.Abstract;

namespace SushiShop.Droid.Views.ViewHolders.Info
{
    public class SocialNetworkItemViewHolder : CardViewHolder<SocialNetworkItemViewModel>
    {
        private View view;
        private ImageView socialNetworkImageView;

        public SocialNetworkItemViewHolder(View view, IMvxAndroidBindingContext context) : base(view, context)
        {
        }

        protected override void DoInit(View view)
        {
            base.DoInit(view);

            this.view = view;
            socialNetworkImageView = view.FindViewById<ImageView>(Resource.Id.social_network_image_view);
        }

        public override void BindData()
        {
            base.BindData();

            using var bindingSet = CreateBindingSet();

            bindingSet.Bind(view).For(v => v.BindClick()).To(vm => vm.OpenBrowserCommand);
            bindingSet.Bind(socialNetworkImageView).For(v => v.BindUrl()).To(vm => vm.ImageUrl);
        }
    }
}
