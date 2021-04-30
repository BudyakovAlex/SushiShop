using Android.Views;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using SushiShop.Core.ViewModels.Promotions.Items;
using SushiShop.Droid.Views.ViewHolders.Abstract;

namespace SushiShop.Droid.Views.ViewHolders.Promotions
{
    public class PromotionItemViewHolder : CardViewHolder<PromotionItemViewModel>
    {
        public PromotionItemViewHolder(View view, IMvxAndroidBindingContext context) : base(view, context)
        {
        }
    }
}
