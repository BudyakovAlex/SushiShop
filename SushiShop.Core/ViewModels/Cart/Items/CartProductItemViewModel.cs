using BuildApps.Core.Mobile.MvvmCross.Commands;
using MvvmCross.Commands;
using SushiShop.Core.Data.Models.Cart;
using SushiShop.Core.Data.Models.Common;
using SushiShop.Core.Managers.Cart;
using SushiShop.Core.NavigationParameters;
using SushiShop.Core.ViewModels.Cart.Items.Abstract;
using SushiShop.Core.ViewModels.ProductDetails;
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
            ShowDetailsCommand = new SafeAsyncCommand(ExecutionStateWrapper, ShowDetailsAsync);
        }

        public IMvxAsyncCommand ShowDetailsCommand { get; }

        private Task ShowDetailsAsync()
        {
            var parameters = new CardProductNavigationParameters(Product.Id, null, Product.IsReadOnly);
            return NavigationManager.NavigateAsync<ProductDetailsViewModel, CardProductNavigationParameters>(parameters);
        }
    }
}