using Android.Views;
using Android.Widget;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Extensions;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using SushiShop.Core.ViewModels.Orders.Items;
using SushiShop.Droid.Extensions;
using SushiShop.Droid.Views.ViewHolders.Abstract;

namespace SushiShop.Droid.Views.ViewHolders.Orders
{
    public class OrderProductItemViewHolder : CardViewHolder<OrderProductItemViewModel>
    {
        private ImageView productImageView;
        private TextView productNameTextView;
        private TextView oldPriceTextView;
        private TextView priceTextView;
        private TextView productWeightTextView;
        private TextView countTextView;

        public OrderProductItemViewHolder(View view, IMvxAndroidBindingContext context) : base(view, context)
        {
        }

        protected override void DoInit(View view)
        {
            base.DoInit(view);

            productImageView = view.FindViewById<ImageView>(Resource.Id.product_image_view);
            productNameTextView = view.FindViewById<TextView>(Resource.Id.product_name_text_view);
            oldPriceTextView = view.FindViewById<TextView>(Resource.Id.old_price_text_view);
            priceTextView = view.FindViewById<TextView>(Resource.Id.price_text_view);
            productWeightTextView = view.FindViewById<TextView>(Resource.Id.product_weight_text_view);
            countTextView = view.FindViewById<TextView>(Resource.Id.count_text_view);

            productImageView.SetRoundedCorners(view.Context.DpToPx(6f));
        }

        public override void BindData()
        {
            base.BindData();

            using var bindingSet = CreateBindingSet();

            bindingSet.Bind(productImageView).For(v => v.BindUrl()).To(vm => vm.ImageUrl);
            bindingSet.Bind(productNameTextView).For(v => v.Text).To(vm => vm.Title);
            bindingSet.Bind(oldPriceTextView).For(v => v.Text).To(vm => vm.OldPrice);
            bindingSet.Bind(priceTextView).For(v => v.Text).To(vm => vm.Price);
            bindingSet.Bind(productWeightTextView).For(v => v.Text).To(vm => vm.Value);
            bindingSet.Bind(countTextView).For(v => v.Text).To(vm => vm.Count);
        }
    }
}
