using Android.Views;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using SushiShop.Core.ViewModels.Orders.Sections;
using SushiShop.Droid.Views.ViewHolders.Abstract;

namespace SushiShop.Droid.Views.ViewHolders.Orders
{
    public class DeliveryOrderSectionViewHolder : CardViewHolder<DeliveryOrderSectionViewModel>
    {
        public DeliveryOrderSectionViewHolder(View view, IMvxAndroidBindingContext context) : base(view, context)
        {
        }
    }
}
