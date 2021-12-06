using Android.Views;
using Android.Widget;
using AndroidX.CardView.Widget;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Adapters.ViewHolders;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using SushiShop.Core.Data.Models.Stickers;
using SushiShop.Core.ViewModels.Menu.Items;
using SushiShop.Droid.Extensions;
using SushiShop.Droid.Views.ViewHolders.Abstract;

namespace SushiShop.Droid.Views.ViewHolders.Menu.Products
{
    public class ProductStickerViewHolder : CardViewHolder<StickerViewModel>
    {
        private CardView _stickerCard;
        private ImageView _stickerImage;

        public ProductStickerViewHolder(View view, IMvxAndroidBindingContext context) : base(view, context)
        {
        }

        
        protected override void DoInit(View view)
        {
            base.DoInit(view);

            _stickerImage = view.FindViewById<ImageView>(Resource.Id.sticker_image);
            _stickerCard = view.FindViewById<CardView>(Resource.Id.sticker_card);
        }

        public override void BindData()
        {
            base.BindData();

            using var bindingSet = CreateBindingSet();

            bindingSet.Bind(_stickerCard).For(v => v.CardBackgroundColor).To(vm => vm.BackgroundColor);
            bindingSet.Bind(_stickerImage).For(v => v.BindUrl()).To(vm => vm.ImageUrl);
        }
    }
}