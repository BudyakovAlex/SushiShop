using Android.Views;
using Android.Widget;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using SushiShop.Core.ViewModels.CardProduct.Items;
using SushiShop.Droid.Views.Controls;
using SushiShop.Droid.Views.ViewHolders.Abstract;

namespace SushiShop.Droid.Views.ViewHolders.Toppings
{
    public class ToppingItemViewHolder : CardViewHolder<ToppingItemViewModel>
    {
        private TextView titleTextView;
        private TextView priceTextView;
        private StepperView stepperView;

        public ToppingItemViewHolder(View view, IMvxAndroidBindingContext context) : base(view, context)
        {
        }

        protected override void DoInit(View view)
        {
            base.DoInit(view);

            titleTextView = view.FindViewById<TextView>(Resource.Id.title_text_view);
            priceTextView = view.FindViewById<TextView>(Resource.Id.price_text_view);
            stepperView = view.FindViewById<StepperView>(Resource.Id.stepper_view);
        }

        public override void BindData()
        {
            base.BindData();

            using var bindingSet = CreateBindingSet();

            bindingSet.Bind(titleTextView).For(v => v.Text).To(vm => vm.Title);
            bindingSet.Bind(priceTextView).For(v => v.Text).To(vm => vm.Price);
            bindingSet.Bind(stepperView).For(v => v.DataContext).To(vm => vm.StepperViewModel);
        }
    }
}