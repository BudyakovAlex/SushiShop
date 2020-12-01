using BuildApps.Core.Mobile.MvvmCross.Commands;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using SushiShop.Core.Data.Models.Cart;
using SushiShop.Core.Data.Models.Common;
using SushiShop.Core.Managers.Cart;
using SushiShop.Core.NavigationParameters;
using SushiShop.Core.ViewModels.Cart.Items.Abstract;
using SushiShop.Core.ViewModels.ProductDetails;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SushiShop.Core.ViewModels.Cart.Items
{
    public class CartProductItemViewModel : BaseCartProductItemViewModel
    {
        public CartProductItemViewModel(
            ICartManager cartManager,
            CartProduct product,
            Currency? currency,
            string? city) : base(cartManager, product, currency, city)
        {
            Toppings = new MvxObservableCollection<CartProductToppingItemViewModel>(ProductToppingItemViewModels(product.Toppings));

            ShowDetailsCommand = new SafeAsyncCommand(ExecutionStateWrapper, ShowDetailsAsync);
        }

        public IMvxAsyncCommand ShowDetailsCommand { get; }

        public MvxObservableCollection<CartProductToppingItemViewModel> Toppings { get; }

        private Task ShowDetailsAsync()
        {
            var parameters = new CardProductNavigationParameters(Product.Id, null, Product.IsReadOnly);
            return NavigationManager.NavigateAsync<ProductDetailsViewModel, CardProductNavigationParameters>(parameters);
        }

        private IEnumerable<CartProductToppingItemViewModel> ProductToppingItemViewModels(CartTopping[] toppings)
        {
            foreach (var topping in toppings)
            {
                yield return new CartProductToppingItemViewModel(topping);
            }
        }
    }
}