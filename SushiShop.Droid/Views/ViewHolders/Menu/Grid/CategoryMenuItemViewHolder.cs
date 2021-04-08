using Android.Views;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using SushiShop.Core.ViewModels.Menu.Items;
using SushiShop.Droid.Views.ViewHolders.Abstract;

namespace SushiShop.Droid.Views.ViewHolders.Menu.Grid
{
    public class CategoryMenuItemViewHolder : CardViewHolder<CategoryMenuItemViewModel>
    {
        public CategoryMenuItemViewHolder(View view, IMvxAndroidBindingContext context) : base(view, context)
        {
        }
    }
}
