using Android.Views;
using Android.Widget;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using SushiShop.Core.ViewModels.Cart.Items;
using SushiShop.Droid.Views.ViewHolders.Abstract;

namespace SushiShop.Droid.Views.ViewHolders.Cart
{
    public class CartProductToppingItemViewHolder : CardViewHolder<CartProductToppingItemViewModel>
    {
        private TextView titleTextView;
        private TextView countTextView;

        public CartProductToppingItemViewHolder(View view, IMvxAndroidBindingContext context) : base(view, context)
        {
        }

        protected override void DoInit(View view)
        {
            base.DoInit(view);

            titleTextView = view.FindViewById<TextView>(Resource.Id.title_topping_text_view);
            countTextView = view.FindViewById<TextView>(Resource.Id.count_toppings_text_view);
        }

        public override void BindData()
        {
            base.BindData();

            using var bindingSet = CreateBindingSet();

            bindingSet.Bind(titleTextView).For(v => v.Text).To(vm => vm.Title);
            bindingSet.Bind(countTextView).For(v => v.Text).To(vm => vm.Count);
        }
    }
}
