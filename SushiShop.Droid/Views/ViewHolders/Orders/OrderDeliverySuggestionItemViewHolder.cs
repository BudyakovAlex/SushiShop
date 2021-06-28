using Android.Views;
using Android.Widget;
using MvvmCross.Platforms.Android.Binding;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using SushiShop.Core.ViewModels.Orders.Items;
using SushiShop.Droid.Views.ViewHolders.Abstract;

namespace SushiShop.Droid.Views.Activities.Orders
{
    public class OrderDeliverySuggestionItemViewHolder : CardViewHolder<OrderDeliverySuggestionItemViewModel>
    {
        private View view;
        private TextView titleTextView;

        public OrderDeliverySuggestionItemViewHolder(View view, IMvxAndroidBindingContext context) : base(view, context)
        {
        }

        protected override void DoInit(View view)
        {
            base.DoInit(view);

            this.view = view;
            titleTextView = view.FindViewById<TextView>(Resource.Id.title_text_view);
        }

        public override void BindData()
        {
            base.BindData();

            using var bindingSet = CreateBindingSet();

            bindingSet.Bind(titleTextView).For(v => v.Text).To(vm => vm.Address);
            bindingSet.Bind(view).For(v => v.BindClick()).To(vm => vm.SelectCommand);
        }
    }
}
