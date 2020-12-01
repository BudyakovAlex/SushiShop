using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;
using SushiShop.Core.Data.Models.Cart;
using SushiShop.Core.Resources;

namespace SushiShop.Core.ViewModels.Cart.Items
{
    public class CartProductToppingItemViewModel : BaseViewModel
    {
        private readonly CartTopping topping;

        public CartProductToppingItemViewModel(CartTopping topping)
        {
            this.topping = topping;
        }

        public string? Title => topping.PageTitle;

        public string Count => $"{topping.Count} {AppStrings.CountInPC}";
    }
}