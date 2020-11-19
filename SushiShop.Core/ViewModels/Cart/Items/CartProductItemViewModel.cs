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
        private readonly CartProduct product;
        private readonly string? city;

        public CartProductItemViewModel(
            ICartManager cartManager,
            CartProduct product,
            string? city)
        {
            this.cartManager = cartManager;
            this.product = product;
            this.city = city;

            //TODO: make it not nullable
            StepperViewModel = new StepperViewModel(product.Count, OnCountChangedAsync);
            ShowDetailsCommand = new SafeAsyncCommand(ExecutionStateWrapper, ShowDetailsAsync);
        }

        public IMvxAsyncCommand ShowDetailsCommand { get; }

        public StepperViewModel StepperViewModel { get; }

        public string? ImageUrl => product?.MainImageInfo?.JpgUrl;

        private Task ShowDetailsAsync()
        {
            //var parameters = new CardProductNavigationParameters(product.Id, null);
            //return NavigationManager.NavigateAsync<ProductDetailsViewModel, CardProductNavigationParameters>(parameters);
        }

        private async Task OnCountChangedAsync(int count)
        {
            var response = await cartManager.UpdateProductInCartAsync(city, product!.Id, product?.Uid, count, Array.Empty<Topping>());
            if (response.Data is null)
            {
                return;
            }

            product!.Uid = response.Data.Uid;
        }
    }
}
