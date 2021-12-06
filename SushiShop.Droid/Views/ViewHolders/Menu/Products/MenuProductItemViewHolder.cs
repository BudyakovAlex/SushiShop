using Android.App;
using Android.Content;
using Android.Views;
using Android.Widget;
using AndroidX.CardView.Widget;
using AndroidX.RecyclerView.Widget;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Adapter.TemplateSelectors;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Adapters;
using MvvmCross.Commands;
using MvvmCross.DroidX.RecyclerView;
using MvvmCross.Platforms.Android.Binding;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using SushiShop.Core.Data.Models.Stickers;
using SushiShop.Core.ViewModels.Menu.Items;
using SushiShop.Droid.Extensions;
using SushiShop.Droid.Views.Controls;
using SushiShop.Droid.Views.ViewHolders.Abstract;
using SushiShop.Core.Extensions;
using SushiShop.Droid.Views.Activities;
using SushiShop.Droid.Views.Adapters;
using SushiShop.Droid.Views.Decorators;
using SushiShop.Droid.Views.Listeners;

namespace SushiShop.Droid.Views.ViewHolders.Menu.Products
{
    public class MenuProductItemViewHolder : CardViewHolder<ProductItemViewModel>
    {
        private ImageView productImageView;
        private TextView productNameTextView;
        private StepperView stepperView;
        private TextView priceTextView;
        private TextView oldPriceTextView;
        private CardView productCardView;
        private MvxRecyclerView stickersRecycler;

        public MenuProductItemViewHolder(View view, IMvxAndroidBindingContext context) : base(view, context)
        {
        }

        protected override void DoInit(View view)
        {
            base.DoInit(view);

            productImageView = view.FindViewById<ImageView>(Resource.Id.product_image_view);
            productNameTextView = view.FindViewById<TextView>(Resource.Id.product_text_view);
            stepperView = view.FindViewById<StepperView>(Resource.Id.product_stepper_view);
            priceTextView = view.FindViewById<TextView>(Resource.Id.price_text_view);
            oldPriceTextView = view.FindViewById<TextView>(Resource.Id.old_price_text_view);
            productCardView = view.FindViewById<CardView>(Resource.Id.product_card_view);
            
            stickersRecycler = view.FindViewById<MvxRecyclerView>(Resource.Id.stickers_recycler);
            stickersRecycler.Adapter = new RecycleViewBindableAdapter((IMvxAndroidBindingContext)BindingContext);
            stickersRecycler.ItemTemplateSelector = new TemplateSelector()
                .AddElement<StickerViewModel, ProductStickerViewHolder>(Resource.Layout.item_product_sticker_item);
            stickersRecycler.SetLayoutManager(new LinearLayoutManager(view.Context, LinearLayoutManager.Horizontal, false));
            
            oldPriceTextView.PaintFlags |= Android.Graphics.PaintFlags.StrikeThruText;
        }

        public override void BindData()
        {
            base.BindData();

            using var bindingSet = CreateBindingSet();

            bindingSet.Bind(stickersRecycler).For(v => v.ItemsSource).To(vm => vm.Stickers);
            bindingSet.Bind(productImageView).For(v => v.BindAdaptedUrl()).To(vm => vm.ImageUrl);
            bindingSet.Bind(productNameTextView).For(v => v.Text).To(vm => vm.Title);
            bindingSet.Bind(stepperView).For(v => v.DataContext).To(vm => vm.StepperViewModel);
            bindingSet.Bind(productCardView).For(v => v.BindClick()).To(vm => vm.ShowDetailsCommand);
            bindingSet.Bind(priceTextView).For(v => v.Text).To(vm => vm.Price);
            bindingSet.Bind(oldPriceTextView).For(v => v.Text).To(vm => vm.OldPrice);
            bindingSet.Bind(oldPriceTextView).For(v => v.Visibility).To(vm => vm.OldPrice)
                .WithConversion((string value) => string.IsNullOrEmpty(value) ? ViewStates.Gone : ViewStates.Visible);
        }
    }
}
