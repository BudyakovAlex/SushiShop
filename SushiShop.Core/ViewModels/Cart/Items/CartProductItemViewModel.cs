using BuildApps.Core.Mobile.MvvmCross.Commands;
using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;
using MvvmCross.Commands;
using SushiShop.Core.Data.Models.Cart;
using SushiShop.Core.Managers.Cart;
using SushiShop.Core.ViewModels.Common;
using System;
using System.Threading.Tasks;

namespace SushiShop.Core.ViewModels.Cart.Items
{
    public class CartProductItemViewModel : BaseViewModel
    {
        private readonly ICartManager cartManager;
        private readonly CartProduct cartProduct;
        private readonly string? city;

        public CartProductItemViewModel(ICartManager cartManager, CartProduct cartProduct, string? city)
        {
            this.cartManager = cartManager;
            this.cartProduct = cartProduct;
            this.city = city;

            StepperViewModel = new StepperViewModel(cartProduct.Count, OnCountChangedAsync);
            ShowDetailsCommand = new SafeAsyncCommand(ExecutionStateWrapper, ShowDetailsAsync);
        }
        public IMvxAsyncCommand ShowDetailsCommand { get; }

        public StepperViewModel StepperViewModel { get; }

        public string? ImageUrl => cartProduct.MainImageInfo?.JpgUrl;

        private Task ShowDetailsAsync()
        {
            //var parameters = new CardProductNavigationParameters(product.Id, null);
            //return NavigationManager.NavigateAsync<ProductDetailsViewModel, CardProductNavigationParameters>(parameters);
        }

        private async Task OnCountChangedAsync(int count)
        {
            var response = await cartManager.UpdateProductInCartAsync(city, cartProduct!.Id, cartProduct?.Uid, count, Array.Empty<Topping>());
            if (response.Data is null)
            {
                return;
            }

            cartProduct!.Uid = response.Data.Uid;
        }
    }
}
