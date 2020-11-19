using BuildApps.Core.Mobile.MvvmCross.Commands;
using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;
using MvvmCross.Commands;
using SushiShop.Core.Data.Models.Cart;
using SushiShop.Core.Managers.Cart;
using SushiShop.Core.ViewModels.Common;
using System.Threading.Tasks;

namespace SushiShop.Core.ViewModels.Cart.Items
{
    public class CartToppingtItemViewModel : BaseViewModel
    {
        private readonly ICartManager cartManager;
        private readonly CartTopping topping;
        private readonly string? city;

        public CartToppingtItemViewModel(
            ICartManager cartManager,
            CartTopping topping,
            string? city)
        {
            this.cartManager = cartManager;
            this.topping = topping;
            this.city = city;

            StepperViewModel = new StepperViewModel(topping.Count, OnCountChangedAsync);
            ShowDetailsCommand = new SafeAsyncCommand(ExecutionStateWrapper, ShowDetailsAsync);
        }
        public IMvxAsyncCommand ShowDetailsCommand { get; }

        public StepperViewModel StepperViewModel { get; }

        private Task ShowDetailsAsync()
        {
            //var parameters = new CardProductNavigationParameters(product.Id, null);
            //return NavigationManager.NavigateAsync<ProductDetailsViewModel, CardProductNavigationParameters>(parameters);
        }

        private async Task OnCountChangedAsync(int count)
        {
            //var response = await cartManager.UpdateProductInCartAsync(city, product!.Id, product?.Uid, count, Array.Empty<Topping>());
            //if (response.Data is null)
            //{
            //    return;
            //}

            //product!.Uid = response.Data.Uid;
        }
    }
}
