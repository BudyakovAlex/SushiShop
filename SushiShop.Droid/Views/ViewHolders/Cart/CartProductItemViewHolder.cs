using Android.Graphics;
using Android.Views;
using Android.Widget;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Adapter.TemplateSelectors;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Adapters;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Extensions;
using MvvmCross.DroidX.RecyclerView;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using SushiShop.Core.ViewModels.Cart.Items;
using SushiShop.Droid.Extensions;
using SushiShop.Droid.Views.Controls;
using SushiShop.Droid.Views.ViewHolders.Abstract;

namespace SushiShop.Droid.Views.ViewHolders.Cart
{
    public class CartProductItemViewHolder : CardViewHolder<CartProductItemViewModel>
    {
        private ImageView productImageView;
        private TextView productNameTextView;
        private TextView oldPriceTextView;
        private TextView priceTextView;
        private TextView weightTextView;
        private StepperView stepperView;
        private MvxRecyclerView toppingsRecyclerView;

        public CartProductItemViewHolder(View view, IMvxAndroidBindingContext context) : base(view, context)
        {
        }

        protected override void DoInit(View view)
        {
            base.DoInit(view);

            productImageView = view.FindViewById<ImageView>(Resource.Id.product_image_view);
            productNameTextView = view.FindViewById<TextView>(Resource.Id.product_name_text_view);
            oldPriceTextView = view.FindViewById<TextView>(Resource.Id.old_price_text_view);
            priceTextView = view.FindViewById<TextView>(Resource.Id.price_text_view);
            weightTextView = view.FindViewById<TextView>(Resource.Id.product_weight_text_view);
            stepperView = view.FindViewById<StepperView>(Resource.Id.product_stepper_view);

            oldPriceTextView.PaintFlags |= PaintFlags.StrikeThruText;
            productImageView.SetRoundedCorners(view.Context.DpToPx(6f));

            InitializeToppingsRecyclerView();
        }

        public override void BindData()
        {
            base.BindData();

            using var bindingSet = CreateBindingSet();

            bindingSet.Bind(productImageView).For(v => v.BindAdaptedUrl()).To(vm => vm.ImageUrl);
            bindingSet.Bind(productNameTextView).For(v => v.Text).To(vm => vm.Title);
            bindingSet.Bind(oldPriceTextView).For(v => v.Text).To(vm => vm.OldPrice);
            bindingSet.Bind(priceTextView).For(v => v.Text).To(vm => vm.Price);
            bindingSet.Bind(weightTextView).For(v => v.Text).To(vm => vm.Value);
            bindingSet.Bind(toppingsRecyclerView).For(v => v.ItemsSource).To(vm => vm.Toppings);
            bindingSet.Bind(stepperView).For(v => v.DataContext).To(vm => vm.StepperViewModel);
        }

        private void InitializeToppingsRecyclerView()
        {
            toppingsRecyclerView = ItemView.FindViewById<MvxRecyclerView>(Resource.Id.toppings_recycler_view);
            toppingsRecyclerView.Adapter = new RecycleViewBindableAdapter((IMvxAndroidBindingContext)BindingContext);
            toppingsRecyclerView.ItemTemplateSelector = new TemplateSelector()
                .AddElement<CartProductToppingItemViewModel, CartProductToppingItemViewHolder>(Resource.Layout.item_cart_product_topping);
        }
    }
}
